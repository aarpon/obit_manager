using Microsoft.VisualStudio.TestTools.UnitTesting;
using obit_manager_api;


namespace obit_manager_test
{
    /// <summary>
    /// Test user creation and retrieval
    /// </summary>
    [TestClass]
    public class LocalUserTests
    {
        [TestMethod]
        public void TestUserOperations()
        {
            // User name
            string username = "obit_test_user";

            // If the user already exists, delete it first
            if (LocalUserManager.UserExists(username))
            {
                // Delete the user
                Assert.AreEqual(LocalUserManager.DeleteUser(username), 
                    true, "Could not delete user!");
            }

            // Create a test user with password that never exprires
            bool success = LocalUserManager.CreateUser(username, "Areh45wldy8d",
                out string errorMessage, "oBIT Test User", "Users", true);
            Assert.AreEqual(success, true, errorMessage);

            // Make sure the user really exists
            Assert.AreEqual(LocalUserManager.UserExists(username), true,
                "Could not find the newly created user!");

            // Clean up after creation
            Assert.AreEqual(LocalUserManager.DeleteUser(username),
                true, "Could not delete user!");
        }
    }
}
