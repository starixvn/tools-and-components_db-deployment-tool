namespace DbDeployer.UserControls
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;
    using SQLDMO;


    public class SqlServer
    {
        private readonly SQLServer2 myServer;
        private bool isConnected;

        public SqlServer()
        {
            this.myServer = new SQLServer2();
        }

        /// <summary>
        /// Get List of available SQL Server on network
        /// </summary>
        /// <returns></returns>
        /// 
        public ArrayList GetServers()
        {

            var ret = new ArrayList();
            try
            {
                var nl = this.myServer.Application.ListAvailableSQLServers();
                int i, n = nl.Count;
                for (i = 0; i < n; i++)
                {
                    var sn = nl.Item(i);
                    if (sn != null)
                        ret.Add(sn);
                }

            }
            catch
            {

            }
            //ret.Add("10.20.20.204");
            return ret;
        }

        /// <summary>
        /// Connect to a SQL Server using Windows Authentication
        /// </summary>
        /// <param name="serverName"></param>
        /// <returns></returns>
        public bool Connect(string serverName)
        {
            this.Disconnect();
            try
            {
                this.myServer.LoginTimeout = 15;
                this.myServer.LoginSecure = true;
                this.myServer.Connect(serverName, null, null);
            }
            catch
            {
                this.isConnected = false;

            }
            this.isConnected = true;
            return this.isConnected;
        }

        /// <summary>
        /// Disconnet to the server 
        /// </summary>
        public void Disconnect()
        {
            try
            {
                this.myServer.DisConnect();
            }
            catch
            {
            }
        }

        public string GetTableNameFromTriggerName(string triggername)
        {
            return triggername.Substring(0, triggername.IndexOf("_"));
        }
        /// <summary>
        /// Connect to a SQL Server using SQL Authentication
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Connect(string serverName, string userName, string password)
        {
            this.Disconnect();
            try
            {
                this.myServer.LoginSecure = false;
                this.myServer.Connect(serverName, userName, password);
                this.isConnected = true;
            }
            catch
            {
                this.isConnected = false;
            }

            return this.isConnected;
        }

        /// <summary>
        /// Retrieve list of available Database names
        /// </summary>
        /// <returns></returns>
        public ArrayList GetDatabaseNames()
        {
            var ret = new ArrayList();
            try
            {
                foreach (_Database db in this.myServer.Databases)
                {
                    if (db != null)
                        ret.Add(db.Name);
                }

            }
            catch
            {

            }
            return ret;
        }

        public static DataSet ConvertDataReaderToDataSet(SqlDataReader reader)
        {
            var dataSet = new DataSet();
            do
            {
                // Create new data table

                var schemaTable = reader.GetSchemaTable();
                var dataTable = new DataTable();

                if (schemaTable != null)
                {
                    // A query returning records was executed

                    for (var i = 0; i < schemaTable.Rows.Count; i++)
                    {
                        var dataRow = schemaTable.Rows[i];
                        // Create a column name that is unique in the data table
                        var columnName = (string)dataRow["ColumnName"]; //+ "<C" + i + "/>";
                        // Add the column definition to the data table
                        var column = new DataColumn(columnName, (Type)dataRow["DataType"]);
                        dataTable.Columns.Add(column);
                    }

                    dataSet.Tables.Add(dataTable);

                    // Fill the data table we just created

                    while (reader.Read())
                    {
                        var dataRow = dataTable.NewRow();

                        for (var i = 0; i < reader.FieldCount; i++)
                            dataRow[i] = reader.GetValue(i);

                        dataTable.Rows.Add(dataRow);
                    }
                }
                else
                {
                    // No records were returned

                    var column = new DataColumn("RowsAffected");
                    dataTable.Columns.Add(column);
                    dataSet.Tables.Add(dataTable);
                    var dataRow = dataTable.NewRow();
                    dataRow[0] = reader.RecordsAffected;
                    dataTable.Rows.Add(dataRow);
                }
            }
            while (reader.NextResult());
            return dataSet;
        }

        public static string GetPermission(string connectionstring, string value, string grantPermissionCmd = "DBA_GrantPermissions")
        {
            SqlDataReader dr;
            string result;
            var sqlConn = new SqlConnection(connectionstring);
            var cmd = new SqlCommand();

            var p = new SqlParameter
                {
                    ParameterName = "@SPs",
                    DbType = DbType.String,
                    Value = value,
                    Direction = ParameterDirection.Input
                };

            var o = new SqlParameter
                {
                    ParameterName = "@strPermission",
                    DbType = DbType.String,
                    Direction = ParameterDirection.ReturnValue
                };

            cmd.CommandText = grantPermissionCmd;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(p);
            cmd.Parameters.Add(o);

            cmd.Connection = sqlConn;

            sqlConn.Open();
            //cmd.ExecuteNonQuery();

            //add try/catch
            try
            {
                dr = cmd.ExecuteReader();
            }
            catch
            {
                throw;
            }

            if (dr.HasRows)
            {
                var ds = ConvertDataReaderToDataSet(dr);
                result = ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                result = "--";
            }
            result = result.Replace("nnn", Environment.NewLine);
            //result = (string)cmd.Parameters["@strPermission"].Value;
            sqlConn.Close();

            return result;
        }

        private string GetSpScript(string dbName, string spName)
        {
            string scriptContent = string.Empty;
            try
            {
                //Try to script store procedures
                scriptContent = this.myServer.Databases.Item(dbName, "dbo").StoredProcedures.Item(spName, "dbo")
                    .Script(SQLDMO_SCRIPT_TYPE.SQLDMOScript_Default
                    | SQLDMO_SCRIPT_TYPE.SQLDMOScript_IncludeHeaders
                    //| SQLDMO_SCRIPT_TYPE.SQLDMOScript_ObjectPermissions
                    //| SQLDMO_SCRIPT_TYPE.SQLDMOScript_DatabasePermissions
                    , null,
                    SQLDMO_SCRIPT2_TYPE.SQLDMOScript2_Default | SQLDMO_SCRIPT2_TYPE.SQLDMOScript2_NoCollation);
            }
            catch
            {
                throw;
            }

            return scriptContent;
        }

        //Smith
        private string GetFunctionScript(string dbName, string functionName)
        {
            string scriptContent = string.Empty;
            try
            {
                var db = this.myServer.Databases.Item(dbName, "dbo") as Database2;
                //Try to script store procedures
                scriptContent = db.UserDefinedFunctions.Item(functionName, "dbo")
                    .Script(SQLDMO_SCRIPT_TYPE.SQLDMOScript_Default
                    | SQLDMO_SCRIPT_TYPE.SQLDMOScript_IncludeHeaders
                    //| SQLDMO_SCRIPT_TYPE.SQLDMOScript_ObjectPermissions
                    //| SQLDMO_SCRIPT_TYPE.SQLDMOScript_DatabasePermissions
                    , null,
                    SQLDMO_SCRIPT2_TYPE.SQLDMOScript2_Default | SQLDMO_SCRIPT2_TYPE.SQLDMOScript2_NoCollation);
            }
            catch
            {
                //ret = "Cannot get Script";// +ex.Message;
                throw;
            }
            return scriptContent;
        }

        public string GetScript(string dbName, string scriptName, bool isSp)
        {
            return isSp ? this.GetSpScript(dbName, scriptName) : this.GetFunctionScript(dbName, scriptName);
        }

        public ArrayList GetSPs(string dbName)
        {
            var ret = new ArrayList();
            for (var i = 1; i <= this.myServer.Databases.Item(dbName, "dbo").StoredProcedures.Count; i++)
                ret.Add(this.myServer.Databases.Item(dbName, "dbo").StoredProcedures.Item(i, "dbo").Name);
            return ret;
        }

        //Smith
        public ArrayList GetFunctions(string dbName)
        {
            var ret = new ArrayList();
            var db = this.myServer.Databases.Item(dbName, "dbo") as Database2;

            foreach (UserDefinedFunction fuction in db.UserDefinedFunctions)
            {
                ret.Add(fuction.Name);
            }
            return ret;
        }

        private bool CreateSp(string dbName, string scriptContent)
        {
            bool result = false;
            var sp = new StoredProcedure();
            try
            {
                sp.Text = scriptContent;
                this.myServer.Databases.Item(dbName, "dbo").StoredProcedures.Add(sp);
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        //Smith
        private bool CreateFunction(string dbName, string functionContent)
        {
            bool result = false;
            var function = new UserDefinedFunction();
            try
            {
                function.Text = functionContent;
                var db = this.myServer.Databases.Item(dbName, "dbo") as Database2;
                db.UserDefinedFunctions.Add(function);
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        //Smith 
        public bool CreateScript(string dbName, string scriptContent, bool isSp)
        {
            return isSp ? this.CreateSp(dbName, scriptContent) : this.CreateFunction(dbName, scriptContent);
        }
        private string DropSp(string dbName, string spName)
        {
            try
            {
                this.myServer.Databases.Item(dbName, "dbo").StoredProcedures.Remove(spName, "dbo");
                return "Deleted " + spName;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //Smith
        private string DropFunction(string dbName, string functionName)
        {
            try
            {
                (this.myServer.Databases.Item(dbName, "dbo") as Database2).UserDefinedFunctions.Remove(functionName, "dbo");
                return "Deleted " + functionName;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string DropScript(string dbName, string scriptName, bool isSp)
        {
            return isSp ? this.DropSp(dbName, scriptName) : this.DropFunction(dbName, scriptName);
        }

        private bool IsSpExisted(string dbName, string spName)
        {
            var exist = false;
            try
            {
                var colSP = this.myServer.Databases.Item(dbName, "dbo").StoredProcedures;
                foreach (_StoredProcedure sp in colSP)
                {
                    if (sp.Name.Equals(spName, StringComparison.OrdinalIgnoreCase))
                    {
                        exist = true;
                        break;
                    }
                }
                return exist;
            }
            catch
            {
                return exist;
            }

        }

        public bool IsScriptExisted(string dbName, string scriptName, bool isSp)
        {
            return isSp ? IsSpExisted(dbName, scriptName) : this.IsFunctionExisted(dbName, scriptName);
        }

        //Smith
        private bool IsFunctionExisted(string dbName, string functionName)
        {
            try
            {
                var functions = this.GetFunctions(dbName);
                foreach (var function in functions)
                {
                    if (function.ToString().Equals(functionName, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
    }
}
