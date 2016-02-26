using System.IO.Abstractions;

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

            // TODO: drive this via the command line
            return application.Run(@"F:\", @"G:\");
        }
    }
}
