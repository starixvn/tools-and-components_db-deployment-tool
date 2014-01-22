namespace DbDeployer.UserControls
{
    partial class ucRollback
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnl_Action = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonZoom = new System.Windows.Forms.Button();
            this.rtxtOutput = new FastColoredTextBoxNS.FastColoredTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnRollback = new System.Windows.Forms.Button();
            this.chk_SaveUserName = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbbSourceServer = new System.Windows.Forms.ComboBox();
            this.chkAllServers = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCheckSpExist = new System.Windows.Forms.Button();
            this.btnOpenConfig = new System.Windows.Forms.Button();
            this.dgvSPList = new System.Windows.Forms.DataGridView();
            this.prgbScript = new System.Windows.Forms.ProgressBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbtRelease = new System.Windows.Forms.RadioButton();
            this.rbtTest = new System.Windows.Forms.RadioButton();
            this.rbtPrerelease = new System.Windows.Forms.RadioButton();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Choose = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ServerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DBName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScriptName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsFunction = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnl_Action.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSPList)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_Action
            // 
            this.pnl_Action.Controls.Add(this.groupBox4);
            this.pnl_Action.Controls.Add(this.groupBox3);
            this.pnl_Action.Location = new System.Drawing.Point(0, 237);
            this.pnl_Action.Name = "pnl_Action";
            this.pnl_Action.Size = new System.Drawing.Size(892, 273);
            this.pnl_Action.TabIndex = 30;
            this.pnl_Action.Visible = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.buttonZoom);
            this.groupBox4.Controls.Add(this.rtxtOutput);
            this.groupBox4.Location = new System.Drawing.Point(6, 62);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(880, 209);
            this.groupBox4.TabIndex = 26;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Output";
            // 
            // buttonZoom
            // 
            this.buttonZoom.BackgroundImage = global::DbDeployer.Properties.Resources.fullscreen;
            this.buttonZoom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonZoom.Location = new System.Drawing.Point(830, 180);
            this.buttonZoom.Name = "buttonZoom";
            this.buttonZoom.Size = new System.Drawing.Size(23, 23);
            this.buttonZoom.TabIndex = 4;
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
            this.rtxtOutput.Size = new System.Drawing.Size(867, 184);
            this.rtxtOutput.TabIndex = 0;
            this.rtxtOutput.DoubleClick += new System.EventHandler(this.rtxtOutput_DoubleClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnRollback);
            this.groupBox3.Controls.Add(this.chk_SaveUserName);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.cbbSourceServer);
            this.groupBox3.Controls.Add(this.chkAllServers);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtPassword);
            this.groupBox3.Controls.Add(this.txtUserName);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(6, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(880, 58);
            this.groupBox3.TabIndex = 25;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Database information";
            // 
            // btnRollback
            // 
            this.btnRollback.Enabled = false;
            this.btnRollback.Location = new System.Drawing.Point(789, 17);
            this.btnRollback.Name = "btnRollback";
            this.btnRollback.Size = new System.Drawing.Size(75, 23);
            this.btnRollback.TabIndex = 16;
            this.btnRollback.Text = "Rollback";
            this.btnRollback.UseVisualStyleBackColor = true;
            this.btnRollback.Click += new System.EventHandler(this.btnRollback_Click);
            // 
            // chk_SaveUserName
            // 
            this.chk_SaveUserName.AutoSize = true;
            this.chk_SaveUserName.Checked = true;
            this.chk_SaveUserName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_SaveUserName.Location = new System.Drawing.Point(71, 39);
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
            this.label8.Location = new System.Drawing.Point(584, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "ServerName";
            // 
            // cbbSourceServer
            // 
            this.cbbSourceServer.Enabled = false;
            this.cbbSourceServer.FormattingEnabled = true;
            this.cbbSourceServer.Location = new System.Drawing.Point(655, 18);
            this.cbbSourceServer.Name = "cbbSourceServer";
            this.cbbSourceServer.Size = new System.Drawing.Size(121, 21);
            this.cbbSourceServer.TabIndex = 5;
            this.cbbSourceServer.SelectedIndexChanged += new System.EventHandler(this.cbbSourceServer_SelectedIndexChanged);
            // 
            // chkAllServers
            // 
            this.chkAllServers.AutoSize = true;
            this.chkAllServers.Checked = true;
            this.chkAllServers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllServers.Enabled = false;
            this.chkAllServers.Location = new System.Drawing.Point(497, 20);
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
            this.label3.Location = new System.Drawing.Point(256, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(320, 18);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(164, 20);
            this.txtPassword.TabIndex = 3;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(71, 18);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(164, 20);
            this.txtUserName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "UserName";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCheckSpExist);
            this.groupBox1.Controls.Add(this.btnOpenConfig);
            this.groupBox1.Controls.Add(this.dgvSPList);
            this.groupBox1.Location = new System.Drawing.Point(6, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(880, 193);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Script list";
            // 
            // btnCheckSpExist
            // 
            this.btnCheckSpExist.Enabled = false;
            this.btnCheckSpExist.Location = new System.Drawing.Point(801, 164);
            this.btnCheckSpExist.Name = "btnCheckSpExist";
            this.btnCheckSpExist.Size = new System.Drawing.Size(75, 23);
            this.btnCheckSpExist.TabIndex = 3;
            this.btnCheckSpExist.Text = "Check Exist";
            this.btnCheckSpExist.UseVisualStyleBackColor = true;
            this.btnCheckSpExist.Click += new System.EventHandler(this.btnCheckSpExist_Click);
            // 
            // btnOpenConfig
            // 
            this.btnOpenConfig.Location = new System.Drawing.Point(721, 164);
            this.btnOpenConfig.Name = "btnOpenConfig";
            this.btnOpenConfig.Size = new System.Drawing.Size(75, 23);
            this.btnOpenConfig.TabIndex = 2;
            this.btnOpenConfig.Text = "&Open";
            this.btnOpenConfig.UseVisualStyleBackColor = true;
            this.btnOpenConfig.Click += new System.EventHandler(this.btnOpenConfig_Click);
            // 
            // dgvSPList
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSPList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvSPList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSPList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No,
            this.Choose,
            this.ServerName,
            this.DBName,
            this.ScriptName,
            this.IsFunction,
            this.IP,
            this.FileName});
            this.dgvSPList.Location = new System.Drawing.Point(4, 18);
            this.dgvSPList.Name = "dgvSPList";
            this.dgvSPList.ReadOnly = true;
            this.dgvSPList.Size = new System.Drawing.Size(872, 140);
            this.dgvSPList.TabIndex = 0;
            this.dgvSPList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSPList_CellContentClick);
            this.dgvSPList.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvSPList_ColumnHeaderMouseClick);
            // 
            // prgbScript
            // 
            this.prgbScript.Location = new System.Drawing.Point(4, 512);
            this.prgbScript.Name = "prgbScript";
            this.prgbScript.Size = new System.Drawing.Size(880, 23);
            this.prgbScript.TabIndex = 27;
            this.prgbScript.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbtRelease);
            this.groupBox2.Controls.Add(this.rbtTest);
            this.groupBox2.Controls.Add(this.rbtPrerelease);
            this.groupBox2.Location = new System.Drawing.Point(117, -1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(651, 46);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Phase";
            // 
            // rbtRelease
            // 
            this.rbtRelease.AutoSize = true;
            this.rbtRelease.Location = new System.Drawing.Point(490, 17);
            this.rbtRelease.Name = "rbtRelease";
            this.rbtRelease.Size = new System.Drawing.Size(64, 17);
            this.rbtRelease.TabIndex = 2;
            this.rbtRelease.TabStop = true;
            this.rbtRelease.Text = "Release";
            this.rbtRelease.UseVisualStyleBackColor = true;
            this.rbtRelease.CheckedChanged += new System.EventHandler(this.rbtRelease_CheckedChanged);
            // 
            // rbtTest
            // 
            this.rbtTest.AutoSize = true;
            this.rbtTest.Location = new System.Drawing.Point(267, 17);
            this.rbtTest.Name = "rbtTest";
            this.rbtTest.Size = new System.Drawing.Size(46, 17);
            this.rbtTest.TabIndex = 1;
            this.rbtTest.TabStop = true;
            this.rbtTest.Text = "Test";
            this.rbtTest.UseVisualStyleBackColor = true;
            this.rbtTest.CheckedChanged += new System.EventHandler(this.rbtTest_CheckedChanged);
            // 
            // rbtPrerelease
            // 
            this.rbtPrerelease.AutoSize = true;
            this.rbtPrerelease.Checked = true;
            this.rbtPrerelease.Location = new System.Drawing.Point(65, 17);
            this.rbtPrerelease.Name = "rbtPrerelease";
            this.rbtPrerelease.Size = new System.Drawing.Size(78, 17);
            this.rbtPrerelease.TabIndex = 0;
            this.rbtPrerelease.TabStop = true;
            this.rbtPrerelease.Text = "Pre-release";
            this.rbtPrerelease.UseVisualStyleBackColor = true;
            this.rbtPrerelease.CheckedChanged += new System.EventHandler(this.rbtPrerelease_CheckedChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "No";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn1.HeaderText = "#";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 39;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.DataPropertyName = "Select";
            this.dataGridViewCheckBoxColumn1.HeaderText = "Select";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.ReadOnly = true;
            this.dataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewCheckBoxColumn1.Width = 70;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "ServerName";
            this.dataGridViewTextBoxColumn2.HeaderText = "ServerName";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "DBName";
            this.dataGridViewTextBoxColumn3.HeaderText = "DBName";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "SPName";
            this.dataGridViewTextBoxColumn4.HeaderText = "SPName";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn4.Width = 200;
            // 
            // dataGridViewCheckBoxColumn2
            // 
            this.dataGridViewCheckBoxColumn2.DataPropertyName = "IsFunction";
            this.dataGridViewCheckBoxColumn2.HeaderText = "Function";
            this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
            this.dataGridViewCheckBoxColumn2.ReadOnly = true;
            this.dataGridViewCheckBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewCheckBoxColumn2.Width = 65;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "IP";
            this.dataGridViewTextBoxColumn5.HeaderText = "Function";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn5.Width = 70;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "FileName";
            this.dataGridViewTextBoxColumn6.HeaderText = "ServerIP";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn6.Width = 150;
            // 
            // No
            // 
            this.No.DataPropertyName = "No";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.No.DefaultCellStyle = dataGridViewCellStyle8;
            this.No.HeaderText = "#";
            this.No.Name = "No";
            this.No.ReadOnly = true;
            this.No.Resizable = System.Windows.Forms.DataGridViewTriState.False;
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
            this.Choose.Width = 65;
            // 
            // ServerName
            // 
            this.ServerName.DataPropertyName = "ServerName";
            this.ServerName.HeaderText = "Server Name";
            this.ServerName.Name = "ServerName";
            this.ServerName.ReadOnly = true;
            this.ServerName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // DBName
            // 
            this.DBName.DataPropertyName = "DBName";
            this.DBName.HeaderText = "DB Name";
            this.DBName.Name = "DBName";
            this.DBName.ReadOnly = true;
            this.DBName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // ScriptName
            // 
            this.ScriptName.DataPropertyName = "ScriptName";
            this.ScriptName.HeaderText = "SP Name";
            this.ScriptName.Name = "ScriptName";
            this.ScriptName.ReadOnly = true;
            this.ScriptName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ScriptName.Width = 219;
            // 
            // IsFunction
            // 
            this.IsFunction.DataPropertyName = "IsFunction";
            this.IsFunction.HeaderText = "Function";
            this.IsFunction.Name = "IsFunction";
            this.IsFunction.ReadOnly = true;
            this.IsFunction.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.IsFunction.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.IsFunction.Width = 65;
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
            this.FileName.Width = 150;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "FileName";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Width = 150;
            // 
            // ucRollback
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.pnl_Action);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.prgbScript);
            this.Controls.Add(this.groupBox2);
            this.Name = "ucRollback";
            this.Load += new System.EventHandler(this.ucRollback_Load);
            this.pnl_Action.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSPList)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbbSourceServer;
        private System.Windows.Forms.CheckBox chkAllServers;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dgvSPList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCheckSpExist;
        private System.Windows.Forms.Button btnOpenConfig;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.ProgressBar prgbScript;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnRollback;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbtRelease;
        private System.Windows.Forms.RadioButton rbtTest;
        private System.Windows.Forms.RadioButton rbtPrerelease;
        private FastColoredTextBoxNS.FastColoredTextBox rtxtOutput;
        private System.Windows.Forms.Button buttonZoom;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
        private System.Windows.Forms.Panel pnl_Action;
        private System.Windows.Forms.CheckBox chk_SaveUserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Choose;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DBName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScriptName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsFunction;
        private System.Windows.Forms.DataGridViewTextBoxColumn IP;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
    }
}
