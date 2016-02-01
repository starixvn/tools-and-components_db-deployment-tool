namespace DbDeployer.UserControls
{
    partial class ucDeployment
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnl_Action = new System.Windows.Forms.Panel();
            this.Actions = new System.Windows.Forms.GroupBox();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnDeploy = new System.Windows.Forms.Button();
            this.chkbBackup = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chk_SaveUserName = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbbSourceServer = new System.Windows.Forms.ComboBox();
            this.chkAllServers = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonZoom = new System.Windows.Forms.Button();
            this.rtxtOutput = new FastColoredTextBoxNS.FastColoredTextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.rbtUAT4 = new System.Windows.Forms.RadioButton();
            this.rbtUAT3 = new System.Windows.Forms.RadioButton();
            this.rbtUAT2 = new System.Windows.Forms.RadioButton();
            this.rbtUAT1 = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.cbbScriptSource = new System.Windows.Forms.ComboBox();
            this.rbtRelease = new System.Windows.Forms.RadioButton();
            this.rbtPrerelease = new System.Windows.Forms.RadioButton();
            this.rbtTest = new System.Windows.Forms.RadioButton();
            this.prgbScript = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCheckSpExist = new System.Windows.Forms.Button();
            this.btnOpenConfig = new System.Windows.Forms.Button();
            this.dgvSPList = new System.Windows.Forms.DataGridView();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Choose = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.OnlyRename = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ServerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DBName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScriptName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsFunction = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnl_Action.SuspendLayout();
            this.Actions.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSPList)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "No";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTextBoxColumn1.HeaderText = "No.";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 30;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.DataPropertyName = "Select";
            this.dataGridViewCheckBoxColumn1.HeaderText = "";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.ReadOnly = true;
            this.dataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewCheckBoxColumn1.Width = 65;
            // 
            // dataGridViewCheckBoxColumn2
            // 
            this.dataGridViewCheckBoxColumn2.DataPropertyName = "OnlyRename";
            this.dataGridViewCheckBoxColumn2.HeaderText = "Only Rename";
            this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
            this.dataGridViewCheckBoxColumn2.ReadOnly = true;
            this.dataGridViewCheckBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewCheckBoxColumn2.Width = 75;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "ServerName";
            this.dataGridViewTextBoxColumn2.HeaderText = "Server Name";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 80;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "DBName";
            this.dataGridViewTextBoxColumn3.HeaderText = "DB Name";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn3.Width = 80;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "ScriptName";
            this.dataGridViewTextBoxColumn4.HeaderText = "Script Name";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn4.Width = 187;
            // 
            // dataGridViewCheckBoxColumn3
            // 
            this.dataGridViewCheckBoxColumn3.DataPropertyName = "IsFunction";
            this.dataGridViewCheckBoxColumn3.HeaderText = "IsFunction";
            this.dataGridViewCheckBoxColumn3.Name = "dataGridViewCheckBoxColumn3";
            this.dataGridViewCheckBoxColumn3.ReadOnly = true;
            this.dataGridViewCheckBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewCheckBoxColumn3.Width = 65;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "IP";
            this.dataGridViewTextBoxColumn5.HeaderText = "Server IP";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "FileName";
            this.dataGridViewTextBoxColumn6.HeaderText = "File Name";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn6.Width = 150;
            // 
            // pnl_Action
            // 
            this.pnl_Action.Controls.Add(this.Actions);
            this.pnl_Action.Controls.Add(this.groupBox3);
            this.pnl_Action.Controls.Add(this.groupBox4);
            this.pnl_Action.Controls.Add(this.groupBox7);
            this.pnl_Action.Location = new System.Drawing.Point(0, 197);
            this.pnl_Action.Name = "pnl_Action";
            this.pnl_Action.Size = new System.Drawing.Size(992, 311);
            this.pnl_Action.TabIndex = 26;
            this.pnl_Action.Visible = false;
            // 
            // Actions
            // 
            this.Actions.Controls.Add(this.btnPreview);
            this.Actions.Controls.Add(this.btnDeploy);
            this.Actions.Controls.Add(this.chkbBackup);
            this.Actions.Location = new System.Drawing.Point(829, 0);
            this.Actions.Name = "Actions";
            this.Actions.Size = new System.Drawing.Size(160, 87);
            this.Actions.TabIndex = 25;
            this.Actions.TabStop = false;
            this.Actions.Text = "Actions";
            // 
            // btnPreview
            // 
            this.btnPreview.Enabled = false;
            this.btnPreview.Location = new System.Drawing.Point(4, 49);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(73, 23);
            this.btnPreview.TabIndex = 27;
            this.btnPreview.Text = "&Preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnDeploy
            // 
            this.btnDeploy.Enabled = false;
            this.btnDeploy.Location = new System.Drawing.Point(81, 49);
            this.btnDeploy.Name = "btnDeploy";
            this.btnDeploy.Size = new System.Drawing.Size(75, 23);
            this.btnDeploy.TabIndex = 25;
            this.btnDeploy.Text = "Deploy";
            this.btnDeploy.UseVisualStyleBackColor = true;
            this.btnDeploy.Click += new System.EventHandler(this.btnDeploy_Click);
            // 
            // chkbBackup
            // 
            this.chkbBackup.AutoSize = true;
            this.chkbBackup.Checked = true;
            this.chkbBackup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkbBackup.Location = new System.Drawing.Point(7, 21);
            this.chkbBackup.Name = "chkbBackup";
            this.chkbBackup.Size = new System.Drawing.Size(68, 17);
            this.chkbBackup.TabIndex = 26;
            this.chkbBackup.Text = "Back Up";
            this.chkbBackup.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chk_SaveUserName);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.cbbSourceServer);
            this.groupBox3.Controls.Add(this.chkAllServers);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtPassword);
            this.groupBox3.Controls.Add(this.txtUserName);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(3, 1);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(352, 86);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Database information";
            // 
            // chk_SaveUserName
            // 
            this.chk_SaveUserName.AutoSize = true;
            this.chk_SaveUserName.Checked = true;
            this.chk_SaveUserName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_SaveUserName.Location = new System.Drawing.Point(71, 66);
            this.chk_SaveUserName.Name = "chk_SaveUserName";
            this.chk_SaveUserName.Size = new System.Drawing.Size(126, 17);
            this.chk_SaveUserName.TabIndex = 17;
            this.chk_SaveUserName.Text = "Remember username";
            this.chk_SaveUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_SaveUserName.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(203, 44);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Server";
            // 
            // cbbSourceServer
            // 
            this.cbbSourceServer.Enabled = false;
            this.cbbSourceServer.FormattingEnabled = true;
            this.cbbSourceServer.Location = new System.Drawing.Point(246, 40);
            this.cbbSourceServer.Name = "cbbSourceServer";
            this.cbbSourceServer.Size = new System.Drawing.Size(98, 21);
            this.cbbSourceServer.TabIndex = 5;
            this.cbbSourceServer.SelectedIndexChanged += new System.EventHandler(this.cbbSourceServer_SelectedIndexChanged);
            // 
            // chkAllServers
            // 
            this.chkAllServers.AutoSize = true;
            this.chkAllServers.Checked = true;
            this.chkAllServers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllServers.Enabled = false;
            this.chkAllServers.Location = new System.Drawing.Point(246, 18);
            this.chkAllServers.Name = "chkAllServers";
            this.chkAllServers.Size = new System.Drawing.Size(74, 17);
            this.chkAllServers.TabIndex = 4;
            this.chkAllServers.Text = "All servers";
            this.chkAllServers.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAllServers.UseVisualStyleBackColor = true;
            this.chkAllServers.CheckedChanged += new System.EventHandler(this.chkAllServers_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(71, 40);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(118, 20);
            this.txtPassword.TabIndex = 3;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(71, 16);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(118, 20);
            this.txtUserName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Username";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.buttonZoom);
            this.groupBox4.Controls.Add(this.rtxtOutput);
            this.groupBox4.Location = new System.Drawing.Point(3, 90);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(985, 228);
            this.groupBox4.TabIndex = 22;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Output";
            // 
            // buttonZoom
            // 
            this.buttonZoom.BackgroundImage = global::DbDeployer.Properties.Resources.fullscreen;
            this.buttonZoom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonZoom.Location = new System.Drawing.Point(954, 195);
            this.buttonZoom.Name = "buttonZoom";
            this.buttonZoom.Size = new System.Drawing.Size(23, 23);
            this.buttonZoom.TabIndex = 5;
            this.buttonZoom.UseVisualStyleBackColor = true;
            this.buttonZoom.Click += new System.EventHandler(this.buttonZoom_Click);
            // 
            // rtxtOutput
            // 
            this.rtxtOutput.AutoScrollMinSize = new System.Drawing.Size(29, 16);
            this.rtxtOutput.BackBrush = null;
            this.rtxtOutput.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.rtxtOutput.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.rtxtOutput.Font = new System.Drawing.Font("Courier New", 11F);
            this.rtxtOutput.Language = FastColoredTextBoxNS.Language.SQL;
            this.rtxtOutput.Location = new System.Drawing.Point(6, 19);
            this.rtxtOutput.Name = "rtxtOutput";
            this.rtxtOutput.Paddings = new System.Windows.Forms.Padding(0);
            this.rtxtOutput.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.rtxtOutput.Size = new System.Drawing.Size(973, 203);
            this.rtxtOutput.TabIndex = 0;
            this.rtxtOutput.DoubleClick += new System.EventHandler(this.rtxtOutput_DoubleClick);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.rbtUAT4);
            this.groupBox7.Controls.Add(this.rbtUAT3);
            this.groupBox7.Controls.Add(this.rbtUAT2);
            this.groupBox7.Controls.Add(this.rbtUAT1);
            this.groupBox7.Controls.Add(this.label4);
            this.groupBox7.Controls.Add(this.cbbScriptSource);
            this.groupBox7.Controls.Add(this.rbtRelease);
            this.groupBox7.Controls.Add(this.rbtPrerelease);
            this.groupBox7.Controls.Add(this.rbtTest);
            this.groupBox7.Location = new System.Drawing.Point(362, 0);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(461, 87);
            this.groupBox7.TabIndex = 20;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Phases";
            // 
            // rbtUAT4
            // 
            this.rbtUAT4.AutoSize = true;
            this.rbtUAT4.Location = new System.Drawing.Point(382, 21);
            this.rbtUAT4.Name = "rbtUAT4";
            this.rbtUAT4.Size = new System.Drawing.Size(53, 17);
            this.rbtUAT4.TabIndex = 17;
            this.rbtUAT4.Text = "UAT4";
            this.rbtUAT4.UseVisualStyleBackColor = true;
            // 
            // rbtUAT3
            // 
            this.rbtUAT3.AutoSize = true;
            this.rbtUAT3.Location = new System.Drawing.Point(307, 21);
            this.rbtUAT3.Name = "rbtUAT3";
            this.rbtUAT3.Size = new System.Drawing.Size(53, 17);
            this.rbtUAT3.TabIndex = 16;
            this.rbtUAT3.Text = "UAT3";
            this.rbtUAT3.UseVisualStyleBackColor = true;
            // 
            // rbtUAT2
            // 
            this.rbtUAT2.AutoSize = true;
            this.rbtUAT2.Location = new System.Drawing.Point(232, 21);
            this.rbtUAT2.Name = "rbtUAT2";
            this.rbtUAT2.Size = new System.Drawing.Size(53, 17);
            this.rbtUAT2.TabIndex = 15;
            this.rbtUAT2.Text = "UAT2";
            this.rbtUAT2.UseVisualStyleBackColor = true;
            // 
            // rbtUAT1
            // 
            this.rbtUAT1.AutoSize = true;
            this.rbtUAT1.Location = new System.Drawing.Point(157, 21);
            this.rbtUAT1.Name = "rbtUAT1";
            this.rbtUAT1.Size = new System.Drawing.Size(53, 17);
            this.rbtUAT1.TabIndex = 14;
            this.rbtUAT1.Text = "UAT1";
            this.rbtUAT1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(84, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "From :";
            // 
            // cbbScriptSource
            // 
            this.cbbScriptSource.Enabled = false;
            this.cbbScriptSource.FormattingEnabled = true;
            this.cbbScriptSource.Items.AddRange(new object[] {
            "",
            "Origin",
            "Pre-release",
            "Test",
            "UAT1",
            "UAT2",
            "UAT3",
            "UAT4"});
            this.cbbScriptSource.Location = new System.Drawing.Point(126, 50);
            this.cbbScriptSource.Name = "cbbScriptSource";
            this.cbbScriptSource.Size = new System.Drawing.Size(135, 21);
            this.cbbScriptSource.TabIndex = 13;
            // 
            // rbtRelease
            // 
            this.rbtRelease.AutoSize = true;
            this.rbtRelease.Location = new System.Drawing.Point(9, 52);
            this.rbtRelease.Name = "rbtRelease";
            this.rbtRelease.Size = new System.Drawing.Size(64, 17);
            this.rbtRelease.TabIndex = 11;
            this.rbtRelease.Text = "Release";
            this.rbtRelease.UseVisualStyleBackColor = true;
            this.rbtRelease.CheckedChanged += new System.EventHandler(this.rbtrelease_CheckedChanged);
            // 
            // rbtPrerelease
            // 
            this.rbtPrerelease.AutoSize = true;
            this.rbtPrerelease.Checked = true;
            this.rbtPrerelease.Location = new System.Drawing.Point(9, 21);
            this.rbtPrerelease.Name = "rbtPrerelease";
            this.rbtPrerelease.Size = new System.Drawing.Size(78, 17);
            this.rbtPrerelease.TabIndex = 5;
            this.rbtPrerelease.TabStop = true;
            this.rbtPrerelease.Text = "Pre-release";
            this.rbtPrerelease.UseVisualStyleBackColor = true;
            // 
            // rbtTest
            // 
            this.rbtTest.AutoSize = true;
            this.rbtTest.Location = new System.Drawing.Point(95, 21);
            this.rbtTest.Name = "rbtTest";
            this.rbtTest.Size = new System.Drawing.Size(46, 17);
            this.rbtTest.TabIndex = 6;
            this.rbtTest.Text = "Test";
            this.rbtTest.UseVisualStyleBackColor = true;
            // 
            // prgbScript
            // 
            this.prgbScript.Location = new System.Drawing.Point(3, 514);
            this.prgbScript.Name = "prgbScript";
            this.prgbScript.Size = new System.Drawing.Size(986, 23);
            this.prgbScript.TabIndex = 23;
            this.prgbScript.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCheckSpExist);
            this.groupBox1.Controls.Add(this.btnOpenConfig);
            this.groupBox1.Controls.Add(this.dgvSPList);
            this.groupBox1.Location = new System.Drawing.Point(3, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(986, 197);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Script list";
            // 
            // btnCheckSpExist
            // 
            this.btnCheckSpExist.Enabled = false;
            this.btnCheckSpExist.Location = new System.Drawing.Point(905, 167);
            this.btnCheckSpExist.Name = "btnCheckSpExist";
            this.btnCheckSpExist.Size = new System.Drawing.Size(75, 23);
            this.btnCheckSpExist.TabIndex = 3;
            this.btnCheckSpExist.Text = "Check Exist";
            this.btnCheckSpExist.UseVisualStyleBackColor = true;
            this.btnCheckSpExist.Click += new System.EventHandler(this.btnCheckSpExist_Click);
            // 
            // btnOpenConfig
            // 
            this.btnOpenConfig.Location = new System.Drawing.Point(826, 167);
            this.btnOpenConfig.Name = "btnOpenConfig";
            this.btnOpenConfig.Size = new System.Drawing.Size(75, 23);
            this.btnOpenConfig.TabIndex = 2;
            this.btnOpenConfig.Text = "&Open";
            this.btnOpenConfig.UseVisualStyleBackColor = true;
            this.btnOpenConfig.Click += new System.EventHandler(this.btnOpenConfig_Click);
            // 
            // dgvSPList
            // 
            this.dgvSPList.AllowUserToResizeColumns = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSPList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSPList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSPList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No,
            this.Choose,
            this.OnlyRename,
            this.ServerName,
            this.DBName,
            this.ScriptName,
            this.IsFunction,
            this.IP,
            this.FileName});
            this.dgvSPList.Location = new System.Drawing.Point(4, 14);
            this.dgvSPList.Name = "dgvSPList";
            this.dgvSPList.ReadOnly = true;
            this.dgvSPList.Size = new System.Drawing.Size(976, 148);
            this.dgvSPList.TabIndex = 0;
            this.dgvSPList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSPList_CellContentClick);
            this.dgvSPList.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSPList_CellEndEdit);
            this.dgvSPList.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvSPList_ColumnHeaderMouseClick);
            // 
            // No
            // 
            this.No.DataPropertyName = "No";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.No.DefaultCellStyle = dataGridViewCellStyle3;
            this.No.HeaderText = "#";
            this.No.Name = "No";
            this.No.ReadOnly = true;
            this.No.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.No.Width = 30;
            // 
            // Choose
            // 
            this.Choose.DataPropertyName = "Choose";
            this.Choose.HeaderText = "Select  ";
            this.Choose.Name = "Choose";
            this.Choose.ReadOnly = true;
            this.Choose.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Choose.Width = 75;
            // 
            // OnlyRename
            // 
            this.OnlyRename.DataPropertyName = "OnlyRename";
            this.OnlyRename.HeaderText = "Only Rename";
            this.OnlyRename.Name = "OnlyRename";
            this.OnlyRename.ReadOnly = true;
            this.OnlyRename.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.OnlyRename.Width = 77;
            // 
            // ServerName
            // 
            this.ServerName.DataPropertyName = "ServerName";
            this.ServerName.HeaderText = "Server Name";
            this.ServerName.Name = "ServerName";
            this.ServerName.ReadOnly = true;
            this.ServerName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ServerName.Width = 93;
            // 
            // DBName
            // 
            this.DBName.DataPropertyName = "DBName";
            this.DBName.HeaderText = "DB Name";
            this.DBName.Name = "DBName";
            this.DBName.ReadOnly = true;
            this.DBName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DBName.Width = 80;
            // 
            // ScriptName
            // 
            this.ScriptName.DataPropertyName = "ScriptName";
            this.ScriptName.HeaderText = "SP Name";
            this.ScriptName.Name = "ScriptName";
            this.ScriptName.ReadOnly = true;
            this.ScriptName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ScriptName.Width = 340;
            // 
            // IsFunction
            // 
            this.IsFunction.DataPropertyName = "IsFunction";
            this.IsFunction.HeaderText = "Function";
            this.IsFunction.Name = "IsFunction";
            this.IsFunction.ReadOnly = true;
            this.IsFunction.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.IsFunction.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.IsFunction.Width = 55;
            // 
            // IP
            // 
            this.IP.DataPropertyName = "IP";
            this.IP.HeaderText = "Server IP";
            this.IP.Name = "IP";
            this.IP.ReadOnly = true;
            this.IP.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // FileName
            // 
            this.FileName.DataPropertyName = "FileName";
            this.FileName.HeaderText = "File Name";
            this.FileName.Name = "FileName";
            this.FileName.ReadOnly = true;
            this.FileName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.FileName.Width = 83;
            // 
            // ucDeployment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_Action);
            this.Controls.Add(this.prgbScript);
            this.Controls.Add(this.groupBox1);
            this.Name = "ucDeployment";
            this.Size = new System.Drawing.Size(992, 541);
            this.Load += new System.EventHandler(this.ucDeployment_Load);
            this.pnl_Action.ResumeLayout(false);
            this.Actions.ResumeLayout(false);
            this.Actions.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSPList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnOpenConfig;
        private System.Windows.Forms.DataGridView dgvSPList;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbbSourceServer;
        private System.Windows.Forms.CheckBox chkAllServers;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbbScriptSource;
        private System.Windows.Forms.RadioButton rbtRelease;
        private System.Windows.Forms.RadioButton rbtPrerelease;
        private System.Windows.Forms.RadioButton rbtTest;
        private System.Windows.Forms.Button btnCheckSpExist;
        private System.Windows.Forms.ProgressBar prgbScript;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox Actions;
        private System.Windows.Forms.Button btnDeploy;
        private System.Windows.Forms.CheckBox chkbBackup;
        private System.Windows.Forms.Button btnPreview;
        private FastColoredTextBoxNS.FastColoredTextBox rtxtOutput;
        private System.Windows.Forms.Button buttonZoom;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.Panel pnl_Action;
        private System.Windows.Forms.CheckBox chk_SaveUserName;
        private System.Windows.Forms.RadioButton rbtUAT4;
        private System.Windows.Forms.RadioButton rbtUAT3;
        private System.Windows.Forms.RadioButton rbtUAT2;
        private System.Windows.Forms.RadioButton rbtUAT1;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Choose;
        private System.Windows.Forms.DataGridViewCheckBoxColumn OnlyRename;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DBName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScriptName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsFunction;
        private System.Windows.Forms.DataGridViewTextBoxColumn IP;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
    }
}
