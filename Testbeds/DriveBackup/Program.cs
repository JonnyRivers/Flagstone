using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Flagstone.Logger;

namespace DriveBackup
{
    class Program
    {
        static int Main(string[] args)
        {
            ILogger logger = new ConsoleLogger();
            IFileSystem fileSystem = new FileSystem();

            Application application = new Application(logger, fileSystem);

            return application.Run(@"F:\", @"G:\");
        }
    }
}
