using System;

namespace Flagstone.Logger
{
    public interface ILogger
    {
        void Info(string format, params object[] vargs);
        void Warning(string format, params object[] vargs);
        void Error(string format, params object[] vargs);
        void Exception(Exception exception, string format, params object[] vargs);
    }
}
