namespace DbDeployer.UserControls
{
    partial class ucMNLBckup
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Accounting");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Agent");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Agent Wap");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Affiliate");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Hung Test");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("All", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("BODB02");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("DB02", new System.Windows.Forms.TreeNode[] {
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("On_User");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("BODB02");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("DB03", new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode10});
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("BODB02");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Affiliate_12bet");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("DB05", new System.Windows.Forms.TreeNode[] {
            treeNode12,
            treeNode13});
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("BODB02");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("DB06", new System.Windows.Forms.TreeNode[] {
            treeNode15});
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("All", new System.Windows.Forms.TreeNode[] {
            treeNode8,
            treeNode11,
            treeNode14,
            treeNode16});
            this.rtxtOutput = new FastColoredTextBoxNS.FastColoredTextBox();
            this.btnRestore = new System.Windows.Forms.Button();
            this.grpStatistic = new System.Windows.Forms.GroupBox();
            this.btnOpenConfig = new System.Windows.Forms.Button();
            this.dgvStat = new System.Windows.Forms.DataGridView();
            this.clDB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.acc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.age = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.agw = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aff = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prgbScript = new System.Windows.Forms.ProgressBar();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rbtnRestore = new System.Windows.Forms.RadioButton();
            this.rbtnScript = new System.Windows.Forms.RadioButton();
            this.grpControl = new System.Windows.Forms.GroupBox();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowserFolder = new System.Windows.Forms.Button();
            this.txtScriptTo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnScript = new System.Windows.Forms.Button();
            this.grpSystem = new System.Windows.Forms.GroupBox();
            this.trwSystem = new System.Windows.Forms.TreeView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grpDatabase = new System.Windows.Forms.GroupBox();
            this.trwDB = new System.Windows.Forms.TreeView();
            this.buttonZoom = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpStatistic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStat)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.grpControl.SuspendLayout();
            this.grpSystem.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.grpDatabase.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtxtOutput
            // 
            this.rtxtOutput.AutoScrollMinSize = new System.Drawing.Size(29, 16);
            this.rtxtOutput.BackBrush = null;
            this.rtxtOutput.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.rtxtOutput.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.rtxtOutput.Font = new System.Drawing.Font("Courier New", 11F);
            this.rtxtOutput.Language = FastColoredTextBoxNS.Language.SQL;
            this.rtxtOutput.Location = new System.Drawing.Point(194, 409);
            this.rtxtOutput.Name = "rtxtOutput";
            this.rtxtOutput.Paddings = new System.Windows.Forms.Padding(0);
            this.rtxtOutput.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.rtxtOutput.Size = new System.Drawing.Size(446, 100);
            this.rtxtOutput.TabIndex = 31;
            this.rtxtOutput.DoubleClick += new System.EventHandler(this.rtxtOutput_DoubleClick);
            // 
            // btnRestore
            // 
            this.btnRestore.Location = new System.Drawing.Point(655, 486);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(75, 23);
            this.btnRestore.TabIndex = 30;
            this.btnRestore.Text = "Restore";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Visible = false;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // grpStatistic
            // 
            this.grpStatistic.Controls.Add(this.btnOpenConfig);
            this.grpStatistic.Controls.Add(this.dgvStat);
            this.grpStatistic.Location = new System.Drawing.Point(176, 89);
            this.grpStatistic.Name = "grpStatistic";
            this.grpStatistic.Size = new System.Drawing.Size(578, 314);
            this.grpStatistic.TabIndex = 29;
            this.grpStatistic.TabStop = false;
            this.grpStatistic.Text = "SP statistic";
            this.grpStatistic.Visible = false;
            // 
            // btnOpenConfig
            // 
            this.btnOpenConfig.Location = new System.Drawing.Point(486, 281);
            this.btnOpenConfig.Name = "btnOpenConfig";
            this.btnOpenConfig.Size = new System.Drawing.Size(75, 23);
            this.btnOpenConfig.TabIndex = 32;
            this.btnOpenConfig.Text = "Open Config";
            this.btnOpenConfig.UseVisualStyleBackColor = true;
            this.btnOpenConfig.Visible = false;
            this.btnOpenConfig.Click += new System.EventHandler(this.btnOpenConfig_Click);
            // 
            // dgvStat
            // 
            this.dgvStat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStat.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clDB,
            this.acc,
            this.age,
            this.agw,
            this.aff,
            this.hung});
            this.dgvStat.Location = new System.Drawing.Point(18, 19);
            this.dgvStat.Name = "dgvStat";
            this.dgvStat.Size = new System.Drawing.Size(543, 256);
            this.dgvStat.TabIndex = 0;
            // 
            // clDB
            // 
            this.clDB.HeaderText = "";
            this.clDB.Name = "clDB";
            // 
            // acc
            // 
            this.acc.HeaderText = "Accounting";
            this.acc.Name = "acc";
            // 
            // age
            // 
            this.age.HeaderText = "Agent";
            this.age.Name = "age";
            // 
            // agw
            // 
            this.agw.HeaderText = "Agent Wap";
            this.agw.Name = "agw";
            // 
            // aff
            // 
            this.aff.HeaderText = "Affliate";
            this.aff.Name = "aff";
            // 
            // hung
            // 
            this.hung.HeaderText = "hung";
            this.hung.Name = "hung";
            // 
            // prgbScript
            // 
            this.prgbScript.Location = new System.Drawing.Point(3, 515);
            this.prgbScript.Name = "prgbScript";
            this.prgbScript.Size = new System.Drawing.Size(882, 23);
            this.prgbScript.TabIndex = 27;
            this.prgbScript.Visible = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.rbtnRestore);
            this.groupBox5.Controls.Add(this.rbtnScript);
            this.groupBox5.Location = new System.Drawing.Point(522, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(102, 80);
            this.groupBox5.TabIndex = 25;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Action";
            // 
            // rbtnRestore
            // 
            this.rbtnRestore.AutoSize = true;
            this.rbtnRestore.Location = new System.Drawing.Point(8, 47);
            this.rbtnRestore.Name = "rbtnRestore";
            this.rbtnRestore.Size = new System.Drawing.Size(62, 17);
            this.rbtnRestore.TabIndex = 1;
            this.rbtnRestore.TabStop = true;
            this.rbtnRestore.Text = "Restore";
            this.rbtnRestore.UseVisualStyleBackColor = true;
            this.rbtnRestore.CheckedChanged += new System.EventHandler(this.rbtnScript_CheckedChanged);
            // 
            // rbtnScript
            // 
            this.rbtnScript.AutoSize = true;
            this.rbtnScript.Checked = true;
            this.rbtnScript.Location = new System.Drawing.Point(8, 24);
            this.rbtnScript.Name = "rbtnScript";
            this.rbtnScript.Size = new System.Drawing.Size(52, 17);
            this.rbtnScript.TabIndex = 0;
            this.rbtnScript.TabStop = true;
            this.rbtnScript.Text = "Script";
            this.rbtnScript.UseVisualStyleBackColor = true;
            this.rbtnScript.CheckedChanged += new System.EventHandler(this.rbtnScript_CheckedChanged);
            // 
            // grpControl
            // 
            this.grpControl.Controls.Add(this.txtDesc);
            this.grpControl.Controls.Add(this.label1);
            this.grpControl.Controls.Add(this.btnBrowserFolder);
            this.grpControl.Controls.Add(this.txtScriptTo);
            this.grpControl.Controls.Add(this.label7);
            this.grpControl.Location = new System.Drawing.Point(247, 92);
            this.grpControl.Name = "grpControl";
            this.grpControl.Size = new System.Drawing.Size(409, 100);
            this.grpControl.TabIndex = 24;
            this.grpControl.TabStop = false;
            this.grpControl.Text = "Control";
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(100, 57);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(243, 20);
            this.txtDesc.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Description";
            // 
            // btnBrowserFolder
            // 
            this.btnBrowserFolder.Location = new System.Drawing.Point(351, 20);
            this.btnBrowserFolder.Name = "btnBrowserFolder";
            this.btnBrowserFolder.Size = new System.Drawing.Size(37, 23);
            this.btnBrowserFolder.TabIndex = 29;
            this.btnBrowserFolder.Text = "...";
            this.btnBrowserFolder.UseVisualStyleBackColor = true;
            this.btnBrowserFolder.Click += new System.EventHandler(this.btnBrowserFolder_Click);
            // 
            // txtScriptTo
            // 
            this.txtScriptTo.Location = new System.Drawing.Point(99, 22);
            this.txtScriptTo.Name = "txtScriptTo";
            this.txtScriptTo.ReadOnly = true;
            this.txtScriptTo.Size = new System.Drawing.Size(244, 20);
            this.txtScriptTo.TabIndex = 28;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(34, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "Script to";
            // 
            // btnScript
            // 
            this.btnScript.Location = new System.Drawing.Point(655, 486);
            this.btnScript.Name = "btnScript";
            this.btnScript.Size = new System.Drawing.Size(75, 23);
            this.btnScript.TabIndex = 22;
            this.btnScript.Text = "Script";
            this.btnScript.UseVisualStyleBackColor = true;
            this.btnScript.Click += new System.EventHandler(this.btnScript_Click);
            // 
            // grpSystem
            // 
            this.grpSystem.Controls.Add(this.trwSystem);
            this.grpSystem.Location = new System.Drawing.Point(496, 198);
            this.grpSystem.Name = "grpSystem";
            this.grpSystem.Size = new System.Drawing.Size(234, 205);
            this.grpSystem.TabIndex = 21;
            this.grpSystem.TabStop = false;
            this.grpSystem.Text = "System";
            // 
            // trwSystem
            // 
            this.trwSystem.CheckBoxes = true;
            this.trwSystem.Location = new System.Drawing.Point(9, 19);
            this.trwSystem.Name = "trwSystem";
            treeNode1.Name = "acc_";
            treeNode1.Text = "Accounting";
            treeNode2.Name = "age_";
            treeNode2.Text = "Agent";
            treeNode3.Name = "agw_";
            treeNode3.Text = "Agent Wap";
            treeNode4.Name = "aff_";
            treeNode4.Text = "Affiliate";
            treeNode5.Name = "hung_";
            treeNode5.Text = "Hung Test";
            treeNode6.Name = "tnodeAll";
            treeNode6.Text = "All";
            this.trwSystem.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode6});
            this.trwSystem.Size = new System.Drawing.Size(216, 180);
            this.trwSystem.TabIndex = 0;
            this.trwSystem.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtPassword);
            this.groupBox3.Controls.Add(this.txtUserName);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(303, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 80);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "DB Account";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(73, 48);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(120, 20);
            this.txtPassword.TabIndex = 3;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(73, 21);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(120, 20);
            this.txtUserName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "UserName";
            // 
            // grpDatabase
            // 
            this.grpDatabase.Controls.Add(this.trwDB);
            this.grpDatabase.Location = new System.Drawing.Point(191, 198);
            this.grpDatabase.Name = "grpDatabase";
            this.grpDatabase.Size = new System.Drawing.Size(275, 205);
            this.grpDatabase.TabIndex = 0;
            this.grpDatabase.TabStop = false;
            this.grpDatabase.Text = "Database";
            // 
            // trwDB
            // 
            this.trwDB.CheckBoxes = true;
            this.trwDB.Location = new System.Drawing.Point(15, 19);
            this.trwDB.Name = "trwDB";
            treeNode7.Name = "tnodeDB02_BODB02";
            treeNode7.Text = "BODB02";
            treeNode8.Name = "tnodeDB02";
            treeNode8.Text = "DB02";
            treeNode9.Name = "tnodeDB03_OnUser";
            treeNode9.Text = "On_User";
            treeNode10.Name = "tnodeDB03_BODB02";
            treeNode10.Text = "BODB02";
            treeNode11.Name = "tnodeDB03";
            treeNode11.Text = "DB03";
            treeNode12.Name = "tnodeDB05_BODB02";
            treeNode12.Text = "BODB02";
            treeNode13.Name = "tnodeDB05_Aff";
            treeNode13.Text = "Affiliate_12bet";
            treeNode14.Name = "tnodeDB05";
            treeNode14.Text = "DB05";
            treeNode15.Name = "tnodeDB06_BODB02";
            treeNode15.Text = "BODB02";
            treeNode16.Name = "tnodeDB06";
            treeNode16.Text = "DB06";
            treeNode17.Name = "tnodeAll";
            treeNode17.Text = "All";
            this.trwDB.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode17});
            this.trwDB.Size = new System.Drawing.Size(245, 180);
            this.trwDB.TabIndex = 0;
            this.trwDB.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
            // 
            // buttonZoom
            // 
            this.buttonZoom.BackgroundImage = global::DbDeployer.Properties.Resources.fullscreen;
            this.buttonZoom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonZoom.Location = new System.Drawing.Point(596, 466);
            this.buttonZoom.Name = "buttonZoom";
            this.buttonZoom.Size = new System.Drawing.Size(23, 23);
            this.buttonZoom.TabIndex = 32;
            this.buttonZoom.UseVisualStyleBackColor = true;
            this.buttonZoom.Click += new System.EventHandler(this.buttonZoom_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Accounting";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Agent";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Agent Wap";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Affliate";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "hung";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // ucMNLBckup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonZoom);
            this.Controls.Add(this.rtxtOutput);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.grpStatistic);
            this.Controls.Add(this.prgbScript);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.grpControl);
            this.Controls.Add(this.btnScript);
            this.Controls.Add(this.grpSystem);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.grpDatabase);
            this.Name = "ucMNLBckup";
            this.Load += new System.EventHandler(this.ucMNLBckup_Load);
            this.grpStatistic.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStat)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.grpControl.ResumeLayout(false);
            this.grpControl.PerformLayout();
            this.grpSystem.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.grpDatabase.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpDatabase;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grpSystem;
        private System.Windows.Forms.Button btnScript;
        private System.Windows.Forms.TreeView trwDB;
        private System.Windows.Forms.GroupBox grpControl;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowserFolder;
        private System.Windows.Forms.TextBox txtScriptTo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TreeView trwSystem;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton rbtnRestore;
        private System.Windows.Forms.RadioButton rbtnScript;
        private System.Windows.Forms.ProgressBar prgbScript;
        private System.Windows.Forms.GroupBox grpStatistic;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.DataGridView dgvStat;
        private System.Windows.Forms.Button btnOpenConfig;
        private System.Windows.Forms.DataGridViewTextBoxColumn clDB;
        private System.Windows.Forms.DataGridViewTextBoxColumn acc;
        private System.Windows.Forms.DataGridViewTextBoxColumn age;
        private System.Windows.Forms.DataGridViewTextBoxColumn agw;
        private System.Windows.Forms.DataGridViewTextBoxColumn aff;
        private System.Windows.Forms.DataGridViewTextBoxColumn hung;
        private FastColoredTextBoxNS.FastColoredTextBox rtxtOutput;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.Button buttonZoom;
    }
}
