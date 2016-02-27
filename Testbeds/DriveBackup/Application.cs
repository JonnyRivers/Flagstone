using System.IO.Abstractions;

using Flagstone.IO;
using Flagstone.Logger;

namespace DriveBackup
{
    public class Application
    {
        private readonly IFileSystem m_fileSystem;
        private readonly ILogger m_logger;

        public Application(IFileSystem fileSystem, ILogger logger)
        {
            m_fileSystem = fileSystem;
            m_logger = logger;
        }

        public int Run(string sourceRootDirectory, string destinationRootDirectory)
        {
            string[] sourceFileNames = m_fileSystem.GetFileNamesRecursive(sourceRootDirectory, "*");
            m_logger.Info("Found {0} files at source directory \"{1}\"", sourceFileNames.Length, sourceRootDirectory);

            foreach (string sourceFileName in sourceFileNames)
            {
                string destinationFileName = sourceFileName.Replace(sourceRootDirectory, destinationRootDirectory);

                if (m_fileSystem.File.Exists(destinationFileName))
                {
                    if (m_fileSystem.IsFirstFileNewer(sourceFileName, destinationFileName))
                    {
                        m_logger.Info("Overwriting '{0}'", destinationFileName);
                        m_fileSystem.SafeCopy(sourceFileName, destinationFileName, true);
                    }
                }
                else
                {
                    m_logger.Info("Copying '{0}'", sourceFileName);
                    m_fileSystem.SafeCopy(sourceFileName, destinationFileName, false);
                }
            }

            return 0;
        }
    }
}
