using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using obit_manager_api;


namespace obit_manager_test
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void TestUserOperations()
        {
            // Create a user
            LocalUserManager.CreateUser("aaron", "password");
        }
    }
}
