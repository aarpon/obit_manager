using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Cryptography;
using NLog;

namespace obit_manager_api.core
{
    public static class FileSystem
    {
        /// <summary>
        /// Logger
        /// </summary>
        private static readonly Logger sLogger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Create a folder is it does not exist.
        /// </summary>
        /// <param name="folder">Full path to the folder to check or create.</param>
        /// <returns>True if the folder was created, false if it already existed.</returns>
        public static bool CreateIfDoesNotExist(string folder)
        {
            // Does the folder exist?
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);

                // Return true, since the folder was created.
                return true;
            }

            // Return false, since the folder already existed.
            return false;
        }

        /// <summary>
        /// Copy folder recursively.
        /// </summary>
        /// <param name="sourceFolder">Source folder.</param>
        /// <param name="destFolder">Destination folder.</param>
        public static void CopyFolderRecursively(string sourceFolder, string destFolder)
        {
            // Does the source folder exist?
            if (!Directory.Exists(sourceFolder))
            {
                // Inform
                sLogger.Error("Folder '" + sourceFolder + "' does not exist!");
                throw new FileNotFoundException("Folder '" + sourceFolder + "' does not exist!");
            }

            // Does the destination folder exist?
            if (!Directory.Exists(destFolder))
            {
                Directory.CreateDirectory(destFolder);
            }

            // Get files in sourceFolder
            string[] files = Directory.GetFiles(sourceFolder);

            // Copy them
            foreach (string file in files)
            {
                string name = Path.GetFileName(file);
                string dest = Path.Combine(destFolder, name);
                File.Copy(file, dest);
            }

            // Get folders in sourceFolder
            string[] folders = Directory.GetDirectories(sourceFolder);

            // Copy them (recursively)
            foreach (string folder in folders)
            {
                string name = Path.GetFileName(folder);
                string dest = Path.Combine(destFolder, name);
                CopyFolderRecursively(folder, dest);
            }

            // Inform
            sLogger.Info("Copied folder '" + sourceFolder + "' recursively to '" + destFolder + "'.");
        }

        /// <summary>
        /// Extract ZIP archive to a specified destination folder.
        /// </summary>
        /// <param name="zipFile">Full path to the ZIP archive to extract.</param>
        /// <param name="destFolder">Full path to the destination folder.</param>
        /// <returns>True if the ZIP file could be extracted successfully, false otherwise.</returns>
        public static bool ExtractZIPFileToFolder(string zipFile, string destFolder)
        {
            try
            {
                ZipFile.ExtractToDirectory(zipFile, destFolder);
            }
            catch (Exception e)
            {
                // Inform
                sLogger.Error("Could not extract ZIP file '" + zipFile + "'. The error was " + e.Message);
                return false;
            }

            // Inform
            sLogger.Info("Extracted archive '" + zipFile + "' to '" + destFolder + "'.");

            return true;
        }

        /// <summary>
        /// Delete a folder recursively.
        /// </summary>
        /// <param name="folderName">Full path to the folder to delete with full content.</param>
        /// <returns></returns>
        public static bool DeleteFolderRecursively(string folderName)
        {
            try
            {
                Directory.Delete(folderName, recursive: true);
            }
            catch (Exception e)
            {
                // Inform
                sLogger.Error("Could not recursively delete folder '" + folderName + "'. The error was " + e.Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Calculate the MD5 checksum of a file and returns it as string.
        /// </summary>
        /// <param name="filename">Full path of the file for which to calculate the MD5 checksum.</param>
        /// <returns>The MD5 checksum as string.</returns>
        public static string CalculateMD5Checksum(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        /// <summary>
        /// Read the MD5 checksum from the specified file and returns it as string.
        /// </summary>
        /// <param name="filename">Full path of the file from which to read the MD5 checksum.</param>
        /// <returns>The MD5 checksum as string or an empty string if it could not be read from the file.</returns>
        public static string ReadMD5ChecksumFromFile(string filename)
        {
            StreamReader file = new StreamReader(filename);

            // Read the first line only
            string line = file.ReadLine();

            // Close the file
            file.Close();

            // Did we get a line at least?
            if (line == null)
            {
                return "";
            }

            // Prepare the checksum string
            string checksum = line.TrimEnd('\r', '\n');
            if (checksum.Length != 32)
            {
                return "";
            }

            // Return the checksum
            return checksum;
        }

        /// <summary>
        /// Check if two paths point to the same physical location.
        /// </summary>
        /// <param name="path1">First path to compare.</param>
        /// <param name="path2">Second path to compare.</param>
        /// <returns></returns>
        public static bool ComparePaths(string path1, string path2)
        {
            return FileSystem.NormalizePath(path1).ToUpperInvariant().Equals(
                    FileSystem.NormalizePath(path2).ToUpperInvariant()
                );
        }

        /// <summary>
        /// Make sure backslashes in paths are replaced by forward slashes.
        /// </summary>
        /// <param name="path">Path to be preocessed.</param>
        /// <returns>Processed path.</returns>
        public static string ChangeBackwardToForwardSlashesInPath(string path)
        {
            path = path.Replace(@"\\\\", "/");
            path = path.Replace(@"\\", "/");
            path = path.Replace(@"\", "/");
            return path;
        }

        /// <summary>
        /// Normalize path.
        /// </summary>
        /// <param name="path">Path to be normalized.</param>
        /// <returns></returns>
        public static string NormalizePath(string path)
        {
            return Path.GetFullPath(new Uri(path).LocalPath)
                .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        }

        /// <summary>
        /// Grant full access to Everyone on the specified folder.
        /// </summary>
        /// <param name="fullPath">Full path to the folder to which the permissions should be set.</param>
        public static void GrantFullAccessToEveryone(string fullPath)
        {
            // Is the function being run by an administrator?
            if (!LocalUserManager.IsAdministrator())
            {
                // Inform
                const string msg = "This method must be run by an administrator.";

                sLogger.Error(msg);
                throw new InvalidOperationException(msg);
            }

            // Get a DirectoryInfo object
            DirectoryInfo dInfo = new DirectoryInfo(fullPath);

            // Make sure we are working on a directory
            if (!dInfo.Attributes.HasFlag(FileAttributes.Directory))
            {
                // Inform
                const string msg = "Directory expected!";

                sLogger.Error(msg);
                throw new Exception(msg);
            }

            // Set the permissions to Full Control for Everyone
            DirectorySecurity dSecurity = dInfo.GetAccessControl();
            dSecurity.AddAccessRule(new FileSystemAccessRule(
                new System.Security.Principal.SecurityIdentifier(
                    System.Security.Principal.WellKnownSidType.WorldSid, null),
                FileSystemRights.FullControl,
                InheritanceFlags.ObjectInherit |
                    InheritanceFlags.ContainerInherit,
                PropagationFlags.NoPropagateInherit,
                AccessControlType.Allow));
            dInfo.SetAccessControl(dSecurity);

            // Now recurse into subdirectories
            string[] subdirectories = Directory.GetDirectories(fullPath);
            foreach (string subdirectory in subdirectories)
            {
                GrantFullAccessToEveryone(Path.Combine(fullPath, subdirectory));
            }
        }

        /// <summary>
        /// Grant full access to a given user on the specified folder.
        /// </summary>
        /// <param name="fullPath">Full path to the folder to which the permissions should be set.</param>
        /// <param name="username">Name of the user that gets full access on the folder. It must exist already.</param>
        public static void GrantFullAccessToUser(string fullPath, string username)
        {
            // Is the function being run by an administrator?
            if (!LocalUserManager.IsAdministrator())
            {
                // Inform
                const string msg = "This method must be run by an administrator.";

                sLogger.Error(msg);
                throw new InvalidOperationException(msg);
            }

            // Get a DirectoryInfo object
            DirectoryInfo dInfo = new DirectoryInfo(fullPath);

            // Make sure we are working on a directory
            if (!dInfo.Attributes.HasFlag(FileAttributes.Directory))
            {
                // Inform
                const string msg = "Directory expected!";

                sLogger.Error(msg);
                throw new Exception(msg);
            }

            // Set the permissions to Full Control for Everyone
            DirectorySecurity dSecurity = dInfo.GetAccessControl();
            dSecurity.AddAccessRule(new FileSystemAccessRule(
                username,
                FileSystemRights.FullControl,
                InheritanceFlags.ObjectInherit |
                    InheritanceFlags.ContainerInherit,
                PropagationFlags.NoPropagateInherit,
                AccessControlType.Allow));
            dInfo.SetAccessControl(dSecurity);

            // Now recurse into subdirectories
            string[] subdirectories = Directory.GetDirectories(fullPath);
            foreach (string subdirectory in subdirectories)
            {
                GrantFullAccessToEveryone(Path.Combine(fullPath, subdirectory));
            }
        }

        ///<summary>
        ///Remove inherited permission from the specified folder.
        ///</summary>
        ///<param name="fullPath">Full path to the folder to which inherited
        ///    permissions should be removed.</param>
        public static void RemoveInheritedPermissionsForFolder(string fullPath)
        {
            // Is the function being run by an administrator?
            if (!LocalUserManager.IsAdministrator())
            {
                // Inform
                const string msg = "This method must be run by an administrator.";

                sLogger.Error(msg);
                throw new InvalidOperationException(msg);
            }

            DirectorySecurity directorySecurity = Directory.GetAccessControl(fullPath);
            directorySecurity.SetAccessRuleProtection(isProtected: true, preserveInheritance: false);
            Directory.SetAccessControl(fullPath, directorySecurity);
        }

        /// <summary>
        /// Remove all user permissions from folder.
        /// </summary>
        /// If the user with name `username` exists, it will be preserved.
        /// <param name="fullPath">Full path to the folder on which to remove
        ///     all user permissions.</param>
        /// <param name="username">Name of the user that will keep the
        ///     permissions.</param>
        public static void RemovePermissionsForAllUsersButOne(string fullPath, string username)
        {
            // Is the function being run by an administrator?
            if (!LocalUserManager.IsAdministrator())
            {
                // Inform
                const string msg = "This method must be run by an administrator.";

                sLogger.Error(msg);
                throw new InvalidOperationException(msg);
            }

            DirectoryInfo dirinfo = new DirectoryInfo(fullPath);
            DirectorySecurity dsec = dirinfo.GetAccessControl(AccessControlSections.All);

            AuthorizationRuleCollection rules =
                dsec.GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount));
            foreach (AccessRule rule in rules)
            {
                if (rule.IdentityReference.Value != username)
                {
                    dsec.PurgeAccessRules(rule.IdentityReference);
                    dsec.ModifyAccessRule(AccessControlModification.RemoveAll, rule, out bool value);

                    // Inform
                    sLogger.Info("Removed permission from '" + fullPath + "' for '" + username + "'.");
                }
            }
        }

        /// <summary>
        /// Return a list of all users who have any permissions on the folder.
        /// </summary>
        /// <param name="fullPath">Full path to the folder to query form users
        ///     with permissions.</param>
        /// <returns>List of user names.</returns>
        public static List<string> GetUsersWithPermissions(string fullPath)
        {
            // Is the function being run by an administrator?
            if (!LocalUserManager.IsAdministrator())
            {
                // Inform
                const string msg = "This method must be run by an administrator.";

                sLogger.Error(msg);
                throw new InvalidOperationException(msg);
            }

            // Initialize list of user names
            var userNames = new List<string>();

            // Get directory security
            DirectoryInfo dirinfo = new DirectoryInfo(fullPath);
            DirectorySecurity dsec = dirinfo.GetAccessControl(AccessControlSections.All);

            // Extract user names
            AuthorizationRuleCollection rules = dsec.GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount));
            foreach (AccessRule rule in rules)
            {
                userNames.Add(rule.IdentityReference.Value);
            }

            // Return the list of user names
            return userNames;
        }

        /// <summary>
        /// Get the ownership of a folder or file.
        /// </summary>
        /// <param name="fullPath">Full path to the folder or file for which to
        ///     query the ownership.</param>
        /// <returns>Owner's username.</returns>
        public static string GetOwner(string fullPath)
        {
            // Is the function being run by an administrator?
            if (!LocalUserManager.IsAdministrator())
            {
                // Inform
                const string msg = "This method must be run by an administrator.";

                sLogger.Error(msg);
                throw new InvalidOperationException(msg);
            }

            // Owner's user name
            string username = "";

            // Depending on whether we have a file or a folder, we call different methods
            if (Directory.Exists(fullPath))
            {
                var ds = File.GetAccessControl(fullPath);
                username = ds.GetOwner(typeof(System.Security.Principal.NTAccount)).ToString();
            }
            else if (File.Exists(fullPath))
            {
                var fs = File.GetAccessControl(fullPath);
                username = fs.GetOwner(typeof(System.Security.Principal.NTAccount)).ToString();
            }

            //Return the username
            return username;
        }

        /// <summary>
        /// Set the ownership on a folder to the specified user.
        /// </summary>
        /// <param name="fullPath">Full path to the folder on which to set the
        ///     ownership.</param>
        /// <param name="username">Name of the user that will take ownership. It
        ///     must exist.</param>
        public static void SetOwnership(string fullPath, string username)
        {
            // Is the function being run by an administrator?
            if (!LocalUserManager.IsAdministrator())
            {
                // Inform
                const string msg = "This method must be run by an administrator.";

                sLogger.Error(msg);
                throw new InvalidOperationException(msg);
            }

            // Depending on whether we have a file or a folder, we call different methods
            if (Directory.Exists(fullPath))
            {
                DirectoryInfo dInfo = new DirectoryInfo(fullPath);
                SetOwnershipRecursively(dInfo, username);
            }
            else if (File.Exists(fullPath))
            {
                FileInfo fi = new FileInfo(fullPath);
                SetOwnershipOnFile(fi, username);
            }
        }

        /// <summary>
        /// Set the ownership on a folder to the specified user.
        /// </summary>
        /// <param name="root">Full path to the folder on which to set the
        ///     ownership.</param>
        /// <param name="username">Name of the user that will take ownership. It
        ///     must exist.</param>
        private static void SetOwnershipRecursively(DirectoryInfo root, string username)
        {
            // Is the function being run by an administrator?
            if (!LocalUserManager.IsAdministrator())
            {
                // Inform
                const string msg = "This method must be run by an administrator.";

                sLogger.Error(msg);
                throw new InvalidOperationException(msg);
            }

            // Set permission on the directory itself before we recurse into subfolders
            SetOwnershipOnDirectory(root, username);

            // Store files and directories to process
            FileInfo[] children = null;
            DirectoryInfo[] subDirs = null;

            // First, process all the files directly under this folder
            try
            {
                children = root.GetFiles("*");
            }
            // This is thrown if even one of the files requires permissions greater
            // than the application provides.
            catch (UnauthorizedAccessException e)
            {
                // Inform
                sLogger.Error("Cannot access folder '" + root + "'. The error was: " + e.Message);
            }

            catch (DirectoryNotFoundException e)
            {
                // Inform
                sLogger.Error("Folder '" + root + "' not found. The error was: " + e.Message);
            }

            if (children != null)
            {
                foreach (FileInfo fi in children)
                {
                    // In this example, we only access the existing FileInfo object. If we
                    // want to open, delete or modify the file, then
                    // a try-catch block is required here to handle the case
                    // where the file has been deleted since the call to TraverseTree().
                    SetOwnershipOnFile(fi, username);
                }

                // Now find all the subdirectories under this directory.
                subDirs = root.GetDirectories();

                foreach (DirectoryInfo dirInfo in subDirs)
                {
                    // Set ownership on the directory itself, and then recurse
                    SetOwnershipOnDirectory(dirInfo, username);

                    // Resursive call for each subdirectory.
                    SetOwnershipRecursively(dirInfo, username);
                }
            }
        }

        /// <summary>
        /// Set the ownership on an item to the specified user.
        /// </summary>
        /// <param name="item">A FileInfo object on which to set the ownership.
        ///     </param>
        /// <param name="username">Name of the user that will take ownership. It
        ///     must exist.</param>
        private static void SetOwnershipOnFile(FileInfo item, string username)
        {
            // Is the function being run by an administrator?
            if (!LocalUserManager.IsAdministrator())
            {
                // Inform
                const string msg = "This method must be run by an administrator.";

                sLogger.Error(msg);
                throw new InvalidOperationException(msg);
            }

            // Allow this process to circumvent ACL restrictions
            NativeMethods.ModifyPrivilege(PrivilegeName.SeRestorePrivilege, true);

            // Sometimes this is required and other times it works without it. Not sure when.
            NativeMethods.ModifyPrivilege(PrivilegeName.SeTakeOwnershipPrivilege, true);

            // Set owner to `username`
            var fs = File.GetAccessControl(item.FullName);
            fs.SetOwner(new System.Security.Principal.NTAccount(username));
            item.SetAccessControl(fs);
        }

        /// <summary>
        /// Set the ownership on an item to the specified user.
        /// </summary>
        /// <param name="item">A FileInfo object on which to set the ownership.
        ///     </param>
        /// <param name="username">Name of the user that will take ownership. It
        ///     must exist.</param>
        private static void SetOwnershipOnDirectory(DirectoryInfo item, string username)
        {
            // Is the function being run by an administrator?
            if (!LocalUserManager.IsAdministrator())
            {
                // Inform
                const string msg = "This method must be run by an administrator.";

                sLogger.Error(msg);
                throw new InvalidOperationException(msg);
            }

            // Allow this process to circumvent ACL restrictions
            NativeMethods.ModifyPrivilege(PrivilegeName.SeRestorePrivilege, true);

            // Sometimes this is required and other times it works without it. Not sure when.
            NativeMethods.ModifyPrivilege(PrivilegeName.SeTakeOwnershipPrivilege, true);

            // Set owner to SYSTEM
            var fs = File.GetAccessControl(item.FullName);
            fs.SetOwner(new System.Security.Principal.NTAccount(username));
            File.SetAccessControl(item.FullName, fs);
        }

        /// <summary>
        /// From
        /// https://stackoverflow.com/questions/37992462/how-to-set-the-owner-of-a-file-to-system.
        /// </summary>
        private static class NativeMethods
        {
            /// <summary>
            /// Enables or disables the specified privilege on the primary
            /// access token of the current process.
            /// </summary>
            /// <param name="privilege">Privilege to enable or disable.</param>
            /// <param name="enable">True to enable the privilege, false to
            ///     disable it.</param>
            /// <returns>True if the privilege was enabled prior to the change,
            ///     false if it was disabled.</returns>
            public static bool ModifyPrivilege(PrivilegeName privilege, bool enable)
            {
                if (!LookupPrivilegeValue(null, privilege.ToString(), out LUID luid))
                    throw new System.ComponentModel.Win32Exception();

                using (var identity = System.Security.Principal.WindowsIdentity.GetCurrent(
                    System.Security.Principal.TokenAccessLevels.AdjustPrivileges |
                    System.Security.Principal.TokenAccessLevels.Query))
                {
                    var newPriv = new TOKEN_PRIVILEGES
                    {
                        Privileges = new LUID_AND_ATTRIBUTES[1],
                        PrivilegeCount = 1
                    };
                    newPriv.Privileges[0].Luid = luid;
                    newPriv.Privileges[0].Attributes = enable ? SE_PRIVILEGE_ENABLED : 0;

                    var prevPriv = new TOKEN_PRIVILEGES
                    {
                        Privileges = new LUID_AND_ATTRIBUTES[1],
                        PrivilegeCount = 1
                    };

                    if (!AdjustTokenPrivileges(identity.Token, false, ref newPriv,
                        (uint)Marshal.SizeOf(prevPriv), ref prevPriv, out uint returnedBytes))
                        throw new System.ComponentModel.Win32Exception();

                    return prevPriv.PrivilegeCount == 0 ? enable /* didn't make a change */ : ((prevPriv.Privileges[0].Attributes & SE_PRIVILEGE_ENABLED) != 0);
                }
            }

            const uint SE_PRIVILEGE_ENABLED = 2;

            [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            static extern bool AdjustTokenPrivileges(IntPtr TokenHandle,
                [MarshalAs(UnmanagedType.Bool)] bool DisableAllPrivileges,
                ref TOKEN_PRIVILEGES NewState,
               UInt32 BufferLengthInBytes, ref TOKEN_PRIVILEGES PreviousState, out UInt32 ReturnLengthInBytes);

            [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            static extern bool LookupPrivilegeValue(string lpSystemName, string lpName, out LUID lpLuid);

            struct TOKEN_PRIVILEGES
            {
                public UInt32 PrivilegeCount;
                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1 /*ANYSIZE_ARRAY*/)]
                public LUID_AND_ATTRIBUTES[] Privileges;
            }

            [StructLayout(LayoutKind.Sequential)]
            struct LUID_AND_ATTRIBUTES
            {
                public LUID Luid;
                public UInt32 Attributes;
            }

            [StructLayout(LayoutKind.Sequential)]
            struct LUID
            {
                public uint LowPart;
                public int HighPart;
            }
        }

        private enum PrivilegeName
        {
            SeAssignPrimaryTokenPrivilege,
            SeAuditPrivilege,
            SeBackupPrivilege,
            SeChangeNotifyPrivilege,
            SeCreateGlobalPrivilege,
            SeCreatePagefilePrivilege,
            SeCreatePermanentPrivilege,
            SeCreateSymbolicLinkPrivilege,
            SeCreateTokenPrivilege,
            SeDebugPrivilege,
            SeEnableDelegationPrivilege,
            SeImpersonatePrivilege,
            SeIncreaseBasePriorityPrivilege,
            SeIncreaseQuotaPrivilege,
            SeIncreaseWorkingSetPrivilege,
            SeLoadDriverPrivilege,
            SeLockMemoryPrivilege,
            SeMachineAccountPrivilege,
            SeManageVolumePrivilege,
            SeProfileSingleProcessPrivilege,
            SeRelabelPrivilege,
            SeRemoteShutdownPrivilege,
            SeRestorePrivilege,
            SeSecurityPrivilege,
            SeShutdownPrivilege,
            SeSyncAgentPrivilege,
            SeSystemEnvironmentPrivilege,
            SeSystemProfilePrivilege,
            SeSystemtimePrivilege,
            SeTakeOwnershipPrivilege,
            SeTcbPrivilege,
            SeTimeZonePrivilege,
            SeTrustedCredManAccessPrivilege,
            SeUndockPrivilege,
            SeUnsolicitedInputPrivilege,
        }
    }
}
