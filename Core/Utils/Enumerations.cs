namespace DbDeployer.Core.Utils
{
    public enum ScriptFormater
    {
        /// <summary>
        /// serverName.databaseName.dbo.spName
        /// </summary>
        FullFormat = 4,
        /// <summary>
        /// .databaseName.dbo.spName
        /// </summary>
        WithoutServerName = 3,
        SchemaAndSpName = 2,
        SpNameOnlye = 1

    }
}
