namespace DbDeployer.UserControls
{
    partial class ucSetting
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.gpbIpSetting = new System.Windows.Forms.GroupBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dgvIpConfig = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DBName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IpXpre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IpXtest = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IpRelease = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Standby = new System.Windows.Forms.DataGridViewLinkColumn();
            this.gpbIpSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIpConfig)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(332, 496);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(228, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "© by Nexcel Solutions 2012. All rights reserved";
            // 
            // gpbIpSetting
            // 
            this.gpbIpSetting.Controls.Add(this.btnReset);
            this.gpbIpSetting.Controls.Add(this.btnSave);
            this.gpbIpSetting.Controls.Add(this.dgvIpConfig);
            this.gpbIpSetting.Location = new System.Drawing.Point(16, 13);
            this.gpbIpSetting.Name = "gpbIpSetting";
            this.gpbIpSetting.Size = new System.Drawing.Size(860, 468);
            this.gpbIpSetting.TabIndex = 0;
            this.gpbIpSetting.TabStop = false;
            this.gpbIpSetting.Text = "IP Setting";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(778, 430);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "&Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(692, 430);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dgvIpConfig
            // 
            this.dgvIpConfig.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvIpConfig.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvIpConfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIpConfig.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ServerName,
            this.DBName,
            this.Ip,
            this.IpXpre,
            this.IpXtest,
            this.IpRelease,
            this.Standby});
            this.dgvIpConfig.Location = new System.Drawing.Point(6, 19);
            this.dgvIpConfig.Name = "dgvIpConfig";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvIpConfig.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvIpConfig.Size = new System.Drawing.Size(847, 399);
            this.dgvIpConfig.TabIndex = 0;
            this.dgvIpConfig.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvIpConfig_CellClick);
            this.dgvIpConfig.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvIpConfig_CellEndEdit);
            this.dgvIpConfig.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvIpConfig_CellFormatting);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Server Name";
            this.dataGridViewTextBoxColumn1.HeaderText = "Server Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "IP";
            this.dataGridViewTextBoxColumn2.HeaderText = "IP";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "IP Pre-release";
            this.dataGridViewTextBoxColumn3.HeaderText = "IP Pre-release";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 120;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "IP Test";
            this.dataGridViewTextBoxColumn4.HeaderText = "IP Test";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn4.Width = 120;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "IP Release";
            this.dataGridViewTextBoxColumn5.HeaderText = "IP Release";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn5.Width = 120;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Standby servers";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn6.HeaderText = "Standby servers";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn6.Width = 118;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Standby_servers";
            this.dataGridViewTextBoxColumn7.HeaderText = "Standby servers";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn7.Width = 124;
            // 
            // ServerName
            // 
            this.ServerName.DataPropertyName = "ServerName";
            this.ServerName.HeaderText = "Server Name";
            this.ServerName.Name = "ServerName";
            // 
            // DBName
            // 
            this.DBName.DataPropertyName = "DbName";
            this.DBName.HeaderText = "DB Name";
            this.DBName.Name = "DBName";
            this.DBName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Ip
            // 
            this.Ip.DataPropertyName = "Ip";
            this.Ip.HeaderText = "IP";
            this.Ip.Name = "Ip";
            this.Ip.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Ip.Width = 120;
            // 
            // IpXpre
            // 
            this.IpXpre.DataPropertyName = "IpXpre";
            this.IpXpre.HeaderText = "IP Pre-release";
            this.IpXpre.Name = "IpXpre";
            this.IpXpre.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.IpXpre.Width = 120;
            // 
            // IpXtest
            // 
            this.IpXtest.DataPropertyName = "IpXtest";
            this.IpXtest.HeaderText = "IP Test/UAT";
            this.IpXtest.Name = "IpXtest";
            this.IpXtest.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.IpXtest.Width = 120;
            // 
            // IpRelease
            // 
            this.IpRelease.DataPropertyName = "IpRelease";
            this.IpRelease.HeaderText = "IP Release";
            this.IpRelease.Name = "IpRelease";
            this.IpRelease.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.IpRelease.Width = 120;
            // 
            // Standby
            // 
            this.Standby.DataPropertyName = "Standby";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Standby.DefaultCellStyle = dataGridViewCellStyle2;
            this.Standby.HeaderText = "Standby DBs";
            this.Standby.Name = "Standby";
            this.Standby.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Standby.Width = 124;
            // 
            // ucSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gpbIpSetting);
            this.Name = "ucSetting";
            this.Load += new System.EventHandler(this.ucSetting_Load);
            this.gpbIpSetting.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvIpConfig)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gpbIpSetting;
        private System.Windows.Forms.DataGridView dgvIpConfig;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DBName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ip;
        private System.Windows.Forms.DataGridViewTextBoxColumn IpXpre;
        private System.Windows.Forms.DataGridViewTextBoxColumn IpXtest;
        private System.Windows.Forms.DataGridViewTextBoxColumn IpRelease;
        private System.Windows.Forms.DataGridViewLinkColumn Standby;
    }
}
