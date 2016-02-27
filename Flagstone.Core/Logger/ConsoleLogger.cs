using System;

namespace Flagstone.Logger
{
    internal class ConsoleLogger : ILogger
    {
        const string c_traceText = "TRACE";
        const string c_debugText = "DEBUG";
        const string c_infoText = "INFO";
        const string c_warningText = "WARNING";
        const string c_errorText = "ERROR";
        const string c_fatalText = "FATAL";
        const string c_levelSuffix = ": ";

        internal ConsoleLogger()
        {
        }

        public void Trace(string format, params object[] vargs)
        {
            Log(c_traceText, format, vargs);
        }

        public void Trace(Exception exception, string format, params object[] vargs)
        {
            Log(exception, c_traceText, format, vargs);
        }

        public void Debug(string format, params object[] vargs)
        {
            Log(c_debugText, format, vargs);
        }

        public void Debug(Exception exception, string format, params object[] vargs)
        {
            Log(exception, c_debugText, format, vargs);
        }

        public void Info(string format, params object[] vargs)
        {
            Log(c_infoText, format, vargs);
        }

        public void Info(Exception exception, string format, params object[] vargs)
        {
            Log(exception, c_infoText, format, vargs);
        }

        public void Warning(string format, params object[] vargs)
        {
            Log(c_warningText, format, vargs);
        }

        public void Warning(Exception exception, string format, params object[] vargs)
        {
            Log(exception, c_warningText, format, vargs);
        }

        public void Error(string format, params object[] vargs)
        {
            Log(c_errorText, format, vargs);
        }

        public void Error(Exception exception, string format, params object[] vargs)
        {
            Log(exception, c_errorText, format, vargs);
        }

        public void Fatal(string format, params object[] vargs)
        {
            Log(c_fatalText, format, vargs);
        }

        public void Fatal(Exception exception, string format, params object[] vargs)
        {
            Log(exception, c_fatalText, format, vargs);
        }


        private void Log(string prefix, string format, params object[] vargs)
        {
            String message = String.Format(prefix + c_levelSuffix + format, vargs);
            Log(message);
        }

        private void Log(Exception exception, string prefix, string format, params object[] vargs)
        {
            Log(prefix, format, vargs);
            Log(exception.ToString());
        }

        private void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
