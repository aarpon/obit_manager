using Microsoft.VisualStudio.TestTools.UnitTesting;
using obit_manager_api.core;
using System.IO;

namespace obit_manager_test
{
    /// <summary>
    /// Test user creation and retrieval
    /// </summary>
    [TestClass]
    public class LocalUserTests
    {
        private readonly string InstallationFolder = @"C:\temp";

        // User credentials
        private readonly string Username = "obit_test_user";
        private readonly string Password = "Areh45wldy8d";

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

            // If the user still exists, delete it
            if (LocalUserManager.UserExists(this.Username))
            {
                // Delete the user
                Assert.AreEqual(LocalUserManager.DeleteUser(this.Username),
                    true, "Could not delete user!");
            }
        }

        [TestMethod]
        public void TestUserOperations()
        {
            // If the user already exists, delete it first
            if (LocalUserManager.UserExists(this.Username))
            {
                // Delete the user
                Assert.AreEqual(LocalUserManager.DeleteUser(this.Username), 
                    true, "Could not delete user!");
            }

            // Create a test user with password that never exprires
            bool success = LocalUserManager.CreateUser(this.Username, this.Password,
                out string errorMessage, "oBIT Test User", "Users", true);
            Assert.AreEqual(success, true, errorMessage);

            // Make sure the user really exists
            Assert.AreEqual(LocalUserManager.UserExists(this.Username), true,
                "Could not find the newly created user!");

            // Run a command as the user we just created
            System.Security.SecureString securePassword = new System.Security.SecureString();
            foreach (char c in this.Password)
            {
                securePassword.AppendChar(c);
            }
            string output = Shell.RunAs("whoami.exe", "", this.Username, securePassword, "");
            output = output.Replace(System.Environment.NewLine, "");
            string[] parts = output.Split('\\');
            Assert.AreEqual(parts[0].ToLower(), System.Environment.MachineName.ToLower(),
                "Retured user name is not the one expected.");
            Assert.AreEqual(parts[1].ToLower(), this.Username.ToLower(),
                "Retured user name is not the one expected.");

            // Clean up after creation
            Assert.AreEqual(LocalUserManager.DeleteUser(this.Username),
                true, "Could not delete user!");

            // Run a command as the current user
            output = Shell.Run("whoami.exe", "");
            output = output.Replace(System.Environment.NewLine, "");
            Assert.AreEqual(output.ToLower(), 
                System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToLower(),
                "Retured user name is not the one expected.");
        }
    }
}
