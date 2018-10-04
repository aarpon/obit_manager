using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using obit_manager_api;


namespace obit_manager_driver
{
    [TestClass]
    public class FileSystemTests
    {
        [TestMethod]
        static void TestPathOperations(string[] args)
        {
            bool success = false;

            // Current user
            string username = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            // Path to work with
            string fullPath = @"E:\temp";

            // Create if it does not exist
            (new System.IO.FileInfo(fullPath)).Directory.Create();

            // Remove inherited permissions
            FileSystem.RemoveInheritedPermissionsForFolder(fullPath);

            // Remove all users' permissions
            FileSystem.removePermissionsForAllUsersButOne(fullPath, username);

            // Grant full access permissions to Everyone
            FileSystem.GrantFullAccessToEveryone(fullPath);

            // Grant full access permission to current user
            FileSystem.GrantFullAccessToUser(fullPath, username);

            // Set ownership to current user
            FileSystem.SetOwnership(fullPath, username);

            success = true;
            Assert.AreEqual(success, true);
        }
    }
}
