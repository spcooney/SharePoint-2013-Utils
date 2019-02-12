namespace Spc.SharePoint.Utils.Core.Helper
{
    using System;
    using System.Text.RegularExpressions;

    public static class RegexUtil
    {
        #region "Properties"

        private const string _whiteSpaceRegex = @"\s+";
        private const string _betweenTagsRegex = @">\s+";
        private const string _lineBreaksRegex = @"\n\s+";
        private const string _stripHtmlRegex = "<[^>]*>";
        private const string _emailAddressRegex = @"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$";
        private const string _hostNameRegex = @"^(([a-z]|[a-z][a-z0-9\-]*[a-z0-9])\.)*([a-z]|[a-z][a-z0-9\-]*[a-z0-9])$";
        private const string _ipV4AddressRegex = @"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$";
        private const string _shortGuidRegex = @"^\{0[x|X][a-fA-F\d]{8},(0[x|X][a-fA-F\d]{4},){2}\{(0[x|X][a-fA-F\d]{2},){7}0[x|X][a-fA-F\d]{2}\}\}$";
        private const string _longGuidRegex = @"^([a-fA-F\d]{8}-([a-fA-F\d]{4}-){3}[a-fA-F\d]{12}|\([a-fA-F\d]{8}-([a-fA-F\d]{4}-){3}[a-fA-F\d]{12}\)|\{[a-fA-F\d]{8}-([a-fA-F\d]{4}-){3}[a-fA-F\d]{12}\}|[a-fA-F\d]{32})$";
        private const string _alphaNumericRegex = "[^a-zA-Z_0-9]+";

        #endregion

        #region "Methods"

        public static bool IsEmailAddressValid(string email)
        {
            return IsRegexValid(_emailAddressRegex, email, RegexOptions.IgnoreCase);
        }

        public static bool IsHostNameValid(string hostName)
        {
            return IsRegexValid(_hostNameRegex, hostName, RegexOptions.IgnoreCase);
        }

        public static bool IsIPv4AddressValid(string address)
        {
            return IsRegexValid(_ipV4AddressRegex, address, RegexOptions.IgnoreCase);
        }

        public static bool IsMatch(this string value, string pattern)
        {
            Regex regEx = new Regex(pattern);
            return regEx.IsMatch(value);
        }

        public static bool IsRegexValid(string pattern, string regexString)
        {
            return IsRegexValid(pattern, regexString, RegexOptions.None);
        }

        public static bool IsRegexValid(string pattern, string regexString, RegexOptions options)
        {
            if (pattern == null)
            {
                throw new ArgumentNullException("regex");
            }
            if (StringUtil.IsNullOrWhitespace(regexString))
            {
                throw new ArgumentNullException("regexString");
            }
            return Regex.IsMatch(regexString.Trim(), pattern.ToString(), options);
        }

        #endregion

        #region "Accessors"

        public static string WhiteSpaceRegex
        {
            get { return _whiteSpaceRegex; }
        }

        public static string BetweenTagsRegex
        {
            get { return _betweenTagsRegex; }
        }

        public static string LineBreaksRegex
        {
            get { return _lineBreaksRegex; }
        }

        public static string StripHtmlRegex
        {
            get { return _stripHtmlRegex; }
        }

        public static string EmailAddressRegex
        {
            get { return _emailAddressRegex; }
        }

        public static string HostNameRegex
        {
            get { return _hostNameRegex; }
        }

        public static string IpV4AddressRegex
        {
            get { return _ipV4AddressRegex; }
        }

        public static string ShortGuidRegex
        {
            get { return _shortGuidRegex; }
        }

        public static string LongGuidRegex
        {
            get { return _longGuidRegex; }
        }

        public static string AlphaNumericRegex
        {
            get { return _alphaNumericRegex; }
        }

        #endregion
    }
}