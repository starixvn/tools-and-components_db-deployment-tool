namespace DbDeployer.Core.Utils
{
    using System;
    using System.Text.RegularExpressions;

    public static class Extensions
    {
        public static string RemoveSquareBracket(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            input = Regex.Replace(input, "\\[([^\\s]*)\\]", "$1");

            return input;            
        }

        public static string RemoveTab(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            return input.Replace('\t', ' ');
        }

        public static string AddBreakLine(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException(input);

            input += "\r\n";

            return input;
        }

        public static string AddTab(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException(input);
            
            input += "\t";

            return input;
        }

        public static string Append(this string input, string append)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException(input);

            if (string.IsNullOrWhiteSpace(append))
                return input;

            input += append;

            return input;
        }
    }
}
