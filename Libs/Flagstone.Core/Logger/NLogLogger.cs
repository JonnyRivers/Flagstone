using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NLog;
using NLog.Config;

namespace Flagstone.Logger
{
    internal class NLogLogger : ILogger
    {
        private readonly NLog.Logger m_logger;

        internal NLogLogger(IFileSystem fileSystem, string applicationName)
        {
            var config = new LoggingConfiguration();

            var consoleTarget = new NLog.Targets.ConsoleTarget();
            config.AddTarget("console", consoleTarget);
            consoleTarget.Layout = "${longdate}|${level}|${message}";

            var fileTarget = new NLog.Targets.FileTarget();

            string localApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string logPath = fileSystem.Path.Combine(localApplicationData, "Flagstone", applicationName, applicationName + ".txt");
            fileTarget.FileName = logPath;
            fileTarget.ArchiveAboveSize = 10 * 1024 * 1024;
            fileTarget.MaxArchiveFiles = 10;
            fileTarget.Layout = "${longdate}|${level}|${message}";

            var consoleRule = new LoggingRule("*", LogLevel.Info, consoleTarget);
            var fileRule = new LoggingRule("*", LogLevel.Trace, fileTarget);
            config.LoggingRules.Add(consoleRule);
            config.LoggingRules.Add(fileRule);

            LogManager.Configuration = config;

            m_logger = LogManager.GetLogger(String.Empty);
        }

        public void Trace(string format, params object[] vargs)
        {
            m_logger.Trace(format, vargs);
        }

        public void Trace(Exception exception, string format, params object[] vargs)
        {
            m_logger.Trace(exception, format, vargs);
        }

        public void Debug(string format, params object[] vargs)
        {
            m_logger.Debug(format, vargs);
        }

        public void Debug(Exception exception, string format, params object[] vargs)
        {
            m_logger.Debug(exception, format, vargs);
        }

        public void Info(string format, params object[] vargs)
        {
            m_logger.Info(format, vargs);
        }

        public void Info(Exception exception, string format, params object[] vargs)
        {
            m_logger.Info(exception, format, vargs);
        }

        public void Warning(string format, params object[] vargs)
        {
            m_logger.Warn(format, vargs);
        }

        public void Warning(Exception exception, string format, params object[] vargs)
        {
            m_logger.Warn(exception, format, vargs);
        }

        public void Error(string format, params object[] vargs)
        {
            m_logger.Error(format, vargs);
        }

        public void Error(Exception exception, string format, params object[] vargs)
        {
            m_logger.Error(exception, format, vargs);
        }

        public void Fatal(string format, params object[] vargs)
        {
            m_logger.Fatal(format, vargs);
        }

        public void Fatal(Exception exception, string format, params object[] vargs)
        {
            m_logger.Fatal(exception, format, vargs);
        }
    }
}
