namespace DbDeployer
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Windows.Forms;
    using System.Xml;
    using DbDeployer.Core;
    using DbDeployer.UserControls;

    public partial class StandbyServer : Form
    {
        public delegate void PassNumberDelegate(int number);
        public PassNumberDelegate PassNumber;

        private ConfigUtility configUtility = new ConfigUtility();
        private MixedCheckBoxesTreeView mixedCheckBoxesTreeView;

        private string _serverName;
        private string _dbName;
        private string _key;

        public StandbyServer(string serverName, string dbName)
        {
            this._serverName = serverName;
            this._dbName = dbName;
            InitializeComponent();
        }

        private void StandbyServer_Load(object sender, EventArgs e)
        {
            GetStandby();
        }

        private void UpdateSettings(string key, string value)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            foreach (XmlElement element in xmlDoc.DocumentElement)
            {
                if (element.Name.Equals("appSettings"))
                {
                    foreach (XmlNode node in element.ChildNodes)
                    {
                        if (node.Attributes[0].Value.Equals(key))
                        {
                            node.Attributes[1].Value = value;
                        }
                    }
                }
            }

            xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            ConfigurationManager.RefreshSection("appSettings");

        }

        private void UpdateStandbyServers(string standbyServers)
        {
            var config = ConfigurationManager.AppSettings["ImageServers"];
            if (!config.Contains(this._key + "--"))
            {
                config = config.Substring(config.Length - 1, 1) == ";"
                    ? config + this._key + "--" + standbyServers
                    : config + ";" + this._key + "--" + standbyServers;
            }
            else
            {
                var startOfOldConfig = config.IndexOf(this._key + "--");
                var sc = config.Substring(startOfOldConfig);
                var endOfOldConfig = sc.IndexOf(";");

                var newConfig = this._key + "--" + standbyServers;

                var length = endOfOldConfig == -1
                ? sc.Length
                : endOfOldConfig;

                var oldConfig = config.Substring(startOfOldConfig, length);
                if (!string.IsNullOrWhiteSpace(oldConfig))
                {
                    config = config.Replace(oldConfig, newConfig);
                }
            }

            UpdateSettings("ImageServers", config);
        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            var result = string.Empty;
            var countCheck = 0;

            for (int i = 0; i < this.mixedCheckBoxesTreeView.Nodes.Count; i++)
            {
                for (int j = 0; j < this.mixedCheckBoxesTreeView.Nodes[i].Nodes.Count; j++)
                {
                    if (this.mixedCheckBoxesTreeView.Nodes[i].Nodes[j].Checked)
                    {
                        result += this.mixedCheckBoxesTreeView.Nodes[i].Text.Trim() + "-" + this.mixedCheckBoxesTreeView.Nodes[i].Nodes[j].Text.ToUpper().Trim() + ",";
                        countCheck++;
                    }
                }
            }

            if (result.Length > 0)
            {
                result = result.Substring(0, result.Length - 1);
            }

            UpdateStandbyServers(result);
            
            ucDeployment._imageServers = this.configUtility.GetStandbyServers();
            ucRollback._imageServers = this.configUtility.GetStandbyServers();

            PassNumber(countCheck);
            var action = MessageBox.Show("Standby servers settings are updated.", "DbDeployer", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

            if (action == System.Windows.Forms.DialogResult.OK)
            {
                this.Close();
            }
        }

        private void GetStandby()
        {
            this.Text = "Standby DBs of " + _serverName + " - " + _dbName;
            this._key = configUtility.GetKey(_serverName, _dbName);
            var ipSetting = configUtility.GetIPSettings();

            var config = ConfigurationManager.AppSettings["ImageServers"];

            // process for load Stanby server which is checked
            string[] arrStandbyServers = null;

            var startIndex = config.IndexOf(this._key + "--");
            if (startIndex != -1)
            {
                // startOfOldConfig is removed string don't necessary
                var startOfOldConfig = config.Substring(startIndex);
                // get end of string
                var endOfOldConfig = startOfOldConfig.IndexOf(";");

                // get leng of current string
                var length = endOfOldConfig == -1
                ? startOfOldConfig.Length - this._key.Length - 2
                : endOfOldConfig - this._key.Length - 2;

                // get string stadbyServer
                var standbyServer = startOfOldConfig.Substring(this._key.Length + 2, length);
                arrStandbyServers = standbyServer.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            }

            mixedCheckBoxesTreeView = new MixedCheckBoxesTreeView();
            mixedCheckBoxesTreeView.Name = "mixedCheckBoxesTreeView";
            mixedCheckBoxesTreeView.Width = 290;
            mixedCheckBoxesTreeView.Height = 214;
            mixedCheckBoxesTreeView.Top = 12;
            mixedCheckBoxesTreeView.Left = 12;
            mixedCheckBoxesTreeView.CheckBoxes = true;
            mixedCheckBoxesTreeView.Nodes.Clear();

            var tempKey = string.Empty;
            HiddenCheckBoxTreeNode hidenCheckNote = new HiddenCheckBoxTreeNode("");
            foreach (string key in ipSetting.Keys)
            {
                if (key != this._key)
                {
                    if (ipSetting[key].ServerName != tempKey)
                    {
                        hidenCheckNote = new HiddenCheckBoxTreeNode(ipSetting[key].ServerName);
                        mixedCheckBoxesTreeView.Nodes.Add(hidenCheckNote);
                    }
                    
                    TreeNode checkNode = new TreeNode(ipSetting[key].DBName);
                    if (arrStandbyServers != null && arrStandbyServers.Contains(key))
                    {
                        checkNode.Checked = true;
                        hidenCheckNote.Expand();
                    }
                    else
                    {
                        checkNode.Checked = false;
                    }

                    hidenCheckNote.Nodes.Add(checkNode);
                    tempKey = ipSetting[key].ServerName;
                }
            }

            this.Controls.Add(mixedCheckBoxesTreeView);
        }
    }
}
