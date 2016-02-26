using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flagstone.Logger;

namespace DriveBackupTests
{
    [TestClass]
    public class ApplicationTests
    {
        [TestMethod]
        public void TestFileCopy()
        {
            // Arrange
            ILogger logger = new ConsoleLogger();

            var mockFileDataMap = new Dictionary<string, MockFileData>() {
                { @"C:\myfile.txt", new MockFileData("Test data") }
            };
            IFileSystem mockFileSystem = new MockFileSystem(mockFileDataMap);

            var application = new DriveBackup.Application(logger, mockFileSystem);

            // Act
            application.Run(@"C:\", @"D:\");

            // Assert
            Assert.IsTrue(mockFileSystem.File.Exists(@"D:\myfile.txt"));
            Assert.AreEqual(
                mockFileSystem.File.ReadAllText(@"C:\myfile.txt"),
                mockFileSystem.File.ReadAllText(@"D:\myfile.txt")
            );
        }

        [TestMethod]
        public void TestOlderFileOverwritten() {
            // Arrange
            ILogger logger = new ConsoleLogger();

            var mockFileDataMap = new Dictionary<string, MockFileData>() {
                { @"C:\myfile.txt", new MockFileData("New data") },
                { @"D:\myfile.txt", new MockFileData("Old data") }
            };
            IFileSystem mockFileSystem = new MockFileSystem(mockFileDataMap);
            FileInfoBase newFileInfo = mockFileSystem.FileInfo.FromFileName(@"C:\myfile.txt");
            newFileInfo.LastWriteTimeUtc = DateTime.UtcNow + new TimeSpan(0, 0, 0, 1);
            FileInfoBase oldFileInfo = mockFileSystem.FileInfo.FromFileName(@"D:\myfile.txt");
            oldFileInfo.LastWriteTimeUtc = DateTime.UtcNow;

            var application = new DriveBackup.Application(logger, mockFileSystem);

            // Act
            application.Run(@"C:\", @"D:\");

            // Assert
            Assert.IsTrue(mockFileSystem.File.Exists(@"D:\myfile.txt"));
            Assert.AreEqual(
                mockFileSystem.FileInfo.FromFileName(@"C:\myfile.txt").LastWriteTimeUtc,
                mockFileSystem.FileInfo.FromFileName(@"D:\myfile.txt").LastWriteTimeUtc
            );
            Assert.AreEqual(
                mockFileSystem.File.ReadAllText(@"C:\myfile.txt"),
                mockFileSystem.File.ReadAllText(@"D:\myfile.txt")
            );
        }

        [TestMethod]
        public void TestNewerFileNotOverwritten()
        {
            // Arrange
            ILogger logger = new ConsoleLogger();

            var mockFileDataMap = new Dictionary<string, MockFileData>() {
                { @"C:\myfile.txt", new MockFileData("Old data") },
                { @"D:\myfile.txt", new MockFileData("New data") }
            };
            IFileSystem mockFileSystem = new MockFileSystem(mockFileDataMap);
            FileInfoBase oldFileInfo = mockFileSystem.FileInfo.FromFileName(@"C:\myfile.txt");
            oldFileInfo.LastWriteTimeUtc = DateTime.UtcNow;
            FileInfoBase newFileInfo = mockFileSystem.FileInfo.FromFileName(@"D:\myfile.txt");
            newFileInfo.LastWriteTimeUtc = DateTime.UtcNow + new TimeSpan(0, 0, 0, 1);
            
            var application = new DriveBackup.Application(logger, mockFileSystem);

            // Act
            application.Run(@"C:\", @"D:\");

            // Assert
            Assert.IsTrue(mockFileSystem.File.Exists(@"D:\myfile.txt"));
            Assert.AreNotEqual(
                mockFileSystem.FileInfo.FromFileName(@"C:\myfile.txt").LastWriteTimeUtc,
                mockFileSystem.FileInfo.FromFileName(@"D:\myfile.txt").LastWriteTimeUtc
            );
            Assert.AreNotEqual(
                mockFileSystem.File.ReadAllText(@"C:\myfile.txt"),
                mockFileSystem.File.ReadAllText(@"D:\myfile.txt")
            );
        }
    }
}
