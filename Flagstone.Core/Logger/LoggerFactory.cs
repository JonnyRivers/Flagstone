using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flagstone.Logger
{
    public class LoggerFactory : ILoggerFactory
    {
        public ILogger CreateConsoleLogger()
        {
            return new ConsoleLogger();
        }

        public ILogger CreateNLogCommandLineApplicationLogger(IFileSystem fileSystem, string applicationName)
        {
            return new NLogLogger(fileSystem, applicationName);
        }
    }
}
