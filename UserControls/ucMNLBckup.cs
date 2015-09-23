namespace DbDeployer.UserControls
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;
    using DbDeployer.Core;
    using DbDeployer.Core.Utils;

    public partial class ucMNLBckup : ucBase
    {
        private string _scriptPath = string.Empty;
        private Dictionary<string, string> dctDB_FolderName;
        private Dictionary<string, string> dctDB_SPList;
        private string _scriptFoler;
        private SqlServer sqlUtil;
        private ConfigUtility _config;
        public ucMNLBckup()
        {
            this.InitializeComponent();
            this._config = new ConfigUtility();
        }
        private void ucMNLBckup_Load(object sender, EventArgs e)
        {
            this.txtScriptTo.Text = this._scriptPath = AppDomain.CurrentDomain.BaseDirectory + @"\MNLBckup";
            if (!Directory.Exists(this._scriptPath))
            {
                Directory.CreateDirectory(this._scriptPath);
            }
            this.sqlUtil = new SqlServer();
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            this.ChangeSubNodeState(e.Node);
            if (e.Node.Checked)
                e.Node.Expand();
        }
        #region helpers
        /*
         * Change the state of sub checkboxes in tree according to super node state
         */
        private void ChangeSubNodeState(TreeNode node)
        {
            foreach (TreeNode sub in node.Nodes)
            {
                sub.Checked = node.Checked;
            }
        }

        private bool CheckPrerequisites()
        {
            //check user name and pass
            if (this.txtUserName.Text == string.Empty || this.txtPassword.Text == string.Empty)
            {
                this.rtxtOutput.Text = string.Empty;
                UIUtility.AppendColoredText(this.rtxtOutput, Properties.Resources.ACCOUNT_ERROR_MESS, Color.Red);
                return false;
            }
            return true;
        }

        //Build DB list from tree

        //Buill SP prefix list from tree
        public ArrayList GetSPPrefixList()
        {
            var ret = new ArrayList();
            for (var i = 0; i < this.trwSystem.Nodes[0].Nodes.Count; i++)
            {
                if (this.trwSystem.Nodes[0].Nodes[i].Checked)
                    ret.Add(this.trwSystem.Nodes[0].Nodes[i].Name);
            }
            return ret;
        }

        private void InitializeProgressBar(int steps)
        {
            //initailize the progress bar
            this.prgbScript.Visible = true;
            this.prgbScript.Maximum = (steps - 1) * 10;
            this.prgbScript.Step = 10;
            this.prgbScript.Value = 0;
        }

        private int[] CountNumOfSPPerSys(ArrayList prefix, string SPListLine)
        {
            //Initialize list num
            var Nums = new int[prefix.Count];
            for (var i = 0; i < Nums.Count(); i++)
            {
                Nums[i] = 0;
            }

            var SPList = SPListLine.Split(',');
            for (var i = 0; i < SPList.Count(); i++)
            {
                for (var j = 0; j < prefix.Count; j++)
                {
                    if (SPList[i].IndexOf(prefix[j].ToString(), StringComparison.OrdinalIgnoreCase) == 0)
                        Nums[j]++;
                }
            }
            return Nums;
        }

        private bool CheckThreadFinished(Thread[] arrT)
        {
            foreach (var t in arrT)
                if (t.ThreadState == ThreadState.Running)
                    return false;
            return true;
        }
        #endregion helpers

        private void btnBrowserFolder_Click(object sender, EventArgs e)
        {
            var folderBrowser = new FolderBrowserDialog();
            //folderBrowser.SelectedPath = ConfigurationSettings.AppSettings["Path"].ToString();

            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                this.txtScriptTo.Text = folderBrowser.SelectedPath + @"\";
            }
            this._scriptPath = this.txtScriptTo.Text;
            folderBrowser.Dispose();
        }

        private void rbtnScript_CheckedChanged(object sender, EventArgs e)
        {
            this.btnScript.Visible = this.grpDatabase.Visible = this.grpSystem.Visible = this.grpControl.Visible = this.rbtnScript.Checked;
            this.btnRestore.Visible = this.grpStatistic.Visible = this.btnOpenConfig.Visible = this.rbtnRestore.Checked;
        }

        #region main action
        private void btnScript_Click(object sender, EventArgs e)
        {
            this.rtxtOutput.Text = string.Empty;
            if (!this.CheckPrerequisites())
                return;

            //get the system prefix
            var arrPrefix = this.GetSPPrefixList();

            var dateTime = StringUtility.GetFormatedCurrentDateTime();
            string desc;
            if (this.txtDesc.Text != string.Empty)
                desc = this.txtDesc.Text;
            else
                desc = "desc";
            //Create folder for this action
            var mainFolder = this._scriptPath + @"\" + dateTime + "-" + desc;
            if (!Directory.Exists(mainFolder))
                Directory.CreateDirectory(mainFolder);
            //Build the list of DBs need to make backup
            //hard code traversing the tree
            var steps = 0;
            foreach (TreeNode tn1 in this.trwDB.Nodes[0].Nodes)
            {
                steps += tn1.Nodes.Count;
            }
            this.InitializeProgressBar(steps);
            //Generate out put to result box
            UIUtility.AppendColoredText(this.rtxtOutput, "Generate script for:", Color.Blue);
            for (var i = 0; i < arrPrefix.Count; i++)
            {
                UIUtility.AppendColoredText(this.rtxtOutput, arrPrefix[i].ToString(), Color.Blue);
            }
            UIUtility.AppendColoredText(this.rtxtOutput, "\r\rFrom:\r\n", Color.Blue);
            string[] arrThreadParam = { mainFolder, dateTime, desc };
            //count the number of needed thread
            var threadNo = 0;
            foreach (TreeNode n1 in this.trwDB.Nodes[0].Nodes)
            {
                foreach (TreeNode n2 in n1.Nodes)
                {
                    if (n2.Checked)
                        threadNo++;
                }
            }
            var arrT = new Thread[threadNo];
            var arrDB = new Database[threadNo];
            var count = 0;
            foreach (TreeNode n1 in this.trwDB.Nodes[0].Nodes)
            {
                foreach (TreeNode n2 in n1.Nodes)
                {
                    if (n2.Checked)
                    {
                        arrDB[count] = new Database(n1.Text, n2.Text);
                        if (!arrDB[count].Connect(this.txtUserName.Text, this.txtPassword.Text))
                        {
                            UIUtility.AppendColoredText(this.rtxtOutput, "Could not connect to " + n1.Text + "\r\n", Color.Red);
                            this.prgbScript.Visible = false;
                            continue;
                        }
                        string ret = arrDB[count].BuildSPList(arrPrefix);
                        if (ret != string.Empty)
                        {
                            UIUtility.AppendColoredText(this.rtxtOutput, "Could not get SP content from " + n1.Text + "." + n2.Text + "\r\n", Color.Red);
                            this.prgbScript.Visible = false;
                            continue;
                        }
                        arrT[count] = new Thread(arrDB[count].GenerateScriptFile);
                        arrT[count].Name = n1.Text + "_" + n2.Text;
                        arrT[count++].Start(arrThreadParam);

                        UIUtility.AppendColoredText(this.rtxtOutput, "-" + n1.Text + "." + n2.Text + "\r\n", Color.Blue);
                    }
                    this.prgbScript.PerformStep();
                }
            }

            for (var i = 0; i < count; i++)
            {
                arrT[i].Join();
            }
            UIUtility.AppendColoredText(this.rtxtOutput, "\r\nTask has been done sucessfully", Color.Blue);
            var dl = MessageBox.Show("Script SP from DB successfully", "Complete Task", MessageBoxButtons.OK);
            if (dl == DialogResult.OK) this.prgbScript.Visible = false;
        }


        private void btnOpenConfig_Click(object sender, EventArgs e)
        {
            this.rtxtOutput.Clear();
            var dlg = new OpenFileDialog
                {
                    Filter = "Config|*.config",
                    CheckFileExists = true,
                    CheckPathExists = true
                };
            try
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this._scriptFoler = dlg.FileName.Substring(0, dlg.FileName.LastIndexOf('\\'));
                    var fs = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read);
                    var sr = new StreamReader(fs);

                    string line1, line2, line3;
                    this.dctDB_FolderName = new Dictionary<string, string>();
                    this.dctDB_SPList = new Dictionary<string, string>();
                    var prefix = new ArrayList();
                    this.dgvStat.Rows.Clear();
                    foreach (TreeNode n in this.trwSystem.Nodes[0].Nodes)
                    {
                        prefix.Add(n.Name);
                    }
                    while ((line1 = sr.ReadLine()) != null)
                    {
                        if (line1.Trim() == string.Empty) continue;
                        else
                        {
                            line2 = sr.ReadLine().Trim();
                            line3 = sr.ReadLine().Trim();
                            //add DB and DB's script file name to dict
                            this.dctDB_FolderName.Add(line1.Trim(), line2.Trim());
                            this.dctDB_SPList.Add(line1, line3);

                            int n = this.dgvStat.Rows.Add();
                            this.dgvStat.Rows[n].Cells[0].Value = line1.Trim();
                            int[] temp = this.CountNumOfSPPerSys(prefix, line3);
                            for (int i = 1; i < temp.Count() + 1; i++)
                            {
                                this.dgvStat.Rows[n].Cells[i].Value = temp[i - 1];
                            }
                        }
                    }
                    sr.Close();
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                this.rtxtOutput.Clear();
                UIUtility.AppendColoredText(this.rtxtOutput, "Error occurred:\r\n" + ex.Message, Color.Red);
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            //Delete the occurrence of SP in restore list from destination database
            if (!this.CheckPrerequisites())
                return;
            string server = string.Empty, db = string.Empty;
            this.rtxtOutput.Clear();
            var NoDB = 0;
            NoDB = this.dctDB_SPList.Keys.Count;
            this.InitializeProgressBar(NoDB);

            var arrDB = new Database[NoDB];
            var arrThread = new Thread[NoDB];

            try
            {
                int count = 0;
                foreach (string key in this.dctDB_SPList.Keys)
                {
                    var threadParam = new string[5];//foldername, SPList, username and password
                    server = key.Split('-')[0];
                    db = key.Split('-')[1];
                    arrDB[count] = new Database(server, db);
                    threadParam[0] = this._scriptFoler + @"\" + this.dctDB_FolderName[key];
                    threadParam[1] = this.dctDB_SPList[key];
                    threadParam[2] = this.txtUserName.Text;
                    threadParam[3] = this.txtPassword.Text;
                    //NOTE: suppport sp only
                    threadParam[4] = "true";

                    arrThread[count] = new Thread(new ParameterizedThreadStart(arrDB[count].Restore));
                    arrThread[count].Name = server + "_" + db;
                    arrThread[count++].Start(threadParam);//just move the ++ operator to the later statement
                    this.prgbScript.PerformStep();
                }
                /*int numOfThreadAlive = count;
                bool done = false;
                while (!done)
                {
                    for (int i = count - 1; i >= 0; i--)
                    {
                        if (!arrThread[i].IsAlive)
                        {
                            numOfThreadAlive--;
                            prgbScript.PerformStep();
                            if (numOfThreadAlive == 0)
                                done = true;
                            Thread.Sleep(60000);
                        }
                    }
                }*/
                for (int i = 0; i < count; i++)
                {
                    arrThread[i].Join();
                }
            }
            catch (Exception ex)
            {
                this.rtxtOutput.Clear();
                UIUtility.AppendColoredText(this.rtxtOutput, "Can not delete existed SP from server. Error details:\r\n" + ex.Message, Color.Red);
                return;
            }
            finally
            {
                if (MessageBox.Show("Restore process done!", "Task done inform", MessageBoxButtons.OK) == DialogResult.OK)
                    this.prgbScript.Visible = false;

            }
        }

        #endregion main action

        private void rtxtOutput_DoubleClick(object sender, EventArgs e)
        {
            if (rtxtOutput.Text.Length == 0) return;
            var f = new ScriptViewer();
            f.UpdateScript(rtxtOutput.Text);
            f.ShowDialog();
        }

        private void buttonZoom_Click(object sender, EventArgs e)
        {
            if (rtxtOutput.Text.Length == 0) return;
            var f = new ScriptViewer();
            f.UpdateScript(rtxtOutput.Text);
            f.ShowDialog();
        }


    }
}
