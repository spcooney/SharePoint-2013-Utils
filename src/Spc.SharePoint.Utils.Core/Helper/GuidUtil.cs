namespace Spc.SharePoint.Utils.Core.Helper
{
    using System;
    using System.Globalization;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class GuidUtil
    {
        #region "Methods"

        /// <summary>
        /// Converts a regular or short Guid to a Guid.
        /// </summary>
        /// <param name="shortOrRegGuid">A string representation of a short or regular Guid.</param>
        /// <returns>A Guid based on the string.</returns>
        public static Guid ToGuid(string shortOrRegGuid)
        {
            Guid fromShort = GuidUtil.ToGuidFromShortGuid(shortOrRegGuid);
            if (fromShort == DbNullUtil.NullGuid)
            {
                fromShort = GuidUtil.GuidFromString(shortOrRegGuid);
            }
            else if (!GuidUtil.IsGuid(fromShort.ToString()))
            {
                fromShort = GuidUtil.GuidFromString(shortOrRegGuid);
            }
            return fromShort;
        }

        /// <summary>
        /// Converts a short Guid to a Guid.
        /// </summary>
        /// <param name="shortGuid">A string representation of a short Guid.</param>
        /// <returns>A guid based on the string.</returns>
        public static Guid ToGuidFromShortGuid(string shortGuid)
        {
            if (String.IsNullOrEmpty(shortGuid) || shortGuid.Length != 22)
            {
                return Guid.Empty;
            }
            StringBuilder sb = new StringBuilder(shortGuid);
            sb.Replace(CharUtil.FigureDash, CharUtil.PlusSign);
            sb.Replace(CharUtil.Underscore, CharUtil.ForwardSlash);
            sb.Append("==");
            return new Guid(Convert.FromBase64String(sb.ToString()));
        }

        /// <summary>
        /// Converts a Guid to a short Guid string.
        /// </summary>
        /// <param name="value">The Guid to convert.</param>
        /// <returns>A string representation of a short Guid.</returns>
        public static string ToShortGuid(this Guid value)
        {
            StringBuilder sb = new StringBuilder(Convert.ToBase64String(value.ToByteArray()));
            sb.Replace(CharUtil.PlusSign, CharUtil.FigureDash);
            sb.Replace(CharUtil.ForwardSlash, CharUtil.Underscore);
            return sb.ToString(0, 22);
        }

        public static string FormatGuid(this Guid value)
        {
            try
            {
                return value.ToString("B", CultureInfo.InvariantCulture).ToUpper(CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                return String.Empty;
            }
        }

        public static bool IsGuid(string guidStr)
        {
            if (String.IsNullOrEmpty(guidStr))
            {
                return false;
            }
            guidStr = guidStr.Trim();
            if (guidStr.Length < 32)
            {
                return false;
            }
            if (guidStr.Contains("x") || guidStr.Contains("X"))
            {
                guidStr = guidStr.Replace(CharUtil.Whitespace.ToString(), String.Empty);
                return Regex.IsMatch(guidStr, RegexUtil.ShortGuidRegex, RegexOptions.Compiled);
            }
            return Regex.IsMatch(guidStr, RegexUtil.LongGuidRegex, RegexOptions.Compiled);
        }

        public static Guid ObjectToGuid(object obj)
        {
            if (obj != null)
            {
                Type type = obj.GetType();
                if (type == typeof(Guid))
                {
                    return (Guid)obj;
                }
                if (type == typeof(byte[]))
                {
                    return new Guid((byte[])obj);
                }
                if (type == typeof(string))
                {
                    return new Guid((string)obj);
                }
            }
            return Guid.Empty;
        }

        public static bool TryParseGuid(string guid, out Guid outGuid)
        {
            outGuid = Guid.Empty;
            if (String.IsNullOrEmpty(guid) || IsGuid(guid))
            {
                return false;
            }
            try
            {
                outGuid = new Guid(guid);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static Guid GuidFromString(string guidString)
        {
            return GuidFromString(guidString, Guid.Empty);
        }

        public static Guid GuidFromString(string guidString, Guid defaultGuid)
        {
            if (StringUtil.IsNullOrWhitespace(guidString))
            {
                return defaultGuid;
            }
            try
            {
                return new Guid(guidString);
            }
            catch
            {
                return defaultGuid;
            }
        }

        #endregion
    }
}