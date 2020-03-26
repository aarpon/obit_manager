using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Cryptography;

namespace obit_manager_api
{
    namespace core
    {
        public static class FileSystem
        {
            /// <summary>
            /// Copy folder recursively.
            /// </summary>
            /// <param name="sourceFolder">Source folder.</param>
            /// <param name="destFolder">Destination folder.</param>
            static public void CopyFolderRecursively(string sourceFolder, string destFolder)
            {
                // Does the source folder exist?
                if (!Directory.Exists(sourceFolder))
                {
                    throw new FileNotFoundException("Folder " + sourceFolder + " does not exist!");
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
                    Console.WriteLine("Could not extract ZIP file " + zipFile + ": the error was " + e.Message);
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
            /// Grant full access to Everyone on the specified folder.
            /// </summary>
            /// <param name="fullPath">Full path to the folder to which the permissions should be set.</param>
            public static void GrantFullAccessToEveryone(string fullPath)
            {
                // Is the function being run by an administrator?
                if (!LocalUserManager.IsAdministrator())
                {
                    throw new InvalidOperationException("This method must be run by an administrator.");
                }

                // Get a DirectoryInfo object
                System.IO.DirectoryInfo dInfo = new System.IO.DirectoryInfo(fullPath);

                // Make sure we are working on a directory
                if (!dInfo.Attributes.HasFlag(System.IO.FileAttributes.Directory))
                {
                    throw new Exception("Directory expected!");
                }

                // Set the permissions to Full Control for Everyone
                System.Security.AccessControl.DirectorySecurity dSecurity = dInfo.GetAccessControl();
                dSecurity.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(
                    new System.Security.Principal.SecurityIdentifier(
                        System.Security.Principal.WellKnownSidType.WorldSid, null),
                    System.Security.AccessControl.FileSystemRights.FullControl,
                    System.Security.AccessControl.InheritanceFlags.ObjectInherit |
                        System.Security.AccessControl.InheritanceFlags.ContainerInherit,
                    System.Security.AccessControl.PropagationFlags.NoPropagateInherit,
                    System.Security.AccessControl.AccessControlType.Allow));
                dInfo.SetAccessControl(dSecurity);

                // Now recurse into subdirectories
                string[] subdirectories = System.IO.Directory.GetDirectories(fullPath);
                foreach (string subdirectory in subdirectories)
                {
                    GrantFullAccessToEveryone(System.IO.Path.Combine(fullPath, subdirectory));
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
                    throw new InvalidOperationException("This method must be run by an administrator.");
                }

                // Get a DirectoryInfo object
                System.IO.DirectoryInfo dInfo = new System.IO.DirectoryInfo(fullPath);

                // Make sure we are working on a directory
                if (!dInfo.Attributes.HasFlag(System.IO.FileAttributes.Directory))
                {
                    throw new Exception("Directory expected!");
                }

                // Set the permissions to Full Control for Everyone
                System.Security.AccessControl.DirectorySecurity dSecurity = dInfo.GetAccessControl();
                dSecurity.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(
                    username,
                    System.Security.AccessControl.FileSystemRights.FullControl,
                    System.Security.AccessControl.InheritanceFlags.ObjectInherit |
                        System.Security.AccessControl.InheritanceFlags.ContainerInherit,
                    System.Security.AccessControl.PropagationFlags.NoPropagateInherit,
                    System.Security.AccessControl.AccessControlType.Allow));
                dInfo.SetAccessControl(dSecurity);

                // Now recurse into subdirectories
                string[] subdirectories = System.IO.Directory.GetDirectories(fullPath);
                foreach (string subdirectory in subdirectories)
                {
                    GrantFullAccessToEveryone(System.IO.Path.Combine(fullPath, subdirectory));
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
                    throw new InvalidOperationException("This method must be run by an administrator.");
                }

                bool keepPermissions = false;
                System.Security.AccessControl.DirectorySecurity directorySecurity =
                    System.IO.Directory.GetAccessControl(fullPath);
                directorySecurity.SetAccessRuleProtection(true, keepPermissions);
                System.IO.Directory.SetAccessControl(fullPath, directorySecurity);
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
                    throw new InvalidOperationException("This method must be run by an administrator.");
                }

                System.IO.DirectoryInfo dirinfo = new System.IO.DirectoryInfo(fullPath);
                System.Security.AccessControl.DirectorySecurity dsec =
                    dirinfo.GetAccessControl(System.Security.AccessControl.AccessControlSections.All);

                AuthorizationRuleCollection rules =
                    dsec.GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount));
                foreach (AccessRule rule in rules)
                {
                    if (rule.IdentityReference.Value != username)
                    {
                        dsec.PurgeAccessRules(rule.IdentityReference);
                        dsec.ModifyAccessRule(AccessControlModification.RemoveAll, rule, out bool value);
                        System.Console.WriteLine("Removed permission from " + fullPath + " for " + username);
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
                    throw new InvalidOperationException("This method must be run by an administrator.");
                }

                // Initialize list of user names
                var userNames = new List<string>();

                // Get directory security
                System.IO.DirectoryInfo dirinfo = new System.IO.DirectoryInfo(fullPath);
                System.Security.AccessControl.DirectorySecurity dsec =
                    dirinfo.GetAccessControl(System.Security.AccessControl.AccessControlSections.All);

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
                    throw new InvalidOperationException("This method must be run by an administrator.");
                }

                // Owner's user name
                string username = "";

