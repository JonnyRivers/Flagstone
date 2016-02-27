using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flagstone.Logger
{
    public interface ILoggerFactory
    {
        ILogger CreateConsoleLogger();
        ILogger CreateNLogCommandLineApplicationLogger(IFileSystem fileSystem, string applicationName);
    }
}
