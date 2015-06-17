namespace DbDeployer.UserControls
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using DbDeployer.Core.Utils;

    public class SqlFunc : ISqlScript
    {
        private string _suffix;
        #region Constants
        private static readonly string CreateCommandProcPattern = Properties.Resources.CREATE_FUNCTION_PATTERN;
        private static readonly string ExecCommandProcPattern = Properties.Resources.EXEC_FUNCTION_PATTERN;
        private static readonly string GrantPermisionCommandPattern = Properties.Resources.GRANT_PERMISSION_PATTERN;
        private static readonly string WriteLogSpExecutionPattern = Properties.Resources.WRITELOG_FUNCTION_PATTERN;
        private static readonly string SpNamePattern = Properties.Resources.FUNCTIONNAME_PARAMETER_PATTERN;
        private static readonly string CreateProcKeywordPattern = Properties.Resources.CREATE_FUNC_KEYWORD_PATTERN;
        #endregion Constants

        #region IScript Members

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
                return false;
            }
        }

        //constructors
        public SqlFunc() { }
        public SqlFunc(
            string spname,
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

        public void LoadScriptFromScriptFileAndAddSuffix(Dictionary<string, string> dicSVDB_SP, SqlServer sqlUtil, Dictionary<string, ServerIpSetting> serverConfig)
        {
            try
            {
                using (var fs = new FileStream(this.ScriptFilePath, FileMode.Open, FileAccess.Read))
                {
                    using (var sr = new StreamReader(fs))
                    {
                        this.Content = new StringBuilder();

                        string line;
                        var isTheFirstCreateProcCommand = true;
                        //var isExecutingWriteLogSP = false;
                        while ((line = sr.ReadLine()) != null)
                        {
                            //must add suffix before adding to the content
                            Match match;
                            string replacement;
                            string addedLine;
                            IList<string> listFunc;
                            //CREATE FUNCTION
                            if ((match = Regex.Match(line, CreateCommandProcPattern, RegexOptions.IgnoreCase)).Success)
                            {
                                isTheFirstCreateProcCommand = CreateFunction(dicSVDB_SP, sqlUtil, serverConfig, line, isTheFirstCreateProcCommand, match, out replacement, out addedLine);
                            }
                            //GRANT PERMISION
                            else if ((match = Regex.Match(line, GrantPermisionCommandPattern, RegexOptions.IgnoreCase)).Success)
                            {
                                GrantPermission(dicSVDB_SP, serverConfig, line, match, out replacement, out addedLine);
                            }
                            // Quinn edit
                            // Call function(s) in function we need to replace all of them also
                            else if (
                               (listFunc = GetListFunc(dicSVDB_SP, line)).Count > 0)
                            {
                                foreach (var m in listFunc)
                                {
                                    var spSpecParts = this.GetSV_DB_SPFromCallingCommand(m);
                                    if (this.IsNeedToBeRenamed(spSpecParts, dicSVDB_SP, serverConfig))
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
            }
            catch
            {
                throw;
            }
        }

        private void GrantPermission(Dictionary<string, string> dicSVDB_SP, Dictionary<string, ServerIpSetting> serverConfig, string line, Match match, out string replacement, out string addedLine)
        {
            replacement = string.Empty;
            addedLine = string.Empty;
            var spSpecParts = this.GetSV_DB_SPFromCallingCommand(match.Value);
            if (this.IsNeedToBeRenamed(spSpecParts, dicSVDB_SP, serverConfig))
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
            {
                addedLine = line;
            }
        }

        private bool CreateFunction(Dictionary<string, string> dicSVDB_SP, SqlServer sqlUtil, Dictionary<string, ServerIpSetting> serverConfig, 
            string line, bool isTheFirstCreateProcCommand, Match match, out string replacement, out string addedLine)
        {
            replacement = string.Empty;
            var spSpecParts = this.GetSV_DB_SPFromCallingCommand(match.Value);
            if (this.IsNeedToBeRenamed(spSpecParts, dicSVDB_SP, serverConfig))
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

                if (isTheFirstCreateProcCommand && sqlUtil.IsScriptExisted(this.DbName, this.Name + this._suffix, false) &&
                    (String.Compare(spSpecParts[spSpecParts.Length - 1], this.Name, true)) == 0)//if Function already exist, change the "CREATE FUNCTION" to "ALTER FUNCTION"
                {
                    replacement = Regex.Replace(replacement, CreateProcKeywordPattern, "ALTER FUNCTION", RegexOptions.IgnoreCase);
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

            return isTheFirstCreateProcCommand;
        }

        public bool LoadScriptFromDBAndRemoveSuffix(Dictionary<string, string> dicSVDB_SP, SqlServer sqlUtil, SqlServer sqlUtil_LoadSP, string strConnection, string suffixToRemove, Dictionary<string, ServerIpSetting> serverConfig)
        {
            this.Content = new StringBuilder();
            var originalContent = new StringBuilder();
            // load content and permission
            originalContent.Append(sqlUtil_LoadSP.GetScript(this.DbName, this.Name + suffixToRemove, false));
            if (originalContent.ToString().IndexOf("Cannot get Script") == -1)
            {
                originalContent.Append("\r\n" + SqlServer.GetPermission(strConnection, this.Name + suffixToRemove));
            }
            else
            {
                return false;
            }

            // Remove suffix
            using (var sr = new StringReader(originalContent.ToString()))
            {
                string line;
                IList<string> listFunc;
                //var isExecutingWriteLogSP = false;
                while ((line = sr.ReadLine()) != null)
                {
                    //must add suffix before adding to the content
                    Match match;
                    string replacement;
                    string addedLine;
                    var isTheFirstCreateProcCommand = true;
                    //CREATE FUNCTION
                    if ((match = Regex.Match(line, CreateCommandProcPattern, RegexOptions.IgnoreCase)).Success)
                    {
                        var spSpecParts = this.GetSV_DB_SPFromCallingCommand_WithoutSuffix(match.Value, suffixToRemove);
                        if (this.IsNeedToBeRenamed(spSpecParts, dicSVDB_SP, serverConfig))
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

                            if (isTheFirstCreateProcCommand && sqlUtil.IsScriptExisted(this.DbName, this.Name + this._suffix, false) &&
                                (String.Compare(spSpecParts[spSpecParts.Length - 1], (this.Name)) == 0))//if Function already exist, change the "CREATE FUNCTION" to "ALTER FUNCTION"
                            {
                                replacement = Regex.Replace(replacement, CreateProcKeywordPattern, "ALTER FUNCTION", RegexOptions.IgnoreCase);
                                addedLine = Regex.Replace(line, CreateCommandProcPattern, replacement, RegexOptions.IgnoreCase);
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
                        if (this.IsNeedToBeRenamed(spSpecParts, dicSVDB_SP, serverConfig))
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
                    // Quinn Edit
                    // Call function(s) in function we need to replace all of them also
                    else if ((listFunc = GetListFunc(dicSVDB_SP, line)).Count > 0)
                    {
                        foreach (var m in listFunc)
                        {
                            string[] spSpecParts = this.GetSV_DB_SPFromCallingCommand_WithoutSuffix(m, suffixToRemove);
                            if (this.IsNeedToBeRenamed(spSpecParts, dicSVDB_SP, serverConfig))
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

            return true;
        }

        public bool Deploy(SqlServer slqUtil)
        {
            try
            {
                return slqUtil.CreateScript(this.DbName, this.Content.ToString(), false);
            }
            catch
            {
                throw;
            }
        }

        public void Backup(SqlServer sqlUtil, string connection, string backupFolderPath, string dateTimeNow, string des)
        {
            var backupContent = new StringBuilder().Append(sqlUtil.GetScript(this.DbName, this.Name + this._suffix, false));
            backupContent.Append(SqlServer.GetPermission(connection, this.Name + this._suffix));
            LogUtility.WriteToFile(backupContent.ToString(), backupFolderPath + @"\" + dateTimeNow + "-"
                                        + this.SourceServerAliasName + "-" + this.DbName + "-" + des + "-" + this.Name + ".sql");
        }

        public void WriteContentToScriptFile(string filePath)
        {

            LogUtility.WriteToFile(this.Content.ToString(), filePath);
        }

        public ISqlScript Copy()
        {
            return (SqlFunc)this.MemberwiseClone();
        }

        #endregion

        private bool IsNeedToBeRenamed(string[] spSpecParts, Dictionary<string, string> dicSVDB_SP, Dictionary<string, ServerIpSetting> serverConfig)
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
                    var aliass = serverConfig.Values.Where(t => t.GetIpByPhase(Phases.PreRelease) == this.DestinationServerIp).Select(t => t.ServerName).ToList();
                    foreach (string alias in aliass)
                    {
                        if (dicSVDB_SP.ContainsKey(alias + "-" + spSpecParts[0]))
                        {
                            return true;
                        }
                    }

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
                // return true;

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
            spCommandLine.Replace('\t', ' ');//avoid tab space appears immediately before CalledSPSpec
            var calledSPSpecification = spCommandLine.Substring(spCommandLine.LastIndexOf(' ') + 1);//CREATE PROC <db02.bodb02.dbo.SampleSP>
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
            if (specParts.Length == 4)//full format
            {
                retVal = new string[3];
                retVal[0] = StringUtility.RemoveSquareBracket(specParts[0].ToUpper());//server
                retVal[1] = StringUtility.RemoveSquareBracket(specParts[1].ToUpper());//DB
                retVal[2] = StringUtility.RemoveSquareBracket(specParts[3]).Substring(0, StringUtility.RemoveSquareBracket(specParts[3]).Length - suffix.Length);//SP
            }
            else if (specParts.Length == 3)//bodb02.dbo.sampleSP
            {
                retVal = new string[2];
                retVal[0] = StringUtility.RemoveSquareBracket(specParts[0].ToUpper());//DB
                retVal[1] = StringUtility.RemoveSquareBracket(specParts[2]).Substring(0, StringUtility.RemoveSquareBracket(specParts[2]).Length - suffix.Length);
            }
            else if (specParts.Length == 2)//dbo.sampleSP
            {
                retVal = new string[1];
                retVal[0] = StringUtility.RemoveSquareBracket(specParts[1]).Substring(0, StringUtility.RemoveSquareBracket(specParts[1]).Length - suffix.Length);//SP
            }
            else if (specParts.Length == 1)//sampleSP
            {
                retVal = new string[1];
                retVal[0] = StringUtility.RemoveSquareBracket(specParts[0]).Substring(0, StringUtility.RemoveSquareBracket(specParts[0]).Length - suffix.Length);//SP
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

                            listFunc.Add(text.Substring(0, i));
                        }
                    }
                }
            }

            return listFunc;
        }
    }
}
