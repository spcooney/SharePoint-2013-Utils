namespace Spc.SharePoint.Utils.Core.Helper
{
    using log4net;
    using System;
    using System.Globalization;
    using System.Text;

    /// <summary>
    /// Wrapper around the Log4Net library for simpler logging.
    /// </summary>
    public class Log4NetHelper
    {
        #region "Properties"

        private static readonly ILog _log;
        private static readonly ILog _traceLog;
        /// <summary>Core</summary>
        private const string CoreLogName = "Core";
        /// <summary>TraceFile</summary>
        private const string TraceLogName = "Trace";
        /// <summary>[{0}] {1}</summary>
        private const string TypeBldStr = "[{0}] {1}";
        /// <summary>[{0}] {1}.</summary>
        private const string TypePeriodBldStr = "[{0}] {1}.";

        #endregion

        #region "Constructors"

        static Log4NetHelper()
        {
            Log4NetHelper._log = LogManager.GetLogger(CoreLogName);
            Log4NetHelper._traceLog = LogManager.GetLogger(TraceLogName);
        }

        #endregion

        #region "Methods"

        public static ILog GetLogger(Type type)
        {
            return LogManager.GetLogger(type);
        }

        public static ILog GetLoggerByName(string logName)
        {
            return LogManager.GetLogger(logName);
        }

        public static void LogError(object message)
        {
            CoreLog.Error(message);
        }

        public static void LogError(Type type, object message)
        {
            CoreLog.Error(ReplaceNewLines(String.Format(TypeBldStr, type, message)));
        }

        public static void LogErrorDot(Type type, string message)
        {
            CoreLog.Error(ReplaceNewLines(String.Format(TypePeriodBldStr, type, message)));
        }

        public static void LogError(Exception ex)
        {
            CoreLog.Error(ReplaceNewLines(ex.Message + ex.StackTrace ?? ex.StackTrace));
        }

        public static void LogError(Type type, Exception ex)
        {
            CoreLog.Error(ReplaceNewLines(String.Format(TypeBldStr, type, (ex.Message + ex.StackTrace ?? ex.StackTrace))));
        }

        public static void LogError(Type type, object message, Exception ex)
        {
            CoreLog.Error(ReplaceNewLines(String.Format(TypeBldStr, type, (ex.Message + ex.StackTrace ?? ex.StackTrace))));
        }

        public static void LogInfo(Type type, object message)
        {
            CoreLog.Info(ReplaceNewLines(String.Format(TypeBldStr, type, message)));
        }

        public static void LogInfo(Type type, params object[] messages)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < messages.Length; i++)
            {
                sb.Append(messages[i]);
            }
            CoreLog.Info(ReplaceNewLines(String.Format(TypeBldStr, type, sb.ToString())));
        }

        /// <summary>
        /// Logs the message with a period at the end.
        /// </summary>
        /// <param name="type">The class instance type.</param>
        /// <param name="message">The message with a period at the end.</param>
        public static void LogInfoDot(Type type, object message)
        {
            CoreLog.Info(ReplaceNewLines(String.Format(TypePeriodBldStr, type, message)));
        }

        /// <summary>
        /// Uses the string format to write log information using the current culture.
        /// </summary>
        /// <param name="type">The class instance type.</param>
        /// <param name="format">The string format.</param>
        /// <param name="values">The string values that go with the format.</param>
        public static void LogInfoStrFormat(Type type, string format, params string[] values)
        {
            CoreLog.Info(ReplaceNewLines(String.Format(CultureInfo.CurrentCulture, format, values)));
        }

        /// <summary>
        /// Uses the string format to write log information using the provided culture.
        /// </summary>
        /// <param name="info">The culture information to format the string.</param>
        /// <param name="type">The class instance type.</param>
        /// <param name="format">The string format.</param>
        /// <param name="values">The string values that go with the format.</param>
        public static void LogInfoStrFormat(Type type, CultureInfo info, string format, params string[] values)
        {
            CoreLog.Info(ReplaceNewLines(String.Format(info, format, values)));
        }

        /// <summary>
        /// Removes carriage returns from the string so they stay on the same line in the log file.
        /// </summary>
        /// <param name="message">The log message.</param>
        /// <returns>A string without new lines.</returns>
        private static string ReplaceNewLines(string message)
        {
            message = message.Replace(Environment.NewLine, CharUtil.WhitespaceStr);
            return message.Replace('\t', CharUtil.Whitespace);
        }

        /// <summary>
        /// Writes messages to the trace logger.
        /// </summary>
        /// <param name="messages">The array of objects (strings) to write to trace log.</param>
        public static void WriteTrace(params object[] messages)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < messages.Length; i++)
            {
                sb.Append(messages[i]);
            }
            GetLoggerByName(TraceLogName).Info(sb.ToString());
        }

        /// <summary>
        /// Writes messages to the trace logger.
        /// </summary>
        /// <param name="messages">The array of objects (strings) to write to trace log.</param>
        public static void WriteTrace(Type type, params object[] messages)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < messages.Length; i++)
            {
                sb.Append(messages[i]);
            }
            GetLoggerByName(TraceLogName).Info(String.Format(TypeBldStr, type, sb.ToString()));
        }

        /// <summary>
        /// Writes messages to the trace logger.
        /// </summary>
        /// <param name="messages">The array of objects (strings) to write to trace log.</param>
        public static void WriteTrace(bool onlyDebug, Type type, params object[] messages)
        {
            if (onlyDebug)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < messages.Length; i++)
                {
                    sb.Append(messages[i]);
                }
#if DEBUG
                GetLoggerByName(TraceLogName).Info(String.Format(TypeBldStr, type, sb.ToString()));
#endif
            }
        }

        public static void WriteTraceInvariantFormat(Type type, string format, params object[] messages)
        {
            GetLoggerByName(TraceLogName).Info(String.Format(CultureInfo.InvariantCulture, format, messages));
        }

        public static void WriteTraceFormat(Type type, CultureInfo info, string format, params object[] messages)
        {
            GetLoggerByName(TraceLogName).Info(String.Format(info, format, messages));
        }

        #endregion

        #region "Properties"

        /// <summary>
        /// The core log instance under the logs directory.
        /// </summary>
        public static ILog CoreLog
        {
            get
            {
                return Log4NetHelper._log;
            }
        }

        /// <summary>
        /// The trace log instance under the trace directory.
        /// </summary>
        public static ILog TraceLog
        {
            get
            {
                return Log4NetHelper._traceLog;
            }
        }

        #endregion
    }
}