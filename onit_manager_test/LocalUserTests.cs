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
        public void TestUserCreation()
        {
            // Create a test user with password that never exprires
            bool success = LocalUserManager.CreateUser("obit_test_user", "Areh45wldy8d",
                out string errorMessage, "oBIT Test User", "Users", true);
            Assert.AreEqual(success, true, errorMessage);
        }
    }
}
