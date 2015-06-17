namespace DbDeployer.UserControls
{
    partial class ucScript
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
            this.pnl_Action = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnBrowserFolder = new System.Windows.Forms.Button();
            this.txtScriptTo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
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
            this.labelStatus = new System.Windows.Forms.Label();
            this.prgbScript = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_Input = new System.Windows.Forms.Button();
            this.btnOpenExcel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dgvSPConfig = new System.Windows.Forms.DataGridView();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Choose = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ServerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DBName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SPName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsFunction = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnl_Action.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSPConfig)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_Action
            // 
            this.pnl_Action.Controls.Add(this.groupBox2);
            this.pnl_Action.Controls.Add(this.groupBox3);
            this.pnl_Action.Controls.Add(this.groupBox4);
            this.pnl_Action.Location = new System.Drawing.Point(0, 201);
            this.pnl_Action.Name = "pnl_Action";
            this.pnl_Action.Size = new System.Drawing.Size(892, 304);
            this.pnl_Action.TabIndex = 23;
            this.pnl_Action.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtDesc);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnGenerate);
            this.groupBox2.Controls.Add(this.btnPreview);
            this.groupBox2.Controls.Add(this.btnBrowserFolder);
            this.groupBox2.Controls.Add(this.txtScriptTo);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(486, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(402, 90);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Control";
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(99, 41);
            this.txtDesc.MaxLength = 50;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(214, 20);
            this.txtDesc.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Description";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Enabled = false;
            this.btnGenerate.Location = new System.Drawing.Point(319, 64);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 31;
            this.btnGenerate.Text = "&Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.Enabled = false;
            this.btnPreview.Location = new System.Drawing.Point(238, 64);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(75, 23);
            this.btnPreview.TabIndex = 30;
            this.btnPreview.Text = "&Preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnBrowserFolder
            // 
            this.btnBrowserFolder.Enabled = false;
            this.btnBrowserFolder.Location = new System.Drawing.Point(319, 14);
            this.btnBrowserFolder.Name = "btnBrowserFolder";
            this.btnBrowserFolder.Size = new System.Drawing.Size(72, 23);
            this.btnBrowserFolder.TabIndex = 29;
            this.btnBrowserFolder.Text = "...";
            this.btnBrowserFolder.UseVisualStyleBackColor = true;
            this.btnBrowserFolder.Click += new System.EventHandler(this.btnBrowserFolder_Click);
            // 
            // txtScriptTo
            // 
            this.txtScriptTo.Location = new System.Drawing.Point(98, 16);
            this.txtScriptTo.Name = "txtScriptTo";
            this.txtScriptTo.ReadOnly = true;
            this.txtScriptTo.Size = new System.Drawing.Size(215, 20);
            this.txtScriptTo.TabIndex = 28;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(33, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "Script to";
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
            this.groupBox3.Location = new System.Drawing.Point(3, 8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(476, 90);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Database Information";
            // 
            // chk_SaveUserName
            // 
            this.chk_SaveUserName.AutoSize = true;
            this.chk_SaveUserName.Checked = true;
            this.chk_SaveUserName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_SaveUserName.Location = new System.Drawing.Point(71, 71);
            this.chk_SaveUserName.Name = "chk_SaveUserName";
            this.chk_SaveUserName.Size = new System.Drawing.Size(126, 17);
            this.chk_SaveUserName.TabIndex = 16;
            this.chk_SaveUserName.Text = "Remember username";
            this.chk_SaveUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_SaveUserName.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(267, 47);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Server";
            // 
            // cbbSourceServer
            // 
            this.cbbSourceServer.Enabled = false;
            this.cbbSourceServer.FormattingEnabled = true;
            this.cbbSourceServer.Location = new System.Drawing.Point(350, 43);
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
            this.chkAllServers.Location = new System.Drawing.Point(270, 19);
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
            this.label3.Location = new System.Drawing.Point(6, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(71, 45);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(164, 20);
            this.txtPassword.TabIndex = 3;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(71, 19);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(164, 20);
            this.txtUserName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Username";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.buttonZoom);
            this.groupBox4.Controls.Add(this.rtxtOutput);
            this.groupBox4.Location = new System.Drawing.Point(3, 101);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(885, 212);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Output";
            // 
            // buttonZoom
            // 
            this.buttonZoom.BackgroundImage = global::DbDeployer.Properties.Resources.fullscreen;
            this.buttonZoom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonZoom.Location = new System.Drawing.Point(829, 163);
            this.buttonZoom.Name = "buttonZoom";
            this.buttonZoom.Size = new System.Drawing.Size(23, 23);
            this.buttonZoom.TabIndex = 3;
            this.buttonZoom.UseVisualStyleBackColor = true;
            this.buttonZoom.Click += new System.EventHandler(this.buttonZoom_Click);
            // 
            // rtxtOutput
            // 
            this.rtxtOutput.AutoScrollMinSize = new System.Drawing.Size(0, 16);
            this.rtxtOutput.BackBrush = null;
            this.rtxtOutput.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.rtxtOutput.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.rtxtOutput.Font = new System.Drawing.Font("Courier New", 11F);
            this.rtxtOutput.Language = FastColoredTextBoxNS.Language.SQL;
            this.rtxtOutput.Location = new System.Drawing.Point(8, 19);
            this.rtxtOutput.Name = "rtxtOutput";
            this.rtxtOutput.Paddings = new System.Windows.Forms.Padding(0);
            this.rtxtOutput.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.rtxtOutput.Size = new System.Drawing.Size(871, 187);
            this.rtxtOutput.TabIndex = 0;
            this.rtxtOutput.WordWrap = true;
            this.rtxtOutput.DoubleClick += new System.EventHandler(this.rtxtOutput_DoubleClick);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(7, 546);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(0, 13);
            this.labelStatus.TabIndex = 22;
            // 
            // prgbScript
            // 
            this.prgbScript.Location = new System.Drawing.Point(7, 513);
            this.prgbScript.Name = "prgbScript";
            this.prgbScript.Size = new System.Drawing.Size(882, 23);
            this.prgbScript.TabIndex = 21;
            this.prgbScript.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_Input);
            this.groupBox1.Controls.Add(this.btnOpenExcel);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.dgvSPConfig);
            this.groupBox1.Location = new System.Drawing.Point(3, -2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(886, 200);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Script configuration";
            // 
            // btn_Input
            // 
            this.btn_Input.Location = new System.Drawing.Point(643, 173);
            this.btn_Input.Name = "btn_Input";
            this.btn_Input.Size = new System.Drawing.Size(75, 23);
            this.btn_Input.TabIndex = 3;
            this.btn_Input.Text = "&Input";
            this.btn_Input.UseVisualStyleBackColor = true;
            this.btn_Input.Visible = false;
            this.btn_Input.Click += new System.EventHandler(this.btn_Input_Click);
            // 
            // btnOpenExcel
            // 
            this.btnOpenExcel.Location = new System.Drawing.Point(724, 173);
            this.btnOpenExcel.Name = "btnOpenExcel";
            this.btnOpenExcel.Size = new System.Drawing.Size(75, 23);
            this.btnOpenExcel.TabIndex = 2;
            this.btnOpenExcel.Text = "&Browse";
            this.btnOpenExcel.UseVisualStyleBackColor = true;
            this.btnOpenExcel.Click += new System.EventHandler(this.btnOpenExcel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(805, 173);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dgvSPConfig
            // 
            this.dgvSPConfig.AllowUserToResizeColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSPConfig.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSPConfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSPConfig.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No,
            this.Choose,
            this.ServerName,
            this.DBName,
            this.SPName,
            this.IsFunction,
            this.IP});
            this.dgvSPConfig.Location = new System.Drawing.Point(4, 17);
            this.dgvSPConfig.Name = "dgvSPConfig";
            this.dgvSPConfig.ReadOnly = true;
            this.dgvSPConfig.Size = new System.Drawing.Size(876, 150);
            this.dgvSPConfig.TabIndex = 0;
            this.dgvSPConfig.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSPConfig_CellContentClick);
            this.dgvSPConfig.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSPConfig_CellEndEdit);
            this.dgvSPConfig.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvSPConfig_ColumnHeaderMouseClick);
            // 
            // No
            // 
            this.No.DataPropertyName = "No";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.No.DefaultCellStyle = dataGridViewCellStyle2;
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
            this.Choose.Width = 65;
            // 
            // ServerName
            // 
            this.ServerName.DataPropertyName = "ServerName";
            this.ServerName.HeaderText = "Server Name";
            this.ServerName.Name = "ServerName";
            this.ServerName.ReadOnly = true;
            // 
            // DBName
            // 
            this.DBName.DataPropertyName = "DBName";
            this.DBName.HeaderText = "DB Name";
            this.DBName.Name = "DBName";
            this.DBName.ReadOnly = true;
            // 
            // SPName
            // 
            this.SPName.DataPropertyName = "SPName";
            this.SPName.HeaderText = "SP Name";
            this.SPName.Name = "SPName";
            this.SPName.ReadOnly = true;
            this.SPName.Width = 323;
            // 
            // IsFunction
            // 
            this.IsFunction.DataPropertyName = "IsFunction";
            this.IsFunction.HeaderText = "Function";
            this.IsFunction.Name = "IsFunction";
            this.IsFunction.ReadOnly = true;
            this.IsFunction.Width = 65;
            // 
            // IP
            // 
            this.IP.DataPropertyName = "IP";
            this.IP.HeaderText = "Server IP";
            this.IP.Name = "IP";
            this.IP.ReadOnly = true;
            this.IP.Width = 150;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "No.";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn1.HeaderText = "No";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 50;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.DataPropertyName = "Select";
            this.dataGridViewCheckBoxColumn1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.dataGridViewCheckBoxColumn1.HeaderText = "Check";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.ReadOnly = true;
            this.dataGridViewCheckBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewCheckBoxColumn1.Width = 50;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "ServerName";
            this.dataGridViewTextBoxColumn2.HeaderText = "ServerName";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 150;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "DBName";
            this.dataGridViewTextBoxColumn3.HeaderText = "DBName";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 150;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "SPName";
            this.dataGridViewTextBoxColumn4.HeaderText = "SPName";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 283;
            // 
            // dataGridViewCheckBoxColumn2
            // 
            this.dataGridViewCheckBoxColumn2.DataPropertyName = "IsFunction";
            this.dataGridViewCheckBoxColumn2.HeaderText = "IsFunction";
            this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
            this.dataGridViewCheckBoxColumn2.ReadOnly = true;
            this.dataGridViewCheckBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewCheckBoxColumn2.Width = 80;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "IP";
            this.dataGridViewTextBoxColumn5.HeaderText = "IsFunction";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 150;
            // 
            // ucScript
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_Action);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.prgbScript);
            this.Controls.Add(this.groupBox1);
            this.Name = "ucScript";
            this.Size = new System.Drawing.Size(892, 567);
            this.Load += new System.EventHandler(this.ucScript_Load);
            this.pnl_Action.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSPConfig)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvSPConfig;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbbSourceServer;
        private System.Windows.Forms.CheckBox chkAllServers;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnBrowserFolder;
        private System.Windows.Forms.TextBox txtScriptTo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ProgressBar prgbScript;
        private System.Windows.Forms.Button btnOpenExcel;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.Label label1;
        private FastColoredTextBoxNS.FastColoredTextBox rtxtOutput;
        private System.Windows.Forms.Button buttonZoom;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
        private System.Windows.Forms.Panel pnl_Action;
        private System.Windows.Forms.CheckBox chk_SaveUserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Choose;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DBName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SPName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsFunction;
        private System.Windows.Forms.DataGridViewTextBoxColumn IP;
        private System.Windows.Forms.Button btn_Input;
    }
}
