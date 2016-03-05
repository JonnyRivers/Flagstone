using System.IO.Abstractions;

using Flagstone.Logger;

namespace DriveBackup
{
    class Program
    {
        static int Main(string[] args)
        {
            IFileSystem fileSystem = new FileSystem();

            ILoggerFactory loggerFactory = new LoggerFactory();
            ILogger logger = loggerFactory.CreateNLogCommandLineApplicationLogger(fileSystem, "DriveBackup");

            Application application = new Application(fileSystem, logger);

            // TODO: drive this via the command line
            return application.Run(@"F:\", @"G:\");
        }
    }
}
