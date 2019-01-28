namespace Spc.SharePoint.Utils.Core.Helper
{
    public static class CharUtil
    {
        #region "Properties"

        private const char _atSign = '@';
        private const char _semiColon = ';';
        private const char _colon = ':';
        private const char _comma = ',';
        private const char _period = '.';
        private const char _backSlash = '\\';
        private const char _forwardSlash = '/';
        private const char _ampersand = '&';
        private const char _equalSign = '=';
        private const char _whitespace = ' ';
        private const char _tilde = '~';
        private const char _star = '*';
        private const char _doubleQuote = '"';
        private const char _singleQuote = '\'';
        private const char _questionMark = '?';
        private const char _figureDash = '-';
        private const char _plusSign = '+';
        private const char _underscore = '_';
        private const char _newLine = '\n';
        private const char _percentageSign = '%';
        private const char _carriageReturn = '\r';
        private const char _tab = '\t';
        private const char _nullChar = '\0';
        private const char _leftBracket = '[';
        private const char _rightBracket = ']';
        private const char _openBrace = '{';
        private const char _closeBrace = '}';
        private const char _poundSign = '#';
        private const char _verticalBar = '|';

        #endregion

        #region "Methods"

        public static char GetCharAt(ref string value, int index)
        {
            int length = value.Length;
            if (index < length)
            {
                return value[index];
            }
            return NullChar;
        }

        public static bool IsWhitespace(this char ch)
        {
            if (((ch != Whitespace) && (ch != Tab)) && (ch != CarriageReturn))
            {
                return (ch == NewLine);
            }
            return true;
        }

        public static bool IsDigit(this char ch)
        {
            return ((ch >= '0') && (ch <= '9'));
        }

        #endregion

        #region "Accessors"

        /// <summary>
        /// Returns the following character: '@'
        /// </summary>
        public static char AtSign
        {
            get { return _atSign; }
        }

        /// <summary>
        /// Returns the following character: "@"
        /// </summary>
        public static string AtSignStr
        {
            get { return AtSign.ToString(); }
        }

        /// <summary>
        /// Returns the following character: ';'
        /// </summary>
        public static char SemiColon
        {
            get { return _semiColon; }
        }

        /// <summary>
        /// Returns the following character: ";"
        /// </summary>
        public static string SemiColonStr
        {
            get { return SemiColon.ToString(); }
        }

        /// <summary>
        /// Returns the following character: ':'
        /// </summary>
        public static char Colon
        {
            get { return _colon; }
        }

        /// <summary>
        /// Returns the following character: ":"
        /// </summary>
        public static string ColonStr
        {
            get { return Colon.ToString(); }
        }

        /// <summary>
        /// Returns the following character: ','
        /// </summary>
        public static char Comma
        {
            get { return _comma; }
        }

        /// <summary>
        /// Returns the following character: ","
        /// </summary>
        public static string CommaStr
        {
            get { return Comma.ToString(); }
        }

        /// <summary>
        /// Returns the following character: '.'
        /// </summary>
        public static char Period
        {
            get { return _period; }
        }

        /// <summary>
        /// Returns the following character: "."
        /// </summary>
        public static string PeriodStr
        {
            get { return Period.ToString(); }
        }

        /// <summary>
        /// Returns the following character: '\\'
        /// </summary>
        public static char BackSlash
        {
            get { return _backSlash; }
        }

        /// <summary>
        /// Returns the following character: "\\"
        /// </summary>
        public static string BackSlashStr
        {
            get { return BackSlash.ToString(); }
        }

        /// <summary>
        /// Returns the following character: '/'
        /// </summary>
        public static char ForwardSlash
        {
            get { return _forwardSlash; }
        }

        /// <summary>
        /// Returns the following character: "/"
        /// </summary>
        public static string ForwardSlashStr
        {
            get { return ForwardSlash.ToString(); }
        }

        /// <summary>
        /// Returns the following character: '&'
        /// </summary>
        public static char Ampersand
        {
            get { return _ampersand; }
        }

        /// <summary>
        /// Returns the following character: "&"
        /// </summary>
        public static string AmpersandStr
        {
            get { return Ampersand.ToString(); }
        }

        /// <summary>
        /// Returns the following character: '='
        /// </summary>
        public static char EqualSign
        {
            get { return _equalSign; }
        }

        /// <summary>
        /// Returns the following character: "="
        /// </summary>
        public static string EqualSignStr
        {
            get { return EqualSign.ToString(); }
        }

        /// <summary>
        /// Returns the following character: ' '
        /// </summary>
        public static char Whitespace
        {
            get { return _whitespace; }
        }

        /// <summary>
        /// Returns the following character: " "
        /// </summary>
        public static string WhitespaceStr
        {
            get { return Whitespace.ToString(); }
        }

        /// <summary>
        /// Returns the following character: '~'
        /// </summary>
        public static char Tilde
        {
            get { return _tilde; }
        }

        /// <summary>
        /// Returns the following character: "~"
        /// </summary>
        public static string TildeStr
        {
            get { return Tilde.ToString(); }
        }

        /// <summary>
        /// Returns the following character: '*'
        /// </summary>
        public static char Star
        {
            get { return _star; }
        }

        /// <summary>
        /// Returns the following character: "*"
        /// </summary>
        public static string StarStr
        {
            get { return Star.ToString(); }
        }

        /// <summary>
        /// Returns the following character: '"'
        /// </summary>
        public static char DoubleQuote
        {
            get { return _doubleQuote; }
        }

        /// <summary>
        /// Returns the following character: """
        /// </summary>
        public static string DoubleQuoteStr
        {
            get { return DoubleQuote.ToString(); }
        }

        /// <summary>
        /// Returns the following character: '''
        /// </summary>
        public static char SingleQuote
        {
            get { return _singleQuote; }
        }

        /// <summary>
        /// Returns the following character: "'"
        /// </summary>
        public static string SingleQuoteStr
        {
            get { return SingleQuote.ToString(); }
        }

        /// <summary>
        /// Returns the following character: '?'
        /// </summary>
        public static char QuestionMark
        {
            get { return _questionMark; }
        }

        /// <summary>
        /// Returns the following character: "?"
        /// </summary>
        public static string QuestionMarkStr
        {
            get { return QuestionMark.ToString(); }
        }

        /// <summary>
        /// Returns the following character: '-'
        /// </summary>
        public static char FigureDash
        {
            get { return _figureDash; }
        }

        /// <summary>
        /// Returns the following character: "-"
        /// </summary>
        public static string FigureDashStr
        {
            get { return FigureDash.ToString(); }
        }

        /// <summary>
        /// Returns the following character: '+'
        /// </summary>
        public static char PlusSign
        {
            get { return _plusSign; }
        }

        /// <summary>
        /// Returns the following character: "+"
        /// </summary>
        public static string PlusSignStr
        {
            get { return PlusSign.ToString(); }
        }

        /// <summary>
        /// Returns the following character: '_'
        /// </summary>
        public static char Underscore
        {
            get { return _underscore; }
        }

        /// <summary>
        /// Returns the following character: "_"
        /// </summary>
        public static string UnderscoreStr
        {
            get { return Underscore.ToString(); }
        }

        /// <summary>
        /// Returns the following character: '\n'
        /// </summary>
        public static char NewLine
        {
            get { return _newLine; }
        }

        /// <summary>
        /// Returns the following character: "\n"
        /// </summary>
        public static string NewLineStr
        {
            get { return NewLine.ToString(); }
        }

        /// <summary>
        /// Returns the following character: '%'
        /// </summary>
        public static char PercentageSign
        {
            get { return _percentageSign; }
        }

        /// <summary>
        /// Returns the following character: "%"
        /// </summary>
        public static string PercentageSignStr
        {
            get { return PercentageSign.ToString(); }
        }

        /// <summary>
        /// Returns the following character: '\r'
        /// </summary>
        public static char CarriageReturn
        {
            get { return _carriageReturn; }
        }

        /// <summary>
        /// Returns the following character: "\r"
        /// </summary>
        public static string CarriageReturnStr
        {
            get { return CarriageReturn.ToString(); }
        }

        /// <summary>
        /// Returns the following character: '\t'
        /// </summary>
        public static char Tab
        {
            get { return _tab; }
        }

        /// <summary>
        /// Returns the following character: "\t"
        /// </summary>
        public static string TabStr
        {
            get { return Tab.ToString(); }
        }

        /// <summary>
        /// Returns the following character: '\0'
        /// </summary>
        public static char NullChar
        {
            get { return _nullChar; }
        }

        /// <summary>
        /// Returns the following character: "\0"
        /// </summary>
        public static string NullCharStr
        {
            get { return NullChar.ToString(); }
        }

        /// <summary>
        /// Returns the following character: '['
        /// </summary>
        public static char LeftBracket
        {
            get { return _leftBracket; }
        }

        /// <summary>
        /// Returns the following character: "["
        /// </summary>
        public static string LeftBracketStr
        {
            get { return LeftBracket.ToString(); }
        }

        /// <summary>
        /// Returns the following character: ']'
        /// </summary>
        public static char RightBracket
        {
            get { return _rightBracket; }
        }

        /// <summary>
        /// Returns the following character: "]"
        /// </summary>
        public static string RightBracketStr
        {
            get { return RightBracket.ToString(); }
        }

        /// <summary>
        /// Returns the following character: '{'
        /// </summary>
        public static char OpenBrace
        {
            get { return _openBrace; }
        }

        /// <summary>
        /// Returns the following character: "{"
        /// </summary>
        public static string OpenBraceStr
        {
            get { return OpenBraceStr.ToString(); }
        }

        /// <summary>
        /// Returns the following character: '}'
        /// </summary>
        public static char CloseBrace
        {
            get { return _closeBrace; }
        }

        /// <summary>
        /// Returns the following character: "}"
        /// </summary>
        public static string CloseBraceStr
        {
            get { return CloseBrace.ToString(); }
        }

        /// <summary>
        /// Returns the following character: '#'
        /// </summary>
        public static char PoundSign
        {
            get { return _poundSign; }
        }

        /// <summary>
        /// Returns the following character: "#"
        /// </summary>
        public static string PoundSignStr
        {
            get { return PoundSign.ToString(); }
        }

        /// <summary>
        /// Returns the following character: '|'
        /// </summary>
        public static char VerticalBar
        {
            get { return _verticalBar; }
        }

        /// <summary>
        /// Returns the following character: "|"
        /// </summary>
        public static string VerticalBarStr
        {
            get { return _verticalBar.ToString(); }
        }

        #endregion
    }
}