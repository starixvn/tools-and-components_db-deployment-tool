namespace DbDeployer.Core.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public static class StringUtility
    {
        public static string RemoveSquareBracket(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            input = Regex.Replace(input, "\\[([^\\s]*)\\]", "$1");

            return input;

            //if (input.IndexOf('[') == -1)
            //    return input;
            //return input.Substring(1, input.IndexOf(']') - 1);
        }

        public static IList<string> GetStringList(string stringArray
            , string sep = ",", bool removeEmptyEntries = true)
        {
            string[] separator = { sep };
            var result = stringArray.Split(separator,
                removeEmptyEntries ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None);

            return result.Select(item => item.Trim()).ToList();
        }

        /// <summary>
        /// Generate datetime with format yyyyMMdd-hhmmss
        /// </summary>
        /// <returns></returns>
        public static string GetFormatedCurrentDateTime()
        {
            return DateTime.Now.ToString("yyyyMMdd-HHmmss");
        }

        private const string ConnectionStringPattern = "Data Source={0};Initial Catalog={1};User Id={2};Password={3}";
        public static string CreateConnectionString(string dataSource, string database, string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(dataSource)
                || string.IsNullOrWhiteSpace(database)
                || string.IsNullOrWhiteSpace(userName)
                || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException();
            }

            return string.Format(ConnectionStringPattern, dataSource, database, userName, password);

        }
    }
}
