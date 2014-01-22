namespace DbDeployer.Core.Utils
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using FastColoredTextBoxNS;

    public static class UIUtility
    {
        public static DataGridView RemoveDuplicateRows(DataGridView dgv, int[] columnIndice)
        {
            List<string> list = new List<string>();
            for (int i = dgv.Rows.Count - 2; i >= 0; i--)
            {
                string key = string.Empty;
                for (int j = 0; j < columnIndice.Length; j++)
                {
                    key += dgv[columnIndice[j], i].Value.ToString().Trim();
                }
                if (list.Contains(key))
                {
                    dgv.Rows.RemoveAt(i);
                }
                else
                {
                    list.Add(key);
                }
            }
            return dgv;
        }

        public static void AppendColoredText(FastColoredTextBox RichTextBox, string Text, Color Color)
        {
            RichTextBox.AppendText(Text);
        }
    }
}
