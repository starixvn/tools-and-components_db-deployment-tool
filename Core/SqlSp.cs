namespace DbDeployer.UserControls
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using DbDeployer.Core.Utils;

    public enum Phases { PreRelease = 0, Test, Release }

    /// <summary>
    /// This class is used to simulate a store procedure on the DBMS
    /// </summary>
    class SqlSp : ISqlScript
    {
        private string _suffix;

        #region Constants
        private static readonly string CreateCommandProcPattern = Properties.Resources.CREATE_PROCEDURE_PATTERN;
        private static readonly string ExecCommandProcPattern = Properties.Resources.EXEC_PROCEDURE_PATTERN;
        private static readonly string GrantPermisionCommandPattern = Properties.Resources.GRANT_PERMISSION_PATTERN;
        private static readonly string WriteLogSpExecutionPattern = Properties.Resources.WRITELOG_SP_PATTERN;
        private static readonly string SpNamePattern = Properties.Resources.SPNAME_PARAMETER_PATTERN;
        private static readonly string CreateProcKeywordPattern = Properties.Resources.CREATE_PROC_KEYWORD_PATTERN;
        #endregion Constants

        #region Properties

        public string Name { get; set; }

        public string SourceServerIp { get; set; }

        public string SourceServerAliasName { get; set; }

        public string DbName { get; set; }

        public Phases Phase { get; set; }

        public string DestinationServerIp { get; set; }

        public string ScriptFilePath { get; set; }

        public StringBuilder Content { get; set; }

        public bool IsSp
        {
            get
            {
                return true;
            }
        }

        #endregion
        //constructors
        public SqlSp() { }
        public SqlSp(string spname,
            string sourceServerIp,
            string sourceServerAliasname,
            string dbName,
            string destinationServer,
            string scriptFilePath,
            Phases phase)
        {
            this.Name = spname;
            this.SourceServerIp = sourceServerIp;
            this.SourceServerAliasName = sourceServerAliasname;
            this.DbName = dbName;
            this.DestinationServerIp = destinationServer;
            this.ScriptFilePath = scriptFilePath;
            this.Phase = phase;

            switch (this.Phase)
            {
                case Phases.PreRelease:
                    this._suffix = "_xpre";
                    break;
                case Phases.Test:
                    this._suffix = "_xtest";
                    break;
                case Phases.Release:
                    this._suffix = string.Empty;
                    break;
            }
        }

        /// <summary>
        /// load content of SP from the script file has been created from "Generate script" step
        /// </summary>
        /// <param name="dicSVDB_SP"></param>
        /// <param name="sqlUtil"></param>
        /// 
        public void LoadScriptFromScriptFileAndAddSuffix(Dictionary<string, string> dicSVDB_SP, SqlServer sqlUtil)
        {
            try
            {
                using (var fs = new FileStream(this.ScriptFilePath, FileMode.Open, FileAccess.Read))
                {
                    using (var sr = new StreamReader(fs))
                    {
                        this.Content = new StringBuilder();
                        string line = string.Empty;
                        var isTheFirstCreateProcCommand = true;
                        while ((line = sr.ReadLine()) != null)
                        {
                            // must add suffix before adding to the content
                            Match match;
                            MatchCollection matches;
                            string replacement = string.Empty;
                            string addedLine = string.Empty;
                            IList<string> listFunc;
                            // CREATE PROCEDURE or CREATE PROC
                            if ((match = Regex.Match(line, CreateCommandProcPattern, RegexOptions.IgnoreCase)).Success)
                            {
                                var spSpecParts = this.GetSV_DB_SPFromCallingCommand(match.Value);
                                if (this.IsNeedToBeRenamed(spSpecParts, dicSVDB_SP))
                                {
                                    //the function is bounded by []
                                    if (match.Value.LastIndexOf(']') == match.Value.Length - 1)
                                    {
                                        replacement = match.Value.Substring(0, match.Value.Length - 1) + this._suffix + "]";
                                    }
                                    else
                                    {
                                        replacement = match.Value + this._suffix;
                                    }

                                    if (isTheFirstCreateProcCommand
                                        && sqlUtil.IsScriptExisted(this.DbName, this.Name + this._suffix, true)
                                        && (String.Compare(spSpecParts[spSpecParts.Length - 1], this.Name, true)) == 0)
                                    //if SP already exist, change the "CREATE PROC" to "ALTER PROC"
                                    {
                                        replacement = Regex.Replace(replacement, CreateProcKeywordPattern, "ALTER PROCEDURE", RegexOptions.IgnoreCase);
                                        addedLine = Regex.Replace(line, CreateCommandProcPattern, replacement, RegexOptions.IgnoreCase);
                                        isTheFirstCreateProcCommand = false;//avoid evaluating the second statement in condition
                                    }
                                    else
                                    {
                                        addedLine = Regex.Replace(line, CreateCommandProcPattern, replacement, RegexOptions.IgnoreCase);
                                    }
                                }
                                else
                                {
                                    addedLine = line;
                                }
                            }
                            // GRANT PERMISION
                            else if ((match = Regex.Match(line, GrantPermisionCommandPattern, RegexOptions.IgnoreCase)).Success)
                            {
                                var spSpecParts = this.GetSV_DB_SPFromCallingCommand(match.Value);
                                if (this.IsNeedToBeRenamed(spSpecParts, dicSVDB_SP))
                                {
                                    //the SP is bounded by []
                                    if (match.Value.LastIndexOf(']') == match.Value.Length - 1)
                                    {
                                        replacement = match.Value.Substring(0, match.Value.Length - 1) + this._suffix + "]";
                                    }
                                    else
                                    {
                                        replacement = match.Value.Substring(0, match.Value.Length) + this._suffix;
                                    }
                                    addedLine = Regex.Replace(line, GrantPermisionCommandPattern, replacement, RegexOptions.IgnoreCase);
                                }
                                else
                                    addedLine = line;
                            }
                            // EXEC SPName. Currently, this code works fine with one EXEC command on this line
                            else if (((matches = Regex.Matches(line, ExecCommandProcPattern, RegexOptions.IgnoreCase)).Count > 0))
                            {
                                foreach (Match m in matches)
                                {
                                    var spSpecParts = this.GetSV_DB_SPFromCallingCommand(m.Value);
                                    if (this.IsNeedToBeRenamed(spSpecParts, dicSVDB_SP))
                                    {
                                        //the SP is bounded by []
                                        if (m.Value.LastIndexOf(']') == m.Value.Length - 1)
                                        {
                                            replacement = m.Value.Substring(0, m.Value.Length - 1) + this._suffix + "]";
                                        }
                                        else
                                        {
                                            replacement = m.Value + this._suffix;
                                        }
                                        //line = Regex.Replace(line, m.Value.ToString(), replacement, RegexOptions.IgnoreCase);
                                        line = line.Replace(m.Value.Trim(), replacement);
                                    }
                                }

                                addedLine = line;
                            }
                            // Quinn edit
                            // Call function(s) in sp we need to replace all of them also
                            else if (
                               (listFunc = GetListFunc(dicSVDB_SP, line)).Count > 0)
                            {
                                foreach (var m in listFunc)
                                {
                                    var spSpecParts = this.GetSV_DB_SPFromCallingCommand(m);
                                    if (this.IsNeedToBeRenamed(spSpecParts, dicSVDB_SP))
                                    {
                                        //the SP is bounded by []
                                        if (m.LastIndexOf(']') == m.Length - 1)
                                        {
                                            replacement = m.Substring(0, m.Length - 1) + this._suffix + "]";
                                        }
                                        else
                                        {
                                            replacement = m.Substring(0, m.Length) + this._suffix;
                                        }
                                        // line = Regex.Replace(line, m.Value.ToString(), replacement, RegexOptions.IgnoreCase);
                                        line = line.Replace(m.Trim(), replacement);
                                    }
                                }

                                addedLine = line;
                            }
                            else
                            {
                                addedLine = line;
                            }
                            this.Content.Append(String.Format("{0}\r\n", addedLine));
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// This method is used when we wish to release SP from pre-release or pre-test. In this situation, 
        /// the content of selected SPs must be retrived from the test server, not the original one as pre-release or test phase.
        /// After retriving the content, all the related SP names have to be removed the suffix which is added in the previous phase.
        /// </summary>
        /// <param name="dicSVDB_SP"></param>
        /// <param name="sqlUtil"></param>
        /// <param name="sqlUtil_LoadSP"></param>
        /// <param name="strConnection"></param>
        /// <param name="suffixToRemove"></param>
        public bool LoadScriptFromDBAndRemoveSuffix(Dictionary<string, string> dicSVDB_SP, SqlServer sqlUtil, SqlServer sqlUtil_LoadSP, string strConnection, string suffixToRemove)
        {
            try
            {
                this.Content = new StringBuilder();
                var originalContent = new StringBuilder();
                //load content and permission
                originalContent.Append(sqlUtil_LoadSP.GetScript(this.DbName, this.Name + suffixToRemove, true));
                if (originalContent.ToString().IndexOf("Cannot get Script") == -1)
                {
                    originalContent.Append("\r\n" + SqlServer.GetPermission(strConnection, this.Name + suffixToRemove));
                }
                else
                {
                    return false;
                }

                //Remove suffix
                using (var sr = new StringReader(originalContent.ToString()))
                {
                    string line;
                    IList<string> listFunc;
                    while ((line = sr.ReadLine()) != null)
                    {
                        //must add suffix before adding to the content
                        Match match;
                        MatchCollection matches;
                        string replacement = string.Empty;
                        string addedLine = string.Empty;
                        var isTheFirstCreateProcCommand = true;
                        //CREATE PROCEDURE or CREATE PROC
                        if ((match = Regex.Match(line, CreateCommandProcPattern, RegexOptions.IgnoreCase)).Success)
                        {
                            string[] spSpecParts = this.GetSV_DB_SPFromCallingCommand_WithoutSuffix(match.Value, suffixToRemove);
                            if (this.IsNeedToBeRenamed(spSpecParts, dicSVDB_SP))
                            {
                                //the SP is bounded by []
                                if (match.Value.LastIndexOf(']') == match.Value.Length - 1)
                                {
                                    replacement = match.Value.Substring(0, match.Value.Length - (1 + suffixToRemove.Length)) + "]";
                                }
                                else
                                {
                                    replacement = match.Value.Substring(0, match.Value.Length - suffixToRemove.Length);
                                }

                                if (isTheFirstCreateProcCommand && sqlUtil.IsScriptExisted(this.DbName, this.Name + this._suffix, true) &&
                                    (String.Compare(spSpecParts[spSpecParts.Length - 1], (this.Name)) == 0))//if SP already exist, change the "CREATE PROC" to "ALTER PROC"
                                {
                                    replacement = Regex.Replace(replacement, CreateProcKeywordPattern, "ALTER PROCEDURE", RegexOptions.IgnoreCase);
                                    addedLine = Regex.Replace(line, CreateCommandProcPattern, replacement, RegexOptions.IgnoreCase);
                                    isTheFirstCreateProcCommand = false;//avoid evaluating the second statement in condition
                                }
                                else
                                {
                                    addedLine = Regex.Replace(line, CreateCommandProcPattern, replacement, RegexOptions.IgnoreCase);
                                }
                            }
                            else
                            {
                                addedLine = line;
                            }
                        }
                        //GRANT PERMISION
                        else if ((match = Regex.Match(line, GrantPermisionCommandPattern, RegexOptions.IgnoreCase)).Success)
                        {
                            var spSpecParts = this.GetSV_DB_SPFromCallingCommand_WithoutSuffix(match.Value, suffixToRemove);
                            if (this.IsNeedToBeRenamed(spSpecParts, dicSVDB_SP))
                            {
                                //the SP is bounded by []
                                if (match.Value.LastIndexOf(']') == match.Value.Length - 1)
                                {
                                    replacement = match.Value.Substring(0, match.Value.Length - (1 + suffixToRemove.Length)) + "]";
                                }
                                else
                                {
                                    replacement = match.Value.Substring(0, match.Value.Length - suffixToRemove.Length);
                                }

                                addedLine = Regex.Replace(line, GrantPermisionCommandPattern, replacement, RegexOptions.IgnoreCase);
                            }
                            else
                            {
                                addedLine = line;
                            }
                        }
                        //EXEC SPName. Currently, this code works fine with one EXEC command on this line
                        else if (((matches = Regex.Matches(line, ExecCommandProcPattern, RegexOptions.IgnoreCase)).Count > 0))
                        {
                            foreach (Match m in matches)
                            {
                                var spSpecParts = this.GetSV_DB_SPFromCallingCommand_WithoutSuffix(m.Value, suffixToRemove);
                                if (this.IsNeedToBeRenamed(spSpecParts, dicSVDB_SP))
                                {
                                    //the SP is bounded by []
                                    if (m.Value.LastIndexOf(']') == m.Value.Length - 1)
                                    {
                                        replacement = m.Value.Substring(0, m.Value.Length - (1 + suffixToRemove.Length)) + "]";
                                    }
                                    else
                                    {
                                        replacement = m.Value.Substring(0, m.Value.Length - suffixToRemove.Length);
                                    }

                                    line = line.Replace(m.Value.Trim(), replacement);
                                }
                            }

                            addedLine = line;
                        }
                        //Smith
                        //Call function(s) in sp we need to replace all of them also
                        else if ((listFunc = GetListFunc(dicSVDB_SP, line)).Count > 0)
                        {
                            foreach (var m in listFunc)
                            {
                                //var spSpecParts = this.GetSV_DB_SPFromCallingCommand(m);
                                string[] spSpecParts = this.GetSV_DB_SPFromCallingCommand_WithoutSuffix(m, suffixToRemove);
                                if (this.IsNeedToBeRenamed(spSpecParts, dicSVDB_SP))
                                {
                                    //the SP is bounded by []
                                    if (m.LastIndexOf(']') == m.Length - 1)
                                    {
                                        replacement = m.Substring(0, m.Length - (1 + suffixToRemove.Length)) + "]";
                                    }
                                    else
                                    {
                                        replacement = m.Substring(0, m.Length - suffixToRemove.Length);
                                    }
                                    //line = Regex.Replace(line, m.Value.ToString(), replacement, RegexOptions.IgnoreCase);
                                    line = line.Replace(m.Trim(), replacement);
                                }
                            }

                            addedLine = line;
                        }
                        else
                        {
                            addedLine = line;
                        }

                        this.Content.Append(String.Format("{0}\r\n", addedLine));
                    }
                }
            }
            catch
            {
                throw;
            }

            return true;
        }

        public bool Deploy(SqlServer slqUtil)
        {
            try
            {
                return slqUtil.CreateScript(this.DbName, this.Content.ToString(), true);
            }
            catch
            {
                throw;
            }
        }

        public void Backup(SqlServer sqlUtil, string connection, string backupFolderPath, string dateTimeNow, string des)
        {
            var backupContent = new StringBuilder().Append(sqlUtil.GetScript(this.DbName, this.Name + this._suffix, true));
            backupContent.Append(SqlServer.GetPermission(connection, this.Name + this._suffix));
            LogUtility.WriteToFile(
                backupContent.ToString(), 
                backupFolderPath + @"\" + dateTimeNow + "-" + this.SourceServerAliasName + "-" + this.DbName + "-" + des + "-" + this.Name + ".sql");
        }

        public void WriteContentToScriptFile(string filePath)
        {
            LogUtility.WriteToFile(this.Content.ToString(), filePath);
        }

        public ISqlScript Copy()
        {
            return (SqlSp)this.MemberwiseClone();
        }

        private bool IsNeedToBeRenamed(string[] spSpecParts, Dictionary<string, string> dicSVDB_SP)
        {
            string[] SPs;
            //there is no SP from DB on SV selected from grid for both Deploy and Only Rename
            if (spSpecParts.Length == 3)
            {
                if (!dicSVDB_SP.ContainsKey(spSpecParts[0] + "-" + spSpecParts[1]))
                {
                    return false;
                }
                else
                {
                    SPs = dicSVDB_SP[spSpecParts[0] + "-" + spSpecParts[1]].Split(',');
                    for (var i = 0; i < SPs.Length; i++)
                    {
                        if ((String.Compare(spSpecParts[2], SPs[i], true)) == 0)
                        {
                            return true;
                        }
                    }
                }
            }
            else if (spSpecParts.Length == 2)
            {
                if (!dicSVDB_SP.ContainsKey(this.SourceServerAliasName + "-" + spSpecParts[0]))
                {
                    return false;
                }
                else
                {
                    SPs = dicSVDB_SP[this.SourceServerAliasName + "-" + spSpecParts[0]].Split(',');
                    for (var i = 0; i < SPs.Length; i++)
                    {
                        if ((String.Compare(spSpecParts[1], SPs[i], true)) == 0)
                        {
                            return true;
                        }
                    }
                }
            }
            else if (spSpecParts.Length == 1)
            {
                //return true;
                if (!dicSVDB_SP.ContainsKey(this.SourceServerAliasName + "-" + this.DbName))
                {
                    return false;
                }
                else
                {
                    SPs = dicSVDB_SP[this.SourceServerAliasName + "-" + this.DbName].Split(',');
                    for (var i = 0; i < SPs.Length; i++)
                    {
                        if ((String.Compare(spSpecParts[0], SPs[i], true)) == 0)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private string[] GetSV_DB_SPFromCallingCommand(string spCommandLine)
        {
            spCommandLine = spCommandLine.Trim();
            string[] retVal = null;
            spCommandLine = spCommandLine.RemoveTab();// avoid tab space appears immediately before CalledSPSpec
            var calledSPSpecification = spCommandLine.Substring(spCommandLine.LastIndexOf(' ') + 1);// CREATE PROC <db02.bodb02.dbo.SampleSP>
            var specParts = calledSPSpecification.Split('.');

            if (specParts.Length == 4)//full format
            {
                retVal = new string[3];
                retVal[0] = StringUtility.RemoveSquareBracket(specParts[0].ToUpper());//server
                retVal[1] = StringUtility.RemoveSquareBracket(specParts[1].ToUpper());//DB
                retVal[2] = StringUtility.RemoveSquareBracket(specParts[3]);//SP
            }
            else if (specParts.Length == 3)//bodb02.dbo.sampleSP
            {
                retVal = new string[2];
                retVal[0] = StringUtility.RemoveSquareBracket(specParts[0].ToUpper());//DB
                retVal[1] = StringUtility.RemoveSquareBracket(specParts[2]);
            }
            else if (specParts.Length == 2)//dbo.sampleSP
            {
                retVal = new string[1];
                retVal[0] = StringUtility.RemoveSquareBracket(specParts[1]);//SP
            }
            else if (specParts.Length == 1)//sampleSP
            {
                retVal = new string[1];
                retVal[0] = StringUtility.RemoveSquareBracket(specParts[0]);//SP
            }

            return retVal;
        }

        private string[] GetSV_DB_SPFromCallingCommand_WithoutSuffix(string spCommandLine, string suffix)
        {

            spCommandLine = spCommandLine.Trim();
            string[] retVal = null;
            spCommandLine = spCommandLine.Replace('\t', ' ');//avoid tab space appears immediately before CalledSPSpec
            var calledSPSpecification = spCommandLine.Substring(spCommandLine.LastIndexOf(' ') + 1);//CREATE PROC <db02.bodb02.dbo.SampleSP>
            var specParts = calledSPSpecification.Split('.');

            if (spCommandLine.IndexOf(suffix) == -1)
            {
                retVal = new string[1];
                retVal[0] = spCommandLine;
            }
            else
            {
                if (specParts.Length == 4)//full format
                {
                    retVal = new string[3];
                    retVal[0] = StringUtility.RemoveSquareBracket(specParts[0].ToUpper());//server
                    retVal[1] = StringUtility.RemoveSquareBracket(specParts[1].ToUpper());//DB
                    int lenNosuffix = StringUtility.RemoveSquareBracket(specParts[3]).Length - suffix.Length;
                    lenNosuffix = lenNosuffix > 0 ? lenNosuffix : StringUtility.RemoveSquareBracket(specParts[3]).Length;
                    retVal[2] = StringUtility.RemoveSquareBracket(specParts[3]).Substring(0, lenNosuffix);//SP
                }
                else if (specParts.Length == 3)//bodb02.dbo.sampleSP
                {
                    retVal = new string[2];
                    retVal[0] = StringUtility.RemoveSquareBracket(specParts[0].ToUpper());//DB
                    int lenNosuffix = StringUtility.RemoveSquareBracket(specParts[2]).Length - suffix.Length;
                    lenNosuffix = lenNosuffix > 0 ? lenNosuffix : StringUtility.RemoveSquareBracket(specParts[2]).Length;
                    retVal[1] = StringUtility.RemoveSquareBracket(specParts[2]).Substring(0, lenNosuffix);
                }
                else if (specParts.Length == 2)//dbo.sampleSP
                {
                    retVal = new string[1];
                    int lenNosuffix = StringUtility.RemoveSquareBracket(specParts[1]).Length - suffix.Length;
                    lenNosuffix = lenNosuffix > 0 ? lenNosuffix : StringUtility.RemoveSquareBracket(specParts[1]).Length;
                    retVal[0] = StringUtility.RemoveSquareBracket(specParts[1]).Substring(0, lenNosuffix);//SP
                }
                else if (specParts.Length == 1)//sampleSP
                {
                    retVal = new string[1];
                    int lenNosuffix = StringUtility.RemoveSquareBracket(specParts[0]).Length - suffix.Length;
                    lenNosuffix = lenNosuffix > 0 ? lenNosuffix : StringUtility.RemoveSquareBracket(specParts[0]).Length;
                    retVal[0] = StringUtility.RemoveSquareBracket(specParts[0]).Substring(0, lenNosuffix);//SP
                }
            }

            return retVal;
        }

        public IList<string> GetListFunc(Dictionary<string, string> dicSVDB_SP, string line)
        {
            List<string> listFunc = new List<string>();
            int index;
            var listText = line.Split(' ', '\t');
            var listKey = dicSVDB_SP.Values.ToList();
            foreach (var items in listKey)
            {
                var keys = items.Split(',');
                foreach (var item in keys)
                {
                    foreach (var text in listText)
                    {
                        index = text.IndexOf(item);
                        if (index > -1)
                        {
                            int i = 0;
                            for (i = index; i < text.Length; i++)
                            {
                                if (text[i] == '(')
                                {
                                    break;
                                }
                            }

                            if (!listFunc.Contains(text.Substring(0, i)))
                            {
                                listFunc.Add(text.Substring(0, i));
                            }
                        }
                    }
                }
            }

            return listFunc;
        }

    }

}

