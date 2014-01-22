using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DbDeployer.Core;

namespace DbDeployer
{
    public partial class InputSPList : Form
    {
        public static DataTable data = null;

        private const string _columnServerName = "ServerName";
        private const string _columnDBName = "DBName";
        private const string _columnSpName = "SPName";
        private const string _columnIsFunction = "IsFunction";

        public InputSPList()
        {
            data = new DataTable();
            InitializeComponent();
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            var isClose = true;
            var dataTable = new DataTable();
            try
            {
                if (this.rtbText.Text.Trim().Length > 0 && this.rtbText.Lines.Count() > 0)
                {
                    DataColumn columnserverName = new DataColumn(_columnServerName, Type.GetType("System.String"));
                    DataColumn columndbName = new DataColumn(_columnDBName, Type.GetType("System.String"));
                    DataColumn columnspName = new DataColumn(_columnSpName, Type.GetType("System.String"));
                    DataColumn columnIsFunction = new DataColumn(_columnIsFunction, Type.GetType("System.String"));

                    dataTable.Columns.Add(columnserverName);
                    dataTable.Columns.Add(columndbName);
                    dataTable.Columns.Add(columnspName);
                    dataTable.Columns.Add(columnIsFunction);

                    var lineText = string.Empty;
                    var serverName = string.Empty;
                    var dbName = string.Empty;
                    var spName = string.Empty;
                    var isSp = false;
                    var temp = string.Empty;
                    
                    for (int i = 0; i < this.rtbText.Lines.Count(); i++)
                    {
                        lineText = rtbText.Lines[i];
                        if (!string.IsNullOrWhiteSpace(lineText))
                        {
                            if (lineText.IndexOf(".") > -1)
                            {
                                var arrContent = lineText.Split('.');
                                if (arrContent.Length >= 3)
                                {
                                    serverName = arrContent[0];
                                    dbName = arrContent[1];
                                    temp = arrContent[2];

                                    if (!string.IsNullOrWhiteSpace(temp) && temp.IndexOf(" ") > -1)
                                    {
                                        var arrTemp = temp.Split(' ');
                                        spName = arrTemp[0];
                                        if (arrTemp[1].IndexOf(" ") > -1)
                                        {
                                            var arrTemp1 = arrTemp[1].Split(' ');

                                            if (arrTemp1[1].Equals("S") || arrTemp1[1].Equals("s") || arrTemp1[1].Equals("F") || arrTemp1[1].Equals("f"))
                                            {
                                                isSp = arrTemp1[1].Equals("S") || arrTemp1[1].Equals("s");
                                                DataRow row = dataTable.NewRow();
                                                row[_columnServerName] = serverName;
                                                row[_columnDBName] = dbName;
                                                row[_columnSpName] = spName;
                                                row[_columnIsFunction] = isSp ? 0 : 1;

                                                dataTable.Rows.Add(row);
                                            }
                                            else
                                            {
                                                MessageBox.Show("Invalid value at row " + i);
                                                isClose = false;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            if (arrTemp[1].Equals("S") || arrTemp[1].Equals("s") || arrTemp[1].Equals("F") || arrTemp[1].Equals("f"))
                                            {
                                                isSp = arrTemp[1].Equals("S") || arrTemp[1].Equals("s");
                                                DataRow row = dataTable.NewRow();
                                                row[_columnServerName] = serverName;
                                                row[_columnDBName] = dbName;
                                                row[_columnSpName] = spName;
                                                row[_columnIsFunction] = isSp ? 0 : 1;

                                                dataTable.Rows.Add(row);
                                            }
                                            else
                                            {
                                                MessageBox.Show("Invalid value at row " + i);
                                                isClose = false;
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Invalid value at row " + i);
                                        isClose = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Invalid value at row " + i);
                                    isClose = false;
                                    break;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Invalid value at row " + i);
                                isClose = false;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please input value!");
                    isClose = false;
                }
            }
            catch
            {
            }

            if (isClose)
            {
                data = dataTable;
                this.Close();
            }
        }

        private void rtbText_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (!string.IsNullOrWhiteSpace(rtbText.SelectedText))
                {
                    contextMenu.Items["copyToolStripMenuItem"].Enabled = true;
                    contextMenu.Items["cutToolStripMenuItem"].Enabled = true;
                }
                else
                {
                    contextMenu.Items["copyToolStripMenuItem"].Enabled = false;
                    contextMenu.Items["cutToolStripMenuItem"].Enabled = false;
                }

                if (Clipboard.ContainsText())
                {
                    contextMenu.Items["pasteToolStripMenuItem"].Enabled = true;
                }
                else
                {
                    contextMenu.Items["pasteToolStripMenuItem"].Enabled = false;
                }

                contextMenu.Show(Cursor.Position);
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(rtbText.SelectedText);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbText.Paste(DataFormats.GetFormat("Text"));
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(rtbText.SelectedText);
            rtbText.SelectedText = string.Empty;
        }
    }
}
