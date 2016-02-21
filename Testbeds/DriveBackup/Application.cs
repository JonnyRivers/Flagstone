using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Flagstone.IO;
using Flagstone.Logger;

namespace DriveBackup
{
    public class Application
    {
        private readonly ILogger m_logger;
        private readonly IFileSystem m_fileSystem;

        public Application(ILogger logger, IFileSystem fileSystem)
        {
            m_logger = logger;
            m_fileSystem = fileSystem;
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
