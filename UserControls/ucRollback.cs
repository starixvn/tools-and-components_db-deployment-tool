namespace DbDeployer.UserControls
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Configuration;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using DbDeployer.Core;
    using DbDeployer.Core.Utils;

    public partial class ucRollback : ucBase
    {
        // column name of grid
        private const string _columnNo = "No";
        private const string _columnSelect = "Choose";
        // private const string _columnOnlyRename = "OnlyRename";
        private const string _columnServerName = "ServerName";
        private const string _columnDBName = "DBName";
        private const string _columnSpName = "ScriptName";
        private const string _columnIsFunction = "IsFunction";
        private const string _columnIp = "IP";
        private const string _columnFileName = "FileName";

        private Color _color1 = Color.Lavender;
        private Color _color2 = Color.White;

        private DataTable _currentTable;
        private DataTable _loadedTable;

        string _scriptFolderNameType = string.Empty;
        private string _description = string.Empty;

        private string _scriptFolderName;
        private string _configFileName;
        private string _suffix;
        Phases _phase;

        private bool _isConnected = false;

        private SqlServer _sqlServerUtil = new SqlServer();
        private ConfigUtility _configUtil = new ConfigUtility();
        public static Dictionary<string, ServerIpSetting> _serverConfig;
        public static Dictionary<string, string> _imageServers = new Dictionary<string, string>();

        public ucRollback()
        {
            _serverConfig = this._configUtil.GetIPSettings();
            _imageServers = this._configUtil.GetStandbyServers();

            this.InitializeComponent();
        }

        private string GetImageServer(string keySetting)
        {
            if (_imageServers.ContainsKey(keySetting))
            {
                return _imageServers[keySetting];
            }

            return string.Empty;
        }

        private string GetSuffix()
        {
            string scriptFolderNameType = string.Empty;

            if (this.rbtPrerelease.Checked)
            {
                this._phase = Phases.PreRelease;
                this._suffix = "_xpre";
                scriptFolderNameType = "Pre-release";
            }
            else if (this.rbtTest.Checked)
            {
                this._phase = Phases.Test;
                this._suffix = "_xtest";
                scriptFolderNameType = "Test";
            }
            else if (this.rbtUAT1.Checked)
            {
                this._phase = Phases.UAT1;
                this._suffix = "_UAT1";
                scriptFolderNameType = "UAT1";
            }
            else if (this.rbtUAT2.Checked)
            {
                this._phase = Phases.UAT2;
                this._suffix = "_UAT2";
                scriptFolderNameType = "UAT2";
            }
            else if (this.rbtUAT3.Checked)
            {
                this._phase = Phases.UAT3;
                this._suffix = "_UAT3";
                scriptFolderNameType = "UAT3";
            }
            else if (this.rbtUAT4.Checked)
            {
                this._phase = Phases.UAT4;
                this._suffix = "_UAT4";
                scriptFolderNameType = "UAT4";
            }
            else// release phase
            {
                this._phase = Phases.Release;
                this._suffix = string.Empty;
                scriptFolderNameType = "Release";
            }

            return scriptFolderNameType;
        }

        private bool CheckPrerequisites()
        {
            this.rtxtOutput.Clear();

            //check user name and pass
            if (string.IsNullOrEmpty(this.txtUserName.Text.Trim())
                || string.IsNullOrEmpty(this.txtPassword.Text.Trim()))
            {
                UIUtility.AppendColoredText(this.rtxtOutput, "'" + Properties.Resources.ACCOUNT_ERROR_MESS + "'", Color.Red);
                return false;
            }

            //if account used for only one server,it must be specified
            if (!this.chkAllServers.Checked &&
                string.IsNullOrEmpty(this.cbbSourceServer.SelectedItem.ToString()))
            {
                UIUtility.AppendColoredText(this.rtxtOutput, "'" + Properties.Resources.SELECT_SERVER_ERROR_MESS + "'", Color.Red);
                return false;
            }

            //check whether there's still some error row in the grid or not
            var strErrorRow = string.Empty;
            //get all rows have error
            for (var j = 0; j < this.dgvSPList.Rows.Count - 1; j++)
            {
                if (this.dgvSPList[_columnSelect, j].Value.ToString() == "True" && !string.IsNullOrEmpty(this.dgvSPList.Rows[j].ErrorText))
                    strErrorRow += " " + (j + 1).ToString();
            }

            if (!string.IsNullOrWhiteSpace(strErrorRow))
            {
                UIUtility.AppendColoredText(this.rtxtOutput, "'" +
                    Properties.Resources.ROWS_ERROR_MESS + strErrorRow + "'", Color.Red);
                return false;
            }

            return true;
        }

        private void InitializeProgressBar()
        {
            //initailize the progress bar
            this.prgbScript.Visible = true;
            this.prgbScript.Maximum = (this.dgvSPList.Rows.Count - 1) * 10;
            this.prgbScript.Step = 10;
            this.prgbScript.Value = 0;
        }

        private void cbbSourceServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._currentTable = new DataTable();
            this._currentTable = this._loadedTable.Copy();
            if (this.cbbSourceServer.SelectedIndex != 0)//"---All Server---" selected
            {
                for (var i = this._currentTable.Rows.Count - 1; i >= 0; i--)
                {
                    if (!(String.Compare(this._currentTable.Rows[i][_columnServerName].ToString(), this.cbbSourceServer.SelectedItem.ToString(), true) == 0))
                    {
                        this._currentTable.Rows.RemoveAt(i);
                    }
                }
            }

            this.dgvSPList.DataSource = this._currentTable;

            ChangeColumnWidth(this._currentTable);
            dgvSPList_ColumnHeaderMouseClick(null, null);
        }

        private void btnOpenConfig_Click(object sender, EventArgs e)
        {
            this._scriptFolderNameType = this.GetSuffix();

            var dlg = new OpenFileDialog
                {
                    Filter = "Config file (*.config)|*.config",
                    CheckFileExists = true,
                    CheckPathExists = true,
                    InitialDirectory = AppDomain.CurrentDomain.BaseDirectory
                };

            dlg.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + this._scriptFolderNameType;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Read config file and populate to the grid
                    var table = new DataTable();
                    table.Columns.Add(new DataColumn(_columnSelect, Type.GetType("System.Boolean")));
                    table.Columns.Add(new DataColumn(_columnServerName, Type.GetType("System.String")));
                    table.Columns.Add(new DataColumn(_columnDBName, Type.GetType("System.String")));
                    table.Columns.Add(new DataColumn(_columnSpName, Type.GetType("System.String")));
                    table.Columns.Add(new DataColumn(_columnIsFunction, Type.GetType("System.String")));
                    table.Columns.Add(new DataColumn(_columnIp, Type.GetType("System.String")));
                    table.Columns.Add(new DataColumn(_columnFileName, Type.GetType("System.String")));

                    // get description
                    this._configFileName = Path.GetFileNameWithoutExtension(dlg.FileName);// file name format date-time-desc
                    var datetime = this._configFileName.Split('-')[0] + "-" + this._configFileName.Split('-')[1];
                    this._scriptFolderName = dlg.FileName.Substring(0, dlg.FileName.LastIndexOf(@"\"));

                    var splitFileName = this._configFileName.Split('-');
                    this._description = splitFileName[2];
                    // incase, there's some '-' character in the description
                    if (splitFileName.Count() > 3)
                    {
                        for (var d = 3; d < splitFileName.Count(); d++)
                        {
                            this._description += "-" + splitFileName[d];
                        }
                    }

                    try
                    {
                        using (var fs = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read))
                        {
                            using (var sr = new StreamReader(fs))
                            {
                                string line = string.Empty;
                                while ((line = sr.ReadLine()) != null)
                                {
                                    // parsing for ServerName, DBName and SPName
                                    var splitOfLine = line.Split(':');
                                    var splitOfKeySetting = splitOfLine[0].Split('-');
                                    var arrSP = splitOfLine[1].Split(',');

                                    var serverName = splitOfKeySetting[0].ToUpper().Trim();
                                    var dbName = splitOfKeySetting[1].Trim();
                                    var keySetting = this._configUtil.GetKey(serverName, dbName);
                                    var ip = _serverConfig[keySetting].Ip;

                                    var filePrefix = String.Format("{0}-{1}-{2}", datetime, keySetting, this._description);

                                    for (var i = 0; i < arrSP.Length; i++)
                                    {
                                        var row = table.NewRow();
                                        row[_columnSelect] = true;
                                        row[_columnServerName] = serverName;
                                        row[_columnDBName] = dbName;
                                        row[_columnSpName] = arrSP[i].Replace("-x-func-x-", string.Empty).Trim();
                                        row[_columnIsFunction] = arrSP[i].Contains("-x-func-x-") ? true : false;
                                        row[_columnIp] = ip;
                                        row[_columnFileName] = String.Format("{0}-{1}{2}", filePrefix, arrSP[i].Replace("-x-func-x-", string.Empty), ".sql");
                                        table.Rows.Add(row);
                                    }

                                }
                            }
                        }

                        table = AutoNumberedTable(table);
                        this.dgvSPList.DataSource = table;
                        ChangeColumnWidth(table);

                        // add checkbox header
                        var rect = this.dgvSPList.GetCellDisplayRectangle(1, -1, true);
                        // set checkbox header to center of header cell.
                        rect.X = rect.Location.X + (rect.Width / 2) + 15;
                        rect.Y = rect.Location.Y + 4;

                        var checkboxHeader = new CheckBox
                            {
                                Name = "checkboxHeader",
                                Size = new Size(14, 14),
                                Location = rect.Location
                            };
                        checkboxHeader.Click += this.checkboxHeader_OnClick;
                        this.dgvSPList.Controls.Add(checkboxHeader);
                        checkboxHeader.Checked = true;

                        // sort table by dbname
                        this.dgvSPList.Sort(this.dgvSPList.Columns[_columnServerName], ListSortDirection.Ascending);

                        // Keep the origin state of the grid
                        this._loadedTable = (DataTable)this.dgvSPList.DataSource;
                        this._currentTable = this._loadedTable.Copy();


                        // Populate server name to combobox
                        var svNames = new ArrayList { "" };

                        for (var i = 0; i < this.dgvSPList.Rows.Count - 1; i++)
                        {
                            if (!svNames.Contains(this.dgvSPList[2, i].Value.ToString().ToUpper()))
                            {
                                svNames.Add(this.dgvSPList[2, i].Value.ToString().ToUpper());
                            }
                        }

                        this.cbbSourceServer.DataSource = svNames;

                        if (this.dgvSPList.Rows.Count > 1)
                        {
                            this.pnl_Action.Visible = true;
                            this.dgvSPList.AllowUserToAddRows = false;
                            this.rtxtOutput.Clear();
                            this.btnCheckSpExist.Enabled = true;
                            this.btnRollback.Enabled = true;
                            this.dgvSPList.ReadOnly = false;
                            this.chkAllServers.Enabled = true;
                            dgvSPList.Columns[_columnNo].ReadOnly = true;
                            dgvSPList.Columns[_columnServerName].ReadOnly = true;
                            dgvSPList.Columns[_columnDBName].ReadOnly = true;
                            dgvSPList.Columns[_columnSpName].ReadOnly = true;
                            dgvSPList.Columns[_columnIsFunction].ReadOnly = true;
                            dgvSPList.Columns[_columnIp].ReadOnly = true;
                            dgvSPList.Columns[_columnFileName].ReadOnly = true;
                            this.chkAllServers.Enabled = true;
                            ColorGrid();
                        }

                    }
                    catch (IOException ex)
                    {
                        Console.Out.WriteLine(ex.Message);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("The source file is invalid. Please choose another file in BACKUP folder");
                    }
                }
                catch
                {
                    MessageBox.Show("The source file is invalid. Please choose another file in BACKUP folder");
                }
            }
        }

        private void checkboxHeader_OnClick(object sender, EventArgs e)
        {
            for (var i = 0; i < this.dgvSPList.RowCount; i++)
            {
                this.dgvSPList[_columnSelect, i].Value = ((CheckBox)this.dgvSPList.Controls.Find("checkboxHeader", true)[0]).Checked;
            }

            this.dgvSPList.EndEdit();
            ChangeColumnWidth(this._currentTable);
            dgvSPList_ColumnHeaderMouseClick(null, null);
        }

        private void ChangeColumnWidth(DataTable data)
        {
            if (data.Rows.Count > 5)
            {
                this.dgvSPList.Columns[_columnFileName].Width = 132;
            }
            else
            {
                this.dgvSPList.Columns[_columnFileName].Width = 150;
            }
        }

        private void btnCheckSpExist_Click(object sender, EventArgs e)
        {
            if (!this.CheckPrerequisites())
            {
                return;
            }

            // this.GetSuffix();

            this.InitializeProgressBar();

            SaveUserIntoConfig();

            this.rtxtOutput.Text = string.Empty;
            var arrSpsName = new ArrayList();
            for (var i = 0; i < this.dgvSPList.RowCount; i++)
            {
                this.dgvSPList.Rows[i].Selected = false;
                var serverName = this.dgvSPList[_columnServerName, i].Value.ToString().ToUpper().Trim();
                var dbName = this.dgvSPList[_columnDBName, i].Value.ToString().Trim();
                var spName = this.dgvSPList[_columnSpName, i].Value.ToString().Trim();
                var keySetting = this._configUtil.GetKey(serverName, dbName);
                var isSp = this.dgvSPList[_columnIsFunction, i].Value.ToString() != "True";

                if (!string.IsNullOrEmpty(this.dgvSPList[_columnSpName, i].Value.ToString())
                    && (this.dgvSPList[_columnSelect, i].Value.ToString() == "True"))
                {
                    // get IP according to selected phase
                    var ip = _serverConfig[keySetting].GetIpByPhase(this._phase);

                    this._isConnected = this._sqlServerUtil.Connect(ip, this.txtUserName.Text, this.txtPassword.Text);
                    if (this._isConnected)
                    {
                        // Check for whether a script already exists or not. If so, adding (*) at the end of script name in array of SPs name.
                        if (this._sqlServerUtil.IsScriptExisted(dbName, spName + this._suffix, isSp))
                        {
                            this.dgvSPList.Rows[i].Selected = true;
                            arrSpsName.Add("Row " + (i + 1).ToString() + ": " + serverName + '.' + dbName + '.' + spName + this._suffix + " (*)");
                        }
                        else
                        {
                            arrSpsName.Add("Row " + (i + 1).ToString() + ": " + serverName + '.' + dbName + '.' + spName + this._suffix);
                        }
                    }
                    else
                    {
                        var error = "Row " + (i + 1).ToString() + ": \"Login failed for user \'" + txtUserName.Text + "\'\"";
                        UIUtility.AppendColoredText(this.rtxtOutput, error + "\r\n", Color.Black);
                    }
                }

                this.prgbScript.PerformStep();
            }

            var countSPExist = 0;// exist SPs counter
            foreach (string sp in arrSpsName)
            {
                // Exist SPs
                if (sp.IndexOf("(*)") > -1)
                {
                    UIUtility.AppendColoredText(this.rtxtOutput, sp + "\r\n", Color.Red);
                    countSPExist += 1;
                }
                else
                {
                    UIUtility.AppendColoredText(this.rtxtOutput, sp + "\r\n", Color.Black);
                }
            }

            UIUtility.AppendColoredText(this.rtxtOutput, "-----------------------------\r\nTotal: " + countSPExist + " script(s) existed." + "\r\n", Color.Red);
            this._sqlServerUtil.Disconnect();

            this.prgbScript.Visible = false;
        }

        private void btnRollback_Click(object sender, EventArgs e)
        {
            if (!this.CheckPrerequisites())
            {
                return;
            }

            this.InitializeProgressBar();
            this.rtxtOutput.Clear();
            this.dgvSPList.Sort(this.dgvSPList.Columns[_columnServerName], ListSortDirection.Ascending);
            var datetime = StringUtility.GetFormatedCurrentDateTime();
            var dicSPDeployToImageSV = new Dictionary<string, ISqlScript>();
            var tempServer = string.Empty;
            StringBuilder resultText = new StringBuilder();

            SaveUserIntoConfig();
            ColorGrid();

            for (var i = 0; i < this.dgvSPList.Rows.Count; i++)
            {
                if (this.dgvSPList[_columnSelect, i].Value.ToString() == "True")
                {
                    var isSp = this.dgvSPList[_columnIsFunction, i].Value.ToString() != "True";
                    var serverName = this.dgvSPList[_columnServerName, i].Value.ToString().ToUpper().Trim();
                    var dbName = this.dgvSPList[_columnDBName, i].Value.ToString().ToUpper().Trim();
                    var spName = this.dgvSPList[_columnSpName, i].Value.ToString().Trim();
                    var keySetting = this._configUtil.GetKey(serverName, dbName);

                    try
                    {
                        // Avoid redundant connection to server
                        if (tempServer != serverName || !this._isConnected)
                        {
                            this._sqlServerUtil.Disconnect();
                            this._isConnected = this._sqlServerUtil.Connect(
                                _serverConfig[keySetting].GetIpByPhase(this._phase),
                                this.txtUserName.Text, this.txtPassword.Text);

                            tempServer = serverName;
                        }

                        // Drop existed sp
                        if (this._isConnected)
                        {
                            string scriptContent = string.Empty;

                            using (var fs = new FileStream(this._scriptFolderName + @"\" + this.dgvSPList[_columnFileName, i].Value, FileMode.Open))
                            {
                                using (var sr = new StreamReader(fs))
                                {
                                    scriptContent = sr.ReadToEnd();
                                }
                            }

                            scriptContent = ChangeScriptCreateOrAlterFollowExistSP(isSp, dbName, spName, scriptContent);

                            this._sqlServerUtil.CreateScript(dbName, scriptContent, isSp);
                            //UIUtility.AppendColoredText(this.rtxtOutput, "Roll back " + keySetting + ": " + spName + ": Success." + "\r\n", Color.Red);
                            resultText.Append("Roll back " + keySetting + ": " + spName + ": Success." + "\r\n");

                            //build up the SPs list for image server
                            var imageServers = this.GetImageServer(keySetting);
                            if (!string.IsNullOrEmpty(imageServers))
                            {
                                var arrImageServers = imageServers.Split(',');
                                foreach (var keyImage in arrImageServers)
                                {
                                    var splitImageServer = keyImage.Split('-');
                                    var imageServerName = splitImageServer[0].ToUpper().Trim();
                                    var imageDBName = splitImageServer[1].ToUpper().Trim();

                                    if (!string.IsNullOrEmpty(keyImage))
                                    {
                                        ISqlScript scriptDeployed;
                                        if (isSp)
                                        {
                                            scriptDeployed = new SqlSp(
                                                // SP Name
                                                spName,
                                                // source server IP
                                                this.dgvSPList[_columnIp, i].Value.ToString().Trim(),
                                                // source server name
                                                imageServerName,
                                                // DB Name
                                                imageDBName,
                                                // destination server IP
                                                _serverConfig[keyImage].GetIpByPhase(this._phase),
                                                // script file path
                                                this.dgvSPList[_columnFileName, i].Value.ToString().Trim(),
                                                // phase
                                                this._phase);
                                        }
                                        else
                                        {
                                            scriptDeployed = new SqlFunc(
                                                // SP Name
                                                spName,
                                                // source server IP
                                                this.dgvSPList[_columnIp, i].Value.ToString().Trim(),
                                                // source server name
                                                imageServerName,
                                                // DB Name
                                                imageDBName,
                                                // destination server IP
                                                _serverConfig[keyImage].GetIpByPhase(this._phase),
                                                // script file path
                                                this.dgvSPList[_columnFileName, i].Value.ToString().Trim(),
                                                // phase
                                                this._phase);
                                        }

                                        scriptDeployed.Content = new StringBuilder();
                                        scriptDeployed.Content.Append(scriptContent);

                                        var key = keySetting + "-" + spName + "," + scriptDeployed.SourceServerAliasName.ToString() + "-" + scriptDeployed.DbName + "-" + scriptDeployed.Name;
                                        if (!dicSPDeployToImageSV.ContainsKey(key))
                                        {
                                            dicSPDeployToImageSV.Add(key, scriptDeployed);
                                        }
                                    }
                                }
                            }

                            LogUtility.WriteLog("Roll back: " + serverName + " - " + dbName + " - " + spName + this._suffix, AppDomain.CurrentDomain.BaseDirectory + @"Log\" + datetime + "-log.txt");
                        }
                        else
                        {
                            UIUtility.AppendColoredText(this.rtxtOutput, "\"Row " + (i + 1) + ": Cannot connect to " + _serverConfig[keySetting].GetIpByPhase(this._phase) + "\"\r\n", Color.Red);
                            this.dgvSPList.Rows[i].DefaultCellStyle.BackColor = Color.Pink;
                        }
                    }
                    catch (Exception ex)
                    {
                        UIUtility.AppendColoredText(this.rtxtOutput, "\"Row " + (i + 1) + ": Roll back " + keySetting + "-" + this.dgvSPList[7, i].Value + ": " + ex.Message + "\"\r\n", Color.Red);
                        this.dgvSPList.Rows[i].DefaultCellStyle.BackColor = Color.Pink;
                    }
                }

                this.prgbScript.PerformStep();
            }

            //Rollback image servers
            var isImageServerConnected = false;
            var previousDesSVIP = string.Empty;

            foreach (var key in dicSPDeployToImageSV.Keys)
            {
                var keyMainServer = key.Split(',')[0];
                var script = dicSPDeployToImageSV[key];
                try
                {
                    // avoid redundant connection
                    if (previousDesSVIP != dicSPDeployToImageSV[key].DestinationServerIp || !isImageServerConnected)
                    {
                        this._sqlServerUtil.Disconnect();
                        isImageServerConnected = this._sqlServerUtil.Connect(dicSPDeployToImageSV[key].DestinationServerIp, this.txtUserName.Text, this.txtPassword.Text);
                        previousDesSVIP = dicSPDeployToImageSV[key].DestinationServerIp;

                        if (isImageServerConnected)
                        {
                            this._sqlServerUtil.CreateScript(dicSPDeployToImageSV[key].DbName, Properties.Resources.DBA_GrantPermission, true);
                        }
                    }

                    if (isImageServerConnected)
                    {
                        
                        var tempContent = new StringBuilder();

                        tempContent.Append(ChangeScriptCreateOrAlterFollowExistSP(script.IsSp, script.DbName, script.Name, script.Content.ToString()));
                        script.Content = tempContent;

                        // deploy server standby
                        script.Deploy(this._sqlServerUtil);
                    }
                    else
                    {
                        UIUtility.AppendColoredText(this.rtxtOutput, "\"Row " + SetColorForImagesServerDeployError(keyMainServer)
                            + "Standby Server-" + dicSPDeployToImageSV[key].SourceServerAliasName
                            + ": Could not connect to destination database " + dicSPDeployToImageSV[key].DestinationServerIp
                            + " to deploy SP/Function " + dicSPDeployToImageSV[key].Name + "\"\n", Color.Red);
                    }
                }
                catch (Exception ex)
                {
                    UIUtility.AppendColoredText(this.rtxtOutput, "\"Row " + SetColorForImagesServerDeployError(keyMainServer)
                                + ": Standby Server-" + script.SourceServerAliasName
                                + "-" + script.ScriptFilePath + ":" + ex.Message + "\"\r\n", Color.Red);
                }
            }

            UIUtility.AppendColoredText(this.rtxtOutput, resultText.ToString(), Color.Black);

            for (int i = 0; i < dgvSPList.Rows.Count; i++)
            {
                dgvSPList.Rows[i].Cells[0].Value = i + 1;
            }

            this._sqlServerUtil.Disconnect();
            this.prgbScript.Visible = false;
        }

        private string ChangeScriptCreateOrAlterFollowExistSP(bool isSp, string dbName, string spName, string scriptContent)
        {
            if (this._sqlServerUtil.IsScriptExisted(dbName, spName + this._suffix, isSp))
            {
                scriptContent = isSp ? scriptContent.Replace("CREATE PROCEDURE", "ALTER PROCEDURE").Replace("CREATE PROC", "ALTER PROCEDURE")
                    : scriptContent.Replace("CREATE FUNCTION", "ALTER FUNCTION").Replace("CREATE FUNC", "ALTER FUNCTION");
            }
            else
            {
                scriptContent = isSp ? scriptContent.Replace("ALTER PROCEDURE", "CREATE PROCEDURE").Replace("ALTER PROC", "CREATE PROCEDURE")
                    : scriptContent.Replace("ALTER FUNCTION", "CREATE FUNCTION").Replace("ALTER FUNC", "CREATE FUNCTION");
            }
            return scriptContent;
        }

        private int SetColorForImagesServerDeployError(string key)
        {
            int i = 0;
            for (i = 0; i < this.dgvSPList.Rows.Count; i++)
            {
                if ((this.dgvSPList.Rows[i].Cells[_columnServerName].Value.ToString() + "-" + this.dgvSPList.Rows[i].Cells[_columnDBName].Value.ToString().ToUpper() + "-" + this.dgvSPList.Rows[i].Cells[_columnSpName].Value.ToString()) == key)
                {
                    this.dgvSPList.Rows[i].DefaultCellStyle.BackColor = Color.Pink;
                    break;
                }
            }

            return (i + 1);
        }

        private void rtxtOutput_DoubleClick(object sender, EventArgs e)
        {
            if (rtxtOutput.Text.Length == 0)
            {
                return;
            }

            var f = new ScriptViewer();
            f.UpdateScript(rtxtOutput.Text);
            f.ShowDialog();
        }

        private void buttonZoom_Click(object sender, EventArgs e)
        {
            if (rtxtOutput.Text.Length == 0)
            {
                return;
            }

            var f = new ScriptViewer();
            f.UpdateScript(rtxtOutput.Text);
            f.ShowDialog();
        }

        public DataTable AutoNumberedTable(DataTable SourceTable)
        {
            DataTable ResultTable = new DataTable();
            DataColumn AutoNumberColumn = new DataColumn(); AutoNumberColumn.ColumnName = "No";

            AutoNumberColumn.DataType = typeof(int);
            AutoNumberColumn.AutoIncrement = true;
            AutoNumberColumn.AutoIncrementSeed = 1;
            AutoNumberColumn.AutoIncrementStep = 1;
            ResultTable.Columns.Add(AutoNumberColumn);

            ResultTable.Merge(SourceTable);
            return ResultTable;
        }

        private void ColorGrid()
        {
            for (var i = 0; i < this.dgvSPList.Rows.Count; i++)
            {
                if ((bool)this.dgvSPList.Rows[i].Cells[_columnSelect].Value)
                {
                    this.dgvSPList.Rows[i].DefaultCellStyle.BackColor = this._color1;
                }
                else
                {
                    this.dgvSPList.Rows[i].DefaultCellStyle.BackColor = this._color2;
                }
            }
        }

        private void dgvSPList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (this.dgvSPList.Columns[e.ColumnIndex].Name == _columnSelect)
                {
                    this.dgvSPList.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    var check = (bool)this.dgvSPList.Rows[e.RowIndex].Cells[_columnSelect].Value;
                    if (check)
                    {
                        this.dgvSPList.Rows[e.RowIndex].DefaultCellStyle.BackColor = this._color1;
                    }
                    else
                    {
                        this.dgvSPList.Rows[e.RowIndex].DefaultCellStyle.BackColor = this._color2;
                    }
                }
            }
            catch { }
        }

        private void dgvSPList_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                for (int i = 0; i < dgvSPList.Rows.Count; i++)
                {
                    dgvSPList.Rows[i].Cells[_columnNo].Value = i + 1;

                    if ((bool)this.dgvSPList.Rows[i].Cells[_columnSelect].Value)
                    {
                        this.dgvSPList.Rows[i].DefaultCellStyle.BackColor = this._color1;
                    }
                    else
                    {
                        this.dgvSPList.Rows[i].DefaultCellStyle.BackColor = this._color2;
                    }
                }
            }
            catch { }
        }

        private void SaveUserIntoConfig()
        {
            if (chk_SaveUserName.Checked)
            {
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["UserName"].Value = txtUserName.Text.Trim();
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        private void ucRollback_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["UserName"].ToString()))
            {
                txtUserName.Text = System.Configuration.ConfigurationManager.AppSettings["UserName"].ToString();
            }
        }

        private void chkAllServers_CheckedChanged(object sender, EventArgs e)
        {
            this.cbbSourceServer.Enabled = !this.cbbSourceServer.Enabled;
            //Reload the original table from excel file
            if (this.chkAllServers.Checked)
            {
                this.cbbSourceServer.SelectedIndex = 0;//set the combobox to empty item
                this.dgvSPList.DataSource = this._loadedTable;
            }

            ChangeColumnWidth(this._loadedTable);
            dgvSPList_ColumnHeaderMouseClick(null, null);
        }

        private void rbtPrerelease_CheckedChanged(object sender, EventArgs e)
        {
            ResetGird();
        }

        private void rbtTest_CheckedChanged(object sender, EventArgs e)
        {
            ResetGird();
        }

        private void rbtRelease_CheckedChanged(object sender, EventArgs e)
        {
            ResetGird();
        }

        private void ResetGird()
        {
            this.dgvSPList.AllowUserToAddRows = true;
            for (int i = this.dgvSPList.Rows.Count - 2; i >= 0; i--)
            {
                this.dgvSPList.Rows.RemoveAt(i);
            }

            this.pnl_Action.Visible = false;
            this.btnCheckSpExist.Enabled = false;
            this.btnRollback.Enabled = false;
        }
    }
}
