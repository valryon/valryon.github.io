using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace Portfolio.Utils.Log
{
    /// <summary>
    /// Sévérité du log
    /// </summary>
    public enum LogLevel
    {
        Debug,
        Info,
        Warning,
        Error,
        Fatal
    }

    /// <summary>
    /// Gestion simplifiée d'un log
    /// </summary>
    /// <remarks>Nécessite une configuration dans le Web.config ou le app.config.</remarks>
    public class Logger
    {
        private static ILog log;
        private static bool isInitialized;

        public static event Action<LogLevel, string> CustomLogEvent;

        /// <summary>
        /// Initialize le logger à partir de la configuration XML
        /// </summary>
        /// <param name="applicationName"></param>
        public static void Initialize(string applicationName)
        {
            log4net.Config.XmlConfigurator.Configure();
            log = LogManager.GetLogger(applicationName);

            isInitialized = true;
        }

        public static ILog GetLog4NetLogger()
        {
            return log;
        }

        /// <summary>
        /// Ecriture d'un message
        /// </summary>
        /// <param name="message"></param>
        public static void Log(LogLevel logLevel, string message)
        {
            if (isInitialized == false)
            {
                throw new InvalidOperationException("Please call \"Logger.Initialize\" before logging.");
            }

            switch (logLevel)
            {
                case LogLevel.Debug:
                    debug(message);
                    break;
                case LogLevel.Info:
                    info(message);
                    break;
                case LogLevel.Warning:
                    warning(message);
                    break;
                case LogLevel.Error:
                    error(message);
                    break;
                case LogLevel.Fatal:
                    fatal(message);
                    break;
            }

            if (CustomLogEvent != null)
            {
                CustomLogEvent(logLevel, message);
            }
        }

        public static bool isEnabled(LogLevel logLevel)
        {
            bool result = false;
            switch (logLevel)
            {
                case LogLevel.Debug:
                    result = log.IsDebugEnabled;
                    break;
                case LogLevel.Info:
                    result = log.IsInfoEnabled;
                    break;
                case LogLevel.Warning:
                    result = log.IsWarnEnabled;
                    break;
                case LogLevel.Error:
                    result = log.IsErrorEnabled;
                    break;
                case LogLevel.Fatal:
                    result = log.IsFatalEnabled;
                    break;
                default:
                    result = log.IsDebugEnabled;
                    break;
            }

            return result;
        }

        /// <summary>
        /// Ecriture d'une exception
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public static void LogException(LogLevel logLevel, string source, Exception e)
        {
            Logger.Log(logLevel, "Exception ! Source : " + source + ". " + e.ToString());
        }

        private static void debug(string message)
        {
            if (log.IsDebugEnabled)
            {
                log.Debug(message);
            }
        }

        private static void info(string message)
        {
            if (log.IsInfoEnabled)
            {
                log.Info(message);
            }
        }

        private static void warning(string message)
        {
            if (log.IsWarnEnabled)
            {
                log.Warn(message);
            }
        }

        private static void error(string message)
        {
            if (log.IsErrorEnabled)
            {
                log.Error(message);
            }
        }

        private static void fatal(string message)
        {
            if (log.IsFatalEnabled)
            {
                log.Fatal(message);
            }
        }

        public static void RegisterSessionID(string SessionID)
        {
            log4net.ThreadContext.Properties["SessionID"] = SessionID;
        }
    }
}
