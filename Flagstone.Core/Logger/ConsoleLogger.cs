using System;

namespace Flagstone.Logger
{
    public class ConsoleLogger : ILogger
    {
        const string c_infoText = "INFO";
        const string c_warningText = "WARNING";
        const string c_errorText = "ERROR";
        const string c_exceptionText = "EXCEPTION";
        const string c_levelSuffix = ": ";

        public void Info(string format, params object[] vargs)
        {
            Log(c_infoText, format, vargs);
        }

        public void Warning(string format, params object[] vargs)
        {
            Log(c_warningText, format, vargs);
        }

        public void Error(string format, params object[] vargs)
        {
            Log(c_errorText, format, vargs);
        }

        public void Exception(Exception exception, string format, params object[] vargs)
        {
            Log(c_exceptionText, format, vargs);
            Log(exception.ToString());
        }

        private void Log(string prefix, string format, params object[] vargs)
        {
            String message = String.Format(prefix + c_levelSuffix + format, vargs);
            Log(message);
        }

        private void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
