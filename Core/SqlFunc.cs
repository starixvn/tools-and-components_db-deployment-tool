namespace DbDeployer.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public class SqlFunc : BaseSql, ISqlScript
    {
        private static readonly string CreateCommandProcPattern = Properties.Resources.CREATE_FUNCTION_PATTERN;
        private static readonly string ExecCommandProcPattern = Properties.Resources.EXEC_FUNCTION_PATTERN;
        private static readonly string GrantPermisionCommandPattern = Properties.Resources.GRANT_PERMISSION_PATTERN;
        private static readonly string WriteLogSpExecutionPattern = Properties.Resources.WRITELOG_FUNCTION_PATTERN;
        private static readonly string SpNamePattern = Properties.Resources.FUNCTIONNAME_PARAMETER_PATTERN;
        private static readonly string CreateProcKeywordPattern = Properties.Resources.CREATE_FUNC_KEYWORD_PATTERN;

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
            this.IsSp = false;
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
            {
                addedLine = line;
            }
        }

        private bool CreateFunction(Dictionary<string, string> dicSVDB_SP, SqlServer sqlUtil, Dictionary<string, ServerIpSetting> serverConfig, 
            string line, bool isTheFirstCreateProcCommand, Match match, out string replacement, out string addedLine)
        {
            replacement = string.Empty;
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

            Phases phase = GetPhaseBySuffix(suffixToRemove);

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
                    // Quinn Edit
                    // Call function(s) in function we need to replace all of them also
                    else if ((listFunc = GetListFunc(dicSVDB_SP, line)).Count > 0)
                    {
                        foreach (var m in listFunc)
                        {
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

            return true;
        }
    }
}
