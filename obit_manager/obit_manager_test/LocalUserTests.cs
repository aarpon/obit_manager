using Microsoft.VisualStudio.TestTools.UnitTesting;
using obit_manager_api;
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
        public void TestUserOperations()
        {
            // User credentials
            string username = "obit_test_user";
            string password = "Areh45wldy8d";

            // If the user already exists, delete it first
            if (LocalUserManager.UserExists(username))
            {
                // Delete the user
                Assert.AreEqual(LocalUserManager.DeleteUser(username), 
                    true, "Could not delete user!");
            }

            // Create a test user with password that never exprires
            bool success = LocalUserManager.CreateUser(username, password,
                out string errorMessage, "oBIT Test User", "Users", true);
            Assert.AreEqual(success, true, errorMessage);

            // Make sure the user really exists
            Assert.AreEqual(LocalUserManager.UserExists(username), true,
                "Could not find the newly created user!");

            // Run a command as the user we just created
            System.Security.SecureString securePassword = new System.Security.SecureString();
            foreach (char c in password)
            {
                securePassword.AppendChar(c);
            }
            string output = Shell.RunAs("whoami.exe", "", username, securePassword, "");
            output = output.Replace(System.Environment.NewLine, "");
            string[] parts = output.Split('\\');
            Assert.AreEqual(parts[0].ToLower(), System.Environment.MachineName.ToLower(),
                "Retured user name is not the one expected.");
            Assert.AreEqual(parts[1].ToLower(), username.ToLower(),
                "Retured user name is not the one expected.");

            // Clean up after creation
            Assert.AreEqual(LocalUserManager.DeleteUser(username),
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
