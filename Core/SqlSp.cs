namespace DbDeployer.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// This class is used to simulate a store procedure on the DBMS
    /// </summary>
    class SqlSp : BaseSql, ISqlScript
    {
        private static readonly string CreateCommandProcPattern = Properties.Resources.CREATE_PROCEDURE_PATTERN;
        private static readonly string ExecCommandProcPattern = Properties.Resources.EXEC_PROCEDURE_PATTERN;
        private static readonly string GrantPermisionCommandPattern = Properties.Resources.GRANT_PERMISSION_PATTERN;
        private static readonly string WriteLogSpExecutionPattern = Properties.Resources.WRITELOG_SP_PATTERN;
        private static readonly string SpNamePattern = Properties.Resources.SPNAME_PARAMETER_PATTERN;
        private static readonly string CreateProcKeywordPattern = Properties.Resources.CREATE_PROC_KEYWORD_PATTERN;

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
            this.IsSp = true;
        }

        /// <summary>
        /// load content of SP from the script file has been created from "Generate script" step
        /// </summary>
        /// <param name="dicSVDB_SP"></param>
        /// <param name="sqlUtil"></param>
        /// 
        public void LoadScriptFromScriptFileAndAddSuffix(Dictionary<string, string> dicSVDB_SP, SqlServer sqlUtil, Dictionary<string, ServerIpSetting> serverConfig)
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
                                if (this.IsNeedToBeRenamed(spSpecParts, dicSVDB_SP, serverConfig, this.Phase))
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
                                if (this.IsNeedToBeRenamed(spSpecParts, dicSVDB_SP, serverConfig, this.Phase))
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
                                    if (this.IsNeedToBeRenamed(spSpecParts, dicSVDB_SP, serverConfig, this.Phase))
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
                                        line = line.Replace(m.Value.Trim(), replacement.Trim());
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
                                    if (this.IsNeedToBeRenamed(spSpecParts, dicSVDB_SP, serverConfig, this.Phase))
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
                                        line = line.Replace(m.Trim(), replacement.Trim());
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
        public bool LoadScriptFromDBAndRemoveSuffix(Dictionary<string, string> dicSVDB_SP, SqlServer sqlUtil, SqlServer sqlUtil_LoadSP, string strConnection, string suffixToRemove, Dictionary<string, ServerIpSetting> serverConfig)
        {
            try
            {
                this.Content = new StringBuilder();
                var originalContent = new StringBuilder();
                //load content and permission
                originalContent.Append(sqlUtil_LoadSP.GetScript(this.DbName, this.Name + suffixToRemove, IsSp));
                if (originalContent.ToString().IndexOf("Cannot get Script") == -1)
                {
                    originalContent.Append("\r\n" + SqlServer.GetPermission(strConnection, this.Name + suffixToRemove));
                }
                else
                {
                    return false;
                }

                Phases phase = GetPhaseBySuffix(suffixToRemove);

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
                            if (this.IsNeedToBeRenamed(spSpecParts, dicSVDB_SP, serverConfig, phase))
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

                                if (isTheFirstCreateProcCommand && sqlUtil.IsScriptExisted(this.DbName, this.Name + this._suffix, IsSp) &&
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
                            if (this.IsNeedToBeRenamed(spSpecParts, dicSVDB_SP, serverConfig, phase))
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
                                if (this.IsNeedToBeRenamed(spSpecParts, dicSVDB_SP, serverConfig, phase))
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
                                if (this.IsNeedToBeRenamed(spSpecParts, dicSVDB_SP, serverConfig, phase))
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
    }
}

