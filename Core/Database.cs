namespace DbDeployer.Core
{
    using System;
    using System.Collections;
    using System.Data.SqlClient;
    using System.IO;
    using System.Text;
    using DbDeployer.Core.Utils;
    using Microsoft.SqlServer.Management.Common;
    using Microsoft.SqlServer.Management.Smo;

    public class Database
    {
        private string _serverName;
        private string _name;
        private ArrayList _spList = new ArrayList();
        private ArrayList _funcList = new ArrayList();
        private string _username;
        private string _password;
        private SqlServer _sqlUtil;
        private ConfigUtility _configUtil;
        private string _connection;
        private string _scriptFolderPath;
        private static object _locker;
        private Server _server;

        public Database()
        {
            this._sqlUtil = new SqlServer();
            this._configUtil = new ConfigUtility();
        }

        public Database(string servername, string name)
        {
            this._serverName = servername;
            this._name = name;

            _locker = new object();

            this._sqlUtil = new SqlServer();
            this._configUtil = new ConfigUtility();
        }

        public string ServerName
        {
            get { return this._serverName; }
            set { this._serverName = value; }
        }

        public string ScriptFolderPath
        {
            set { this._scriptFolderPath = value; }
        }

        public ArrayList SPList
        {
            get { return this._spList; }
            set { this._spList = value; }
        }

        public ArrayList FunctonList
        {
            get { return this._funcList; }
            set { this._funcList = value; }
        }

        #region behavior
        public string BuildSPList(ArrayList prefix)
        {
            this._spList = new ArrayList();
            ArrayList spList;
            try
            {
                spList = this._sqlUtil.GetSPs(this._name);
            }
            catch (Exception e)
            {
                return "Cannot get sp names from DB: " + e.Message;
            }

            spList.Sort();
            for (var i = 0; i < spList.Count; i++)
            {
                for (var j = 0; j < prefix.Count; j++)
                {
                    if (spList[i].ToString().IndexOf(prefix[j].ToString(), StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        this._spList.Add(spList[i]);
                        break;
                    }
                }
            }
            return string.Empty;
        }

        public bool Connect(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException();

            this._connection = StringUtility.CreateConnectionString(
                this._configUtil.GetIPSettings()[this._serverName].IpRelease
                , this._name, username, password);

            return this._sqlUtil.Connect(this._configUtil.GetIPSettings()[this._serverName].IpRelease, this._username = username, this._password = password);
        }

        public void GenerateScriptFile(object threadParam)
        {
            var arrParam = (string[])threadParam;
            //write to cofig file
            var id = Guid.NewGuid().ToString("N");
            var config = new StringBuilder();
            //_scriptOfAllSP = new StringBuilder();
            //create folder
            var folderPath = arrParam[0] + @"\" + arrParam[1] + "-" + arrParam[2] + "-" + id;
            //UtilityFunction.WriteToFile(_scriptOfAllSP.ToString(), path);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            config.AppendLine("\r\n" + this._serverName + "-" + this._name);
            config.AppendLine(arrParam[1] + "-" + arrParam[2] + "-" + id);
            //string createSP = _sqlUtil.CreateSP(this._name, DBDeployment_v2_0.Properties.Resources.DBA_GrantPermission);


            var con = new SqlConnection(this._connection);
            try
            {
                con.Open();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

            //SMO to execute script with "GO" keyword in content
            this._server = new Server(new ServerConnection(con));
            this._server.ConnectionContext.ExecuteNonQuery(Properties.Resources.DBA_GrantPermission);

            var temp = new StringBuilder();
            var errLog = new StringBuilder();

            for (var i = 0; i < this._spList.Count; i++)
            {
                //SmithNOTED
                temp.Append(this._sqlUtil.GetScript(this._name, this._spList[i].ToString(), true));
                if (temp.ToString() != "Cannot get Script")
                {
                    //_scriptOfAllSP.Append(temp);
                    //_scriptOfAllSP.Append("\r\n" + SQLServerUtil.GetPermission(_connection, _SPList[i].ToString()) + "\r\n");
                    temp.AppendLine(SqlServer.GetPermission(this._connection, this._spList[i].ToString()));
                    LogUtility.WriteToFile(temp.ToString(), folderPath + @"\" + this._spList[i] + ".sql");
                    config.Append(this._spList[i] + ",");
                }
                else
                {
                    errLog.AppendLine(temp + " of " + this._spList[i]);
                }

                temp.Remove(0, temp.Length - 1);
            }

            this._sqlUtil.Disconnect();
            //Build file name                        
            var path1 = arrParam[0] + @"\" + arrParam[1] + "-" + arrParam[2] + "-" + id + ".log";
            LogUtility.WriteToFile(errLog.ToString(), path1);
            //write to config file
            StreamWriter sw;
            lock (_locker)
            {
                if (!File.Exists(arrParam[0] + @"\" + arrParam[1] + "-" + arrParam[2] + ".config"))
                {
                    sw = new StreamWriter(File.Create(arrParam[0] + @"\" + arrParam[1] + "-" + arrParam[2] + ".config"));
                }
                else
                {
                    sw = new StreamWriter(arrParam[0] + @"\" + arrParam[1] + "-" + arrParam[2] + ".config", true);
                }
                sw.Write(config.ToString());
                sw.Close();
            }
            con.Close();
        }

        //each DB shoud be dedicated one thread in order to improve the performance
        public void Restore(object threadParam)
        {
            var arrParam = (string[])threadParam;
            var folderpath = arrParam[0];
            var SPLine = arrParam[1];
            var username = arrParam[2];
            var password = arrParam[3];
            var isSp = arrParam[4] == "true";

            var lstSPName = SPLine.Substring(0, SPLine.Length - 1).Split(',');//ignore the last comma caused by the scripting out process
            //connect to server
            var isConnected = this.Connect(username, password);

            StreamReader str;
            var con = new SqlConnection(this._connection);
            this._server = new Server(new ServerConnection(con));

            var errLog = new StringBuilder();
            var tempSpContent = string.Empty;

            if (isConnected)
            {
                for (var i = 0; i < lstSPName.Length; i++)
                {
                    try
                    {
                        //remove SP if existing
                        if (this._sqlUtil.IsScriptExisted(this._name, lstSPName[i], isSp))
                        {
                            this._sqlUtil.DropScript(this._name, lstSPName[i], isSp);
                        }
                        var ft = File.Open(folderpath + @"\" + lstSPName[i] + ".sql", FileMode.Open, FileAccess.Read);
                        str = new StreamReader(ft);
                        tempSpContent = str.ReadToEnd();
                        /*string ret = _sqlUtil.CreateSP(this._name, tempSpContent);
                        if (!(ret == "Success."))
                            Console.WriteLine(ret);*/
                        this._server.ConnectionContext.ExecuteNonQuery(tempSpContent);

                        str.Close();
                        ft.Close();
                    }
                    catch (Exception ex)
                    {
                        errLog.AppendLine(lstSPName[i] + ": " + ex.Message);
                    }
                }
                this._sqlUtil.Disconnect();
                con.Close();
                //write error log to file
                if (!String.IsNullOrEmpty(errLog.ToString()))
                {
                    var rootPath = folderpath.Substring(0, folderpath.LastIndexOf('\\'));
                    var logFolder = rootPath + @"\Log";
                    if (!Directory.Exists(logFolder))
                    {
                        Directory.CreateDirectory(logFolder);
                    }
                    var ft = File.Open(logFolder + @"\" + this._serverName + "-" + this._name +
                                                "-" + DateTime.Now.Year + DateTime.Now.Month +
                                                DateTime.Now.Day + DateTime.Now.Hour +
                                                DateTime.Now.Minute + DateTime.Now.Second + ".log",
                                                FileMode.Create, FileAccess.Write);
                    var sr = new StreamWriter(ft);
                    sr.Write(errLog.ToString());
                    sr.Close();
                    ft.Close();
                }
            }
        }
        #endregion behavior
    }
}
