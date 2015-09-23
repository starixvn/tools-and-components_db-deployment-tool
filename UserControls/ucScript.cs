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
    using System.Text;
    using System.Windows.Forms;
    using DbDeployer.Core;
    using DbDeployer.Core.Utils;

    public partial class ucScript : ucBase
    {
        // column name of grid
        private const string _columnNo = "No";
        private const string _columnSelect = "Choose";
        private const string _columnServerName = "ServerName";
        private const string _columnDBName = "DBName";
        private const string _columnSpName = "SPName";
        private const string _columnIsFunction = "IsFunction";
        private const string _columnIp = "IP";

        // 2: ServerName, 3: DBName, 4: SPName
        private int[] _checkColumnsList = { 2, 3, 4 };

        private Color _color1 = Color.Lavender;
        private Color _color2 = Color.White;

        private string _excelFilePath;
        private ConfigUtility _configUtil;

        private SqlServer _sqlServerUtil = new SqlServer();
        private DataTable _table;// map from the excel file
        private DataTable _loadedTable;// keep the state of grid loaded at the first time
        private DataTable _currentTable;// use for server selection

        public static Dictionary<string, ServerIpSetting> _serverConfig;

        public ucScript()
        {
            this.InitializeComponent();
            this._configUtil = new ConfigUtility();
            this._table = new DataTable();
        }

        private void ucScript_Load(object sender, EventArgs e)
        {
            _serverConfig = this._configUtil.GetIPSettings();

            this.rtxtOutput.AutoScroll = false;

            this.txtScriptTo.Text = AppDomain.CurrentDomain.BaseDirectory + @"Script\";
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Log"))
            {
                try
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Log");
                }
                catch
                {
                    throw;
                }
            }

            if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["UserName"].ToString()))
            {
                txtUserName.Text = System.Configuration.ConfigurationManager.AppSettings["UserName"].ToString();
            }
        }

        #region Excel file processing
        private void btnOpenExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Excel file|*.xls";
            dlg.CheckFileExists = true;
            dlg.CheckPathExists = true;
            dlg.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this._excelFilePath = dlg.FileName;

                    //populate information from excel file to grid
                    this._table = FileUtility.ReadFromExcel(this._excelFilePath);
                    loadSP();
                }

                if (this.dgvSPConfig.Rows.Count > 1)
                {
                    pnl_Action.Visible = true;
                    dgvSPConfig_CellEndEdit(null, null);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
                UIUtility.AppendColoredText(this.rtxtOutput, "Error occurred in excel file", Color.Red);
            }

            dlg.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var dlg = new SaveFileDialog
            {
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,
                CreatePrompt = true,
                Filter = "Excel file(*.xls)|*.xls"
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var tableSave = new DataTable();
                var col1 = new DataColumn(_columnServerName, Type.GetType("System.String"));
                var col2 = new DataColumn(_columnDBName, Type.GetType("System.String"));
                var col3 = new DataColumn(_columnSpName, Type.GetType("System.String"));
                var col4 = new DataColumn(_columnIsFunction, Type.GetType("System.Int32"));
                tableSave.Columns.Add(col1);
                tableSave.Columns.Add(col2);
                tableSave.Columns.Add(col3);
                tableSave.Columns.Add(col4);

                for (var i = 0; i < this.dgvSPConfig.Rows.Count - 1; i++)
                {
                    var row = tableSave.NewRow();
                    row[0] = this.dgvSPConfig[_columnServerName, i].Value.ToString().Trim();
                    row[1] = this.dgvSPConfig[_columnDBName, i].Value.ToString();
                    row[2] = this.dgvSPConfig[_columnSpName, i].Value.ToString();
                    row[3] = this.dgvSPConfig[_columnIsFunction, i].Value.ToString();
                    tableSave.Rows.Add(row);
                }

                try
                {
                    FileUtility.WriteToExcel(dlg.FileName, tableSave);
                }
                catch (Exception ex)
                {
                    Console.Out.WriteLine(ex.Message);
                }
            }

            dlg.Dispose();
        }
        #endregion

        #region grid action

        private void dgvSPConfig_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // with the check box change state, no need to update the IP
                if (e != null)
                {
                    if (this.dgvSPConfig.Columns[e.ColumnIndex].Name == _columnSelect)
                    {
                        if (this.dgvSPConfig[e.ColumnIndex, e.RowIndex].Value.ToString() == "False")
                        {
                            ((CheckBox)this.dgvSPConfig.Controls.Find("checkboxHeader", true)[0]).Checked = false;
                            ((CheckBox)this.dgvSPConfig.Controls.Find("checkboxHeader", true)[0]).Invalidate();
                            return;
                        }

                        return;
                    }
                }

                DataTable tempTable = (DataTable)this.dgvSPConfig.DataSource;
                int i = 0;
                List<int> listDuplicated = new List<int>();

                foreach (DataRow row in tempTable.Rows)
                {
                    var keySetting = _configUtil.GetKey(row[_columnServerName].ToString().Trim().ToUpper(), row[_columnDBName].ToString().Trim());
                    // The server has not been specified the IP in the config file yet
                    if (!_serverConfig.ContainsKey(keySetting))
                    {
                        // row.RowError = "The IP of this server has not been specified in the config file yet";
                        row.RowError = "Server Name & Database Name has not been specified in the config file yet";
                        row[_columnIp] = string.Empty;
                    }
                    else
                    {
                        row[_columnIp] = _serverConfig[keySetting].Ip;
                        row.RowError = string.Empty;
                    }

                    int j = 0;
                    foreach (DataRow row1 in tempTable.Rows)
                    {
                        if (i != j &&
                            row[_columnServerName].ToString().Trim().ToUpper() == row1[_columnServerName].ToString().Trim().ToUpper() &&
                            row[_columnDBName].ToString().Trim().ToUpper() == row1[_columnDBName].ToString().Trim().ToUpper() &&
                            row[_columnSpName].ToString().Trim().ToUpper() == row1[_columnSpName].ToString().Trim().ToUpper())
                        {
                            if (!listDuplicated.Contains(i))
                            {
                                listDuplicated.Add(i);
                            }
                            if (!listDuplicated.Contains(j))
                            {
                                listDuplicated.Add(j);
                            }
                        }

                        j++;
                    }

                    i++;
                }

                foreach (var index in listDuplicated)
                {
                    tempTable.Rows[index].RowError = "Duplicated";
                }

                this.dgvSPConfig.DataSource = tempTable;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }
        }

        private void dgvSPConfig_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                for (int i = 0; i < dgvSPConfig.Rows.Count - 1; i++)
                {
                    dgvSPConfig.Rows[i].Cells[_columnNo].Value = i + 1;

                    if ((bool)this.dgvSPConfig.Rows[i].Cells[_columnSelect].Value)
                    {
                        this.dgvSPConfig.Rows[i].DefaultCellStyle.BackColor = _color1;
                    }
                    else
                    {
                        this.dgvSPConfig.Rows[i].DefaultCellStyle.BackColor = _color2;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }
        }

        private void checkboxHeader_OnClick(object sender, EventArgs e)
        {
            for (int i = 0; i < this.dgvSPConfig.RowCount - 1; i++)
            {
                this.dgvSPConfig[_columnSelect, i].Value = ((CheckBox)sender).Checked;
            }

            this.dgvSPConfig.EndEdit();

            ChangeColumnWidth(this._currentTable);
            dgvSPConfig_ColumnHeaderMouseClick(null, null);
        }

        private void dgvSPConfig_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (this.dgvSPConfig.Columns[e.ColumnIndex].Name == _columnSelect)
                {
                    dgvSPConfig.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    var check = (bool)this.dgvSPConfig.Rows[e.RowIndex].Cells[_columnSelect].Value;
                    if (check)
                    {
                        this.dgvSPConfig.Rows[e.RowIndex].DefaultCellStyle.BackColor = _color1;
                    }
                    else
                    {
                        this.dgvSPConfig.Rows[e.RowIndex].DefaultCellStyle.BackColor = _color2;
                    }
                }
            }
            catch { }
        }

        #endregion

        #region checkbox action

        private void chkAllServers_CheckedChanged(object sender, EventArgs e)
        {
            this.cbbSourceServer.Enabled = !this.cbbSourceServer.Enabled;
            // Reload the original table from excel file
            if (this.chkAllServers.Checked)
            {
                this.cbbSourceServer.SelectedIndex = 0;// set the combobox to empty item
                this.dgvSPConfig.DataSource = this._loadedTable;
            }

            ChangeColumnWidth(this._currentTable);
            dgvSPConfig_ColumnHeaderMouseClick(null, null);
        }

        private void cbbSourceServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._currentTable = new DataTable();
            this._currentTable = this._loadedTable.Copy();
            if (this.cbbSourceServer.SelectedIndex != 0)// "---All Server---" selected
            {
                for (int i = this._currentTable.Rows.Count - 1; i >= 0; i--)
                {
                    if (!(String.Compare(this._currentTable.Rows[i][_columnServerName].ToString().Trim(), this.cbbSourceServer.SelectedItem.ToString().Trim(), true) == 0))
                    {
                        this._currentTable.Rows.RemoveAt(i);
                    }
                }
            }

            this.dgvSPConfig.DataSource = this._currentTable;
            this.dgvSPConfig.Invalidate();

            for (var j = 0; j < this.dgvSPConfig.Rows.Count - 1; j++)
            {
                this.dgvSPConfig[_columnSelect, j].Value = true;
            }

            ChangeColumnWidth(this._currentTable);
            dgvSPConfig_ColumnHeaderMouseClick(null, null);
        }

        #endregion

        private void ChangeColumnWidth(DataTable data)
        {
            if (data.Rows.Count > 4)
            {
                this.dgvSPConfig.Columns[_columnIp].Width = 132;
            }
            else
            {
                this.dgvSPConfig.Columns[_columnIp].Width = 150;
            }
        }

        private void btnBrowserFolder_Click(object sender, EventArgs e)
        {
            var folderBrowser = new FolderBrowserDialog();

            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                this.txtScriptTo.Text = folderBrowser.SelectedPath + @"\";
            }

            folderBrowser.Dispose();
        }

        private void InitializeProgressBar()
        {
            this.prgbScript.Visible = true;
            this.prgbScript.Maximum = (this.dgvSPConfig.Rows.Count - 1) * 10;
            this.prgbScript.Step = 10;
            this.prgbScript.Value = 0;
        }

        #region Actions

        private void btnPreview_Click(object sender, EventArgs e)
        {
            // check account and server selection
            if (!this.CheckPrerequisites())
            {
                return;
            }

            int selection = this.rtxtOutput.SelectionStart;
            string output = string.Empty;
            var tempServerName = string.Empty;
            bool isConnected = false;
            string connection = string.Empty;
            // sort the table by ServerName column
            this.dgvSPConfig.Sort(this.dgvSPConfig.Columns[_columnServerName], ListSortDirection.Ascending);
            this.dgvSPConfig.Invalidate();

            ColorGrid();
            // Clear the output
            this.rtxtOutput.Text = string.Empty;
            this.InitializeProgressBar();
            var errorSPs = new ArrayList();

            SaveUserIntoConfig();

            for (var i = 0; i < this.dgvSPConfig.Rows.Count - 1; i++)
            {
                var serverName = this.dgvSPConfig[_columnServerName, i].Value.ToString().Trim().ToUpper();
                var dbName = this.dgvSPConfig[_columnDBName, i].Value.ToString().Trim();
                var spName = this.dgvSPConfig[_columnSpName, i].Value.ToString().Trim();
                var isSp = this.dgvSPConfig[_columnIsFunction, i].Value.ToString() != "1" && this.dgvSPConfig[_columnIsFunction, i].Value.ToString() != "True";

                var keySetting = _configUtil.GetKey(serverName, dbName);

                if (this.dgvSPConfig[_columnSelect, i].Value.ToString() == "True")
                {
                    try
                    {
                        // check whether this store is on the same server with the previous one. If not, create new connection
                        if (serverName != tempServerName || !isConnected)
                        {
                            tempServerName = serverName;
                            isConnected = this._sqlServerUtil.Connect(
                                _serverConfig[keySetting].Ip,
                                this.txtUserName.Text,
                                this.txtPassword.Text);

                            // true: DBA_GrantPermissions is a sp --> true 
                            this._sqlServerUtil.CreateScript(dbName, Properties.Resources.DBA_GrantPermission, true);
                        }

                        if (isConnected)
                        {
                            connection = "Data Source="
                                + _serverConfig[keySetting].Ip
                                + ";Initial Catalog=" + dbName
                                + ";User Id=" + this.txtUserName.Text
                                + ";Password=" + this.txtPassword.Text + ";";

                            // check permision. if there are not permission --> error 
                            var permission = SqlServer.GetPermission(connection, spName);

                            var spcontent = new StringBuilder();

                            var scriptContent = this._sqlServerUtil.GetScript(dbName, spName, isSp);
                            spcontent.Append(scriptContent);
                            UIUtility.AppendColoredText(this.rtxtOutput, "\r\n\r\n" + spcontent + permission + "\r\n\r\n", System.Drawing.Color.Black);
                        }
                        else
                        {
                            errorSPs.Add("Row " + (i + 1) + ": Can not connect to server: " + serverName);
                            this.dgvSPConfig.Rows[i].DefaultCellStyle.BackColor = Color.Pink;
                        }
                    }
                    catch (Exception ex)
                    {
                        errorSPs.Add(GetError((i + 1), serverName, dbName, spName, ex.Message));
                        this.dgvSPConfig.Rows[i].DefaultCellStyle.BackColor = Color.Pink;
                    }
                }

                this.prgbScript.PerformStep();
            }

            // Report error sp that could not be loaded the content
            output = this.rtxtOutput.Text;
            this.rtxtOutput.Text = string.Empty;
            if (errorSPs.Count > 0)
            {
                labelStatus.Text = "Error! Total SPs can not get script: " + errorSPs.Count.ToString();
                labelStatus.ForeColor = Color.Red;

                UIUtility.AppendColoredText(this.rtxtOutput, "\"Total SPs can not get script: " + errorSPs.Count.ToString() + "\"\r\n", Color.Red);
                foreach (var t in errorSPs)
                {
                    UIUtility.AppendColoredText(this.rtxtOutput, "\"" + t + "\"\r\n", Color.Red);
                }
            }
            else
            {
                labelStatus.Text = string.Empty;
            }

            this._sqlServerUtil.Disconnect();
            this.prgbScript.Visible = false;

            if (string.IsNullOrEmpty(this.rtxtOutput.Text.Trim()))
                this.rtxtOutput.Text = output;
            else
            {
                string line = "\r\n--------------------------------------------------------------------------------------";
                this.rtxtOutput.Text = this.rtxtOutput.Text + line + output;
            }

            for (int i = 0; i < dgvSPConfig.Rows.Count - 1; i++)
            {
                dgvSPConfig.Rows[i].Cells[0].Value = i + 1;
            }

            this.rtxtOutput.SelectionStart = selection;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (!this.CheckPrerequisites())
            {
                return;
            }

            int selection = this.rtxtOutput.SelectionStart;
            ColorGrid();
            this.Cursor = Cursors.WaitCursor;

            bool isConnected = false;
            string connection = string.Empty;
            var tempServerName = string.Empty;
            this.rtxtOutput.Text = string.Empty;
            var dateTime = StringUtility.GetFormatedCurrentDateTime();
            var errorSPs = new ArrayList();
            var successSPs = new ArrayList();

            SaveUserIntoConfig();
            // Get the description for the destination folder
            var desc = string.Empty;
            if (this.txtDesc.Text.Trim() == string.Empty)
            {
                desc = "-Desc";
            }
            else
            {
                desc = "-" + this.txtDesc.Text.Trim();
            }

            var scriptpath = string.Format("{0}{1}{2}", this.txtScriptTo.Text, StringUtility.GetFormatedCurrentDateTime(), desc);
            // sort by server name
            this.dgvSPConfig.Sort(this.dgvSPConfig.Columns[_columnServerName], ListSortDirection.Ascending);
            this.dgvSPConfig.Invalidate();

            if (FileUtility.IsValidFilename(desc))
            {
                var filename = dateTime + desc + ".config";
                var spContent = new StringBuilder();
                var dicconfig = new Dictionary<string, string>();
                this.InitializeProgressBar();

                // remove duplicate rows
                this.dgvSPConfig = UIUtility.RemoveDuplicateRows(this.dgvSPConfig, _checkColumnsList);

                for (var i = 0; i < this.dgvSPConfig.Rows.Count - 1; i++)
                {
                    var serverName = this.dgvSPConfig[_columnServerName, i].Value.ToString().Trim().ToUpper();
                    var dbName = this.dgvSPConfig[_columnDBName, i].Value.ToString().Trim();
                    var spName = this.dgvSPConfig[_columnSpName, i].Value.ToString().Trim();
                    var isSp = this.dgvSPConfig[_columnIsFunction, i].Value.ToString() != "1" && this.dgvSPConfig[_columnIsFunction, i].Value.ToString() != "True";

                    var keySetting = _configUtil.GetKey(serverName, dbName);

                    if (this.dgvSPConfig[_columnSelect, i].Value.ToString() == "True")
                    {
                        try
                        {
                            // check whether this store is on the same server with the previous one. If not, create new connection
                            if (serverName != tempServerName || !isConnected)
                            {
                                tempServerName = serverName;
                                isConnected = this._sqlServerUtil.Connect(
                                    _serverConfig[keySetting].Ip,
                                    this.txtUserName.Text,
                                    this.txtPassword.Text);

                                // true: DBA_GrantPermissions is a sp --> true 
                                this._sqlServerUtil.CreateScript(dbName, Properties.Resources.DBA_GrantPermission, true);
                            }

                            if (isConnected)
                            {
                                connection = "Data Source="
                                    + this._configUtil.GetIPSettings()[keySetting].Ip
                                    + ";Initial Catalog=" + dbName
                                    + ";User Id=" + this.txtUserName.Text
                                    + ";Password=" + this.txtPassword.Text + ";";

                                // check permision. if there are not permission --> error 
                                var permission = SqlServer.GetPermission(connection, spName);

                                var scriptContent = this._sqlServerUtil.GetScript(dbName, spName, isSp);
                                spContent.Append(scriptContent);

                                if (!Directory.Exists(scriptpath))
                                {
                                    Directory.CreateDirectory(scriptpath);
                                }

                                // Get GRANT permission part of SP
                                spContent.Append("\r\n" + permission);
                                // add store name at the end of file name
                                LogUtility.WriteToFile(spContent.ToString(),
                                    String.Format(@"{0}\{1}-{2}-{3}{4}-{5}.sql",
                                    scriptpath,
                                    dateTime,
                                    serverName,
                                    dbName,
                                    desc,
                                    spName)
                                );
                                spContent.Remove(0, spContent.Length - 1);// clear the string builder
                                // write to output and keep log
                                successSPs.Add("Script: " + spName + " success.");

                                var script = spName + (isSp ? string.Empty : "-x-func-x-");

                                if (dicconfig.ContainsKey(serverName + "-" + dbName) == false)
                                {
                                    dicconfig.Add(serverName + "-" + dbName, script);
                                }
                                else
                                {
                                    dicconfig[serverName + "-" + dbName] += "," + script;
                                }
                            }
                            else
                            {
                                errorSPs.Add("Row " + (i + 1) + ": Can not connect to server: " + serverName);
                                this.dgvSPConfig.Rows[i].DefaultCellStyle.BackColor = Color.Pink;
                            }
                        }
                        catch (Exception ex)
                        {
                            errorSPs.Add(GetError((i + 1), serverName, dbName, spName, ex.Message));
                            this.dgvSPConfig.Rows[i].DefaultCellStyle.BackColor = Color.Pink;
                        }
                    }

                    this.prgbScript.PerformStep();
                }

                //write to config file
                if (Directory.Exists(scriptpath))
                {
                    var strconfig = string.Empty;
                    foreach (var key in dicconfig.Keys)
                    {
                        strconfig += key + ":" + dicconfig[key] + "\r\n";
                    }

                    LogUtility.WriteToFile(strconfig, scriptpath + @"\" + filename);
                }

                if (errorSPs.Count > 0)
                {
                    labelStatus.Text = "Error! Total SPs can not Generate script: " + errorSPs.Count.ToString();
                    labelStatus.ForeColor = Color.Red;

                    UIUtility.AppendColoredText(this.rtxtOutput, "\"Total SPs can not Generate script: " + errorSPs.Count.ToString() + "\"\r\n", Color.Red);
                    foreach (var t in errorSPs)
                    {
                        UIUtility.AppendColoredText(this.rtxtOutput, "\r\n\"" + t + "\"", Color.Red);
                    }
                }
                else
                {
                    labelStatus.Text = string.Empty;
                }

                string line = "\r\n--------------------------------------------------------------------------------------";
                if (!string.IsNullOrEmpty(this.rtxtOutput.Text.Trim()))
                    UIUtility.AppendColoredText(this.rtxtOutput, line, Color.Red);
                if (successSPs.Count > 0)
                {
                    foreach (var t in successSPs)
                    {
                        UIUtility.AppendColoredText(this.rtxtOutput, "\r\n" + t, Color.Red);
                    }
                }
            }
            else
            {
                this.rtxtOutput.Text = string.Empty;
                UIUtility.AppendColoredText(this.rtxtOutput, Properties.Resources.INVALID_FILENAME_CHARACTER_MESS, Color.Red);
            }

            for (int i = 0; i < dgvSPConfig.Rows.Count - 1; i++)
            {
                dgvSPConfig.Rows[i].Cells[_columnNo].Value = i + 1;
            }

            this._sqlServerUtil.Disconnect();
            this.prgbScript.Visible = false;
            this.Cursor = Cursors.Arrow;
            this.rtxtOutput.SelectionStart = selection;
        }

        private string GetError(int rowNum, string serverName, string dbName, string spName, string error)
        {
            string outputError = string.Empty;

            outputError = "Row " + rowNum + ": (" + serverName + "-" + dbName + "-" + spName + ")"
                + (string.IsNullOrEmpty(error) ? string.Empty : " ERROR: " + error.Replace('\"', '\'').Replace("\r\n", " "));

            return outputError;
        }

        private bool CheckPrerequisites()
        {
            for (int i = 0; i < dgvSPConfig.Rows.Count - 1; i++)
            {
                dgvSPConfig.Rows[i].Cells[_columnNo].Value = i + 1;
            }

            //check user name and pass
            if (this.txtUserName.Text == string.Empty || this.txtPassword.Text == string.Empty)
            {
                this.rtxtOutput.Text = string.Empty;
                UIUtility.AppendColoredText(this.rtxtOutput, "'" + DbDeployer.Properties.Resources.ACCOUNT_ERROR_MESS + "'", Color.Red);
                return false;
            }

            //if account used for only one server,it must be specified
            if (!this.chkAllServers.Checked &&
                ((this.cbbSourceServer.SelectedItem.ToString() == null) || (this.cbbSourceServer.SelectedItem.ToString() == string.Empty)))
            {
                this.rtxtOutput.Text = string.Empty;
                UIUtility.AppendColoredText(this.rtxtOutput, "'" + DbDeployer.Properties.Resources.SELECT_SERVER_ERROR_MESS + "'", Color.Red);
                return false;
            }

            //check whether there's still some error row in the grid or not
            string strErrorRow = string.Empty;
            //get all rows have error
            for (int j = 0; j < this.dgvSPConfig.Rows.Count - 1; j++)
            {
                if (this.dgvSPConfig[_columnSelect, j].Value.ToString() == "True" && this.dgvSPConfig.Rows[j].ErrorText != string.Empty)
                {
                    strErrorRow += " " + (j + 1).ToString();
                }
            }

            if (strErrorRow != string.Empty)
            {
                this.rtxtOutput.Text = string.Empty;
                UIUtility.AppendColoredText(this.rtxtOutput,
                    DbDeployer.Properties.Resources.ROWS_ERROR_MESS + strErrorRow, Color.Red);
                return false;
            }

            return true;
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

        private void ColorGrid()
        {
            for (var i = 0; i < this.dgvSPConfig.Rows.Count - 1; i++)
            {
                if (!string.IsNullOrEmpty(this.dgvSPConfig.Rows[i].Cells[_columnSelect].Value.ToString()))
                {
                    if ((bool)this.dgvSPConfig.Rows[i].Cells[_columnSelect].Value)
                    {
                        this.dgvSPConfig.Rows[i].DefaultCellStyle.BackColor = _color1;
                    }
                    else
                    {
                        this.dgvSPConfig.Rows[i].DefaultCellStyle.BackColor = _color2;
                    }
                }
            }
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

        private void btn_Input_Click(object sender, EventArgs e)
        {
            InputSPList frm = new InputSPList();
            frm.ShowDialog();
            this._table = InputSPList.data;

            loadSP();

            if (this.dgvSPConfig.Rows.Count > 1)
            {
                pnl_Action.Visible = true;
                dgvSPConfig_CellEndEdit(null, null);
            }
        }

        private void loadSP()
        {
            if (this._table != null && this._table.Rows.Count > 0)
            {
                this.dgvSPConfig.ReadOnly = false;
                this.btnSave.Enabled = true;
                this.btnBrowserFolder.Enabled = true;
                this.btnPreview.Enabled = true;
                this.btnGenerate.Enabled = true;
                this.rtxtOutput.Clear();

                // this._table = FileUtility.ReadFromExcel(this._excelFilePath);

                DataColumn column = this._table.Columns.Add(_columnSelect, System.Type.GetType("System.Boolean"));
                column.SetOrdinal(0);

                //Check the format violation of excel file
                string errorExcel = string.Empty;
                //check column
                if (this._table.Columns.Count > 6)
                {
                    errorExcel = "Error probably occurred in the Excel file at columns: ";
                    for (int i = this._table.Columns.Count - 3; i < this._table.Columns.Count; i++)
                        errorExcel += " " + ((char)(i + 65)).ToString();
                    UIUtility.AppendColoredText(this.rtxtOutput, errorExcel, Color.Red);
                    return;
                }

                // Change all servername and dbname to upper case
                foreach (DataRow row in this._table.Rows)
                {
                    row[_columnServerName] = row[_columnServerName].ToString().Trim().ToUpper();
                    row[_columnDBName] = row[_columnDBName].ToString().Trim();
                }

                //Add "IP" column into the table
                this._table.Columns.Add(_columnIp, Type.GetType("System.String"));

                //add the selection column into table
                //this._table.Columns.Add(new DataColumn("Select", Type.GetType("System.Boolean")));

                foreach (DataRow row in this._table.Rows)
                {
                    var keySetting = this._configUtil.GetKey(row[_columnServerName].ToString().Trim().ToUpper(), row[_columnDBName].ToString().Trim());
                    //The server has not been specified the IP in the config file yet
                    if (!_serverConfig.ContainsKey(keySetting))
                    {
                        // row.RowError = "The IP of this server has not been specified in the config file yet";
                        row.RowError = "Server Name & Database Name has not been specified in the config file yet";
                    }
                    else
                    {
                        row[_columnIp] = _serverConfig[keySetting].Ip;
                        row[_columnSelect] = true;
                    }

                    if (row[_columnIsFunction].ToString() == "1")
                    {
                        row[_columnIsFunction] = true;
                    }
                    else
                    {
                        row[_columnIsFunction] = false;
                    }
                }

                // add value of column No.
                //this._table = AutoNumberedTable(this._table);
                this.dgvSPConfig.DataSource = this._table;

                // change width of all column of grid
                ChangeColumnWidth(this._table);

                // sort grid by column SeverName
                this.dgvSPConfig.Sort(this.dgvSPConfig.Columns[_columnServerName], ListSortDirection.Ascending);

                #region add check box column
                // add checkbox header
                Rectangle rect = this.dgvSPConfig.GetCellDisplayRectangle(1, -1, true);
                // set checkbox header to center of header cell.
                rect.X = rect.Location.X + (rect.Width / 2) + 15;
                rect.Y = rect.Location.Y + 4;

                CheckBox checkboxHeader = new CheckBox();
                checkboxHeader.Name = "checkboxHeader";
                checkboxHeader.Size = new Size(14, 14);
                checkboxHeader.Location = rect.Location;
                checkboxHeader.Click += new EventHandler(this.checkboxHeader_OnClick);
                checkboxHeader.Checked = true;
                this.dgvSPConfig.Controls.Add(checkboxHeader);
                this.dgvSPConfig.EndEdit();
                #endregion

                // copy data of table to loaded Table
                this._loadedTable = this._table;

                // Populate server name to combobox
                ArrayList svNames = new ArrayList();
                svNames.Add("");// empty item selected when account used for all servers

                for (int i = 0; i < this.dgvSPConfig.Rows.Count - 1; i++)
                {
                    if (!svNames.Contains(this.dgvSPConfig[_columnServerName, i].Value.ToString().Trim()))
                    {
                        svNames.Add(this.dgvSPConfig[_columnServerName, i].Value.ToString().Trim());
                    }
                }

                this.cbbSourceServer.DataSource = svNames;
                this.chkAllServers.Enabled = true;

                // Set color for grid
                ColorGrid();
            }
        }
    }
        #endregion
}
