using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using obit_manager_api.core;

namespace obit_manager_test
{
    [TestClass]
    public class FileSystemTests
    {
        [TestMethod]
        public void TestFileSystemOperations()
        {
            // Current user
            string username = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            // Path to test folder to work with
            string fullRootPath = System.IO.Path.Combine(@"C:\obit_test",
                "" + DateTimeOffset.Now.ToUnixTimeMilliseconds());

            // Delete test folder (recursively) if it exists
            if (System.IO.Directory.Exists(fullRootPath))
            {
                System.IO.Directory.Delete(fullRootPath, true);
            }

            // (Re)Create it (default permissions)
            System.IO.Directory.CreateDirectory(fullRootPath);

            // Create also a subfolder, if it does not exist
            string fullSubFolderPath = System.IO.Path.Combine(fullRootPath, "sub");
            System.IO.Directory.CreateDirectory(fullSubFolderPath);

            // Also create an (empty) file
            string fullFilePath = System.IO.Path.Combine(fullSubFolderPath, "test.txt");
            System.IO.File.Create(fullFilePath).Dispose();

            // Get a list of users with any permissions on the folder
            List<string> initialUserNames = FileSystem.GetUsersWithPermissions(fullRootPath);

            // Remove inherited permissions
            FileSystem.RemoveInheritedPermissionsForFolder(fullRootPath);

            // Get the new list of users with permissions
            List<string> updatedUserNames = FileSystem.GetUsersWithPermissions(fullRootPath);

            // Remove all users' permissions
            FileSystem.RemovePermissionsForAllUsersButOne(fullRootPath, username);

            // Get the new list of users with permissions
            List<string> maxOneExpectedUserName = FileSystem.GetUsersWithPermissions(fullRootPath);
            bool numUserNamesMatch = (maxOneExpectedUserName.Count == 0 | maxOneExpectedUserName.Count == 1);
            Assert.AreEqual(numUserNamesMatch, true);

            // Grant full access permissions to Everyone
            FileSystem.GrantFullAccessToEveryone(fullRootPath);

            // Check that only "Everyone" has access to the folder
            List<string> expectedEveryoneUserName = FileSystem.GetUsersWithPermissions(fullRootPath);
            Assert.AreEqual(expectedEveryoneUserName.Count, 1);
            Assert.AreEqual(expectedEveryoneUserName[0], "Everyone");

            // Grant full access permission to current user
            FileSystem.GrantFullAccessToUser(fullRootPath, username);

            // Check that 'username' also has access to the folder
            List<string> expectedUserNames = FileSystem.GetUsersWithPermissions(fullRootPath);
            Assert.AreEqual(expectedUserNames.Count, 2);
            bool userNameFound = false;
            foreach (string name in expectedUserNames)
            {
                if (name.Equals(username))
                {
                    userNameFound = true;
                    break;
                }
            }

            Assert.AreEqual(userNameFound, true);

            // Set ownership to current user
            FileSystem.SetOwnership(fullRootPath, username);

            // Get ownership of the folder
            string owner = FileSystem.GetOwner(fullRootPath);
            Assert.AreEqual(owner, username);

            // Get ownership of the file
            owner = FileSystem.GetOwner(fullFilePath);
            Assert.AreEqual(owner, username);

            // Remove the test folder
            System.IO.Directory.Delete(fullRootPath, true);
        }

        [TestMethod]
        public void TestPathOperations()
        {
            string path = "C:\\\\Test\\\\file.txt";
            string processedPath = FileSystem.ChangeBackwardToForwardSlashesInPath(path);
            string expectedPath = "C:/Test/file.txt";
            Assert.AreEqual(processedPath, expectedPath);

            path = "C:\\Test\\file.txt";
            processedPath = FileSystem.ChangeBackwardToForwardSlashesInPath(path);
            expectedPath = "C:/Test/file.txt";
            Assert.AreEqual(processedPath, expectedPath);

            path = "C:\\Test\\\\file.txt";
            processedPath = FileSystem.ChangeBackwardToForwardSlashesInPath(path);
            expectedPath = "C:/Test/file.txt";
            Assert.AreEqual(processedPath, expectedPath);

            path = "C:/Test/file.txt";
            processedPath = FileSystem.ChangeBackwardToForwardSlashesInPath(path);
            expectedPath = "C:/Test/file.txt";
            Assert.AreEqual(processedPath, expectedPath);
        }
    }
}
