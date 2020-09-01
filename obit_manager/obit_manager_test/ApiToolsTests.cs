using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using obit_manager_gui.tools;

namespace obit_manager_test
{
    [TestClass]
    public class ApiToolsTests
    {
        private readonly string InstallationFolder = @"C:\temp_" + DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();

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
        public async Task TestToolsDownloadsAndSetUp64bitJDK()
        {
            await Operations.DownloadCheckAndInstallJDKAsync(is64bit: true, this.InstallationFolder);
        }

        [TestMethod]
        public async Task TestToolsDownloadsAndSetUp32bitJDK()
        {
            await Operations.DownloadCheckAndInstallJDKAsync(is64bit: false, this.InstallationFolder);
        }

        [TestMethod]
        public async Task TestToolsDownloadsAndSetupAnnotationTool64bit()
        {
            await Operations.DownloadCheckAndInstallAnnotationToolAsync(is64bit: true, this.InstallationFolder);
        }

        [TestMethod]
        public async Task TestToolsDownloadsAndSetupAnnotationTool32bit()
        {
            await Operations.DownloadCheckAndInstallAnnotationToolAsync(is64bit: false, this.InstallationFolder);
        }

        [TestMethod]
        public async Task TestToolsDownloadsAndSetupDatamover64bit()
        {
            await Operations.DownloadCheckAndInstallDatamoverJSL(is64bit: true, this.InstallationFolder);
        }

        [TestMethod]
        public async Task TestToolsDownloadsAndSetupDatamoverWithDifferentName64bit()
        {
            await Operations.DownloadCheckAndInstallDatamoverJSL(is64bit: true, this.InstallationFolder, "obit_datamover_jsl_alt");
        }

        [TestMethod]
        public async Task TestToolsDownloadsAndSetupDatamover32bit()
        {
            await Operations.DownloadCheckAndInstallDatamoverJSL(is64bit: false, this.InstallationFolder);
        }

        [TestMethod]
        public async Task TestToolsDownloadsAndSetupDatamoverWithDifferentName32bit()
        {
            await Operations.DownloadCheckAndInstallDatamoverJSL(is64bit: false, this.InstallationFolder, @"obit_datamover_jsl_alt");
        }

        [TestMethod]
        public void TestDeleteDownloadedArchives64bit()
        {
            Operations.DeleteDownloadedArchives(is64bit: true, this.InstallationFolder);
        }

        [TestMethod]
        public void TestDeleteDownloadedArchives32bit()
        {
            Operations.DeleteDownloadedArchives(is64bit: false, this.InstallationFolder);
        }
    }
}
