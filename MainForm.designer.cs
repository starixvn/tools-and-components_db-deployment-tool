namespace DbDeployer
{
    partial class MainForm
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabScript = new System.Windows.Forms.TabPage();
            this.tabDeploy = new System.Windows.Forms.TabPage();
            this.tabRollback = new System.Windows.Forms.TabPage();
            this.tabMNLBckup = new System.Windows.Forms.TabPage();
            this.tabSetting = new System.Windows.Forms.TabPage();
            this.tabMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabScript);
            this.tabMain.Controls.Add(this.tabDeploy);
            this.tabMain.Controls.Add(this.tabRollback);
            this.tabMain.Controls.Add(this.tabMNLBckup);
            this.tabMain.Controls.Add(this.tabSetting);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(901, 593);
            this.tabMain.TabIndex = 0;
            // 
            // tabScript
            // 
            this.tabScript.Location = new System.Drawing.Point(4, 22);
            this.tabScript.Name = "tabScript";
            this.tabScript.Padding = new System.Windows.Forms.Padding(3);
            this.tabScript.Size = new System.Drawing.Size(893, 567);
            this.tabScript.TabIndex = 0;
            this.tabScript.Text = "Script";
            this.tabScript.UseVisualStyleBackColor = true;
            // 
            // tabDeploy
            // 
            this.tabDeploy.Location = new System.Drawing.Point(4, 22);
            this.tabDeploy.Name = "tabDeploy";
            this.tabDeploy.Padding = new System.Windows.Forms.Padding(3);
            this.tabDeploy.Size = new System.Drawing.Size(893, 567);
            this.tabDeploy.TabIndex = 1;
            this.tabDeploy.Text = "Deploy";
            this.tabDeploy.UseVisualStyleBackColor = true;
            // 
            // tabRollback
            // 
            this.tabRollback.Location = new System.Drawing.Point(4, 22);
            this.tabRollback.Name = "tabRollback";
            this.tabRollback.Padding = new System.Windows.Forms.Padding(3);
            this.tabRollback.Size = new System.Drawing.Size(893, 567);
            this.tabRollback.TabIndex = 2;
            this.tabRollback.Text = "Rollback";
            this.tabRollback.UseVisualStyleBackColor = true;
            // 
            // tabMNLBckup
            // 
            this.tabMNLBckup.Location = new System.Drawing.Point(4, 22);
            this.tabMNLBckup.Name = "tabMNLBckup";
            this.tabMNLBckup.Padding = new System.Windows.Forms.Padding(3);
            this.tabMNLBckup.Size = new System.Drawing.Size(893, 567);
            this.tabMNLBckup.TabIndex = 3;
            this.tabMNLBckup.Text = "MNL Back Up";
            this.tabMNLBckup.UseVisualStyleBackColor = true;
            // 
            // tabSetting
            // 
            this.tabSetting.Location = new System.Drawing.Point(4, 22);
            this.tabSetting.Name = "tabSetting";
            this.tabSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tabSetting.Size = new System.Drawing.Size(893, 567);
            this.tabSetting.TabIndex = 4;
            this.tabSetting.Text = "Setting";
            this.tabSetting.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 593);
            this.Controls.Add(this.tabMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SP & Function Deployment v4.0";
            this.Load += new System.EventHandler(this.SPDeployment_Load);
            this.tabMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabSetting;
        private System.Windows.Forms.TabPage tabScript;
        private System.Windows.Forms.TabPage tabDeploy;
        private System.Windows.Forms.TabPage tabRollback;
        private System.Windows.Forms.TabPage tabMNLBckup;

    }
}