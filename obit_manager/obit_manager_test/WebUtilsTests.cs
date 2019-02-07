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
            string url = Constants.jdkURL64bit;
            string fileName = @"C:\temp\jdk8_64.zip";

            await WebUtils.DownloadAsync(url, fileName);

            Assert.IsTrue(File.Exists(fileName));

            File.Delete(fileName);
        }

        [TestMethod]
        public async Task TestDownloadJDK32Async()
        {
            // Parameters
            string url = Constants.jdkURL32bit;
            string fileName = @"C:\temp\jdk8_32.zip";

            await WebUtils.DownloadAsync(url, fileName);

            Assert.IsTrue(File.Exists(fileName));

            File.Delete(fileName);
        }
    }

}
