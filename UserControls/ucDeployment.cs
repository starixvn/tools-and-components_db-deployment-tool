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
    using DbDeployer.Core.Utils;

    public partial class ucDeployment : ucBase
    {
        #region define var
        // column name of grid
        private const string _columnNo = "No";
        private const string _columnSelect = "Choose";
        private const string _columnOnlyRename = "OnlyRename";
        private const string _columnServerName = "ServerName";
        private const string _columnDBName = "DBName";
        private const string _columnSpName = "ScriptName";
        private const string _columnIsFunction = "IsFunction";
        private const string _columnIp = "IP";
        private const string _columnFileName = "FileName";

        private Color _color1 = Color.Lavender;
        private Color _color2 = Color.White;

        // 3: Server Name, 4: DB Name, 5: SP Name
        private int[] _checkColumnsList = { 3, 4, 5 };
        private DataTable _currentTable;
        private DataTable _loadedTable;

        private string _configFileName = string.Empty;
        private string _scriptFolderName = string.Empty;
        private string _description = string.Empty;

        private string _suffix = string.Empty;
        private Phases _selectedPhase = Phases.PreRelease;
        private string _suffixToRemove = string.Empty;
        private Phases _sourcePhase = Phases.Release;

        private ArrayList _arrScriptDeployed = new ArrayList();
        

        private ConfigUtility _configUtil = new ConfigUtility();
        private SqlServer _sqlServerUtil = new SqlServer();

        // public
        public static Dictionary<string, ServerIpSetting> _serverConfig;
        public static Dictionary<string, string> _imageServers = new Dictionary<string, string>();

        #endregion

        public ucDeployment()
        {
            _serverConfig = this._configUtil.GetIPSettings();
            _imageServers = this._configUtil.GetStandbyServers();
            
            this.InitializeComponent();
        }

        private void ucDeployment_Load(object sender, EventArgs e)
        {
            this.rtxtOutput.AutoScroll = false;
            if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["UserName"].ToString()))
            {
                txtUserName.Text = System.Configuration.ConfigurationManager.AppSettings["UserName"].ToString();
            }
        }

        #region Main actions
        private void btnOpenConfig_Click(object sender, EventArgs e)
        {
            this.rtxtOutput.Clear();
            var dlg = new OpenFileDialog
            {
                Filter = "Config file (*.config)|*.config",
                CheckFileExists = true,
                CheckPathExists = true,
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + @"Script"
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Read config file and populate to the grid
                    var table = new DataTable();
                    table.Columns.Add(new DataColumn(_columnSelect, Type.GetType("System.Boolean")));
                    table.Columns.Add(new DataColumn(_columnOnlyRename, Type.GetType("System.Boolean")));
                    table.Columns.Add(new DataColumn(_columnServerName, Type.GetType("System.String")));
                    table.Columns.Add(new DataColumn(_columnDBName, Type.GetType("System.String")));
                    table.Columns.Add(new DataColumn(_columnSpName, Type.GetType("System.String")));
                    table.Columns.Add(new DataColumn(_columnIsFunction, Type.GetType("System.String")));
                    table.Columns.Add(new DataColumn(_columnIp, Type.GetType("System.String")));
                    table.Columns.Add(new DataColumn(_columnFileName, Type.GetType("System.String")));

                    var datetime = dlg.FileName.Split('\\')[dlg.FileName.Split('\\').Length - 1].Split('.')[0].Replace("-Desc", string.Empty);
                    // get description
                    // file name format date-time-desc
                    this._configFileName = Path.GetFileNameWithoutExtension(dlg.FileName);
                    this._scriptFolderName = dlg.FileName.Substring(0, dlg.FileName.LastIndexOf(@"\"));

                    var splitFileName = this._configFileName.Split('-');
                    this._description = splitFileName[2];
                    // incase, there's some '-' character in the description
                    if (splitFileName.Count() > 3)
                    {
                        for (var i = 3; i < splitFileName.Count(); i++)
                        {
                            this._description += "-" + splitFileName[i];
                        }
                    }

                    try
                    {
                        using (var fs = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read))
                        {
                            using (var sr = new StreamReader(fs))
                            {
                                string line;
                                while ((line = sr.ReadLine()) != null)
                                {
                                    // parsing for ServerName, DBName and SPName
                                    string[] splitOfLine = line.Split(':');
                                    string[] splitOfKeySetting = splitOfLine[0].Split('-');
                                    string[] arrSP = splitOfLine[1].Split(',');

                                    var serverName = splitOfKeySetting[0].ToUpper().Trim();
                                    var dbName = splitOfKeySetting[1].Trim();
                                    var keySetting = this._configUtil.GetKey(serverName, dbName);
                                    string ip = _serverConfig[keySetting].Ip;

                                    string filePrefix = String.Format("{0}-{1}-{2}", datetime, keySetting, this._description);

                                    for (int i = 0; i < arrSP.Length; i++)
                                    {
                                        var spName = arrSP[i].Replace("-x-func-x-", string.Empty).Trim();
                                        DataRow row = table.NewRow();
                                        row[_columnSelect] = true;
                                        row[_columnOnlyRename] = false;
                                        row[_columnServerName] = serverName;
                                        row[_columnDBName] = dbName;
                                        row[_columnSpName] = spName;
                                        row[_columnIsFunction] = arrSP[i].Contains("-x-func-x-") ? true : false;
                                        row[_columnIp] = ip;
                                        row[_columnFileName] = String.Format("{0}-{1}{2}", filePrefix, spName, ".sql");

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

                        // sort table by server name
                        this.dgvSPList.Sort(this.dgvSPList.Columns[_columnServerName], ListSortDirection.Ascending);

                        // Keep the origin state of the grid
                        this._loadedTable = (DataTable)this.dgvSPList.DataSource;
                        this._currentTable = this._loadedTable.Copy();

                        // Populate server name to combobox
                        var svNames = new ArrayList
                        {
                            //empty item selected when account used for all servers
                            ""
                        };

                        for (var i = 0; i < this.dgvSPList.Rows.Count; i++)
                        {
                            if (!svNames.Contains(this.dgvSPList[_columnServerName, i].Value))
                            {
                                svNames.Add(this.dgvSPList[_columnServerName, i].Value);
                            }
                        }

                        this.cbbSourceServer.DataSource = svNames;
                        this.chkAllServers.Enabled = true;

                        if (this.dgvSPList.Rows.Count > 1)
                        {
                            this.pnl_Action.Visible = true;
                            this.dgvSPList.AllowUserToAddRows = false;
                            this.dgvSPList.ReadOnly = false;
                            this.btnCheckSpExist.Enabled = true;
                            this.btnDeploy.Enabled = true;
                            this.btnPreview.Enabled = true;
                            dgvSPList.Columns[_columnNo].ReadOnly = true;
                            dgvSPList.Columns[_columnServerName].ReadOnly = true;
                            dgvSPList.Columns[_columnDBName].ReadOnly = true;
                            dgvSPList.Columns[_columnSpName].ReadOnly = true;
                            dgvSPList.Columns[_columnIsFunction].ReadOnly = true;
                            dgvSPList.Columns[_columnIp].ReadOnly = true;
                            dgvSPList.Columns[_columnFileName].ReadOnly = true;
                            ColorGrid();
                        }
                    }
                    catch (IOException ex)
                    {
                        Console.Out.WriteLine(ex.Message);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("The source file is invalid. Please choose another file");
                    }
                }
                catch
                {
                    MessageBox.Show("The source file is invalid. Please choose another file");
                }
            }

        }

        /// <summary>
        /// Check whether the selected SPs have been already existed on the destination server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckSpExist_Click(object sender, EventArgs e)
        {
            if (!this.CheckPrerequisites())
            {
                return;
            }

            this.GetSuffix();

            this.InitializeProgressBar();

            SaveUserIntoConfig();

            this.rtxtOutput.Text = string.Empty;
            var arrSpsName = new ArrayList();
            for (var i = 0; i < this.dgvSPList.RowCount; i++)
            {
                if (!string.IsNullOrEmpty(this.dgvSPList[_columnSpName, i].Value.ToString())
                    && (this.dgvSPList[_columnSelect, i].Value.ToString() == "True"))
                {
                    this.dgvSPList.Rows[i].Selected = false;
                    var serverName = this.dgvSPList[_columnServerName, i].Value.ToString().ToUpper().Trim();
                    var dbName = this.dgvSPList[_columnDBName, i].Value.ToString().Trim();
                    var spName = this.dgvSPList[_columnSpName, i].Value.ToString().Trim();
                    var keySetting = this._configUtil.GetKey(serverName, dbName);
                    var isSp = (this.dgvSPList[_columnIsFunction, i].Value.ToString()) != "True";
                    //get IP according to selected phase
                    string ip = string.Empty;

                    if (this._suffix == "_xpre")
                    {
                        ip = _serverConfig[keySetting].IpXpre;
                    }
                    else if (this._suffix == "_xtest")
                    {
                        ip = _serverConfig[keySetting].IpXtest;
                    }
                    else
                    {
                        ip = _serverConfig[keySetting].IpRelease;
                    }

                    var isConnected = this._sqlServerUtil.Connect(ip, this.txtUserName.Text, this.txtPassword.Text);
                    if (isConnected)
                    {
                        //Check for whether a SP already exists or not. If so, adding (*) at the end of SP name in array of SPs name.
                        if (this._sqlServerUtil.IsScriptExisted(dbName, spName + this._suffix, isSp))
                        {
                            this.dgvSPList.Rows[i].Selected = true;
                            arrSpsName.Add("Row " + (i + 1).ToString() + ": " + serverName + '.' + dbName + '.' + spName + this._suffix + " (*)" + (!isSp ? " (function)" : ""));
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

            this.rtxtOutput.Text = "";
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

            UIUtility.AppendColoredText(this.rtxtOutput, "----------------------------------------------------\r\nTotal: " + countSPExist + " script(s) existed." + "\r\n", Color.Red);
            this._sqlServerUtil.Disconnect();

            this.prgbScript.Visible = false;
        }

        /// <summary>
        /// Deploy the selected SPs on the grid to the destination server 
        /// that are already defined in the config file with an IP for a specific phase.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeployOrPreview_Click(object sender, EventArgs e)
        {
            if (!this.CheckPrerequisites())
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            this.InitializeProgressBar();
            try
            {
                #region define var

                StringBuilder resultText = new StringBuilder();
                string configFileContent = string.Empty;
                ArrayList arrScriptDeployed = new ArrayList();

                // deploy selected SPs to image server
                var dicSPDeployToImageSV = new Dictionary<string, ISqlScript>();

                // used for loading content of SP from a server
                SqlServer slqServerUtil_LoadSP = new SqlServer();

                string bkconfigname = string.Empty;
                string backupFolderPath = string.Empty;
                string scriptFolderNameType = string.Empty;

                var dictSVDB_SP_Deployed = new Dictionary<string, string>();
                var dictSVDB_SP_OnlyRenamed = new Dictionary<string, string>();
                var dictSVDB_Selected = new Dictionary<string, string>();

                var dictSPs_BackupExistedSP = new Dictionary<string, string>();
                var dicDeployedSucessSV_DB_SP = new List<string>(); // for image servers

                bool isConnectedToDestinationServer = false;
                bool isConnectedToSourceServer = false;
                string connection = string.Empty;
                string dateTimeNow = StringUtility.GetFormatedCurrentDateTime();
                bool isDeploy = ((Button)sender).Name == btnDeploy.Name;
                #endregion

                this.dgvSPList = UIUtility.RemoveDuplicateRows(this.dgvSPList, this._checkColumnsList);
                this.rtxtOutput.Clear();
                
                scriptFolderNameType = this.GetSuffix();
                SaveUserIntoConfig();
                ColorGrid();
                this.dgvSPList.Sort(this.dgvSPList.Columns[_columnServerName], ListSortDirection.Ascending);
                this.dgvSPList.Invalidate();

                this._arrScriptDeployed.Clear();

                #region Create folder
                bkconfigname = dateTimeNow + "-" + this._description;
                string bkfolder = AppDomain.CurrentDomain.BaseDirectory + scriptFolderNameType + @"\" + bkconfigname;

                if (isDeploy)
                {
                    try
                    {
                        if (!Directory.Exists(bkfolder))
                        {
                            Directory.CreateDirectory(bkfolder);
                        }
                        if (!Directory.Exists(bkfolder + @"\Backup"))
                        {
                            backupFolderPath = bkfolder + @"\Backup";
                            Directory.CreateDirectory(backupFolderPath);
                        }
                    }
                    catch
                    {
                    }
                }

                #endregion Create folder
                // description: in, dicSPDeployToImageSV: out, dictSVDB_SP_OnlyRenamed: out, dictSVDB_Selected: out
                ProcessedBeforeDeploy(isDeploy, dicSPDeployToImageSV, dictSVDB_SP_OnlyRenamed, dictSVDB_Selected);

                // Deploy selected SPs without rename only
                #region deploy to main servers & return result
                ISqlScript tempSqlScript = null;
                string tempSourceServerIP = string.Empty;

                foreach (ISqlScript sqlScript in this._arrScriptDeployed)
                {
                    try
                    {
                        var keySetting = this._configUtil.GetKey(sqlScript.SourceServerAliasName, sqlScript.DbName);
                        var isSp = sqlScript.IsSp;

                        isConnectedToDestinationServer = this.CheckConnectToDestinationServer(isConnectedToDestinationServer, tempSqlScript, sqlScript);

                        //if the connection is established successfully, deploy SP to destination server and DB
                        if (isConnectedToDestinationServer)
                        {
                            #region get script from scource
                            // if is Release and from Pre-Release or Test (if case deploy or preview from db)
                            if (this.rbtRelease.Checked && this.cbbScriptSource.SelectedItem.ToString() != "Origin")
                            {
                                // avoid redundant connection to source server
                                var sourceServerIP = _serverConfig[keySetting].GetIpByPhase(this._sourcePhase);

                                isConnectedToSourceServer = CheckConnectToSourceServer(slqServerUtil_LoadSP, isConnectedToSourceServer, tempSourceServerIP, sqlScript, sourceServerIP);

                                if (isConnectedToSourceServer)
                                {
                                    connection = "Data Source=" + sourceServerIP
                                        + ";Initial Catalog=" + sqlScript.DbName
                                        + ";User Id=" + this.txtUserName.Text
                                        + ";Password=" + this.txtPassword.Text + ";";

                                    //Load SP content from xpre or xtest DBMS
                                    sqlScript.LoadScriptFromDBAndRemoveSuffix(
                                        dictSVDB_Selected,
                                        this._sqlServerUtil,
                                        slqServerUtil_LoadSP,
                                        connection,
                                        this._suffixToRemove,
                                        _serverConfig);
                                }
                                else
                                {
                                    UIUtility.AppendColoredText(this.rtxtOutput, "\n\"Could not connect to source database " + sqlScript.SourceServerAliasName + " to get the script of SP " + sqlScript.Name + "\"",
                                                                        Color.Red);
                                }
                            }
                            // if case deploy or preview from script
                            else
                            {
                                //Load SP content from script file
                                sqlScript.LoadScriptFromScriptFileAndAddSuffix(dictSVDB_Selected, this._sqlServerUtil, _serverConfig);
                            }

                            #endregion

                            //if preview command has been invoked, just output the content of SPs to rich textbox
                            if (!isDeploy)
                            {
                                // Add Script into output
                                resultText.Append(sqlScript.Content.ToString());

                                #region Add Result Review success into "dictSVDB_SP_Deployed"
                                if (dictSVDB_SP_Deployed.ContainsKey(keySetting))// SV-DB
                                {
                                    dictSVDB_SP_Deployed[keySetting] += "," + sqlScript.Name + (isSp ? string.Empty : "-x-func-x-");
                                }
                                else
                                {
                                    dictSVDB_SP_Deployed.Add(keySetting, sqlScript.Name + (isSp ? string.Empty : "-x-func-x-"));
                                }
                                #endregion
                            }
                            else
                            {
                                # region Backup action
                                if (this.chkbBackup.Checked &&
                                    this._sqlServerUtil.IsScriptExisted(sqlScript.DbName, sqlScript.Name + this._suffix, isSp))
                                {

                                    var bkConnection = "Data Source=" + sqlScript.DestinationServerIp
                                    + ";Initial Catalog=" + sqlScript.DbName
                                    + ";User Id=" + this.txtUserName.Text
                                    + ";Password=" + this.txtPassword.Text + ";";

                                    sqlScript.Backup(this._sqlServerUtil, bkConnection, backupFolderPath, dateTimeNow, this._description);

                                    var scriptName = sqlScript.Name + (isSp ? string.Empty : "-x-func-x-");
                                    if (dictSPs_BackupExistedSP.ContainsKey(keySetting))
                                    {
                                        dictSPs_BackupExistedSP[keySetting] += "," + scriptName;
                                    }
                                    else
                                    {
                                        dictSPs_BackupExistedSP.Add(keySetting, scriptName);
                                    }
                                }

                                sqlScript.WriteContentToScriptFile(bkfolder + @"\" + dateTimeNow
                                                                                                + "-" + sqlScript.SourceServerAliasName
                                                                                                + "-" + sqlScript.DbName
                                                                                                + "-" + this._description
                                                                                                + "-" + sqlScript.Name + ".sql");
                                #endregion

                                #region Update Script for image Servers
                                // copy content of SP to the copy SP that will be deployed to image server
                                if (dicSPDeployToImageSV.Select(t => t.Key.Split(',')[0].ToString()).ToList().Contains(sqlScript.SourceServerAliasName + "-" + sqlScript.DbName + "-" + sqlScript.Name))
                                {
                                    foreach (var spDeployToImage in dicSPDeployToImageSV)
                                    {
                                        if (spDeployToImage.Key.Split(',')[0].ToString() == (sqlScript.SourceServerAliasName + "-" + sqlScript.DbName + "-" + sqlScript.Name))
                                        {
                                            dicSPDeployToImageSV[spDeployToImage.Key].Content = sqlScript.Content;
                                        }
                                    }
                                }

                                #endregion

                                ChangeScriptCreateOrAlterFollowExistSP(sqlScript);

                                // deploy main server
                                sqlScript.Deploy(this._sqlServerUtil);

                                #region write log sp deploy success
                                // write log sp deploy success
                                dicDeployedSucessSV_DB_SP.Add(sqlScript.SourceServerAliasName + "-" + sqlScript.DbName + "-" + sqlScript.Name);

                                if (dictSVDB_SP_Deployed.ContainsKey(keySetting))// SV-DB
                                {
                                    dictSVDB_SP_Deployed[keySetting] += "," + sqlScript.Name + (isSp ? string.Empty : "-x-func-x-");
                                }
                                else
                                {
                                    dictSVDB_SP_Deployed.Add(keySetting, sqlScript.Name + (isSp ? string.Empty : "-x-func-x-"));
                                }

                                #endregion

                                resultText.Append(dateTimeNow + "-" + sqlScript.SourceServerAliasName
                                    + "-" + sqlScript.DbName + "-" + this._description + "-" + sqlScript.Name + ":"
                                    + "Success.\r\n");

                                //UIUtility.AppendColoredText(this.rtxtOutput,
                                //    dateTimeNow + "-" + sqlScript.SourceServerAliasName
                                //    + "-" + sqlScript.DbName + "-" + this._description + "-" + sqlScript.Name + ":"
                                //    + "Success.\r\n", Color.Black);
                            }
                        }
                        else
                        {
                            UIUtility.AppendColoredText(this.rtxtOutput, "\"Row " + SetColorForImagesServerDeployError(sqlScript.SourceServerAliasName + "-" + sqlScript.DbName + "-" + sqlScript.Name) + ": " + dateTimeNow + " - Could not connect to destination database " + sqlScript.DestinationServerIp + " to deploy SP/Function " + sqlScript.Name + "\"\n",
                                                                Color.Red);
                        }
                    }
                    catch (Exception ex)
                    {
                        UIUtility.AppendColoredText(this.rtxtOutput, "\"Row " + SetColorForImagesServerDeployError(sqlScript.SourceServerAliasName + "-" + sqlScript.DbName + "-" + sqlScript.Name) + ": " +
                                             dateTimeNow + "-" + sqlScript.SourceServerAliasName
                                             + "-" + sqlScript.DbName + "-" + this._description + "-" + sqlScript.Name + ":"
                                             + ex.Message + "\"\r\n", Color.Red);
                    }

                    tempSqlScript = sqlScript;
                    this.prgbScript.PerformStep();
                }

                #endregion

                #region deploy SPs to image servers
                // deploy SPs to image server
                if (isDeploy)
                {
                    DeployToStandByServers(dicSPDeployToImageSV, dicDeployedSucessSV_DB_SP, dateTimeNow);
                }

                #endregion

                #region write config inform
                // create the config file 
                configFileContent += "Deployed\r\n";
                foreach (string key in dictSVDB_SP_Deployed.Keys)
                {
                    configFileContent += key + ":" + dictSVDB_SP_Deployed[key] + "\r\n";
                }

                configFileContent += "o\r\n";
                foreach (string key in dictSVDB_SP_OnlyRenamed.Keys)
                {
                    configFileContent += key + ":" + dictSVDB_SP_OnlyRenamed[key] + "\r\n";
                }

                if (((Button)sender).Name == btnDeploy.Name)
                {
                    // write config file for backup
                    if (dictSPs_BackupExistedSP.Keys.Count > 0)
                    {
                        LogUtility.WriteToFile(configFileContent, bkfolder + @"\" + dateTimeNow + "-" + this._description + ".config");

                        var bkconfigContent = string.Empty;
                        foreach (var key in dictSPs_BackupExistedSP.Keys)
                        {
                            bkconfigContent += key + ":" + dictSPs_BackupExistedSP[key] + "\r\n";
                        }

                        LogUtility.WriteToFile(bkconfigContent, backupFolderPath + @"\" + dateTimeNow + "-" + this._description + ".config");
                    }
                }
                else
                {
                    //if this is the preview process, just out put the config string for user's reference
                    UIUtility.AppendColoredText(this.rtxtOutput, configFileContent.Replace("-x-func-x-", ""), Color.Blue);
                    UIUtility.AppendColoredText(this.rtxtOutput, "*********************************\r\n", Color.Blue);
                }

                UIUtility.AppendColoredText(this.rtxtOutput, resultText.ToString(), Color.Black);
                #endregion write config inform

                for (int i = 0; i < dgvSPList.Rows.Count; i++)
                {
                    dgvSPList.Rows[i].Cells[_columnNo].Value = i + 1;
                }

                this._sqlServerUtil.Disconnect();
            }
            catch 
            { 
            }
            this.Cursor = Cursors.Arrow;
            this.prgbScript.Visible = false;
        }

        #endregion Actions

        #region actions of other controls

        private void rbtrelease_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtRelease.Checked)
            {
                this.cbbScriptSource.Enabled = true;
            }
            else
            {
                this.cbbScriptSource.SelectedIndex = 0;
                this.cbbScriptSource.Enabled = false;
            }
        }

        private void chkAllServers_CheckedChanged(object sender, EventArgs e)
        {
            this.cbbSourceServer.Enabled = !this.cbbSourceServer.Enabled;
            // Reload the original table from excel file
            if (this.chkAllServers.Checked)
            {
                this.cbbSourceServer.SelectedIndex = 0; // set the combobox to empty item
                this.dgvSPList.DataSource = this._loadedTable;
            }

            ChangeColumnWidth(this._loadedTable);
            dgvSPList_ColumnHeaderMouseClick(null, null);
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

        private void cbbSourceServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._currentTable = new DataTable();
            this._currentTable = this._loadedTable.Copy();
            if (this.cbbSourceServer.SelectedIndex != 0)// "---All Server---" selected
            {
                for (var i = this._currentTable.Rows.Count - 1; i >= 0; i--)
                {
                    if (String.Compare(this._currentTable.Rows[i][_columnServerName].ToString(), this.cbbSourceServer.SelectedItem.ToString(), true) != 0)
                    {
                        this._currentTable.Rows.RemoveAt(i);
                    }
                }
            }

            this.dgvSPList.DataSource = this._currentTable;
            ChangeColumnWidth(this._currentTable);
            dgvSPList_ColumnHeaderMouseClick(null, null);
        }

        #endregion

        #region Grid actions

        private void checkboxHeader_OnClick(object sender, EventArgs e)
        {
            for (var i = 0; i < this.dgvSPList.RowCount; i++)
            {
                var checkbox = ((CheckBox)this.dgvSPList.Controls.Find("checkboxHeader", true)[0]);
                this.dgvSPList[_columnSelect, i].Value = checkbox.Checked;
                if (!checkbox.Checked)
                {
                    this.dgvSPList[_columnOnlyRename, i].Value = false;
                }

                this.dgvSPList[_columnOnlyRename, i].ReadOnly = !(bool)checkbox.Checked;
            }

            this.dgvSPList.EndEdit();

            ChangeColumnWidth(this._currentTable);
            dgvSPList_ColumnHeaderMouseClick(null, null);
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
                        this.dgvSPList.Rows[i].DefaultCellStyle.BackColor = _color1;
                    }
                    else
                    {
                        this.dgvSPList.Rows[i].DefaultCellStyle.BackColor = _color2;
                    }
                }
            }
            catch
            {
            }
        }

        private void dgvSPList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //with the check box change state, no need to update the IP

                this.dgvSPList.UpdateCellValue(e.ColumnIndex, e.RowIndex);
                var tempTable = (DataTable)this.dgvSPList.DataSource;

                foreach (DataRow row in tempTable.Rows)
                {
                    var keySetting = this._configUtil.GetKey(
                        row[_columnServerName].ToString().ToUpper().Trim(),
                        row[_columnDBName].ToString().Trim());
                    //The server has not been specified the IP in the config file yet
                    if (!_serverConfig.ContainsKey(keySetting))
                    {
                        row.RowError = "Server Name & Database Name has not been specified in the config file yet";
                        row[_columnIp] = string.Empty;
                    }
                    else
                    {
                        row[_columnIp] = _serverConfig[keySetting].Ip;
                        row.RowError = string.Empty;
                    }
                }
                this.dgvSPList.DataSource = tempTable;
            }
            catch { }
        }

        private void dgvSPList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvSPList.Columns[e.ColumnIndex].Name == _columnSelect)
                {
                    dgvSPList.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    var check = (bool)this.dgvSPList.Rows[e.RowIndex].Cells[_columnSelect].Value;
                    if (check)
                    {
                        this.dgvSPList.Rows[e.RowIndex].DefaultCellStyle.BackColor = _color1;
                    }
                    else
                    {
                        this.dgvSPList.Rows[e.RowIndex].DefaultCellStyle.BackColor = _color2;
                    }

                    if (this.dgvSPList[e.ColumnIndex, e.RowIndex].Value.ToString() == "False")
                    {
                        ((CheckBox)this.dgvSPList.Controls.Find("checkboxHeader", true)[0]).Checked = false;
                        this.dgvSPList.Controls.Find("checkboxHeader", true)[0].Invalidate();
                        this.dgvSPList[e.ColumnIndex + 1, e.RowIndex].Value = false;
                        this.dgvSPList[e.ColumnIndex + 1, e.RowIndex].ReadOnly = true;
                        return;
                    }
                    else
                    {
                        this.dgvSPList[e.ColumnIndex + 1, e.RowIndex].ReadOnly = false;
                    }

                    return;
                }

                dgvSPList.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
            catch { }
        }

        #endregion

        private string GetSuffix()
        {
            string scriptFolderNameType = string.Empty;

            if (this.rbtPrerelease.Checked)// prerelease phase
            {
                this._selectedPhase = Phases.PreRelease;
                this._suffix = "_xpre";
                // for folder name
                scriptFolderNameType = "Pre-release";
            }
            else if (this.rbtTest.Checked)// test phase
            {
                this._selectedPhase = Phases.Test;
                this._suffix = "_xtest";
                // for folder name
                scriptFolderNameType = "Test";
            }
            else// release phase
            {
                this._selectedPhase = Phases.Release;
                this._suffix = string.Empty;
                // for folder name
                scriptFolderNameType = "Release";

                if (this.cbbScriptSource.SelectedItem.ToString() == "Pre-release")
                {
                    this._sourcePhase = Phases.PreRelease;
                    this._suffixToRemove = "_xpre";
                }
                else if (this.cbbScriptSource.SelectedItem.ToString() == "Test")
                {
                    this._sourcePhase = Phases.Test;
                    this._suffixToRemove = "_xtest";
                }
            }

            return scriptFolderNameType;
        }

        /// <summary>
        /// Check all the selections of user to process task respectedly
        /// </summary>
        /// <returns></returns>
        private bool CheckPrerequisites()
        {
            this.rtxtOutput.Clear();

            // check user name and pass
            if (string.IsNullOrEmpty(this.txtUserName.Text.Trim())
                || string.IsNullOrEmpty(this.txtPassword.Text.Trim()))
            {
                UIUtility.AppendColoredText(this.rtxtOutput, "'" + DbDeployer.Properties.Resources.ACCOUNT_ERROR_MESS + "'", Color.Red);
                return false;
            }

            // if account used for only one server,it must be specified
            if (!this.chkAllServers.Checked
                && this.cbbSourceServer.SelectedIndex <=0)
            {
                UIUtility.AppendColoredText(this.rtxtOutput, "'" + DbDeployer.Properties.Resources.SELECT_SERVER_ERROR_MESS + "'", Color.Red);
                return false;
            }

            //Check whether the source of script selected
            if ((this.rbtRelease.Checked) && this.cbbScriptSource.SelectedIndex <= 0)
            {
                UIUtility.AppendColoredText(this.rtxtOutput, "'" + DbDeployer.Properties.Resources.SELECT_SOURCE_ERR_MESS + "'", Color.Red);
                return false;
            }

            //check whether there's still some error row in the grid or not
            string strErrorRow = string.Empty;
            //get all rows have error
            for (int j = 0; j < this.dgvSPList.Rows.Count; j++)
            {
                if (this.dgvSPList[_columnSelect, j].Value.ToString() == "True" && !string.IsNullOrWhiteSpace(this.dgvSPList.Rows[j].ErrorText))
                {
                    strErrorRow += " " + (j + 1).ToString();
                }
            }

            if (!string.IsNullOrWhiteSpace(strErrorRow))
            {
                UIUtility.AppendColoredText(this.rtxtOutput,
                    DbDeployer.Properties.Resources.ROWS_ERROR_MESS + strErrorRow, Color.Red);
                return false;
            }

            return true;
        }

        private void InitializeProgressBar()
        {
            //initailize the progress bar
            this.prgbScript.Visible = true;
            this.prgbScript.Maximum = (this.dgvSPList.Rows.Count) * 10;
            this.prgbScript.Step = 10;
            this.prgbScript.Value = 0;
        }

        private string GetImageServer(string keySetting)
        {
            if (_imageServers.ContainsKey(keySetting))
            {
                return _imageServers[keySetting];
            }

            return string.Empty;
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

        private void ColorGrid()
        {
            for (var i = 0; i < this.dgvSPList.Rows.Count; i++)
            {
                if ((bool)this.dgvSPList.Rows[i].Cells[1].Value)
                {
                    this.dgvSPList.Rows[i].DefaultCellStyle.BackColor = Color.Lavender;
                }
                else
                {
                    this.dgvSPList.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
            }
        }

        private DataTable AutoNumberedTable(DataTable SourceTable)
        {
            DataTable ResultTable = new DataTable();
            DataColumn AutoNumberColumn = new DataColumn();
            AutoNumberColumn.ColumnName = _columnNo;

            AutoNumberColumn.DataType = typeof(int);
            AutoNumberColumn.AutoIncrement = true;
            AutoNumberColumn.AutoIncrementSeed = 1;
            AutoNumberColumn.AutoIncrementStep = 1;
            ResultTable.Columns.Add(AutoNumberColumn);

            ResultTable.Merge(SourceTable);
            return ResultTable;
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

        private void DeployToStandByServers(Dictionary<string, ISqlScript> dicSPDeployToImageSV, List<string> dicDeployedSucessSV_DB_SP, string dateTimeNow)
        {
            var isImageServerConnected = false;
            var previousDesSVIP = string.Empty;

            foreach (var key in dicSPDeployToImageSV.Keys)
            {
                var keyMainServer = key.Split(',')[0];
                var script = dicSPDeployToImageSV[key];

                try
                {
                    if (dicDeployedSucessSV_DB_SP.Contains(keyMainServer))
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
                            this.ChangeScriptCreateOrAlterFollowExistSP(script);
                            // deploy server standby
                            script.Deploy(this._sqlServerUtil);
                        }
                        else
                        {
                            UIUtility.AppendColoredText(this.rtxtOutput, "\"Row " + SetColorForImagesServerDeployError(keyMainServer)
                                + ": Standby Server-" + dateTimeNow + "-" + dicSPDeployToImageSV[key].SourceServerAliasName
                                + ": Could not connect to destination database " + dicSPDeployToImageSV[key].DestinationServerIp
                                + " to deploy SP/Function " + dicSPDeployToImageSV[key].Name + "\"\n", Color.Red);
                        }
                    }
                }
                catch (Exception ex)
                {
                    UIUtility.AppendColoredText(this.rtxtOutput, "\"Row " + SetColorForImagesServerDeployError(keyMainServer)
                                 + ": Standby Server-" + dateTimeNow + "-" + script.SourceServerAliasName
                                 + "-" + script.DbName + "-" + this._description + "-" + script.Name + ":"
                                 + ex.Message + "\"\r\n", Color.Red);
                }
            }
        }

        private void ChangeScriptCreateOrAlterFollowExistSP(ISqlScript sqlScript)
        {

            if (this._sqlServerUtil.IsScriptExisted(sqlScript.DbName, sqlScript.Name + this._suffix, sqlScript.IsSp))
            {
                sqlScript.Content = sqlScript.IsSp ? sqlScript.Content.Replace("CREATE PROCEDURE", "ALTER PROCEDURE").Replace("CREATE PROC", "ALTER PROCEDURE")
                    : sqlScript.Content.Replace("CREATE FUNCTION", "ALTER FUNCTION").Replace("CREATE FUNC", "ALTER FUNCTION");
            }
            else
            {
                sqlScript.Content = sqlScript.IsSp ? sqlScript.Content.Replace("ALTER PROCEDURE", "CREATE PROCEDURE").Replace("ALTER PROC", "CREATE PROCEDURE")
                    : sqlScript.Content.Replace("ALTER FUNCTION", "CREATE FUNCTION").Replace("ALTER FUNC", "CREATE FUNCTION");
            }
        }

        private bool CheckConnectToSourceServer(SqlServer slqServerUtil_LoadSP, bool isConnectedToSourceServer, string tempSourceServerIP, ISqlScript sqlScript, string sourceServerIP)
        {
            if (sourceServerIP != tempSourceServerIP || !isConnectedToSourceServer)
            {
                slqServerUtil_LoadSP.Disconnect();

                isConnectedToSourceServer = slqServerUtil_LoadSP.Connect(
                    sourceServerIP,
                    this.txtUserName.Text,
                    this.txtPassword.Text);

                if (isConnectedToSourceServer)
                {
                    slqServerUtil_LoadSP.CreateScript(sqlScript.DbName, Properties.Resources.DBA_GrantPermission, true);
                }
            }

            return isConnectedToSourceServer;
        }

        private bool CheckConnectToDestinationServer(bool isConnectedToDestinationServer, ISqlScript tempSqlScript, ISqlScript sqlScript)
        {
            if (tempSqlScript == null)
            {
                isConnectedToDestinationServer = this._sqlServerUtil.Connect(
                    sqlScript.DestinationServerIp,
                    this.txtUserName.Text,
                    this.txtPassword.Text);

                if (isConnectedToDestinationServer)
                {
                    this._sqlServerUtil.CreateScript(sqlScript.DbName, Properties.Resources.DBA_GrantPermission, true);
                }
            }
            else
            {
                if (sqlScript.DestinationServerIp != tempSqlScript.DestinationServerIp || !isConnectedToDestinationServer)
                {
                    this._sqlServerUtil.Disconnect();

                    isConnectedToDestinationServer = this._sqlServerUtil.Connect(
                        sqlScript.DestinationServerIp,
                        this.txtUserName.Text,
                        this.txtPassword.Text);

                    if (isConnectedToDestinationServer)
                    {
                        this._sqlServerUtil.CreateScript(sqlScript.DbName, Properties.Resources.DBA_GrantPermission, true);
                    }
                }
            }

            return isConnectedToDestinationServer;
        }

        private void ProcessedBeforeDeploy(bool isDeploy, Dictionary<string, ISqlScript> dicSPDeployToImageSV, Dictionary<string, string> dictSVDB_SP_OnlyRenamed, Dictionary<string, string> dictSVDB_Selected)
        {
            for (var i = 0; i < this.dgvSPList.Rows.Count; i++)
            {
                var isSp = (this.dgvSPList[_columnIsFunction, i].Value.ToString()) != "True";
                var serverName = this.dgvSPList[_columnServerName, i].Value.ToString().ToUpper().Trim();
                var dbName = this.dgvSPList[_columnDBName, i].Value.ToString().ToUpper().Trim();
                var spName = this.dgvSPList[_columnSpName, i].Value.ToString().Trim();
                var keySetting = this._configUtil.GetKey(serverName, dbName);

                if (this.dgvSPList[_columnSelect, i].Value.ToString() == "True")
                {
                    // SP selected
                    if (dictSVDB_Selected.ContainsKey(keySetting))// SV-DB
                    {
                        dictSVDB_Selected[keySetting] += "," + spName;
                    }
                    else
                    {
                        dictSVDB_Selected.Add(keySetting, spName);
                    }

                    // SP selected & deploy
                    if (this.dgvSPList[_columnOnlyRename, i].Value.ToString() == "False")
                    {
                        #region get script to prepare deploy
                        var scriptFilePath = String.Format("{0}\\{1}-{2}-{3}-{4}-{5}-{6}.sql",
                                                        this._scriptFolderName,
                                                        this._configFileName.Split('-')[0],
                                                        this._configFileName.Split('-')[1],
                                                        serverName,
                                                        dbName,
                                                        this._description,
                                                        spName);
                        ISqlScript scriptDeployed;

                        if (isSp)
                        {
                            scriptDeployed = new SqlSp(
                                // SP Name
                                spName,
                                // source server IP
                                this.dgvSPList[_columnIp, i].Value.ToString().Trim(),
                                // source server name
                                serverName,
                                // DB Name
                                dbName,
                                // destination server IP
                                _serverConfig[keySetting].GetIpByPhase(this._selectedPhase),
                                // script file path
                                scriptFilePath,
                                // Phase
                                this._selectedPhase);
                        }
                        else
                        {
                            scriptDeployed = new SqlFunc(
                                // SP Name
                               spName,
                                // source server IP
                               this.dgvSPList[_columnIp, i].Value.ToString().Trim(),
                                // source server name
                               serverName,
                                // DB Name
                               dbName,
                                // destination server IP
                               _serverConfig[keySetting].GetIpByPhase(this._selectedPhase),
                                // script file path
                               scriptFilePath,
                                // Phase
                               this._selectedPhase);
                        }

                        this._arrScriptDeployed.Add(scriptDeployed);

                        #endregion

                        #region build up the SPs list for image server

                        if (isDeploy)
                        {
                            // build up the SPs list for image server
                            var imageServers = this.GetImageServer(keySetting);

                            if (!string.IsNullOrEmpty(imageServers))
                            {
                                var arrImageServers = imageServers.Split(',');
                                foreach (var str in arrImageServers)
                                {
                                    var imageServer = str.Trim();
                                    if (string.IsNullOrEmpty(imageServer))
                                        continue;

                                    var copy = scriptDeployed.Copy();
                                    copy.SourceServerAliasName = imageServer.Split('-')[0];
                                    copy.DbName = imageServer.Split('-')[1];
                                    // change the destination server
                                    copy.DestinationServerIp = _serverConfig[imageServer].GetIpByPhase(this._selectedPhase);

                                    var key = scriptDeployed.SourceServerAliasName + "-" + scriptDeployed.DbName + "-" + scriptDeployed.Name
                                        + "," + imageServer + "-" + copy.Name;
                                    if (!dicSPDeployToImageSV.ContainsKey(key))
                                    {
                                        dicSPDeployToImageSV.Add(key, copy);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    // SP to rename only. With this kind of SPs, no need to load the content
                    else
                    {
                        #region SP to rename only. With this kind of SPs, no need to load the content
                        ISqlScript spOnlyRenamed;

                        if (isSp)
                        {
                            spOnlyRenamed = new SqlSp(
                                // SP Name
                                spName,
                                // source server IP
                                this.dgvSPList[_columnIp, i].Value.ToString().Trim(),
                                // source server name
                                serverName,
                                // DB Name
                                dbName,
                                // destination server IP
                                _serverConfig[keySetting].GetIpByPhase(this._selectedPhase),
                                // script file path
                                null, //script file path would be null because this kind of SPs does not need to be content loaded
                                // Phase
                                this._selectedPhase);
                        }
                        else
                        {
                            spOnlyRenamed = new SqlFunc(
                                // SP Name
                               spName,
                                // source server IP
                               this.dgvSPList[_columnIp, i].Value.ToString().Trim(),
                                // source server name
                               serverName,
                                // DB Name
                               dbName,
                                // destination server IP
                               _serverConfig[keySetting].GetIpByPhase(this._selectedPhase),
                                // script file path
                               null, //script file path would be null because this kind of SPs does not need to be content loaded
                                // Phase
                               this._selectedPhase);
                        }

                        if (dictSVDB_SP_OnlyRenamed.ContainsKey(keySetting))// SV-DB
                        {
                            dictSVDB_SP_OnlyRenamed[keySetting] += "," + spName + (isSp ? "" : "-x-func-x-");
                        }
                        else
                        {
                            dictSVDB_SP_OnlyRenamed.Add(keySetting, spName + (isSp ? "" : "-x-func-x-"));
                        }

                        #endregion
                    }
                }
            }
        }

    }
}
