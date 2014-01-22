namespace DbDeployer.Core.Utils
{
    using System;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using GemBox.Spreadsheet;
    using NativeExcel;

    public static class FileUtility
    {

        public static void TryToDisplayGeneratedFile(string fileName)
        {
            try
            {
                System.Diagnostics.Process.Start(fileName);
            }
            catch
            {
                throw;
            }
        }


        public static bool IsValidFilename(string fileName)
        {
            string strvalid = string.Empty;
            char[] invalid;
            invalid = Path.GetInvalidFileNameChars();
            for (int i = 0; i < invalid.Count(); i++)
            {
                strvalid += invalid[i].ToString() + ",";
            }

            strvalid = strvalid.Substring(0, strvalid.Length - 1);
            Regex containsABadCharacter = new Regex("[" + Regex.Escape(strvalid) + "]");

            return !containsABadCharacter.IsMatch(fileName);
        }

        public static string ReadFile(string path)
        {
            string fileContent = string.Empty;
            try
            {
                if (string.IsNullOrWhiteSpace(path))
                    throw new ArgumentNullException();

                if (!File.Exists(path))
                    throw new FileNotFoundException();

                using (FileStream file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    StreamReader sr = new StreamReader(file);
                    fileContent = sr.ReadToEnd();
                    sr.Close();
                    file.Close();
                }
            }
            catch
            {
                throw;
            }

            return fileContent;
        }

        public static DataTable ReadFromExcel(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException();

            if (!File.Exists(filePath))
                throw new FileNotFoundException();

            var book = Factory.OpenWorkbook(filePath);
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

                // remove empty cells
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
            // can't open workbook  
        }

        public static bool WriteToExcel(string filename, DataTable table)
        {
            bool isSuccessful = false;
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
                isSuccessful = true;
            }
            catch
            {
                isSuccessful = false;
                throw;
            }

            return isSuccessful;
        }    
    }
}
