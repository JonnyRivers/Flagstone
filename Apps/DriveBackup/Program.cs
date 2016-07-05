using System.IO.Abstractions;

using Flagstone.Logger;

namespace DriveBackup
{
    class Program
    {
        static int Main(string[] args)
        {
            string sourceDirectory = args[0];
            string destinationDirectory = args[1];

            IFileSystem fileSystem = new FileSystem();

            ILoggerFactory loggerFactory = new LoggerFactory();
            ILogger logger = loggerFactory.CreateNLogCommandLineApplicationLogger(fileSystem, "DriveBackup");

            Application application = new Application(fileSystem, logger);

            return application.Run(sourceDirectory, destinationDirectory);
        }
    }
}