                // Depending on whether we have a file or a folder, we call different methods
                if (System.IO.Directory.Exists(fullPath))
                {
                    var ds = System.IO.File.GetAccessControl(fullPath);
                    username = ds.GetOwner(typeof(System.Security.Principal.NTAccount)).ToString();
                }
                else if (System.IO.File.Exists(fullPath))
                {
                    var fs = System.IO.File.GetAccessControl(fullPath);
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
                    throw new InvalidOperationException("This method must be run by an administrator.");
                }

                // Depending on whether we have a file or a folder, we call different methods
                if (System.IO.Directory.Exists(fullPath))
                {
                    System.IO.DirectoryInfo dInfo = new System.IO.DirectoryInfo(fullPath);
                    SetOwnershipRecursively(dInfo, username);
                }
                else if (System.IO.File.Exists(fullPath))
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(fullPath);
                    SetOwnershipOnFile(fi, username);
                }
            }

            /// <summary>
            /// Set the ownership on a folder to the specified user.
            /// </summary>
            /// <param name="fullPath">Full path to the folder on which to set the
            ///     ownership.</param>
            /// <param name="username">Name of the user that will take ownership. It
            ///     must exist.</param>
            private static void SetOwnershipRecursively(System.IO.DirectoryInfo root, string username)
            {
                // Is the function being run by an administrator?
                if (!LocalUserManager.IsAdministrator())
                {
                    throw new InvalidOperationException("This method must be run by an administrator.");
                }

                // Set permission on the directory itself before we recurse into subfolders
                SetOwnershipOnDirectory(root, username);

                // Store files and directories to process
                System.IO.FileInfo[] children = null;
                System.IO.DirectoryInfo[] subDirs = null;

                // First, process all the files directly under this folder
                try
                {
                    children = root.GetFiles("*");
                }
                // This is thrown if even one of the files requires permissions greater
                // than the application provides.
                catch (UnauthorizedAccessException e)
                {
                    System.Console.WriteLine("Cannot access folder " + root + ". Error was: " + e.Message);
                }

                catch (System.IO.DirectoryNotFoundException e)
                {
                    System.Console.WriteLine("Folder " + root + " not found. Error was: " + e.Message);
                }

                if (children != null)
                {
                    foreach (System.IO.FileInfo fi in children)
                    {
                        // In this example, we only access the existing FileInfo object. If we
                        // want to open, delete or modify the file, then
                        // a try-catch block is required here to handle the case
                        // where the file has been deleted since the call to TraverseTree().
                        SetOwnershipOnFile(fi, username);
                    }

                    // Now find all the subdirectories under this directory.
                    subDirs = root.GetDirectories();

                    foreach (System.IO.DirectoryInfo dirInfo in subDirs)
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
            private static void SetOwnershipOnFile(System.IO.FileInfo item, string username)
            {
                // Is the function being run by an administrator?
                if (!LocalUserManager.IsAdministrator())
                {
                    throw new InvalidOperationException("This method must be run by an administrator.");
                }

                // Allow this process to circumvent ACL restrictions
                NativeMethods.ModifyPrivilege(PrivilegeName.SeRestorePrivilege, true);

                // Sometimes this is required and other times it works without it. Not sure when.
                NativeMethods.ModifyPrivilege(PrivilegeName.SeTakeOwnershipPrivilege, true);

                // Set owner to `username`
                var fs = System.IO.File.GetAccessControl(item.FullName);
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
            private static void SetOwnershipOnDirectory(System.IO.DirectoryInfo item, string username)
            {
                // Is the function being run by an administrator?
                if (!LocalUserManager.IsAdministrator())
                {
                    throw new InvalidOperationException("This method must be run by an administrator.");
                }

                // Allow this process to circumvent ACL restrictions
                NativeMethods.ModifyPrivilege(PrivilegeName.SeRestorePrivilege, true);

                // Sometimes this is required and other times it works without it. Not sure when.
                NativeMethods.ModifyPrivilege(PrivilegeName.SeTakeOwnershipPrivilege, true);

                // Set owner to SYSTEM
                var fs = System.IO.File.GetAccessControl(item.FullName);
                fs.SetOwner(new System.Security.Principal.NTAccount(username));
                System.IO.File.SetAccessControl(item.FullName, fs);
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
                            (uint)System.Runtime.InteropServices.Marshal.SizeOf(prevPriv), ref prevPriv, out uint returnedBytes))
                            throw new System.ComponentModel.Win32Exception();

                        return prevPriv.PrivilegeCount == 0 ? enable /* didn't make a change */ : ((prevPriv.Privileges[0].Attributes & SE_PRIVILEGE_ENABLED) != 0);
                    }
                }

                const uint SE_PRIVILEGE_ENABLED = 2;

                [System.Runtime.InteropServices.DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
                [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
                static extern bool AdjustTokenPrivileges(IntPtr TokenHandle,
                    [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)] bool DisableAllPrivileges,
                    ref TOKEN_PRIVILEGES NewState,
                   UInt32 BufferLengthInBytes, ref TOKEN_PRIVILEGES PreviousState, out UInt32 ReturnLengthInBytes);

                [System.Runtime.InteropServices.DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
                [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
                static extern bool LookupPrivilegeValue(string lpSystemName, string lpName, out LUID lpLuid);

                struct TOKEN_PRIVILEGES
                {
                    public UInt32 PrivilegeCount;
                    [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 1 /*ANYSIZE_ARRAY*/)]
                    public LUID_AND_ATTRIBUTES[] Privileges;
                }

                [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
                struct LUID_AND_ATTRIBUTES
                {
                    public LUID Luid;
                    public UInt32 Attributes;
                }

                [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
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
}
