namespace DbDeployer.UserControls
{
    partial class ScriptViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptViewer));
            this.fastColoredTextBox = new FastColoredTextBoxNS.FastColoredTextBox();
            this.SuspendLayout();
            // 
            // fastColoredTextBox
            // 
            this.fastColoredTextBox.AutoScrollMinSize = new System.Drawing.Size(31, 18);
            this.fastColoredTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fastColoredTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastColoredTextBox.Font = new System.Drawing.Font("Courier New", 12F);
            this.fastColoredTextBox.Language = FastColoredTextBoxNS.Language.SQL;
            this.fastColoredTextBox.Location = new System.Drawing.Point(0, 0);
            this.fastColoredTextBox.Name = "fastColoredTextBox";
            this.fastColoredTextBox.Size = new System.Drawing.Size(944, 482);
            this.fastColoredTextBox.TabIndex = 0;
            // 
            // ScriptViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 482);
            this.Controls.Add(this.fastColoredTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ScriptViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Script Viewer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private FastColoredTextBoxNS.FastColoredTextBox fastColoredTextBox;
    }
}