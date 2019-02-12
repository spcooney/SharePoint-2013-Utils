namespace Spc.SharePoint.Utils.Core.Helper
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Text;

    public static class ConvertUtil
    {
        #region "Methods"

        /// <summary>
        /// Attempts to convert a <see cref="System.String"/> to a <see cref="System.Int32"/>.
        /// </summary>
        /// <param name="value">A <see cref="System.String"/> representing a <see cref="System.Int32"/>.</param>
        /// <returns>A <see cref="System.Int32"/>, otherwise a minimum <see cref="System.Int32"/>.</returns>
        public static int ToInt32(string value)
        {
            return ToInt32(value, Int32.MinValue);
        }

        public static int ToInt32(string value, int defaultValue)
        {
            if (String.IsNullOrEmpty(value))
            {
                return defaultValue;
            }
            int output;
            if (Int32.TryParse(value, out output))
            {
                return output;
            }
            return defaultValue;
        }

        public static long ToInt64(object value)
        {
            return ToInt64(value.ToString(), Int64.MinValue);
        }

        public static long ToInt64(object value, long defaultValue)
        {
            return ToInt64(value.ToString(), defaultValue);
        }

        public static long ToInt64(string value)
        {
            return ToInt64(value, Int64.MinValue);
        }

        public static long ToInt64(string value, long defaultValue)
        {
            if (String.IsNullOrEmpty(value))
            {
                return defaultValue;
            }
            long output;
            if (Int64.TryParse(value, out output))
            {
                return output;
            }
            return defaultValue;
        }

        public static double ToDouble(string value)
        {
            return ToDouble(value, Double.MinValue);
        }

        public static double ToDouble(string value, double defaultValue)
        {
            if (String.IsNullOrEmpty(value))
            {
                return defaultValue;
            }
            double output;
            if (Double.TryParse(value, out output))
            {
                return output;
            }
            return defaultValue;
        }

        public static decimal ToDecimal(string value)
        {
            return ToDecimal(value, Decimal.MinValue);
        }

        public static decimal ToDecimal(string value, decimal defaultValue)
        {
            if (String.IsNullOrEmpty(value))
            {
                return defaultValue;
            }
            decimal output;
            if (Decimal.TryParse(value, out output))
            {
                return output;
            }
            return defaultValue;
        }

        public static decimal ToDecimal(string value, int decimalPlaces)
        {
            return ToDecimal(value, decimalPlaces, Decimal.MinValue);
        }

        public static decimal ToDecimal(string value, int decimalPlaces, decimal defaultValue)
        {
            if (String.IsNullOrEmpty(value))
            {
                return defaultValue;
            }
            decimal output;
            if (Decimal.TryParse(value, out output))
            {
                return Math.Round(output, decimalPlaces, MidpointRounding.AwayFromZero);
            }
            return defaultValue;
        }

        public static decimal? ToNullableDecimal(string value, int decimalPlaces)
        {
            return ToNullableDecimal(value, decimalPlaces, null);
        }

        public static decimal? ToNullableDecimal(string value, int decimalPlaces, decimal? defaultValue)
        {
            if (String.IsNullOrEmpty(value))
            {
                return defaultValue;
            }
            decimal output;
            if (Decimal.TryParse(value, out output))
            {
                return Math.Round(output, decimalPlaces, MidpointRounding.AwayFromZero);
            }
            return defaultValue;
        }

        public static DateTime ToDateTime(string value)
        {
            return ToDateTime(value, DateTime.MinValue);
        }

        public static DateTime ToDateTime(string value, DateTime defaultValue)
        {
            if (String.IsNullOrEmpty(value))
            {
                return defaultValue;
            }
            DateTime output;
            if (DateTime.TryParse(value, out output))
            {
                return output;
            }
            return defaultValue;
        }

        public static Guid ToGuid(string value)
        {
            return ToGuid(value, Guid.Empty);
        }

        public static Guid ToGuid(string value, Guid defaultValue)
        {
            if (String.IsNullOrEmpty(value))
            {
                return defaultValue;
            }
            Guid output;
            if (GuidUtil.TryParseGuid(value, out output))
            {
                return output;
            }
            return defaultValue;
        }

        /// <summary>
        /// Attempts to convert a <see cref="System.String"/> to a <see cref="System.Boolean"/>.
        /// </summary>
        /// <param name="value">A <see cref="System.String"/> representing a <see cref="System.Boolean"/>.</param>
        /// <returns>A <see cref="System.Boolean"/>, otherwise false.</returns>
        public static bool ToBoolean(object value)
        {
            return ToBoolean(value.ToString(), false);
        }

        /// <summary>
        /// Attempts to convert a <see cref="System.String"/> to a <see cref="System.Boolean"/>.
        /// </summary>
        /// <param name="value">A <see cref="System.String"/> representing a <see cref="System.Boolean"/>.</param>
        /// <returns>A <see cref="System.Boolean"/>, otherwise false.</returns>
        public static bool ToBoolean(string value)
        {
            return ToBoolean(value, false);
        }

        /// <summary>
        /// Attempts to convert a <see cref="System.String"/> to a <see cref="System.Boolean"/>.
        /// </summary>
        /// <param name="value">A <see cref="System.String"/> representing a <see cref="System.Boolean"/>.</param>
        /// <param name="defaultValue">The default value if the parse fails.</param>
        /// <returns>A <see cref="System.Boolean"/>, otherwise the default value.</returns>
        public static bool ToBoolean(object value, bool defaultValue)
        {
            return ToBoolean(value.ToString(), defaultValue);
        }

        /// <summary>
        /// Attempts to convert a <see cref="System.String"/> to a <see cref="System.Boolean"/>.
        /// </summary>
        /// <param name="value">A <see cref="System.String"/> representing a <see cref="System.Boolean"/>.</param>
        /// <param name="defaultValue">The default value if the parse fails.</param>
        /// <returns>A <see cref="System.Boolean"/>, otherwise the default value.</returns>
        public static bool ToBoolean(string value, bool defaultValue)
        {
            if (String.IsNullOrEmpty(value))
            {
                return defaultValue;
            }
            bool output;
            if (Boolean.TryParse(value, out output))
            {
                return output;
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Attemps to convert a <see cref="System.String"/> to a <see cref="System.Byte"/>.
        /// </summary>
        /// <param name="value">A <see cref="System.Byte"/> representing a UTF-16 <see cref="System.String"/>.</param>
        /// <returns>A <see cref="System.Byte"/>.</returns>
        public static byte[] ToByteArray(string value)
        {
            return Encoding.Unicode.GetBytes(value);
        }

        /// <summary>
        /// Atempts to convert a <see cref="System.Byte"/> to a UTF-16 <see cref="System.String"/>.
        /// </summary>
        /// <param name="bytes">The <see cref="System.Byte"/> array to convert to a <see cref="System.String"/>.</param>
        /// <returns>A UTF-16 <see cref="System.String"/>.</returns>
        public static string ByteArrayToString(byte[] bytes)
        {
            return Encoding.Unicode.GetString(bytes);
        }

        public static T FromString<T>(string value)
        {
            return FromString<T>(value, default(T));
        }

        public static T FromString<T>(string value, T defaultValue)
        {
            if (value == null)
            {
                return defaultValue;
            }
            if (typeof(T).IsAssignableFrom(typeof(string)))
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            if (!converter.CanConvertFrom(typeof(string)))
            {
                return defaultValue;
            }
            try
            {
                return (T)converter.ConvertFromInvariantString(value);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public static T ParseEnum<T>(this string value)
        {
            return value.ParseEnum<T>(true);
        }

        public static T ParseEnum<T>(this string value, bool ignoreCase)
        {
            return (T)Enum.Parse(typeof(T), value, ignoreCase);
        }

        public static bool TryParseEnum<T>(this string value, out T result)
        {
            return value.TryParseEnum<T>(true, out result);
        }

        public static bool TryParseEnum<T>(this string value, bool ignoreCase, out T result)
        {
            result = default(T);
            if (!value.IsNullOrWhitespace())
            {
                try
                {
                    result = value.ParseEnum<T>(ignoreCase);
                    return true;
                }
                catch (ArgumentException)
                {
                }
                catch (OverflowException)
                {
                }
            }
            return false;
        }

        public static T ParseEnumDefault<T>(this string value)
        {
            return value.ParseEnumDefault<T>(true, default(T));
        }

        public static T ParseEnumDefault<T>(this string value, T defaultValue)
        {
            return value.ParseEnumDefault<T>(true, defaultValue);
        }

        public static T ParseEnumDefault<T>(this string value, bool ignoreCase, T defaultValue)
        {
            T local = defaultValue;
            if (!value.IsNullOrWhitespace())
            {
                try
                {
                    local = value.ParseEnum<T>(ignoreCase);
                }
                catch (ArgumentException)
                {
                }
                catch (OverflowException)
                {
                }
            }
            return local;
        }

        internal static string QuoteString(string value, char delimiter)
        {
            StringBuilder sb = null;
            if (value.IsNullOrWhitespace())
            {
                return String.Empty;
            }
            int startIndex = 0;
            int count = 0;
            for (int i = 0; i < value.Length; i++)
            {
                char ch = value[i];
                if ((ch == CharUtil.BackSlash) || (ch == delimiter))
                {
                    if (sb == null)
                    {
                        sb = new StringBuilder(value.Length + 5);
                    }
                    if (count > 0)
                    {
                        sb.Append(value, startIndex, count);
                    }
                    sb.Append(CharUtil.BackSlash);
                    sb.Append(ch);
                    startIndex = i + 1;
                    count = 0;
                }
                else
                {
                    count++;
                }
            }
            if (sb == null)
            {
                return value;
            }
            if (count > 0)
            {
                sb.Append(value, startIndex, count);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Joins a collection of items into a semi-colon delimited <see cref="System.String"/>.
        /// </summary>
        /// <typeparam name="T">An IEnumerable type.</typeparam>
        /// <param name="values">An IEnumerable collection.</param>
        /// <returns>A semi-colon delimited <see cref="System.String"/>.</returns>
        public static string StringJoin<T>(this IEnumerable<T> values)
        {
            return values.StringJoin<T>(CharUtil.SemiColon);
        }

        /// <summary>
        /// Joins a collection of items into a <see cref="System.Char"/> delimited <see cref="System.String"/>.
        /// </summary>
        /// <typeparam name="T">An IEnumerable type.</typeparam>
        /// <param name="values">An IEnumerable collection.</param>
        /// <param name="delimiter">The <see cref="System.Char"/> delimiter.</param>
        /// <returns>A <see cref="System.Char"/> delimited <see cref="System.String"/></returns>
        public static string StringJoin<T>(this IEnumerable<T> values, char delimiter)
        {
            List<string> list = new List<string>();
            foreach (T local in values)
            {
                list.Add(QuoteString(ToString<T>(local), delimiter));
            }
            return String.Join(delimiter.ToString(), list.ToArray());
        }

        /// <summary>
        /// Converts the specified value to a culture-invariant <see cref="System.String"/> representation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">A value.</param>
        /// <returns>A culture-invariant <see cref="System.String"/>.</returns>
        public static string ToString<T>(T value)
        {
            return TypeDescriptor.GetConverter(typeof(T)).ConvertToInvariantString(value);
        }

        /// <summary>
        /// Counts the number of bytes and displays the appropriate label size.
        /// </summary>
        /// <param name="cbSize">An unsigned long.</param>
        /// <returns>A string representation of the size of bytes.</returns>
        public static string BitSizeToString(ulong cbSize)
        {
            if (cbSize > 1024L)
            {
                double num;
                if (cbSize > 1073741824L)
                {
                    num = Math.Round((double)(Convert.ToDouble(cbSize) / 1073741824.0), 1);
                    return String.Format("{0} GBs", new object[] { num.ToString() });
                }
                if (cbSize > 1048576L)
                {
                    num = Math.Round((double)(Convert.ToDouble(cbSize) / 1048576.0), 1);
                    return String.Format("{0} MBs", new object[] { num.ToString() });
                }
                num = Math.Round((double)(Convert.ToDouble(cbSize) / 1024.0), 1);
                return String.Format("{0} KBs", new object[] { num.ToString() });
            }
            if (cbSize > 0L)
            {
                return String.Format("{0} Ks", new object[0]);
            }
            return String.Format("{0} KBs", new object[] { "0" });
        }

        public static string FormatFileSize(long cbSize)
        {
            if (cbSize > 1024L)
            {
                double num;
                if (cbSize > 1073741824L)
                {
                    num = Math.Round((double)(Convert.ToDouble(cbSize) / 1073741824.0), 1);
                    return String.Format("File Size GB: {0}", new object[] { num.ToString() });
                }
                if (cbSize > 1048576L)
                {
                    num = Math.Round((double)(Convert.ToDouble(cbSize) / 1048576.0), 1);
                    return String.Format("File Size MB: {0}", new object[] { num.ToString() });
                }
                num = Math.Round((double)(Convert.ToDouble(cbSize) / 1024.0), 1);
                return String.Format("File Size KB: {0}", new object[] { num.ToString() });
            }
            if (cbSize > 0L)
            {
                return String.Format("File Size Small: {0}", new object[0]);
            }
            return String.Format("File Size KB: {0}", new object[] { "0" });
        }

        public static bool ParseBoolean(object value)
        {
            try
            {
                if (value == null)
                {
                    return false;
                }
                string str = value as string;
                if (str != null)
                {
                    if (str.Length <= 0)
                    {
                        return false;
                    }
                    if (String.Compare(str, "1", StringComparison.Ordinal) == 0)
                    {
                        return true;
                    }
                    if (String.Compare(str, "0", StringComparison.Ordinal) == 0)
                    {
                        return false;
                    }
                }
                return Convert.ToBoolean(value, CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                return false;
            }
        }

        #endregion
    }
}