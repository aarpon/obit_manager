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
            string installationFolder = @"C:\temp";
            
            String downloadURL = Constants.Jdk64bitURL;
            String targetFileName = Path.Combine(installationFolder, Constants.Jdk64bitFileName);
            String jdkExtractPath = Path.Combine(installationFolder, Constants.Jdk64bitExtractDirName);
            String jdkFinalPath = Path.Combine(installationFolder, Constants.Jdk64bitPath);

            // Download the file
            await WebUtils.DownloadAsync(downloadURL, targetFileName);
            Assert.IsTrue(File.Exists(targetFileName));

            // Calculate and compare MD5 checksum
            Assert.IsTrue(FileSystem.CalculateMD5Checksum(targetFileName).Equals(Constants.Jdk64bitMD5Checksum));

            // Decompress the file
            FileSystem.ExtractZIPFileToFolder(targetFileName, installationFolder);

            // Check that the extract folder exists
            Assert.IsTrue(Directory.Exists(jdkExtractPath));

            // Move the JRE subfolder in the final location
            Directory.Move(Path.Combine(jdkExtractPath, "jre"), jdkFinalPath);

            // Delete temporary files and folders
            File.Delete(targetFileName);
            Directory.Delete(jdkExtractPath, recursive: true);

            // Finally, check if the jre folder is in the final location
            Assert.IsTrue(Directory.Exists(jdkFinalPath));

            // Now it can be deleted
            Directory.Delete(jdkFinalPath, recursive: true);
        }

        [TestMethod]
        public async Task TestDownloadJDK32Async()
        {
            string installationFolder = @"C:\temp";

            String downloadURL = Constants.Jdk32bitURL;
            String targetFileName = Path.Combine(installationFolder, Constants.Jdk32bitFileName);
            String jdkExtractPath = Path.Combine(installationFolder, Constants.Jdk32bitExtractDirName);
            String jdkFinalPath = Path.Combine(installationFolder, Constants.Jdk32bitPath);

            // Download the file
            await WebUtils.DownloadAsync(downloadURL, targetFileName);
            Assert.IsTrue(File.Exists(targetFileName));

            // Calculate and compare MD5 checksum
            Assert.IsTrue(FileSystem.CalculateMD5Checksum(targetFileName).Equals(Constants.Jdk32bitMD5Checksum));

            // Decompress the file
            FileSystem.ExtractZIPFileToFolder(targetFileName, installationFolder);

            // Check that the extract folder exists
            Assert.IsTrue(Directory.Exists(jdkExtractPath));

            // Move the JRE subfolder in the final location
            Directory.Move(Path.Combine(jdkExtractPath, "jre"), jdkFinalPath);

            // Delete temporary files and folders
            File.Delete(targetFileName);
            Directory.Delete(jdkExtractPath, recursive: true);

            // Finally, check if the jre folder is in the final location
            Assert.IsTrue(Directory.Exists(jdkFinalPath));

            // Now it can be deleted
            Directory.Delete(jdkFinalPath, recursive: true);
        }

        [TestMethod]
        public async Task TestDownloadDatamoverJSL()
        {
            // Parameters
            string url = Constants.DatamoverJslURL;
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
            string url = Constants.DatamoverJslURL;
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
            string url = Constants.AnnotationTool64bitURL;
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
            string url = Constants.AnnotationTool32bitURL;
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
