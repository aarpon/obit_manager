using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using obit_manager_api;

namespace obit_manager_test
{
    [TestClass]
    public class WebUtilsTests
    {
        [TestMethod]
        public async Task TestDownloadJDK64Async()
        {
            // Parameters
            string url = Constants.jdk64bitURL;
            string fileName = @"C:\temp\jdk8_64.zip";
            string dirName = @"C:\temp\jdk8_64";

            // Download the archive
            await WebUtils.DownloadAsync(url, fileName);
            Assert.IsTrue(File.Exists(fileName));

            // Decompress the file
            FileSystem.ExtractZIPFileToFolder(fileName, dirName);
            Assert.IsTrue(Directory.Exists(dirName));

            // Clean up
            File.Delete(fileName);
            Directory.Delete(dirName, recursive: true);
        }

        [TestMethod]
        public async Task TestDownloadJDK32Async()
        {
            // Parameters
            string url = Constants.jdk32bitURL;
            string fileName = @"C:\temp\jdk8_32.zip";
            string dirName = @"C:\temp\jdk8_32";

            // Download the archive
            await WebUtils.DownloadAsync(url, fileName);
            Assert.IsTrue(File.Exists(fileName));

            // Decompress the file
            FileSystem.ExtractZIPFileToFolder(fileName, dirName);
            Assert.IsTrue(Directory.Exists(dirName));

            // Clean up
            File.Delete(fileName);
            Directory.Delete(dirName, recursive: true);
        }

        [TestMethod]
        public async Task TestDownloadDatamoverJSL()
        {
            // Parameters
            string url = Constants.datamoverJslURL;
            string fileName = @"C:\temp\datamoverJsl.zip";
            string dirName = @"C:\temp\datamoverJsl";

            // Download the archive
            await WebUtils.DownloadAsync(url, fileName);
            Assert.IsTrue(File.Exists(fileName));

            // Decompress the file
            FileSystem.ExtractZIPFileToFolder(fileName, dirName);
            Assert.IsTrue(Directory.Exists(dirName));

            // Clean up
            File.Delete(fileName);
            Directory.Delete(dirName, recursive: true);
        }

        [TestMethod]
        public async Task TestDownloadDatamover()
        {
            // Parameters
            string url = Constants.datamoverJslURL;
            string fileName = @"C:\temp\datamover.zip";
            string dirName = @"C:\temp\datamover";

            // Download the archive
            await WebUtils.DownloadAsync(url, fileName);
            Assert.IsTrue(File.Exists(fileName));

            // Decompress the file
            FileSystem.ExtractZIPFileToFolder(fileName, dirName);
            Assert.IsTrue(Directory.Exists(dirName));

            // Clean up
            File.Delete(fileName);
            Directory.Delete(dirName, recursive: true);
        }

        [TestMethod]
        public async Task TestDownloadAnnotationTool64bit()
        {
            // Parameters
            string url = Constants.annotationTool64bitURL;
            string fileName = @"C:\temp\annotationTool64bit.zip";
            string dirName = @"C:\temp\annotationTool64bit";

            // Download the archive
            await WebUtils.DownloadAsync(url, fileName);
            Assert.IsTrue(File.Exists(fileName));

            // Decompress the file
            FileSystem.ExtractZIPFileToFolder(fileName, dirName);
            Assert.IsTrue(Directory.Exists(dirName));

            // Clean up
            File.Delete(fileName);
            Directory.Delete(dirName, recursive: true);
        }

        [TestMethod]
        public async Task TestDownloadAnnotationTool32bit()
        {
            // Parameters
            string url = Constants.annotationTool32bitURL;
            string fileName = @"C:\temp\annotationTool32bit.zip";
            string dirName = @"C:\temp\annotationTool32bit";

            // Download the archive
            await WebUtils.DownloadAsync(url, fileName);
            Assert.IsTrue(File.Exists(fileName));

            // Decompress the file
            FileSystem.ExtractZIPFileToFolder(fileName, dirName);
            Assert.IsTrue(Directory.Exists(dirName));

            // Clean up
            File.Delete(fileName);
            Directory.Delete(dirName, recursive: true);
        }
    }

}
