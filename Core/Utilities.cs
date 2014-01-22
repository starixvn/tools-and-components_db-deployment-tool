namespace DbDeployer.Core
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using FastColoredTextBoxNS;
    using GemBox.Spreadsheet;
    using NativeExcel;



    public static class Utilities
    {
        public static string RemoveSquareBracket(string input)
        {
            if (input.IndexOf('[') == -1)
                return input;
            return input.Substring(1, input.IndexOf(']') - 1);
        }

        public static string ReplaceStringInstance(string OrigString, string FindString, string ReplaceWith, int Instance, bool CaseInsensitive)
        {
            if (Instance == -1)
                return ReplaceString(OrigString, FindString, ReplaceWith, CaseInsensitive);
            int at1 = 0;
            for (int x = 0; x < Instance; x++)
            {
                if (CaseInsensitive)
                    at1 = OrigString.IndexOf(FindString, at1, OrigString.Length - at1, StringComparison.OrdinalIgnoreCase);
                else
                    at1 = OrigString.IndexOf(FindString, at1);
                if (at1 == -1)
                    return OrigString;
                if (x < Instance - 1)
                    at1 += FindString.Length;
            }
            return OrigString.Substring(0, at1) + ReplaceWith + OrigString.Substring(at1 + FindString.Length);
        }

        public static string ReplaceString(string OrigString, string FindString, string ReplaceString, bool CaseInsensitive)
        {
            int at1 = 0;
            while (true)
            {
                if (CaseInsensitive)
                    at1 = OrigString.IndexOf(FindString, at1, OrigString.Length - at1, StringComparison.OrdinalIgnoreCase);
                else
                    at1 = OrigString.IndexOf(FindString, at1);
                if (at1 == -1)
                    break;
                OrigString = OrigString.Substring(0, at1) + ReplaceString + OrigString.Substring(at1 + FindString.Length);
                at1 += ReplaceString.Length;
            }
            return OrigString;
        }

        public static void WriteLog(string logMessage, string filename)
        {
            StreamWriter w = File.AppendText(filename);
            w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
            w.WriteLine(" {0}", logMessage);
            w.WriteLine("-------------------------------");
            // Update the underlying file.
            w.Flush();
            w.Close();
        }

        public static void WriteToFile(string content, string filename)
        {
            File.WriteAllText(filename, content);
        }

        public static string DumpLog(string filename)
        {
            // While not at the end of the file, read and write lines.
            if (File.Exists(filename))
            {
                StreamReader r = File.OpenText(filename);
                string line = "";
                string log = "";
                while ((line = r.ReadLine()) != null)
                {
                    log += line + "\r\n";
                }
                r.Close();
                return log;
            }
            else
            {
                return "Cannot read the file: " + filename;
            }
        }

        public static void TryToDisplayGeneratedFile(string fileName)
        {
            try
            {
                System.Diagnostics.Process.Start(fileName);
            }
            catch (Exception)
            {
                Console.WriteLine(fileName + " created in application folder.");
            }
        }

        public static void AppendColoredText(FastColoredTextBox RichTextBox, string Text, Color Color)
        {
            //int StartIndex = RichTextBox.TextLength;
            RichTextBox.AppendText(Text);
            //int EndIndex = RichTextBox.TextLength;
            //RichTextBox.Select(StartIndex, EndIndex - StartIndex);
            //RichTextBox.SelectionColor = Color;
        }

        public static string CreateMap(string path)
        {
            var oNetDrive = new NetworkDrive();
            try
            {
                //set propertys
                oNetDrive.Force = true;
                oNetDrive.Persistent = true;
                oNetDrive.LocalDrive = "X:";
                oNetDrive.PromptForCredentials = false;
                oNetDrive.ShareName = path; //@"\\10.20.20.204\deployment";
                oNetDrive.SaveCredentials = false;
                //match call to options provided
                oNetDrive.MapDrive("lanina", "123456");
                oNetDrive = null;
                return @"X:\";

            }
            catch
            {
                oNetDrive = null;
                return "Cannot find the path.";
            }

        }

        public static bool IsValidFilename(string testName)
        {
            string strvalid = "";
            char[] invalid;
            invalid = System.IO.Path.GetInvalidFileNameChars();
            for (int i = 0; i < invalid.Count(); i++)
            {
                strvalid += invalid[i].ToString() + ",";
            }
            strvalid = strvalid.Substring(0, strvalid.Length - 1);
            Regex containsABadCharacter = new Regex("[" + Regex.Escape(strvalid) + "]");
            if (containsABadCharacter.IsMatch(testName))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static string ReadFile(string Path)
        {
            try
            {
                FileStream file = new FileStream(Path, FileMode.OpenOrCreate, FileAccess.Read);
                StreamReader sr = new StreamReader(file);
                string s = sr.ReadToEnd();
                sr.Close();
                file.Close();
                return s;
            }
            catch { return ""; }
        }

        public static string GetCurrentDateTime()
        {
            string year = DateTime.Now.Year.ToString();
            //string scriptcontentfile = "";
            string month = "";
            if (DateTime.Now.Month < 10 && DateTime.Now.Month.ToString().Length < 2)
            {
                month = ("0" + DateTime.Now.Month.ToString()).Substring(0, 2);
            }
            else
            {
                month = DateTime.Now.Month.ToString();
            }
            string day;
            if (DateTime.Now.Day < 10 && DateTime.Now.Day.ToString().Length < 2)
            {
                day = ("0" + DateTime.Now.Day.ToString()).Substring(0, 2);
            }
            else
            {
                day = DateTime.Now.Day.ToString();
            }

            string hour;
            if (DateTime.Now.Hour < 10 && DateTime.Now.Hour.ToString().Length < 2)
            {
                hour = ("0" + DateTime.Now.Hour.ToString()).Substring(0, 2);
            }
            else
            {
                hour = DateTime.Now.Hour.ToString();
            }

            string minute;
            if (DateTime.Now.Minute < 10 && DateTime.Now.Minute.ToString().Length < 2)
            {
                minute = ("0" + DateTime.Now.Minute.ToString()).Substring(0, 2);
            }
            else
            {
                minute = DateTime.Now.Minute.ToString();
            }

            string second;
            if (DateTime.Now.Second < 10 && DateTime.Now.Second.ToString().Length < 2)
            {
                second = ("0" + DateTime.Now.Second.ToString()).Substring(0, 2);
            }
            else
            {
                second = DateTime.Now.Second.ToString();
            }

            string datenow = year + month + day + "-" + hour + minute + second;
            return datenow;
        }

        public static DataGridView RemoveDuplicateRows(DataGridView dgv, int[] columnIndice)
        {
            List<string> list = new List<string>();
            for (int i = dgv.Rows.Count - 2; i >= 0; i--)
            {
                string key = string.Empty;
                for (int j = 0; j < columnIndice.Length; j++)
                {
                    key += dgv[j, i].Value.ToString().Trim();
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

        public static DataTable ReadFromExcel(string filename)
        {
            var book = Factory.OpenWorkbook(filename);
            var dt = new DataTable();
            if (book != null)
            {
                try
                {
                    dt = book.Worksheets[1].UsedRange.GetDataTable(true, false);

                }
                catch (Exception ex)
                {
                    throw ex;
                }

                //remove empty cells
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(dt.Rows[i][0].ToString().Trim())
                        || string.IsNullOrEmpty(dt.Rows[i][1].ToString().Trim())
                        || string.IsNullOrEmpty(dt.Rows[i][2].ToString().Trim()))
                    {
                        dt.Rows[i].Delete();
                        i = 0;
                    }
                }
                return dt;

            }
            return null;
            //can't open workbook  
        }

        public static bool WriteToExcel(string filename, DataTable table)
        {
            var book = new ExcelFile();

            var sheet = book.Worksheets.Add("Sheet1");
            //header 

            sheet.Cells[0, 0].Value = "ServerName";
            sheet.Cells[0, 1].Value = "DBName";
            sheet.Cells[0, 2].Value = "SPName";
            sheet.Cells[0, 3].Value = "IsFunction";
            for (var i = 0; i < table.Rows.Count; i++)
            {
                for (var j = 0; j < table.Columns.Count; j++)
                {
                    sheet.Cells[i + 1, j].Value = table.Rows[i][j].ToString();
                }
            }
            try
            {
                book.SaveXls(filename);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static IList<string> GetStringList(string stringArray, string sep = ",", bool removeEmptyEntries = true)
        {
            string[] separator = { sep };
            var result = stringArray.Split(separator, removeEmptyEntries ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None);

            return result.Select(item => item.Trim()).ToList();
        }
    }
}
