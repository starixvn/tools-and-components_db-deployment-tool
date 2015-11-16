// --------------------------------------------------------------------------
// <copyright file="BaseSql.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2015.9.23">Created</change>
// </history>

namespace DbDeployer.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DbDeployer.Core.Utils;

    public class BaseSql
    {
        public Phases Phase { get; set; }

        public string Name { get; set; }

        public string SourceServerIp { get; set; }

        public string SourceServerAliasName { get; set; }

        public string DbName { get; set; }

        public string DestinationServerIp { get; set; }

        public string ScriptFilePath { get; set; }

        public StringBuilder Content { get; set; }

        public bool IsSp { get; internal set; }

        protected string _suffix
        {
            get
            {
                switch (this.Phase)
                {
                    case Phases.PreRelease:
                        return "_xpre";
                    case Phases.Test:
                        return "_xtest";
                    case Phases.UAT1:
                        return "_UAT1";
                    case Phases.UAT2:
                        return "_UAT2";
                    case Phases.UAT3:
                        return "_UAT3";
                    case Phases.UAT4:
                        return "_UAT4";
                    default:
                        return string.Empty;
                }
            }
        }

        public Phases GetPhaseBySuffix(string phase)
        {
            switch (phase)
            {
                case "_xpre":
                    return Phases.PreRelease;
                case "_xtest":
                    return Phases.Test;
                case "_UAT1":
                    return Phases.UAT1;
                case "_UAT2":
                    return Phases.UAT2;
                case "_UAT3":
                    return Phases.UAT3;
                case "_UAT4":
                    return Phases.UAT4;
                default:
                    return Phases.Release;
            }
        }

        public bool Deploy(SqlServer slqUtil)
        {
            try
            {
                return slqUtil.CreateScript(this.DbName, this.Content.ToString(), IsSp);
            }
            catch
            {
                throw;
            }
        }

        public ISqlScript Copy()
        {
            return (ISqlScript)this.MemberwiseClone();
        }

        public void Backup(SqlServer sqlUtil, string connection, string backupFolderPath, string dateTimeNow, string des)
        {
            var backupContent = new StringBuilder().Append(sqlUtil.GetScript(this.DbName, this.Name + this._suffix, IsSp));
            backupContent.Append(SqlServer.GetPermission(connection, this.Name + this._suffix));
            LogUtility.WriteToFile(
                backupContent.ToString(),
                backupFolderPath + @"\" + dateTimeNow + "-" + this.SourceServerAliasName + "-" + this.DbName + "-" + des + "-" + this.Name + ".sql");
        }

        public void WriteContentToScriptFile(string filePath)
        {
            LogUtility.WriteToFile(this.Content.ToString(), filePath);
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

        protected string[] GetSV_DB_SPFromCallingCommand(string spCommandLine)
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

        protected string[] GetSV_DB_SPFromCallingCommand_WithoutSuffix(string spCommandLine, string suffix)
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

        protected bool IsNeedToBeRenamed(string[] spSpecParts, Dictionary<string, string> dicSVDB_SP, Dictionary<string, ServerIpSetting> serverConfig, Phases phase)
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
                if (dicSVDB_SP.ContainsKey(this.SourceServerAliasName + "-" + this.DbName))
                {
                    SPs = dicSVDB_SP[this.SourceServerAliasName + "-" + this.DbName].Split(',');
                    for (var i = 0; i < SPs.Length; i++)
                    {
                        if ((String.Compare(spSpecParts[1], SPs[i], true)) == 0)
                        {
                            return true;
                        }
                    }
                }

                var aliass = serverConfig.Values.Where(t => (t.GetIpByPhase(phase) == this.DestinationServerIp))
                    .Select(t => new { t.ServerName, t.DBName}).ToList();
                if (aliass.Any())
                {
                    foreach (var alias in aliass)
                    {
                        if (dicSVDB_SP.ContainsKey(alias.ServerName + "-" + alias.DBName.ToUpper()))
                        {
                            SPs = dicSVDB_SP[alias.ServerName+ "-" + alias.DBName.ToUpper()].Split(',');

                            for (var i = 0; i < SPs.Length; i++)
                            {
                                if ((String.Compare(spSpecParts[1], SPs[i], true)) == 0)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }

                return false;
            }
            else if (spSpecParts.Length == 1)
            {
                //return true;
                if (dicSVDB_SP.ContainsKey(this.SourceServerAliasName + "-" + this.DbName))
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

                var aliass = serverConfig.Values.Where(t => (t.GetIpByPhase(phase) == this.DestinationServerIp && t.DBName.ToUpper() == this.DbName))
                    .Where(t => (t.ServerName.ToUpper() != this.SourceServerAliasName))
                    .Select(t => t.ServerName).ToList();
                if (aliass.Any())
                {
                    foreach (string alias in aliass)
                    {
                        if (dicSVDB_SP.ContainsKey(alias + "-" + this.DbName))
                        {
                            SPs = dicSVDB_SP[alias + "-" + this.DbName].Split(',');

                            for (var i = 0; i < SPs.Length; i++)
                            {
                                if ((String.Compare(spSpecParts[0], SPs[i], true)) == 0)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }

                return false;
            }

            return false;
        }
    }
}
