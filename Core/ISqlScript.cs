using System.Collections.Generic;
using System.Text;

namespace DbDeployer.UserControls
{
    public interface ISqlScript
    {
        string Name { get; set; }

        string SourceServerIp { get; set; }

        string SourceServerAliasName { get; set; }

        string DbName { get; set; }

        Phases Phase { get; set; }

        string DestinationServerIp { get; set; }

        string ScriptFilePath { get; set; }

        StringBuilder Content { get; set; }

        bool IsSp { get; }

        /// <summary>
        /// load content of script from the script file has been created from "Generate script" step
        /// </summary>
        /// <param name="dicSVDB_SP"></param>
        /// <param name="sqlUtil"></param>
        /// 
        void LoadScriptFromScriptFileAndAddSuffix(Dictionary<string, string> dicSVDB_SP, SqlServer sqlUtil, Dictionary<string, ServerIpSetting> serverConfig);

        /// <summary>
        /// This method is used when we wish to release Script from pre-release or pre-test. In this situation, 
        /// the content of selected SPs must be retrived from the test server, not the original one as pre-release or test phase.
        /// After retriving the content, all the related SP names have to be removed the suffix which is added in the previous phase.
        /// </summary>
        /// <param name="dicSVDB_SP"></param>
        /// <param name="sqlUtil"></param>
        /// <param name="sqlUtil_LoadSP"></param>
        /// <param name="strConnection"></param>
        /// <param name="suffixToRemove"></param>
        bool LoadScriptFromDBAndRemoveSuffix(Dictionary<string, string> dicSVDB_SP
            , SqlServer sqlUtil
            , SqlServer sqlUtil_LoadSP
            , string strConnection
            , string suffixToRemove
            , Dictionary<string, ServerIpSetting> serverConfig);

        bool Deploy(SqlServer slqUtil);

        void Backup(SqlServer sqlUtil, string connection, string backupFolderPath, string dateTimeNow, string des);

        void WriteContentToScriptFile(string filePath);

        ISqlScript Copy();
    }
}
