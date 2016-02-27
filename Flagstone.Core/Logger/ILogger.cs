using System;

namespace Flagstone.Logger
{
    /// <summary>
    /// ILogger is the generic interface for all logging.
    /// 
    /// Trace messages are used to trace entry and exit of methods and are useful for tracking down
    /// bugs that are difficult to reproduce.
    /// 
    /// Debug messages are intended to be seen by developers only.
    /// 
    /// Info messages are standard status logging messages.  
    /// Where appropriate, end users should be able to understand these messages.
    /// 
    /// Warning messages should be used when an operation completed but there were unexpected
    /// problems along the way.  
    /// Where appropriate, end users should be able to understand these messages.
    /// 
    /// Error messages should be used when an operation failed but the application can continue.
    /// Where appropriate, end users should be able to understand these messages.
    /// 
    /// Fatal messages should be used when the application has to terminate.
    /// Where appropriate, end users should be able to understand these messages.
    /// </summary>
    public interface ILogger
    {
        void Trace(string format, params object[] vargs);
        void Trace(Exception exception, string format, params object[] vargs);
        void Debug(string format, params object[] vargs);
        void Debug(Exception exception, string format, params object[] vargs);
        void Info(string format, params object[] vargs);
        void Info(Exception exception, string format, params object[] vargs);
        void Warning(string format, params object[] vargs);
        void Warning(Exception exception, string format, params object[] vargs);
        void Error(string format, params object[] vargs);
        void Error(Exception exception, string format, params object[] vargs);
        void Fatal(string format, params object[] vargs);
        void Fatal(Exception exception, string format, params object[] vargs);
    }
}
