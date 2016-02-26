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
            ILogger logger = new ConsoleLogger();

            var mockFileDataMap = new Dictionary<string, MockFileData>() {
                { @"C:\myfile.txt", new MockFileData("Test data") }
            };
            IFileSystem mockFileSystem = new MockFileSystem(mockFileDataMap);

            var application = new DriveBackup.Application(logger, mockFileSystem);

            application.Run(@"C:\", @"D:\");

            Assert.IsTrue(mockFileSystem.File.Exists(@"D:\myfile.txt"));
        }
    }
}
