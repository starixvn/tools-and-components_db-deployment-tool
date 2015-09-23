namespace DbDeployer.UserControls
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using DbDeployer.Core;
    using DbDeployer.Core.Utils;

    public partial class ucSetting : ucBase
    {
        // column name of grid
        private const string _columnServerName = "ServerName";
        private const string _columnDBName = "DbName";
        private const string _columnIp = "Ip";
        private const string _columnIpXpre = "IpXpre";
        private const string _columnIpXtest = "IpXtest";
        private const string _columnIpRelease = "IpRelease";
        private const string _columnStandby = "Standby";

        private Color _color1 = Color.Lavender;
        private Color _color2 = Color.White;

        private int _activeRow;
        private int _activeColumn;

        private ConfigUtility _configUtility = new ConfigUtility();

        public ucSetting()
        {
            this.InitializeComponent();
        }

        private void ucSetting_Load(object sender, EventArgs e)
        {
            this.LoadIpSettingFromIpSettingConfig();
        }

        /// <summary>
        /// Populate destination server IPs for each phase (xpre, xtest, release) to 
        /// grid accordingly to the app.config. Each source server has a specific server 
        /// for each phase. For example, store from DB02 (10.20.20.214) would have server IP
        /// 10.20.20.204 for all phases.
        /// </summary>
        private void LoadIpSettingFromIpSettingConfig()
        {
            DataTable tblIpSetting = new DataTable();

            // add a set of columns to show setting info
            DataColumn serverName = new DataColumn(_columnServerName, Type.GetType("System.String"));
            DataColumn dbName = new DataColumn(_columnDBName, Type.GetType("System.String"));
            DataColumn ip = new DataColumn(_columnIp, Type.GetType("System.String"));
            DataColumn ipXpre = new DataColumn(_columnIpXpre, Type.GetType("System.String"));
            DataColumn ipXtest = new DataColumn(_columnIpXtest, Type.GetType("System.String"));
            DataColumn ipRelease = new DataColumn(_columnIpRelease, Type.GetType("System.String"));
            DataColumn standbyServers = new DataColumn(_columnStandby, Type.GetType("System.String"));

            tblIpSetting.Columns.Add(serverName);
            tblIpSetting.Columns.Add(dbName);
            tblIpSetting.Columns.Add(ip);
            tblIpSetting.Columns.Add(ipXpre);
            tblIpSetting.Columns.Add(ipXtest);
            tblIpSetting.Columns.Add(ipRelease);
            tblIpSetting.Columns.Add(standbyServers);

            // traverse throughout keys in app.config to get DBXX key
            Dictionary<string, ServerIpSetting> ipSetting = this._configUtility.GetIPSettings();

            var config = ConfigurationManager.AppSettings["ImageServers"];
            foreach (string key in ipSetting.Keys)
            {
                DataRow row = tblIpSetting.NewRow();
                row[_columnServerName] = ipSetting[key].ServerName;
                row[_columnDBName] = ipSetting[key].DBName;
                row[_columnIp] = ipSetting[key].Ip;
                row[_columnIpXpre] = ipSetting[key].IpXpre;
                row[_columnIpXtest] = ipSetting[key].IpXtest;
                row[_columnIpRelease] = ipSetting[key].IpRelease;

                // process for load Stanby server which is checked
                string[] arrStandbyServers = null;

                var startIndex = config.IndexOf(key + "--");
                if (startIndex != -1)
                {
                    // startOfOldConfig is removed string don't necessary
                    var startOfOldConfig = config.Substring(startIndex);
                    //// get end of string
                    var endOfOldConfig = startOfOldConfig.IndexOf(";");

                    // get leng of current string
                    var length = endOfOldConfig == -1
                    ? startOfOldConfig.Length - key.Length - 2
                    : endOfOldConfig - key.Length - 2;

                    // get string stadbyServer
                    var standbyServer = startOfOldConfig.Substring(key.Length + 2, length);
                    arrStandbyServers = standbyServer.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                }

                var number = arrStandbyServers == null ? 0 : arrStandbyServers.Length;
                row[_columnStandby] = number > 0 ? "Edit (" + number + ")" : "Add";

                tblIpSetting.Rows.Add(row);
            }

            this.dgvIpConfig.DataSource = tblIpSetting;
            tblIpSetting.Dispose();
        }

        /// <summary>
        /// Reset the grid according to the original app.cofig
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            this.LoadIpSettingFromIpSettingConfig();
        }

        /// <summary>
        /// Save back IP setting from the grid to the app.config 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            // delete all ip settings in IPSetting.xml
            this._configUtility.DeleteIPSettings();

            // save all ip settings from grid to IPSetting.xml
            for (int i = 0; i < this.dgvIpConfig.Rows.Count - 1; i++)
            {
                if (this.dgvIpConfig[_columnServerName, i].Value.ToString().Trim() != string.Empty
                    && this.dgvIpConfig[_columnDBName, i].Value.ToString().Trim() != string.Empty)
                {
                    this._configUtility.AddSetting(
                        this.dgvIpConfig[_columnServerName, i].Value.ToString().Trim(),
                        this.dgvIpConfig[_columnDBName, i].Value.ToString().Trim(),
                        this.dgvIpConfig[_columnIp, i].Value.ToString().Trim(),
                        this.dgvIpConfig[_columnIpXpre, i].Value.ToString().Trim(),
                        this.dgvIpConfig[_columnIpXtest, i].Value.ToString().Trim(),
                        this.dgvIpConfig[_columnIpRelease, i].Value.ToString().Trim());
                }
            }

            ucScript._serverConfig = this._configUtility.GetIPSettings();
            ucDeployment._serverConfig = this._configUtility.GetIPSettings();
            ucRollback._serverConfig = this._configUtility.GetIPSettings();

            MessageBox.Show("IP setting is saved successfully", "Save Setting", MessageBoxButtons.OK);
        }

        private void passNumber(int number)
        {
            dgvIpConfig[_activeColumn, _activeRow].Value = number > 0
                ? "Edit (" + number + ")"
                : "Add";
        }

        private void dgvIpConfig_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1)
            {
                return;
            }

            if (this.dgvIpConfig.Columns[e.ColumnIndex].Name != _columnStandby || e.RowIndex == -1)
            {
                return;
            }

            _activeRow = e.RowIndex;
            _activeColumn = e.ColumnIndex;

            if (string.IsNullOrEmpty(dgvIpConfig[_columnServerName, e.RowIndex].Value.ToString().Trim()))
            {
                return;
            }

            StandbyServer standbyServer = new StandbyServer(
                dgvIpConfig[_columnServerName, e.RowIndex].Value.ToString(), 
                dgvIpConfig[_columnDBName, e.RowIndex].Value.ToString());

            standbyServer.PassNumber = passNumber;
            standbyServer.ShowDialog();
        }

        private void dgvIpConfig_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            btnSave.Enabled = true;
            DataTable tempTable = (DataTable)this.dgvIpConfig.DataSource;
            int i = 0;
            int j = 0;
            List<int> listDuplicated = new List<int>();
            List<int> listInvalidIP = new List<int>();
            List<int> listInvalidServerName = new List<int>();
            List<int> listInvalidDbName = new List<int>();

            foreach (DataRow row in tempTable.Rows)
            {
                // The server has not been specified the IP in the config file yet
                j = 0;
                foreach (DataRow row1 in tempTable.Rows)
                {
                    if (i != j 
                        && row[_columnServerName].ToString().ToUpper().Trim() == row1[_columnServerName].ToString().ToUpper().Trim()
                        && row[_columnDBName].ToString().ToUpper().Trim() == row1[_columnDBName].ToString().ToUpper().Trim())
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

                // ServerName-Invalid
                if (row[_columnServerName].ToString().Trim().IndexOf(' ') != -1)
                {
                    if (!listInvalidServerName.Contains(i))
                    {
                        listInvalidServerName.Add(i);
                    }
                }

                // DBName-Invalid
                if (row[_columnDBName].ToString().Trim().IndexOf(' ') != -1)
                {
                    if (!listInvalidDbName.Contains(i))
                    {
                        listInvalidDbName.Add(i);
                    }
                }

                // IP-Invalid
                //if (!IsIPv4(row[_columnIp].ToString())
                //    || !IsIPv4(row[_columnIpXpre].ToString())
                //    || !IsIPv4(row[_columnIpXtest].ToString())
                //    || !IsIPv4(row[_columnIpRelease].ToString()))
                //{
                //    if (!listInvalidIP.Contains(i))
                //    {
                //        listInvalidIP.Add(i);
                //    }
                //}

                i++;
                row.RowError = string.Empty;
            }

            foreach (var index in listInvalidIP)
            {
                tempTable.Rows[index].RowError = "Invalid IP";
            }

            foreach (var index in listInvalidDbName)
            {
                tempTable.Rows[index].RowError = "Invalid DB Name";
            }

            foreach (var index in listInvalidServerName)
            {
                tempTable.Rows[index].RowError = "Invalid Server Name";
            }

            foreach (var index in listDuplicated)
            {
                tempTable.Rows[index].RowError = "Sever Name & DB Nane is Duplicated";
            }

            if (listDuplicated.Count > 0 || listInvalidIP.Count > 0)
            {
                btnSave.Enabled = false;
            }
            
            this.dgvIpConfig.DataSource = tempTable;
        }

        public static bool IsIPv4(string value)
        {
            var quads = value.Split('.');

            // if we do not have 4 quads, return false
            if (!(quads.Length == 4)) return false;

            // for each quad
            foreach (var quad in quads)
            {
                int q;
                // if parse fails 
                // or length of parsed int != length of quad string (i.e.; '1' vs '001')
                // or parsed int < 0
                // or parsed int > 255
                // return false
                if (!Int32.TryParse(quad, out q)
                    || !q.ToString().Length.Equals(quad.Length)
                    || q < 0
                    || q > 255) { return false; }

            }

            return true;
        }

        private void dgvIpConfig_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var tempValue = string.Empty;
            var currentColor = _color1;

            foreach (DataGridViewRow row in this.dgvIpConfig.Rows)
            {
                if (row.Cells[_columnServerName].Value != null)
                {
                    if (tempValue != row.Cells[_columnServerName].Value.ToString().Trim())
                    {
                        if (currentColor == _color1)
                        {
                            currentColor = _color2;
                        }
                        else
                        {
                            currentColor = _color1;
                        }
                    }

                    row.DefaultCellStyle.BackColor = currentColor;
                    tempValue = row.Cells[_columnServerName].Value.ToString().Trim();
                }
            }
        }
    }
}
