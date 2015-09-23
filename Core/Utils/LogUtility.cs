namespace DbDeployer.Core.Utils
{
    using System;
    using System.IO;

    public static class LogUtility
    {
        public static void WriteLog(string logMessage, string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
                throw new ArgumentNullException();

            using (StreamWriter w = File.AppendText(filename))
            {
                w.Write("\r\nLog Entry : ");
                w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                w.WriteLine(" {0}", logMessage);
                w.WriteLine("-------------------------------");
                // Update the underlying file.
                w.Flush();
                w.Close();
            }
        }

        public static void WriteToFile(string content, string filename)
        {
            File.WriteAllText(filename, content);
        }

        public static string DumpLog(string filename)
        {
            // While not at the end of the file, read and write lines.
            string log = string.Empty;
            if (File.Exists(filename))
            {
                using (StreamReader r = File.OpenText(filename))
                {
                    string line = string.Empty;
                    log = string.Empty;
                    while ((line = r.ReadLine()) != null)
                    {
                        log += line + "\r\n";
                    }
                    r.Close();
                }
            }
            else
            {
                log = "Cannot read the file: " + filename;
            }

            return log;
        }
    }
}
