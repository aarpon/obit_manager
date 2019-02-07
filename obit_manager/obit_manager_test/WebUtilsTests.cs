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

            await WebUtils.DownloadAsync(url, fileName);

            Assert.IsTrue(File.Exists(fileName));

            File.Delete(fileName);
        }

        [TestMethod]
        public async Task TestDownloadJDK32Async()
        {
            // Parameters
            string url = Constants.jdk32bitURL;
            string fileName = @"C:\temp\jdk8_32.zip";

            await WebUtils.DownloadAsync(url, fileName);

            Assert.IsTrue(File.Exists(fileName));

            File.Delete(fileName);
        }

        [TestMethod]
        public async Task TestDownloadDatamoverJSL()
        {
            // Parameters
            string url = Constants.datamoverJslURL;
            string fileName = @"C:\temp\datamoverJsl.zip";

            await WebUtils.DownloadAsync(url, fileName);

            Assert.IsTrue(File.Exists(fileName));

            File.Delete(fileName);
        }

        [TestMethod]
        public async Task TestDownloadDatamover()
        {
            // Parameters
            string url = Constants.datamoverJslURL;
            string fileName = @"C:\temp\datamover.zip";

            await WebUtils.DownloadAsync(url, fileName);

            Assert.IsTrue(File.Exists(fileName));

            File.Delete(fileName);
        }

        [TestMethod]
        public async Task TestDownloadAnnotationTool64bit()
        {
            // Parameters
            string url = Constants.annotationTool64bitURL;
            string fileName = @"C:\temp\annotationTool64bit.zip";

            await WebUtils.DownloadAsync(url, fileName);

            Assert.IsTrue(File.Exists(fileName));

            File.Delete(fileName);
        }

        [TestMethod]
        public async Task TestDownloadAnnotationTool32bit()
        {
            // Parameters
            string url = Constants.annotationTool32bitURL;
            string fileName = @"C:\temp\annotationTool32bit.zip";

            await WebUtils.DownloadAsync(url, fileName);

            Assert.IsTrue(File.Exists(fileName));

            File.Delete(fileName);
        }
    }

}
