using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            string[] sourceFileNames = GetFileNamesRecursive(sourceRootDirectory, "*");
            m_logger.Info("Found {0} files at source directory \"{1}\"", sourceFileNames.Length, sourceRootDirectory);

            foreach (string sourceFileName in sourceFileNames)
            {
                string destinationFileName = sourceFileName.Replace(sourceRootDirectory, destinationRootDirectory);

                if (m_fileSystem.File.Exists(destinationFileName))
                {
                    // TODO: this is generally useful
                    // TODO: Think about how we extend IFileSystem
                    FileInfoBase sourceFileInfo = m_fileSystem.FileInfo.FromFileName(sourceFileName);
                    FileInfoBase destinationFileInfo = m_fileSystem.FileInfo.FromFileName(destinationFileName);

                    if (sourceFileInfo.LastWriteTime > destinationFileInfo.LastWriteTime)
                    {
                        m_logger.Info("Overwriting '{0}'", destinationFileName);

                        // TODO: This block is duplicated.
                        // TODO: Think about how we extend IFileSystem
                        String destinationDirectory = m_fileSystem.Path.GetDirectoryName(destinationFileName);
                        if (!m_fileSystem.Directory.Exists(destinationDirectory))
                        {
                            m_fileSystem.Directory.CreateDirectory(destinationDirectory);
                        }
                        
                        m_fileSystem.File.Copy(sourceFileName, destinationFileName, true);
                    }
                }
                else
                {
                    m_logger.Info("Copying '{0}'", sourceFileName);

                    // TODO: This block is duplicated.
                    // TODO: Think about how we extend IFileSystem
                    String destinationDirectory = m_fileSystem.Path.GetDirectoryName(destinationFileName);
                    if (!m_fileSystem.Directory.Exists(destinationDirectory))
                    {
                        m_fileSystem.Directory.CreateDirectory(destinationDirectory);
                    }

                    m_fileSystem.File.Copy(sourceFileName, destinationFileName);
                }
            }

            return 0;
        }

        // TODO: Think about how we extend IFileSystem
        public string[] GetFileNamesRecursive(string directory, string searchPattern)
        {
            var fileNames = new List<string>();
            GetFileNamesRecursive(directory, searchPattern, fileNames);

            return fileNames.ToArray();
        }

        // TODO: Handle access issues more gracefully
        private void GetFileNamesRecursive(string directory, string searchPattern, List<string> fileNames)
        {
            String[] subDirectories = m_fileSystem.Directory.GetDirectories(directory);
            foreach (String subDirectory in subDirectories)
            {
                DirectoryInfoBase di = m_fileSystem.DirectoryInfo.FromDirectoryName(subDirectory);
                if (di.Name == "$RECYCLE.BIN" || di.Name == "System Volume Information")
                    continue;

                GetFileNamesRecursive(subDirectory, searchPattern, fileNames);
            }

            String[] files = m_fileSystem.Directory.GetFiles(directory);
            if (files.Length > 0)
                fileNames.AddRange(files);
        }
    }
}
