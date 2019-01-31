namespace Spc.SharePoint.Utils.Core.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Text;

    public static class StringUtil
    {
        /// <summary>"abcdefghijklmnopqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ123456789"</summary>
        private const string _abcNumChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ123456789";
        private static string _quote = CharUtil.DoubleQuote.ToString();
        private const string _whitespace = " ";
        private const string _nullString = "null";

        /// <summary>
        /// Adds a double quote (") to the beginning and end of the string.
        /// </summary>
        /// <param name="value">The value to double quote.</param>
        /// <returns>A double quoted string.</returns>
        public static string AddQuote(string value)
        {
            return (_quote + value + _quote);
        }

        /// <summary>
        /// Adds the quoteChar to the beginning and end of the string.
        /// </summary>
        /// <param name="value">The value to quote.</param>
        /// <param name="quoteChar">The character to append to the beginning and end of the string.</param>
        /// <returns>A string with a quoteChar at the beginning and end of the string.</returns>
        public static string AddQuote(string value, char quoteChar)
        {
            return (quoteChar + value + quoteChar);
        }

        /// <summary>
        /// Adds a double quote (") to the beginning and end of each string in the list provided.
        /// </summary>
        /// <param name="values">A list of strings.</param>
        /// <returns>A comma delimited list of double quoted strings.</returns>
        public static string AddQuote(List<string> values)
        {
            return AddQuote(values, _quote, CharUtil.Comma.ToString());
        }

        /// <summary>
        /// Adds the quoteChar to the beginning and end of each string in the list.
        /// </summary>
        /// <param name="values">A list of strings.</param>
        /// <param name="quoteChar">The character to append to the beginning and end of the string.</param>
        /// <param name="joinText">The string used to separate each quoted string.</param>
        /// <returns>A string wrapped by the quoteChar and joined by the joinText.</returns>
        public static string AddQuote(List<string> values, string quoteChar, string joinText)
        {
            List<string> list = new List<string>();
            foreach (string str in values)
            {
                list.Add(quoteChar + str + quoteChar);
            }
            return String.Join(joinText, list.ToArray());
        }

        /// <summary>
        /// Removes a double quote (") from the first and last character of the string.
        /// </summary>
        /// <param name="value">The double quoted string.</param>
        /// <returns>A string without double quotes at the beginning and end of the string.</returns>
        public static string RemoveQuote(string value)
        {
            if (value == null)
            {
                return (String)null;
            }
            if ((!value.StartsWith(CharUtil.DoubleQuote.ToString(), StringComparison.Ordinal)) || (!value.EndsWith(CharUtil.DoubleQuote.ToString(), StringComparison.Ordinal)))
            {
                return value;
            }
            else
            {
                return value.Substring(1, (value.Length - 2));
            }
        }

        /// <summary>
        /// Determines if the string is a number.  This method will parse every character in the string and 
        /// if a non-number exists it will return false.
        /// </summary>
        /// <param name="number">The string number to parse.</param>
        /// <returns>True, if every character makes up a number.  Otherwise, false.</returns>
        /// <remarks>This method will not handle decimal places or comma separators.  Please remove those 
        /// characters before calling this method.</remarks>
        public static bool IsNumber(this string number)
        {
            if (String.IsNullOrEmpty(number))
            {
                return false;
            }
            for (int i = 0; i < number.Length; i++)
            {
                if (!Char.IsNumber(number[i]))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Deliminates a <see cref="System.String"/> array.
        /// </summary>
        /// <param name="strArray">The <see cref="System.String"/> array to deliniate.</param>
        /// <param name="separator">The delineator <see cref="System.String"/>.</param>
        /// <returns>A deliniated <see cref="System.String"/>.</returns>
        public static string DelineateString(this string[] strArray, string separator)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < strArray.Length; i++)
            {
                if (i > 0)
                {
                    sb.Append(separator);
                    sb.Append(CharUtil.Whitespace.ToString());
                }
                sb.Append(strArray[i]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Splits a <see cref="System.String"/> based on the <see cref="System.Char"/> seperator specified.
        /// </summary>
        /// <param name="delimitedString">The <see cref="System.String"/> to split.</param>
        /// <param name="separator">The <see cref="System.Char"/> seperator.</param>
        /// <returns>A <see cref="System.String"/> array.</returns>
        public static string[] ParseDelimitedString(this string delimitedString, char separator)
        {
            return delimitedString.Split(new char[] { separator }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Checks if a <see cref="System.String"/> is null or only whitespace.
        /// </summary>
        /// <param name="value">The <see cref="System.String"/> to check.</param>
        /// <returns>True, if null or only whitespace.  Otherwise, false.</returns>
        public static bool IsNullOrWhitespace(this string value)
        {
            return ((value == null) || (value.Trim().Length == 0));
        }

        public static bool IsNotNullOrWhitespace(this string value)
        {
            return (!IsNullOrWhitespace(value));
        }

        public static string ToBinaryHexString(byte[] inArray)
        {
            return Encode(inArray, 0, inArray.Length);
        }

        public static string EscapeString(string value, char escapeChar)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char ch in value)
            {
                sb.Append(ch);
                if (escapeChar == ch)
                {
                    sb.Append(ch);
                }
            }
            return sb.ToString();
        }

        public static string Encode(byte[] inArray, int offsetIn, int count)
        {
            if (inArray == null)
            {
                throw new ArgumentNullException("inArray");
            }
            if (0 > offsetIn)
            {
                throw new ArgumentOutOfRangeException("offsetIn");
            }
            if (0 > count)
            {
                throw new ArgumentOutOfRangeException("count");
            }
            if (count > (inArray.Length - offsetIn))
            {
                throw new ArgumentOutOfRangeException("count");
            }
            char[] chrArray = new char[2 * count];
            return new String(chrArray, 0, Encode(inArray, offsetIn, count, chrArray));
        }

        private static int Encode(byte[] inArray, int offsetIn, int count, char[] outArray)
        {
            int num = 0;
            int num2 = 0;
            int length = outArray.Length;
            for (int i = 0; i < count; i++)
            {
                byte num3 = inArray[offsetIn++];
                outArray[num++] = "0123456789ABCDEF"[num3 >> 4];
                if (num == length)
                {
                    break;
                }
                outArray[num++] = "0123456789ABCDEF"[num3 & 15];
                if (num == length)
                {
                    break;
                }
            }
            return (num - num2);
        }

        /// <summary>
        /// Converts a <see cref="System.Byte"/> array to a hex <see cref="System.String"/>.
        /// </summary>
        /// <param name="bytes">The <see cref="System.Byte"/> array.</param>
        /// <returns>A hex <see cref="System.String"/>.</returns>
        public static string ToHexString(this string value, byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }
            StringBuilder sb = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                sb.AppendFormat("{0:x2}", b);
            }
            return sb.ToString();
        }

        public static bool IsStringHex(this string value)
        {
            for (int i = 0; i < value.Length; ++i)
            {
                char ch = value[i];
                if ((int)ch < 48 || (int)ch > 57 && (int)ch < 65 || ((int)ch > 70 && (int)ch < 97 || (int)ch > 102))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool SPValidString(string strIn)
        {
            return StringUtil.SPValidString(strIn, 2048);
        }

        public static bool SPValidString(string strIn, uint nMaxLength)
        {
            return (!String.IsNullOrEmpty(strIn) && (strIn.Length <= nMaxLength));
        }

        /// <summary>
        /// Tries to parse a hex <see cref="System.String"/> to a <see cref="System.Byte"/> array.
        /// </summary>
        /// <param name="value">The hex <see cref="System.String"/>.</param>
        /// <param name="bytes">A <see cref="System.Byte"/> array.</param>
        /// <returns>True if the hex <see cref="System.String"/> was successfully parsed.</returns>
        public static bool TryParseHexString(this string value, out byte[] bytes)
        {
            try
            {
                bytes = ParseHexString(value);
            }
            catch
            {
                bytes = null;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Parses a hex <see cref="System.String"/> into its equivalent <see cref="System.Byte"/> array.
        /// </summary>
        /// <param name="value">The hex <see cref="System.String"/> to parse.</param>
        /// <returns>The <see cref="System.Byte"/> equivalent of the hex <see cref="System.String"/>.</returns>
        public static byte[] ParseHexString(this string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("s");
            }
            byte[] bytes;
            if ((value.Length & 1) != 0)
            {
                value = "0" + value; // make length of s even
            }
            bytes = new byte[value.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                string hex = value.Substring(2 * i, 2);
                try
                {
                    byte b = Convert.ToByte(hex, 16);
                    bytes[i] = b;
                }
                catch (FormatException ex)
                {
                    Log4NetHelper.LogError(ex);
                    throw new FormatException(
                        string.Format("Invalid hex string {0}. Problem with substring {1} starting at position {2}",
                        value,
                        hex,
                        2 * i),
                        ex);
                }
            }
            return bytes;
        }

        /// <summary>
        /// Parses all of the line breaks from a UTF-8 based <see cref="System.String"/>.
        /// </summary>
        /// <param name="value">The <see cref="System.String"/> to parse.</param>
        /// <returns>A <see cref="System.String"/> array of each line.</returns>
        public static string[] ReadAllLinesToArray(this string value)
        {
            return ReadAllLinesToArray(value, Encoding.UTF8);
        }

        /// <summary>
        /// Parses all of the line breaks from an encoded <see cref="System.String"/>.
        /// </summary>
        /// <param name="value">The <see cref="System.String"/> to parse.</param>
        /// <returns>A <see cref="System.String"/> array of each line.</returns>
        public static string[] ReadAllLinesToArray(this string value, Encoding encoding)
        {
            byte[] bArray = encoding.GetBytes(value);
            using (MemoryStream memStream = new MemoryStream(bArray))
            {
                using (StreamReader streamRdr = new StreamReader(memStream))
                {
                    List<string> list = new List<string>();
                    string str;
                    while ((str = streamRdr.ReadLine()) != null)
                    {
                        list.Add(str);
                    }
                    return list.ToArray();
                }
            }
        }

        public static byte[] ToByteArray(this string value)
        {
            byte[] bytes = new byte[value.Length * sizeof(char)];
            if (!StringUtil.IsNullOrWhitespace(value))
            {
                Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);
            }
            return bytes;
        }

        public static byte[] ToByteArray(this string value, Encoding encoding)
        {
            return encoding.GetBytes(value);
        }

        public static bool StringStartsWith(string s, char c)
        {
            return ((s.Length != 0) && (s[0] == c));
        }

        public static bool StringStartsWith(this string value, string prefix, int startIndex)
        {
            int len = prefix.Length;
            int pos = startIndex;
            int prefPos = 0;
            if ((startIndex < 0) || (startIndex > prefix.Length))
            {
                return false;
            }
            while (--len >= 0)
            {
                if (value[pos++] != prefix[prefPos++])
                {
                    return false;
                }
            }
            return true;
        }

        public static bool StringEndsWith(this string value, char c)
        {
            int length = value.Length;
            return ((length != 0) && (value[length - 1] == c));
        }

        public static bool StringStartsWithIgnoreCase(string s1, string s2)
        {
            if (!s1.IsNullOrWhitespace() || !s2.IsNullOrWhitespace())
            {
                return false;
            }
            if (s2.Length > s1.Length)
            {
                return false;
            }
            return (0 == string.Compare(s1, 0, s2, 0, s2.Length, StringComparison.OrdinalIgnoreCase));
        }

        public static bool StringSuffixEndsWith(string strBody, string strEnd)
        {
            return CultureInfo.InvariantCulture.CompareInfo.IsSuffix(strBody, strEnd, CompareOptions.IgnoreCase);
        }

        public static bool StringEndsWithIgnoreCase(string s1, string s2)
        {
            int indexA = s1.Length - s2.Length;
            if (indexA < 0)
            {
                return false;
            }
            return (0 == String.Compare(s1, indexA, s2, 0, s2.Length, StringComparison.OrdinalIgnoreCase));
        }

        public static bool IsAlphaNumeric(this string value)
        {
            if (StringUtil.IsNullOrWhitespace(value))
            {
                return false;
            }
            foreach (char ch in value)
            {
                if ((((ch < '0') || (ch > '9')) && ((ch < 'a') || (ch > 'z'))) && ((ch < 'A') || (ch > 'Z')))
                {
                    return false;
                }
            }
            return true;
        }

        public static string StripEnd(string stringToStrip, string[] stringsToStrip)
        {
            string str = stringToStrip.TrimEnd(new char[0]);
            foreach (string str2 in stringsToStrip)
            {
                if ((!StringUtil.IsNullOrWhitespace(str2)) && (str.EndsWith(str2, StringComparison.OrdinalIgnoreCase)))
                {
                    return str.Substring(0, (stringToStrip.Length - str2.Length));
                }
            }
            return stringToStrip;
        }

        public static int CalculateWidth(string value, Encoding encoding)
        {
            if (encoding.IsSingleByte)
            {
                return value.Length;
            }
            return encoding.GetByteCount(value);
        }

        public static int CalculateWidth(char[] chars, Encoding encoding)
        {
            if (encoding.IsSingleByte)
            {
                return chars.Length;
            }
            return encoding.GetByteCount(chars);
        }

        public static bool CheckStringNullOrEmpty(this string value)
        {
            return CheckStringNullOrEmpty(value, false);
        }

        public static bool CheckStringNullOrEmpty(this string value, bool throwException)
        {
            bool flag = ((value == null) || (value.Length == 0)) || (value[0] == '\0');
            if (!flag || !throwException)
            {
                return flag;
            }
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            throw new ArgumentException("value");
        }

        /// <summary>
        /// Truncates a string to a specified length.
        /// </summary>
        /// <param name="text">The text to truncate.</param>
        /// <param name="length">How long the <see cref="System.String"/> should be.</param>
        /// <returns>A trucated <see cref="System.String"/> based on the length.</returns>
        public static string Truncate(this string text, int length)
        {
            if (text.Length <= length)
            {
                return text;
            }
            return text.Substring(0, length);
        }

        public static string Truncate(this string value, int width, Encoding encoding)
        {
            if (!StringUtil.IsNullOrWhitespace(value))
            {
                if (CalculateWidth(value, encoding) > width)
                {
                    int num1 = 0;
                    char[] chars = new char[1];
                    int num2 = 0;
                    while (num2 < value.Length)
                    {
                        chars[0] = value[num2];
                        num1 += CalculateWidth(chars, encoding);
                        if (num1 == width)
                        {
                            break;
                        }
                        if (num1 > width)
                        {
                            num2--;
                            break;
                        }
                        num2++;
                    }
                    value = value.Substring(0, (num2 + 1));
                }
                foreach (char ch in Environment.NewLine)
                {
                    value = value.Replace(ch, CharUtil.Whitespace);
                }
            }
            return value;
        }

        public static string TruncateLeft(this string value, int width, Encoding encoding)
        {
            if (!StringUtil.IsNullOrWhitespace(value))
            {
                if (CalculateWidth(value, encoding) > width)
                {
                    int num1 = 0;
                    char[] chars = new char[1];
                    int num2 = value.Length - 1;
                    while (num2 >= 0)
                    {
                        chars[0] = value[num2];
                        num1 += CalculateWidth(chars, encoding);
                        if (num1 == width)
                        {
                            break;
                        }
                        if (num1 > width)
                        {
                            num2--;
                            break;
                        }
                        num2--;
                    }
                    value = value.Substring(value.Length - num2);
                }
                foreach (char ch in Environment.NewLine)
                {
                    value = value.Replace(ch, CharUtil.Whitespace);
                }
            }
            return value;
        }

        public static string RemoveLineBreaks(string value)
        {
            StringBuilder sb = new StringBuilder(value.Length);
            for (int i = 0; i < value.Length; i++)
            {
                char c = value[i];
                if (c != CharUtil.NewLine)
                {
                    if (String.CompareOrdinal(value, i, Environment.NewLine, 0, Environment.NewLine.Length) == 0)
                    {
                        i += Environment.NewLine.Length - 1;
                    }
                    else if (c != CharUtil.CarriageReturn)
                    {
                        sb.Append(c);
                    }
                }
            }
            return sb.ToString();
        }

        public static SecureString CreateSecureString(this string value)
        {
            SecureString str1 = new SecureString();
            if (!IsNullOrWhitespace(value))
            {
                foreach (char ch in value.ToCharArray())
                {
                    str1.AppendChar(ch);
                }
            }
            return str1;
        }

        public static string SecureStringToString(this SecureString value)
        {
            return SecureStringToString(value, Encoding.Unicode);
        }

        public static string SecureStringToString(this SecureString value, Encoding encodingType)
        {
            IntPtr ptr = IntPtr.Zero;
            string outStr = String.Empty;
            if (encodingType == Encoding.Unicode)
            {
                try
                {
                    ptr = Marshal.SecureStringToGlobalAllocUnicode(value);
                    outStr = Marshal.PtrToStringUni(ptr);
                }
                finally
                {
                    Marshal.ZeroFreeGlobalAllocUnicode(ptr);
                }
            }
            else if (encodingType == Encoding.ASCII)
            {
                try
                {
                    ptr = Marshal.SecureStringToGlobalAllocAnsi(value);
                    outStr = Marshal.PtrToStringAnsi(ptr);
                }
                finally
                {
                    Marshal.ZeroFreeGlobalAllocAnsi(ptr);
                }
            }
            return outStr;
        }

        public static string Replace(this string value, string oldValue, string newValue, StringComparison comparison)
        {
            StringBuilder sb = new StringBuilder(value.Length);
            int num1 = 0;
            int startIndex = 0;
            while (true)
            {
                num1 = value.IndexOf(oldValue, startIndex, comparison);
                if (num1 < startIndex)
                {
                    break;
                }
                int length = num1 - startIndex;
                if (length > 0)
                {
                    sb.Append(value.Substring(startIndex, length));
                }
                sb.Append(newValue);
                startIndex = num1 + oldValue.Length;
            }
            if (sb.Length == 0)
            {
                return value;
            }
            if (startIndex < value.Length)
            {
                sb.Append(value.Substring(startIndex));
            }
            return sb.ToString();
        }

        public static string ReplaceFirst(this string value, string oldValue, string newValue, StringComparison comparison)
        {
            NullUtil.CheckForNull(oldValue, "oldValue");
            if (newValue == null)
            {
                newValue = String.Empty;
            }
            string str1 = value;
            if (StringUtil.IsNotNullOrWhitespace(value))
            {
                int len = value.IndexOf(oldValue, comparison);
                if ((0 <= len) && (len < value.Length))
                {
                    string str2 = String.Empty;
                    if (len > 0)
                    {
                        str2 = value.Substring(0, len);
                    }
                    string str3 = String.Empty;
                    int startIndex = checked(len + oldValue.Length);
                    if (startIndex < value.Length)
                    {
                        str3 = value.Substring(startIndex);
                    }
                    str1 = (str2 + newValue + str3);
                }
            }
            return str1;
        }

        public static void SBReplaceCharWithString(this StringBuilder sb, char oldChar, string newValue)
        {
            int startIndex = 0;
            int length = newValue.Length;
            while (startIndex < sb.Length)
            {
                if (sb[startIndex] == oldChar)
                {
                    sb.Remove(startIndex, 1);
                    sb.Insert(startIndex, newValue);
                    startIndex += length;
                }
                else
                {
                    startIndex++;
                }
            }
        }

        public static bool HasTrailingZeros(string value, int index)
        {
            for (int i = index; i < value.Length; i++)
            {
                if (value[i] != '\0')
                {
                    return false;
                }
            }
            return true;
        }

        public static int FirstNonWhitespaceIndex(this string value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                if (!Char.IsWhiteSpace(value[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        public static char[] GetChars(this string value, int startIndex, int length)
        {
            if (StringUtil.IsNullOrWhitespace(value))
            {
                throw new ArgumentNullException("value");
            }
            if (startIndex < 0)
            {
                throw new ArgumentOutOfRangeException("startIndex");
            }
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length");
            }
            return value.ToCharArray(startIndex, length);
        }

        public static string GetString(this byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        public static string GetString(this byte[] bytes, Encoding encoding)
        {
            return encoding.GetString(bytes);
        }

        public static string ByteArrayToString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; ++i)
            {
                sb.AppendFormat("{0:X2}", bytes[i]);
            }
            return sb.ToString();
        }

        public static string EnsureNotNull(this string value)
        {
            if (value == null)
            {
                return String.Empty;
            }
            return value;
        }

        public static string EnsureEndWithSemiColon(this string value)
        {
            if (value != null)
            {
                int len = value.Length;
                if ((len > 0) && (value[len - 1] != CharUtil.SemiColon))
                {
                    return (value + CharUtil.SemiColon);
                }
            }
            return value;
        }

        public static bool ContainsWhitespace(this string value)
        {
            for (int i = value.Length - 1; i >= 0; i--)
            {
                if (Char.IsWhiteSpace(value[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public static string[] ObjectArrayToString(object[] objectArray)
        {
            string[] strArray = new string[objectArray.Length];
            objectArray.CopyTo(strArray, 0);
            return strArray;
        }

        public static string Join(string separator, IList<String> values)
        {
            return Join(separator, values, 0, values.Count);
        }

        public static string Join(string separator, IList<String> values, int startIndex, int count)
        {
            if (startIndex < 0)
            {
                throw new ArgumentOutOfRangeException("startIndex");
            }
            if ((count < 0) || ((startIndex + count) > values.Count))
            {
                throw new ArgumentOutOfRangeException("count");
            }
            if (count == 0)
            {
                return String.Empty;
            }
            int capacity = 0;
            int len = separator.Length;
            for (int i = 0; i < count; i++)
            {
                capacity += (values[i + startIndex].Length + len);
            }
            capacity -= len;
            if (capacity == 0)
            {
                return String.Empty;
            }
            StringBuilder sb = new StringBuilder(capacity, capacity);
            sb.Append(values[startIndex]);
            for (int j = 1; j < count; j++)
            {
                sb.Append(separator);
                sb.Append(values[j + startIndex]);
            }
            return sb.ToString();
        }

        public static string Repeat(string value, int times)
        {
            StringBuilder sb = new StringBuilder(value.Length * times);
            for (int i = 0; i < times; i++)
            {
                sb.Append(value);
            }
            return sb.ToString();
        }

        public static string GetSafeString(object value)
        {
            if ((value != DBNull.Value) && (value != null))
            {
                return value.ToString();
            }
            return String.Empty;
        }

        public static string EnumerableToString<T>(IEnumerable<T> enumerable, string separator)
        {
            if (enumerable == null)
            {
                return String.Empty;
            }
            NullUtil.CheckForNull(separator, "separator");
            StringBuilder sb = new StringBuilder();
            foreach (T obj in enumerable)
            {
                if (obj != null)
                {
                    string str = obj.ToString();
                    if (StringUtil.IsNotNullOrWhitespace(str))
                    {
                        sb.Append(str);
                        sb.Append(separator);
                    }
                }
            }
            if (sb.Length != 0)
            {
                return sb.Remove((sb.Length - separator.Length), separator.Length).ToString();
            }
            return String.Empty;
        }

        public static string Whitespace
        {
            get { return _whitespace; }
        }

        public static string NullString
        {
            get { return _nullString; }
        }
    }
}