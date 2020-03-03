using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using obit_manager_api;
using obit_manager_settings;

namespace obit_manager_test
{
    [TestClass]
    public class WebUtilsTests
    {
        private readonly string InstallationFolder = @"C:\temp";

        [TestInitialize]
        public void Initialize() 
        {
            // Create the installation folder
            Directory.CreateDirectory(this.InstallationFolder);
        }

        [TestCleanup()]
        public void Cleanup()
        {
            // Delete the installation folder
            Directory.Delete(this.InstallationFolder, recursive: true);
        }

        [TestMethod]
        public async Task TestDownloadJDK64Async()
        {
           
            String downloadURL = Constants.Jdk64bitURL;
            String targetFileName = Path.Combine(this.InstallationFolder, Constants.Jdk64bitFileName);
            String jdkExtractPath = Path.Combine(this.InstallationFolder, Constants.Jdk64bitExtractDirName);
            String jdkFinalPath = Path.Combine(this.InstallationFolder, Constants.Jdk64bitPath);
            String jvmDllPath = Path.Combine(jdkFinalPath, @"bin\server\jvm.dll");

            // Download the file
            await WebUtils.DownloadAsync(downloadURL, targetFileName);
            Assert.IsTrue(File.Exists(targetFileName));

            // Calculate and compare MD5 checksum
            Assert.IsTrue(FileSystem.CalculateMD5Checksum(targetFileName).Equals(Constants.Jdk64bitMD5Checksum));

            // Decompress the file
            FileSystem.ExtractZIPFileToFolder(targetFileName, this.InstallationFolder);

            // Check that the extract folder exists
            Assert.IsTrue(Directory.Exists(jdkExtractPath));

            // Rename the JRE folder
            Directory.Move(jdkExtractPath, jdkFinalPath);

            // Finally, check if the jre folder is in the final location
            Assert.IsTrue(Directory.Exists(jdkFinalPath));

            // Check that the jvm.dll file is in the expected place
            Assert.IsTrue(File.Exists(jvmDllPath));

            // Now it can be deleted
            Directory.Delete(jdkFinalPath, recursive: true);
        }

        [TestMethod]
        public async Task TestDownloadJDK32Async()
        {
            String downloadURL = Constants.Jdk32bitURL;
            String targetFileName = Path.Combine(InstallationFolder, Constants.Jdk32bitFileName);
            String jdkExtractPath = Path.Combine(InstallationFolder, Constants.Jdk32bitExtractDirName);
            String jdkFinalPath = Path.Combine(InstallationFolder, Constants.Jdk32bitPath);
            String jvmDllPath = Path.Combine(jdkFinalPath, @"bin\server\jvm.dll");

            // Download the file
            await WebUtils.DownloadAsync(downloadURL, targetFileName);
            Assert.IsTrue(File.Exists(targetFileName));

            // Calculate and compare MD5 checksum
            string md5 = FileSystem.CalculateMD5Checksum(targetFileName);
            Assert.IsTrue(FileSystem.CalculateMD5Checksum(targetFileName).Equals(Constants.Jdk32bitMD5Checksum));

            // Decompress the file
            FileSystem.ExtractZIPFileToFolder(targetFileName, this.InstallationFolder);

            // Check that the extract folder exists
            Assert.IsTrue(Directory.Exists(jdkExtractPath));

            // Rename the JRE folder
            Directory.Move(jdkExtractPath, jdkFinalPath);

            // Finally, check if the jre folder is in the final location
            Assert.IsTrue(Directory.Exists(jdkFinalPath));

            // Check that the jvm.dll file is in the expected place
            Assert.IsTrue(File.Exists(jvmDllPath));

            // Now it can be deleted
            Directory.Delete(jdkFinalPath, recursive: true);
        }

        [TestMethod]
        public async Task TestDownloadDatamoverJSL()
        {
            // Parameters
            string url = Constants.DatamoverJslURL;
            string fileName = Path.Combine(this.InstallationFolder, "datamoverJsl.zip");
            string dirName = Path.Combine(this.InstallationFolder, Constants.DatamoverJslPath);

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
            string url = Constants.DatamoverURL;
            string jslDirName = Path.Combine(this.InstallationFolder, Constants.DatamoverJslPath);
            string fileName = Path.Combine(jslDirName, "datamover.zip");
            string dirName = Path.Combine(this.InstallationFolder, Constants.DatamoverPath);

            // Make sure that the JSL directory exists
            Directory.CreateDirectory(jslDirName);

            // Download the archive
            await WebUtils.DownloadAsync(url, fileName);
            Assert.IsTrue(File.Exists(fileName));

            // Decompress the file
            FileSystem.ExtractZIPFileToFolder(fileName, jslDirName);
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
            string fileName = Path.Combine(this.InstallationFolder, "annotationTool64bit.zip");
            string dirName = Path.Combine(this.InstallationFolder, Constants.AnnotationTool64bitPath);

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
            string fileName = Path.Combine(this.InstallationFolder, "annotationTool32bit.zip");
            string dirName = Path.Combine(this.InstallationFolder, Constants.AnnotationTool32bitPath);

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
