using System;
using System.Collections.Generic;
using System.IO.Abstractions;

namespace Flagstone.IO
{
    public static class FileSystemExtensionMethods
    {
        public static void SafeCopy(this IFileSystem fileSystem, string sourcePath, string destinationPath, bool overwrite){
            String destinationDirectory = fileSystem.Path.GetDirectoryName(destinationPath);
            if (!fileSystem.Directory.Exists(destinationDirectory)) {
                fileSystem.Directory.CreateDirectory(destinationDirectory);
            }

            fileSystem.File.Copy(sourcePath, destinationPath, overwrite);
        }

        public static bool IsFirstFileNewer(this IFileSystem fileSystem, string sourcePath, string destinationPath)
        {
            FileInfoBase sourceFileInfo = fileSystem.FileInfo.FromFileName(sourcePath);
            FileInfoBase destinationFileInfo = fileSystem.FileInfo.FromFileName(destinationPath);

            return (sourceFileInfo.LastWriteTime > destinationFileInfo.LastWriteTime);
        }

        public static string[] GetFileNamesRecursive(this IFileSystem fileSystem, string directory, string searchPattern)
        {
            var fileNames = new List<string>();
            fileSystem.GetFileNamesRecursive(directory, searchPattern, fileNames);

            return fileNames.ToArray();
        }

        private static void GetFileNamesRecursive(this IFileSystem fileSystem, string directory, string searchPattern, List<string> fileNames)
        {
            String[] subDirectories = fileSystem.Directory.GetDirectories(directory);
            foreach (String subDirectory in subDirectories)
            {
                DirectoryInfoBase di = fileSystem.DirectoryInfo.FromDirectoryName(subDirectory);

                // TODO: remove this deplorable hack
                if (di.Name == "$RECYCLE.BIN" || di.Name == "System Volume Information")
                    continue;

                fileSystem.GetFileNamesRecursive(subDirectory, searchPattern, fileNames);
            }

            String[] files = fileSystem.Directory.GetFiles(directory);
            if (files.Length > 0)
                fileNames.AddRange(files);
        }
    }
}
