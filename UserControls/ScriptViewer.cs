namespace DbDeployer.UserControls
{
    using System.Windows.Forms;

    public partial class ScriptViewer : Form
    {
        public ScriptViewer()
        {
            InitializeComponent();
        }
        public void UpdateScript(string script)
        {
            fastColoredTextBox.Text = script;
        }

        private void buttonCopyToClipBoard_Click(object sender, System.EventArgs e)
        {
            Clipboard.SetText(fastColoredTextBox.Text);
        }
    }
}
