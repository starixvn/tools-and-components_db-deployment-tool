namespace DbDeployer
{
    using System;
    using System.Windows.Forms;
    using DbDeployer.UserControls;

    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void SPDeployment_Load(object sender, EventArgs e)
        {
            tabSetting.Controls.Add(new ucSetting());
            tabScript.Controls.Add(new ucScript());
            tabDeploy.Controls.Add(new ucDeployment());
            tabRollback.Controls.Add(new ucRollback());
            tabMNLBckup.Controls.Add(new ucMNLBckup());
        }
    }
}
