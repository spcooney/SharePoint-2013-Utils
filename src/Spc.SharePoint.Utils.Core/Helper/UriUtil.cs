namespace Spc.SharePoint.Utils.Core.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Web;
    using System.Web.UI;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class UriUtil
    {
        #region "Properties"

        private static readonly char[] _trimChars = new char[] { CharUtil.ForwardSlash };
        private static readonly string[] _webProtocols = new string[] { "http://", "https://", "file://", @"file:\\", "ftp://", 
            "mailto:", "news:", "nntp:", "rtsp://", "tel:", "pnm://", "mms://" };
        private const string _virtualSlash = "~/";
        private const string HttpStr = "http";
        private const string HttpsStr = "https";
        private static readonly string[] UnicodeEncodeHexStrs = new string[256]
        {
          "%00", "%01", "%02", "%03", "%04", "%05", "%06", "%07", "%08", "%09", "%0A", "%0B", "%0C", "%0D", "%0E",
          "%0F", "%10", "%11", "%12", "%13", "%14", "%15", "%16", "%17", "%18", "%19", "%1A", "%1B", "%1C", "%1D",
          "%1E", "%1F", "%20", "%21", "%22", "%23", "%24", "%25", "%26", "%27", "%28", "%29", "%2A", "%2B", "%2C",
          "%2D", "%2E", "%2F", "%30", "%31", "%32", "%33", "%34", "%35", "%36", "%37", "%38", "%39", "%3A", "%3B",
          "%3C", "%3D", "%3E", "%3F", "%40", "%41", "%42", "%43", "%44", "%45", "%46", "%47", "%48", "%49", "%4A",
          "%4B", "%4C", "%4D", "%4E", "%4F", "%50", "%51", "%52", "%53", "%54", "%55", "%56", "%57", "%58", "%59",
          "%5A", "%5B", "%5C", "%5D", "%5E", "%5F", "%60", "%61", "%62", "%63", "%64", "%65", "%66", "%67", "%68",
          "%69", "%6A", "%6B", "%6C", "%6D", "%6E", "%6F", "%70", "%71", "%72", "%73", "%74", "%75", "%76", "%77",
          "%78", "%79", "%7A", "%7B", "%7C", "%7D", "%7E", "%7F", "%80", "%81", "%82", "%83", "%84", "%85", "%86",
          "%87", "%88", "%89", "%8A", "%8B", "%8C", "%8D", "%8E", "%8F", "%90", "%91", "%92", "%93", "%94", "%95",
          "%96", "%97", "%98", "%99", "%9A", "%9B", "%9C", "%9D", "%9E", "%9F", "%A0", "%A1", "%A2", "%A3", "%A4",
          "%A5", "%A6", "%A7", "%A8", "%A9", "%AA", "%AB", "%AC", "%AD", "%AE", "%AF", "%B0", "%B1", "%B2", "%B3",
          "%B4", "%B5", "%B6", "%B7", "%B8", "%B9", "%BA", "%BB", "%BC", "%BD", "%BE", "%BF", "%C0", "%C1", "%C2",
          "%C3", "%C4", "%C5", "%C6", "%C7", "%C8", "%C9", "%CA", "%CB", "%CC", "%CD", "%CE", "%CF", "%D0", "%D1",
          "%D2", "%D3", "%D4", "%D5", "%D6", "%D7", "%D8", "%D9", "%DA", "%DB", "%DC", "%DD", "%DE", "%DF", "%E0",
          "%E1", "%E2", "%E3", "%E4", "%E5", "%E6", "%E7", "%E8", "%E9", "%EA", "%EB", "%EC", "%ED", "%EE", "%EF",
          "%F0", "%F1", "%F2", "%F3", "%F4", "%F5", "%F6", "%F7", "%F8", "%F9", "%FA", "%FB", "%FC", "%FD", "%FE",
          "%FF"
        };

        #endregion

        #region "Methods"

        public static bool IsNonAsciiByte(byte b)
        {
            if (b < 127)
            {
                return (b < 32);
            }
            return true;
        }

        public static string UrlEncode(string value)
        {
            if (value == null)
            {
                return null;
            }
            return UrlEncode(value, Encoding.UTF8);
        }

        public static string UrlEncode(string str, Encoding e)
        {
            if (str == null)
            {
                return null;
            }
            return Encoding.ASCII.GetString(UrlEncodeToBytes(str, e));
        }

        private static byte[] UrlEncode(byte[] bytes, int offset, int count)
        {
            if (!ValidateUrlEncodingParameters(bytes, offset, count))
            {
                return null;
            }
            int num = 0;
            int num2 = 0;
            for (int i = 0; i < count; i++)
            {
                char ch = (char)bytes[offset + i];
                if (ch == CharUtil.Whitespace)
                {
                    num++;
                }
                else if (!IsUrlSafeChar(ch))
                {
                    num2++;
                }
            }
            if ((num == 0) && (num2 == 0))
            {
                return bytes;
            }
            byte[] buffer = new byte[count + (num2 * 2)];
            int num4 = 0;
            for (int j = 0; j < count; j++)
            {
                byte num6 = bytes[offset + j];
                char ch2 = (char)num6;
                if (IsUrlSafeChar(ch2))
                {
                    buffer[num4++] = num6;
                }
                else if (ch2 == CharUtil.Whitespace)
                {
                    buffer[num4++] = 43;
                }
                else
                {
                    buffer[num4++] = 37;
                    buffer[num4++] = (byte)IntToHex((num6 >> 4) & 15);
                    buffer[num4++] = (byte)IntToHex(num6 & 15);
                }
            }
            return buffer;
        }

        private static byte[] UrlEncode(byte[] bytes, int offset, int count, bool alwaysCreateNewReturnValue)
        {
            byte[] buffer = UrlEncode(bytes, offset, count);
            if ((alwaysCreateNewReturnValue && (buffer != null)) && (buffer == bytes))
            {
                return (byte[])buffer.Clone();
            }
            return buffer;
        }

        public static string UrlEncodeUnicode(string str)
        {
            if (str == null)
            {
                return null;
            }
            return UrlEncodeUnicodeStringToString(str, false);
        }

        public static string UrlEncodeUnicode(string str, bool ignoreAscii)
        {
            if (str == null)
            {
                return null;
            }
            return UrlEncodeUnicodeStringToString(str, ignoreAscii);
        }

        private static string UrlEncodeUnicodeStringToString(string s, bool ignoreAscii)
        {
            int len = s.Length;
            StringBuilder sb = new StringBuilder(len);
            for (int i = 0; i < len; i++)
            {
                char ch = s[i];
                if ((ch & 65408) == 0)
                {
                    if (ignoreAscii || IsUrlSafeChar(ch))
                    {
                        sb.Append(ch);
                    }
                    else if (ch == CharUtil.Whitespace)
                    {
                        sb.Append(CharUtil.PlusSign);
                    }
                    else
                    {
                        sb.Append(CharUtil.PercentageSign);
                        sb.Append(IntToHex((ch >> 4) & '\x000f'));
                        sb.Append(IntToHex(ch & '\x000f'));
                    }
                }
                else
                {
                    sb.Append("%u");
                    sb.Append(IntToHex((ch >> 12) & '\x000f'));
                    sb.Append(IntToHex((ch >> 8) & '\x000f'));
                    sb.Append(IntToHex((ch >> 4) & '\x000f'));
                    sb.Append(IntToHex(ch & '\x000f'));
                }
            }
            return sb.ToString();
        }

        public static string UrlEncodeKeyValue(string keyOrValue)
        {
            if ((keyOrValue == null) || (keyOrValue.Length == 0))
            {
                return keyOrValue;
            }
            StringBuilder sb = new StringBuilder((int)Byte.MaxValue);
            HtmlTextWriter tw = new HtmlTextWriter(new StringWriter(sb, CultureInfo.InvariantCulture));
            UrlEncodeKeyValue(keyOrValue, tw);
            return sb.ToString();
        }

        public static void UrlEncodeKeyValue(string keyOrValue, TextWriter tw)
        {
            if ((keyOrValue == null) || (keyOrValue.Length == 0) || (tw == null))
            {
                return;
            }
            bool nextChar = false;
            int startIndex = 0;
            int len1 = 0;
            int len2 = keyOrValue.Length;
            for (int i = 0; i < len2; ++i)
            {
                char ch = keyOrValue[i];
                if (48 <= (int)ch && (int)ch <= 57 || 97 <= (int)ch && (int)ch <= 122 || 65 <= (int)ch && (int)ch <= 90)
                {
                    ++len1;
                }
                else
                {
                    if (len1 > 0)
                    {
                        tw.Write(keyOrValue.Substring(startIndex, len1));
                        len1 = 0;
                    }
                    UrlEncodeUnicodeChar(tw, keyOrValue[i], (i < len2 - 1 ? keyOrValue[i + 1] : Char.MinValue), out nextChar);
                    if (nextChar)
                    {
                        ++i;
                        startIndex = (i + 1);
                    }
                }
            }
            if ((startIndex >= len2) || (tw == null))
            {
                return;
            }
            tw.Write(keyOrValue.Substring(startIndex));
        }

        public static void UrlEncodeUnicodeChar(TextWriter tw, char ch, char chNext, out bool usedChar)
        {
            bool invalidUnicode = false;
            UrlEncodeUnicodeChar(tw, ch, chNext, ref invalidUnicode, out usedChar);
        }

        public static void UrlEncodeUnicodeChar(TextWriter tw, char ch, char chNext, ref bool invalidUnicode, out bool usedChar)
        {
            int num1 = 192;
            int num2 = 224;
            int num3 = 240;
            int num4 = 128;
            int num5 = 55296;
            int num6 = 64512;
            int num7 = 65536;
            usedChar = false;
            int index1 = (int)ch;
            if (index1 <= (int)SByte.MaxValue)
            {
                tw.Write(UnicodeEncodeHexStrs[index1]);
            }
            else if (index1 < 2047)
            {
                int index2 = (num1 | index1 >> 6);
                tw.Write(UnicodeEncodeHexStrs[index2]);
                int index3 = (num4 | index1 & 63);
                tw.Write(UnicodeEncodeHexStrs[index3]);
            }
            else if ((index1 & num6) != num5)
            {
                int index2 = (num2 | index1 >> 12);
                tw.Write(UnicodeEncodeHexStrs[index2]);
                int index3 = (num4 | (index1 & 4032) >> 6);
                tw.Write(UnicodeEncodeHexStrs[index3]);
                int index4 = (num4 | index1 & 63);
                tw.Write(UnicodeEncodeHexStrs[index4]);
            }
            else if ((int)chNext != 0)
            {
                int num8 = ((index1 & 1023) << 10);
                usedChar = true;
                int num9 = ((num8 | (int)chNext & 1023) + num7);
                int index2 = (num3 | num9 >> 18);
                tw.Write(UnicodeEncodeHexStrs[index2]);
                int index3 = (num4 | (num9 & 258048) >> 12);
                tw.Write(UnicodeEncodeHexStrs[index3]);
                int index4 = (num4 | (num9 & 4032) >> 6);
                tw.Write(UnicodeEncodeHexStrs[index4]);
                int index5 = (num4 | num9 & 63);
                tw.Write(UnicodeEncodeHexStrs[index5]);
            }
            else
            {
                invalidUnicode = true;
            }
        }

        public static string UrlEncodeNonAscii(string str, Encoding e)
        {
            if (StringUtil.IsNullOrWhitespace(str))
            {
                return str;
            }
            if (e == null)
            {
                e = Encoding.UTF8;
            }
            byte[] bytes = e.GetBytes(str);
            bytes = UrlEncodeBytesToBytesNonAscii(bytes, 0, bytes.Length, false);
            return Encoding.ASCII.GetString(bytes);
        }

        private static byte[] UrlEncodeBytesToBytesNonAscii(byte[] bytes, int offset, int count, bool alwaysCreateVal)
        {
            int num = 0;
            for (int i = 0; i < count; i++)
            {
                if (IsNonAsciiByte(bytes[offset + i]))
                {
                    num++;
                }
            }
            if (!alwaysCreateVal && (num == 0))
            {
                return bytes;
            }
            byte[] buffer = new byte[count + (num * 2)];
            int num1 = 0;
            for (int j = 0; j < count; j++)
            {
                byte b = bytes[offset + j];
                if (IsNonAsciiByte(b))
                {
                    buffer[num1++] = 37;
                    buffer[num1++] = (byte)IntToHex((b >> 4) & 15);
                    buffer[num1++] = (byte)IntToHex(b & 15);
                }
                else
                {
                    buffer[num1++] = b;
                }
            }
            return buffer;
        }

        public static byte[] UrlEncodeToBytes(string str, Encoding e)
        {
            if (str == null)
            {
                return null;
            }
            byte[] bytes = e.GetBytes(str);
            return UrlEncode(bytes, 0, bytes.Length, false);
        }

        public static string UrlEncodeSpaces(string str)
        {
            if ((str != null) && (str.IndexOf(CharUtil.Whitespace) >= 0))
            {
                str = str.Replace(StringUtil.Whitespace, "%20");
            }
            return str;
        }

        public static string UrlPathEncode(string str)
        {
            if (str == null)
            {
                return null;
            }
            int index = str.IndexOf(CharUtil.QuestionMark);
            if (index >= 0)
            {
                return (UrlPathEncode(str.Substring(0, index)) + str.Substring(index));
            }
            return UrlEncodeSpaces(UrlEncodeNonAscii(str, Encoding.UTF8));
        }

        public static char IntToHex(int n)
        {
            if (n <= 9)
            {
                return (char)(n + 48);
            }
            return (char)((n - 10) + 97);
        }

        public static string UrlDecode(string str, Encoding e)
        {
            if (str == null)
            {
                return null;
            }
            return UrlDecodeStringFromString(str, e);
        }

        private static string UrlDecodeStringFromString(string s, Encoding e)
        {
            int len = s.Length;
            UrlDecoder decoder = new UrlDecoder(len, e);
            for (int i = 0; i < len; i++)
            {
                char ch = s[i];
                if (ch == CharUtil.PlusSign)
                {
                    ch = CharUtil.Whitespace;
                }
                else if ((ch == CharUtil.PercentageSign) && (i < (len - 2)))
                {
                    if ((s[i + 1] == 'u') && (i < (len - 5)))
                    {
                        int num1 = HexToInt(s[i + 2]);
                        int num2 = HexToInt(s[i + 3]);
                        int num3 = HexToInt(s[i + 4]);
                        int num4 = HexToInt(s[i + 5]);
                        if (((num1 < 0) || (num2 < 0)) || ((num3 < 0) || (num4 < 0)))
                        {
                            goto AddChars;
                        }
                        ch = (char)((((num1 << 12) | (num2 << 8)) | (num3 << 4)) | num4);
                        i += 5;
                        decoder.AddChar(ch);
                        continue;
                    }
                    int num5 = HexToInt(s[i + 1]);
                    int num6 = HexToInt(s[i + 2]);
                    if ((num5 >= 0) && (num6 >= 0))
                    {
                        byte b = (byte)((num5 << 4) | num6);
                        i += 2;
                        decoder.AddByte(b);
                        continue;
                    }
                }
            AddChars:
                if ((ch & 65408) == 0)
                {
                    decoder.AddByte((byte)ch);
                }
                else
                {
                    decoder.AddChar(ch);
                }
            }
            return decoder.GetString();
        }

        public static int HexToInt(char h)
        {
            if ((h >= '0') && (h <= '9'))
            {
                return (h - '0');
            }
            if ((h >= 'a') && (h <= 'f'))
            {
                return ((h - 'a') + 10);
            }
            if ((h >= 'A') && (h <= 'F'))
            {
                return ((h - 'A') + 10);
            }
            return -1;
        }

        public static unsafe void HtmlEncode(string value, TextWriter output)
        {
            if (value != null)
            {
                if (output == null)
                {
                    throw new ArgumentNullException("output");
                }
                int num = IndexOfHtmlEncodingChars(value, 0);
                if (num == -1)
                {
                    output.Write(value);
                }
                else
                {
                    int num2 = value.Length - num;
                    fixed (char* str = value)
                    {
                        char* chPtr2 = str;
                        while (num-- > 0)
                        {
                            chPtr2++;
                            output.Write(chPtr2[0]);
                        }
                        while (num2-- > 0)
                        {
                            chPtr2++;
                            char ch = chPtr2[0];
                            if (ch <= '>')
                            {
                                switch (ch)
                                {
                                    case '&':
                                        {
                                            output.Write("&amp;");
                                            continue;
                                        }
                                    case '\'':
                                        {
                                            output.Write("&#39;");
                                            continue;
                                        }
                                    case '"':
                                        {
                                            output.Write("&quot;");
                                            continue;
                                        }
                                    case '<':
                                        {
                                            output.Write("&lt;");
                                            continue;
                                        }
                                    case '>':
                                        {
                                            output.Write("&gt;");
                                            continue;
                                        }
                                }
                                output.Write(ch);
                                continue;
                            }
                            if ((ch >= '\x00a0') && (ch < 'Ā'))
                            {
                                output.Write("&#");
                                output.Write(((int)ch).ToString(NumberFormatInfo.InvariantInfo));
                                output.Write(';');
                            }
                            else
                            {
                                output.Write(ch);
                            }
                        }
                    }
                }
            }
        }

        private static unsafe int IndexOfHtmlEncodingChars(string s, int startPos)
        {
            int num = s.Length - startPos;
            fixed (char* str = s)
            {
                char* chPtr = str;
                char* chPtr2 = chPtr + startPos;
                while (num > 0)
                {
                    char ch = chPtr2[0];
                    if (ch <= '>')
                    {
                        switch (ch)
                        {
                            case '&':
                            case '\'':
                            case '"':
                            case '<':
                            case '>':
                                return (s.Length - num);
                        }
                    }
                    else if ((ch >= '\x00a0') && (ch < 'Ā'))
                    {
                        return (s.Length - num);
                    }
                    chPtr2++;
                    num--;
                }
            }
            return -1;
        }

        private static bool ValidateUrlEncodingParameters(byte[] bytes, int offset, int count)
        {
            if ((bytes == null) && (count == 0))
            {
                return false;
            }
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }
            if ((offset < 0) || (offset > bytes.Length))
            {
                throw new ArgumentOutOfRangeException("offset");
            }
            if ((count < 0) || ((offset + count) > bytes.Length))
            {
                throw new ArgumentOutOfRangeException("count");
            }
            return true;
        }

        public static bool IsUrlSafeChar(char ch)
        {
            if ((((ch >= 'a') && (ch <= 'z')) || ((ch >= 'A') && (ch <= 'Z'))) || ((ch >= '0') && (ch <= '9')))
            {
                return true;
            }
            switch (ch)
            {
                case '(':
                case ')':
                case '*':
                case '-':
                case '.':
                case '_':
                case '!':
                    return true;
            }
            return false;
        }

        public static string AppendSlashToPath(string path)
        {
            if (path == null)
            {
                return null;
            }
            int length = path.Length;
            if ((length != 0) && (path[length - 1] != CharUtil.ForwardSlash))
            {
                path = path + CharUtil.ForwardSlash;
            }
            return path;
        }

        public static void CheckRelativePath(string relativePath)
        {
            if (!relativePath.IsNullOrWhitespace())
            {
                try
                {
                    relativePath = relativePath.Replace(CharUtil.BackSlash, CharUtil.ForwardSlash);
                    Uri uri = new Uri(relativePath, UriKind.RelativeOrAbsolute);
                }
                catch (Exception)
                {
                }
            }
        }

        public static string ConcatUrls(params string[] parts)
        {
            if ((parts == null) || (parts.Length == 0))
            {
                return String.Empty;
            }
            string firstPart = parts[0];
            int index = 1;
            while (index < parts.Length)
            {
                firstPart = ConcatUrls(firstPart, parts[index], CharUtil.ForwardSlash.ToString());
                checked { ++index; }
            }
            return firstPart;
        }

        public static string ConcatUrls(string firstPart, string secondPart)
        {
            return ConcatUrls(firstPart, secondPart, CharUtil.ForwardSlash.ToString());
        }

        public static string ConcatUrls(string firstPart, string secondPart, string separator)
        {
            if (firstPart.EndsWith(separator, StringComparison.OrdinalIgnoreCase))
            {
                if (secondPart.StartsWith(separator, StringComparison.OrdinalIgnoreCase))
                {
                    firstPart = (firstPart.TrimEnd(separator.ToCharArray()));
                }
                return (firstPart + secondPart);
            }
            else if (secondPart.StartsWith(separator, StringComparison.OrdinalIgnoreCase))
            {
                return (firstPart + secondPart);
            }
            else
            {
                return (firstPart + separator + secondPart);
            }
        }

        public static Uri Combine(string baseUri, string relativePath)
        {
            Uri uri = new Uri(baseUri);
            return Combine(uri, relativePath);
        }

        public static Uri Combine(Uri baseUri, string relativePath)
        {
            if (baseUri == null)
            {
                throw new ArgumentNullException("baseUri");
            }
            if (relativePath == null)
            {
                throw new ArgumentNullException("relativePath");
            }
            UriBuilder blder = new UriBuilder(baseUri);
            blder.Path = blder.Path.TrimEnd(_trimChars);
            blder.Path = AppendSlashToPath(blder.Path);
            if (relativePath.StartsWith(CharUtil.ForwardSlash.ToString(), StringComparison.InvariantCulture))
            {
                relativePath = relativePath.TrimStart(_trimChars);
            }
            CheckRelativePath(relativePath);
            return new Uri(blder.Uri, new Uri(relativePath, UriKind.Relative));
        }

        public static string CombinePath(string path1, string path2)
        {
            if (path1.IsNullOrWhitespace())
            {
                return path2;
            }
            if (path2.IsNullOrWhitespace())
            {
                return path1;
            }
            return String.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[] { path1.TrimEnd(_trimChars), path2.TrimStart(_trimChars) });
        }

        public static string ReplaceFileNameInUrl(string oldUrl, string newFileName)
        {
            if (oldUrl.Contains(CharUtil.ForwardSlash.ToString()))
            {
                return newFileName;
            }
            return ConcatUrls(oldUrl.Substring(0, checked(oldUrl.LastIndexOf(CharUtil.ForwardSlash) + 1)), newFileName);
        }

        public static string RemoveTrailingSlash(string source)
        {
            if (source.IsNullOrWhitespace())
            {
                return String.Empty;
            }
            if (source.EndsWith(CharUtil.BackSlash.ToString()) ||
                source.EndsWith(CharUtil.ForwardSlash.ToString()))
            {
                return source.Substring(0, source.Length - 1);
            }
            return source;
        }

        public static bool IsSameMachine(string host1, string host2)
        {
            bool flag = false;
            if ((host1.IsNullOrWhitespace()) || (host2.IsNullOrWhitespace()))
            {
                return false;
            }
            if (String.Equals(host1, host2, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            try
            {
                string hostName1 = Dns.GetHostEntry(host1).HostName;
                string hostName2 = Dns.GetHostEntry(host2).HostName;
                flag = String.Equals(hostName1, hostName2, StringComparison.OrdinalIgnoreCase);
            }
            catch (SocketException)
            {
            }
            return flag;
        }

        public static bool IsPathOnSameServer(string absoluteUri, Uri currentRequestUri)
        {
            Uri uri;
            if (!Uri.TryCreate(absoluteUri, UriKind.Absolute, out uri))
            {
                if ((!IsRooted(absoluteUri)) && (!IsRelativeUri(absoluteUri)))
                {
                    return false;
                }
                return (!absoluteUri.TrimStart(CharUtil.Whitespace).StartsWith(CharUtil.ForwardSlash.ToString()));
            }
            if (uri.IsLoopback)
            {
                return String.Equals(currentRequestUri.Host, uri.Host, StringComparison.OrdinalIgnoreCase);
            }
            return true;
        }

        public static bool IsUrlCharSafe(char ch)
        {
            if ((((ch >= 'a') && (ch <= 'z')) || ((ch >= 'A') && (ch <= 'Z'))) || ((ch >= '0') && (ch <= '9')))
            {
                return true;
            }
            switch (ch)
            {
                case '(':
                case ')':
                case '*':
                case '-':
                case '.':
                case '_':
                case '!':
                    return true;
            }
            return false;
        }

        public static Uri StripEnd(Uri uri, string[] stringsToStrip)
        {
            string absoluteUri = uri.AbsoluteUri;
            string uriString = StringUtil.StripEnd(absoluteUri, stringsToStrip);
            if (absoluteUri != uriString)
            {
                return new Uri(uriString);
            }
            return uri;
        }

        public static bool IsValidVirtualPathWithoutProtocol(string uri)
        {
            if (uri == null)
            {
                return false;
            }
            if (uri.IndexOf(CharUtil.Colon.ToString(), StringComparison.Ordinal) != 1)
            {
                return false;
            }
            return true;
        }

        public static bool IsProtocolAllowed(string fullOrRelativeUri)
        {
            return IsProtocolAllowed(fullOrRelativeUri, true);
        }

        public static bool IsProtocolAllowed(string fullOrRelativeUri, bool allowRelativeUri)
        {
            if ((fullOrRelativeUri == null) || (fullOrRelativeUri.Length <= 0))
            {
                return allowRelativeUri;
            }
            fullOrRelativeUri = fullOrRelativeUri.Split(CharUtil.QuestionMark)[0];
            if (fullOrRelativeUri.IndexOf(CharUtil.Colon) == -1)
            {
                return allowRelativeUri;
            }
            if (_webProtocols != null)
            {
                fullOrRelativeUri = fullOrRelativeUri.ToLower(CultureInfo.InvariantCulture).TrimStart(new char[0]);
                foreach (string str in _webProtocols)
                {
                    if (fullOrRelativeUri.StartsWith(str))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool HasTrailingSlash(string uri)
        {
            return (uri[uri.Length - 1] == CharUtil.ForwardSlash);
        }

        public static bool IsRooted(string baseUri)
        {
            if ((!baseUri.IsNullOrWhitespace()) && (baseUri[0] != CharUtil.ForwardSlash))
            {
                return (baseUri[0] == CharUtil.BackSlash);
            }
            return true;
        }

        public static bool IsDirectorySeparatorChar(char ch)
        {
            if (ch != CharUtil.BackSlash)
            {
                return (ch == CharUtil.ForwardSlash);
            }
            return true;
        }

        public static bool IsRelativeUri(string virtualUri)
        {
            if (virtualUri.IndexOf(CharUtil.Colon.ToString(), StringComparison.Ordinal) != -1)
            {
                return false;
            }
            return !IsRooted(virtualUri);
        }

        public static bool IsAbsolutePhysicalPath(string uri)
        {
            if ((uri == null) || (uri.Length < 3))
            {
                return false;
            }
            return (((uri[1] == CharUtil.Colon) && IsDirectorySeparatorChar(uri[2])) || IsUncSharePath(uri));
        }

        public static bool IsUncSharePath(string uri)
        {
            return (((uri.Length > 2) && IsDirectorySeparatorChar(uri[0])) && IsDirectorySeparatorChar(uri[1]));
        }

        public static bool IsAppRelativePath(string uri)
        {
            if (uri == null)
            {
                return false;
            }
            int len = uri.Length;
            if (len == 0)
            {
                return false;
            }
            if (uri[0] != CharUtil.Tilde)
            {
                return false;
            }
            if ((len != 1) && (uri[1] != CharUtil.BackSlash))
            {
                return (uri[1] == CharUtil.ForwardSlash);
            }
            return true;
        }

        public static bool IsUriFull(string uri)
        {
            Uri url;
            return Uri.TryCreate(uri, UriKind.Absolute, out url);
        }

        public static bool IsUriRelative(string uri)
        {
            Uri url;
            return Uri.TryCreate(uri, UriKind.Relative, out url);
        }

        public static string ConstructUriWithParams(string url, string name, string value)
        {
            NameValueCollection nvc = new NameValueCollection(1);
            nvc.Add(name, value);
            return ConstructUriWithParams(url, nvc);
        }

        public static string ConstructUriWithParams(string url, NameValueCollection queryParams)
        {
            if (String.IsNullOrEmpty(url))
            {
                return null;
            }
            StringBuilder sb = new StringBuilder();
            string str = url;
            int index = url.IndexOf(CharUtil.QuestionMark);
            if (index > -1)
            {
                str = url.Substring(0, index);
            }
            bool flag = true;
            foreach (string queryStrs in queryParams.Keys)
            {
                string param = CharUtil.Ampersand.ToString();
                if (flag)
                {
                    param = CharUtil.QuestionMark.ToString();
                    flag = false;
                }
                string final = str;
                sb.Append(final);
                sb.Append(param);
                sb.Append(queryStrs);
                sb.Append(CharUtil.EqualSign.ToString());
                sb.Append(queryParams[queryStrs]);
            }
            return sb.ToString();
        }

        public static IList<String> ExtractQueryStringKeys(Uri sourceUri)
        {
            if (sourceUri == null)
            {
                return new List<String>();
            }
            List<String> list = new List<string>();
            string query = sourceUri.Query;
            if ((!String.IsNullOrEmpty(query)) && (query.Length > 1))
            {
                query = query.Substring(1);
                StringSplitOptions removeEntries = StringSplitOptions.RemoveEmptyEntries;
                foreach (string str in query.Split(new char[] { CharUtil.Ampersand }, removeEntries))
                {
                    list.Add(str.Split(new char[] { CharUtil.EqualSign }, removeEntries)[0]);
                }
            }
            return list;
        }

        public static string MakeVirtualPathAppAbsolute(string virtualUri)
        {
            return MakeVirtualPathAppAbsolute(virtualUri, HttpRuntime.AppDomainAppVirtualPath);
        }

        public static string MakeVirtualPathAppAbsolute(string virtualUri, string appPath)
        {
            if ((virtualUri.Length == 1) && (virtualUri[0] == CharUtil.Tilde))
            {
                return appPath;
            }
            if (((virtualUri.Length >= 2) && (virtualUri[0] == CharUtil.Tilde)) && ((virtualUri[1] == CharUtil.ForwardSlash) || (virtualUri[1] == CharUtil.BackSlash)))
            {
                if (appPath.Length > 1)
                {
                    return (appPath + virtualUri.Substring(2));
                }
                return (CharUtil.ForwardSlash + virtualUri.Substring(2));
            }
            if (!IsRooted(virtualUri))
            {
                return String.Empty;
            }
            return virtualUri;
        }

        public static string MakeVirtualPathAppRelative(string virtualUri)
        {
            return MakeVirtualPathAppRelative(virtualUri, HttpRuntime.AppDomainAppVirtualPath, false);
        }

        public static string MakeVirtualPathAppRelative(string virtualUri, string appPath, bool nullIfNotApp)
        {
            if (virtualUri == null)
            {
                return String.Empty;
            }
            int length = appPath.Length;
            int num1 = virtualUri.Length;
            if ((num1 == (length - 1)) && (StringUtil.StringStartsWithIgnoreCase(appPath, virtualUri)))
            {
                return _virtualSlash;
            }
            if (!VirtualPathStartsWithVirtualPath(virtualUri, appPath))
            {
                if (nullIfNotApp)
                {
                    return null;
                }
                return virtualUri;
            }
            if (num1 == length)
            {
                return _virtualSlash;
            }
            if (length == 1)
            {
                return (CharUtil.Tilde + virtualUri);
            }
            return (CharUtil.Tilde + virtualUri.Substring(length - 1));
        }

        private static bool VirtualPathStartsWithVirtualPath(string virtualUri1, string virtualUri2)
        {
            if (virtualUri1 == null)
            {
                return false;
            }
            if (virtualUri2 == null)
            {
                return false;
            }
            if (!StringUtil.StringStartsWithIgnoreCase(virtualUri1, virtualUri2))
            {
                return false;
            }
            int length = virtualUri2.Length;
            if (virtualUri1.Length != length)
            {
                if (length == 1)
                {
                    return true;
                }
                if (virtualUri2[length - 1] == CharUtil.ForwardSlash)
                {
                    return true;
                }
                if (virtualUri1[length] != CharUtil.ForwardSlash)
                {
                    return false;
                }
            }
            return true;
        }

        public static string RemoveUriKey(string uri, string keyName)
        {
            if (uri == null)
            {
                return null;
            }
            int index = uri.IndexOf(CharUtil.Ampersand + keyName + CharUtil.EqualSign, StringComparison.OrdinalIgnoreCase);
            if (index < 0)
            {
                index = uri.IndexOf(CharUtil.QuestionMark + keyName + CharUtil.EqualSign, StringComparison.OrdinalIgnoreCase);
            }
            if (index < 0)
            {
                return uri;
            }
            int num = uri.IndexOf(CharUtil.Ampersand, index + 1);
            if (num < 0)
            {
                return uri.Substring(0, index);
            }
            return uri.Remove(index + 1, num - index);
        }

        public static string AppendSlashToUri(string uri)
        {
            if (String.IsNullOrEmpty(uri))
            {
                return uri;
            }
            string path = uri;
            string fileName = Path.GetFileName(path);
            if ((String.IsNullOrEmpty(fileName) || (fileName.IndexOf(CharUtil.Period) == -1)) && !path.EndsWith(CharUtil.ForwardSlash.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                path = path + CharUtil.ForwardSlash;
            }
            return path;
        }

        public static string BuildQueryStrings(NameValueCollection col, bool append)
        {
            return String.Format(CultureInfo.InvariantCulture, "{0}{1}", new object[2]
            {
                append ? CharUtil.Ampersand : CharUtil.QuestionMark,
                (object) string.Join(CharUtil.Ampersand.ToString(), Array.ConvertAll<String, String>(col.AllKeys, (Converter<String, String>) (key => String.Format(CultureInfo.InvariantCulture, "{0}={1}", new object[2]
                {
                    HttpUtility.UrlEncode(key),
                    UrlEncodeKeyValue(col[key])
                }))))
            });
        }

        public static Uri ConvertUrlToUseSsl(Uri url, ushort sslPort)
        {
            if (!url.Scheme.Equals(HttpStr))
            {
                throw new InvalidOperationException();
            }
            string https = _webProtocols[1];
            string host = url.Host;
            if (sslPort != 443)
            {
                host = (host + ":" + sslPort.ToString(NumberFormatInfo.InvariantInfo));
            }
            return new Uri(https + host + url.PathAndQuery);
        }

        public static string StripUrlFileExtension(string url)
        {
            int length = url.LastIndexOf(CharUtil.Period);
            if (length >= 0)
            {
                return url.Substring(0, length);
            }
            return url;
        }

        public static bool EndsWithAspNetSuffix(string str)
        {
            for (int i = 0; i < SharePointUtil.AspWebSuffixes.Length; ++i)
            {
                if (StringUtil.StringSuffixEndsWith(str, SharePointUtil.AspWebSuffixes[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public static string EnsureUrlStartsWithSlash(string url)
        {
            if (StringUtil.IsNullOrWhitespace(url))
            {
                return CharUtil.ForwardSlash.ToString();
            }
            if (!url.StartsWith(CharUtil.ForwardSlash.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                return (CharUtil.ForwardSlash + url);
            }
            return url;
        }

        public static string EnsureUrlStartsAndEndsWithSlash(string url)
        {
            return EnsureUrlStartsWithSlash(AppendSlashToPath(url));
        }

        public static string GetUrlKeyValue(string url, string keyName)
        {
            int start;
            int index;
            return GetUrlKeyValue(url, keyName, out start, out index);
        }

        public static string GetUrlKeyValue(string url, string keyName, out int start, out int index)
        {
            string str = null;
            if (LocateUrlKeyValue(url, keyName, out start, out index))
            {
                str = HttpUtility.UrlDecode(url.Substring(start, (index - start)));
            }
            return str;
        }

        public static bool LocateUrlKeyValue(string url, string keyName, out int start, out int index)
        {
            start = 0;
            index = 0;
            if (url == null)
            {
                return false;
            }
            int num = url.IndexOf(CharUtil.Ampersand + keyName + CharUtil.EqualSign, StringComparison.OrdinalIgnoreCase);
            if (num < 0)
            {
                num = url.IndexOf(CharUtil.QuestionMark + keyName + CharUtil.EqualSign, StringComparison.OrdinalIgnoreCase);
            }
            if (num < 0)
            {
                return false;
            }
            index = url.IndexOf(CharUtil.Ampersand, (num + 1));
            if (index < 0)
            {
                index = url.Length;
            }
            start = (num + keyName.Length + 2);
            return true;
        }

        public static string MakeUrlWithKeyValue(string url, string key, string value)
        {
            if (url == null)
            {
                return null;
            }
            StringBuilder sb = new StringBuilder(url.Length + key.Length + value.Length + 2);
            int count = url.IndexOf(CharUtil.QuestionMark);
            if (count < 0)
            {
                count = url.IndexOf(CharUtil.PoundSign);
            }
            if (count < 0)
            {
                sb.Append(url);
            }
            else
            {
                sb.Append(url, 0, count);
            }
            sb.Append(CharUtil.QuestionMark);
            sb.Append(key);
            sb.Append(CharUtil.EqualSign);
            sb.Append(value);
            return sb.ToString();
        }

        public static List<String> GetQueryStringKeys(Uri uri)
        {
            NullUtil.CheckForNull(uri, "uri");
            List<String> list = new List<String>();
            string query = uri.Query;
            if ((StringUtil.IsNotNullOrWhitespace(query)) && (query.Length > 1))
            {
                string str = query.Substring(1);
                StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries;
                string str2 = str;
                char[] sep = new char[1] { CharUtil.Ampersand };
                int num = (int)options;
                foreach (string str3 in str2.Split(sep, options))
                {
                    list.Add(str3.Split(new char[1] { CharUtil.EqualSign }, options)[0]);
                }
            }
            return list;
        }

        public static void SplitUrl(string fullOrRelativeUri, out string dirName, out string leafName)
        {
            if (fullOrRelativeUri == null)
            {
                dirName = String.Empty;
                leafName = String.Empty;
            }
            else
            {
                if ((fullOrRelativeUri.Length > 0) && ((int)fullOrRelativeUri[0] == 47))
                {
                    fullOrRelativeUri = fullOrRelativeUri.Substring(1);
                }
                int len = fullOrRelativeUri.LastIndexOf(CharUtil.ForwardSlash);
                if (len == -1)
                {
                    dirName = String.Empty;
                    if (fullOrRelativeUri.Length > 0)
                    {
                        leafName = (int)fullOrRelativeUri[0] == 47 ? fullOrRelativeUri.Substring(1) : fullOrRelativeUri;
                    }
                    else
                    {
                        leafName = String.Empty;
                    }
                }
                else
                {
                    dirName = fullOrRelativeUri.Substring(0, len);
                    leafName = fullOrRelativeUri.Substring(len + 1);
                }
            }
        }

        public static string GetUrlFileName(string url)
        {
            NullUtil.CheckForNull(url, "url");
            int len = url.IndexOf(CharUtil.QuestionMark);
            if (len >= 0)
            {
                url = url.Substring(0, len);
            }
            string dirName;
            string leafName;
            SplitUrl(url, out dirName, out leafName);
            return leafName;
        }

        public static bool GetFileNameWithoutExtension(string fileName, string[] validExtensions, out string strippedFileName)
        {
            strippedFileName = fileName;
            int num = strippedFileName.LastIndexOf(CharUtil.Period.ToString(), StringComparison.Ordinal);
            if (num >= 0)
            {
                string str = strippedFileName.Substring(num, checked(strippedFileName.Length - num));
                foreach (string str2 in validExtensions)
                {
                    if (String.Compare(str, str2, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        strippedFileName = fileName.Substring(0, num);
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool GetFileNameWithoutExtension(string fileName, string validExtensions, out string strippedFileName)
        {
            return GetFileNameWithoutExtension(fileName, new string[1] { validExtensions }, out strippedFileName);
        }

        public static string GetFileNameWithoutExtension(string fileName)
        {
            if (StringUtil.IsNullOrWhitespace(fileName))
            {
                return fileName;
            }
            int len = fileName.LastIndexOf(CharUtil.Period);
            if (len >= 0)
            {
                return fileName.Substring(0, len);
            }
            return fileName;
        }

        public static string GetUrlDirectory(string url)
        {
            NullUtil.CheckForNull(url, "url");
            int len = url.IndexOf(CharUtil.QuestionMark);
            if (len >= 0)
            {
                url = url.Substring(0, len);
            }
            string dirName;
            string leafName;
            SplitUrl(url, out dirName, out leafName);
            return dirName;
        }

        public static string GetHostWithPort(string url)
        {
            return GetUrlWithComponents(url, (UriComponents.Host | UriComponents.Port));
        }

        public static string GetUrlWithComponents(string url, UriComponents urlParts)
        {
            Uri nUrl = null;
            if (Uri.TryCreate(url, UriKind.Absolute, out nUrl))
            {
                return GetParts(nUrl, urlParts, UriFormat.UriEscaped);
            }
            return String.Empty;
        }

        public static string GetParts(Uri uri, UriComponents urlParts, UriFormat format)
        {
            return uri.GetComponents(urlParts, format);
        }

        #endregion

        #region "Accessors"

        public static string[] WebProtocols
        {
            get { return (_webProtocols.Clone() as string[]); }
        }

        #endregion
    }

    class UrlDecoder
    {
        #region "Properties"

        private int _bufferSize;
        private byte[] _byteBuffer;
        private char[] _charBuffer;
        private Encoding _encoding;
        private int _numOfBytes;
        private int _numOfChars;

        #endregion

        #region "Constructors"

        internal UrlDecoder(int bufferSize, Encoding encoding)
        {
            this._bufferSize = bufferSize;
            this._encoding = encoding;
            this._charBuffer = new char[bufferSize];
        }

        #endregion

        #region "Methods"

        internal void AddByte(byte b)
        {
            if (this._byteBuffer == null)
            {
                this._byteBuffer = new byte[this._bufferSize];
            }
            this._byteBuffer[this._numOfBytes++] = b;
        }

        internal void AddChar(char ch)
        {
            if (this._numOfBytes > 0)
            {
                this.FlushBytes();
            }
            this._charBuffer[this._numOfChars++] = ch;
        }

        private void FlushBytes()
        {
            if (this._numOfBytes > 0)
            {
                this._numOfChars += this._encoding.GetChars(this._byteBuffer, 0, this._numOfBytes, this._charBuffer, this._numOfChars);
                this._numOfBytes = 0;
            }
        }

        internal string GetString()
        {
            if (this._numOfBytes > 0)
            {
                this.FlushBytes();
            }
            if (this._numOfChars > 0)
            {
                return new String(this._charBuffer, 0, this._numOfChars);
            }
            return String.Empty;
        }

        #endregion
    }
}