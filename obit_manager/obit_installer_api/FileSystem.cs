using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace obit_manager_api
{
    public static class FileSystem
    {
        /**
         * <summary>Grant full access to Everyone on the specified folder.</summary>
         * 
         * This function can only be run by an administrator.
         * 
         * <param name="fullPath">Full path to the folder to which the permissions should be set.</param>
         **/
        public static void GrantFullAccessToEveryone(string fullPath)
        {
            // Is the function being run by an administrator?
            if (! Utils.IsAdministrator())
            {
                throw new InvalidOperationException("This method must be run by an administrator.");
            }

            // Get a DirectoryInfo object
            System.IO.DirectoryInfo dInfo = new System.IO.DirectoryInfo(fullPath);

            // Make sure we are working on a directory
            if (! dInfo.Attributes.HasFlag(System.IO.FileAttributes.Directory))
            {
                throw new Exception("Directory expected!");
            }

            // Set the permissions to Full Control for Everyone
            System.Security.AccessControl.DirectorySecurity dSecurity = dInfo.GetAccessControl();
            dSecurity.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(
                new System.Security.Principal.SecurityIdentifier(
                    System.Security.Principal.WellKnownSidType.WorldSid, null),
                System.Security.AccessControl.FileSystemRights.FullControl,
                System.Security.AccessControl.InheritanceFlags.ObjectInherit | System.Security.AccessControl.InheritanceFlags.ContainerInherit,
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

        /**
         * <summary>Grant full access to a given user on the specified folder.</summary>
         * 
         * <param name="fullPath">Full path to the folder to which the permissions should be set.</param>
         * <param name="username">Name of the user that gets full access on the folder. It must exist already.</param>
         **/
        public static void GrantFullAccessToUser(string fullPath, string username)
        {
            // Is the function being run by an administrator?
            if (!Utils.IsAdministrator())
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
                System.Security.AccessControl.InheritanceFlags.ObjectInherit | System.Security.AccessControl.InheritanceFlags.ContainerInherit,
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

        /**
         * <summary>Remove inherited permission from the specified folder.</summary>
         * 
         * <param name="fullPath">Full path to the folder to which inherited permissions should be removed.</param>
         **/
        public static void RemoveInheritedPermissionsForFolder(string fullPath)
        {
            // Is the function being run by an administrator?
            if (!Utils.IsAdministrator())
            {
                throw new InvalidOperationException("This method must be run by an administrator.");
            }

            bool keepPermissions = false;
            System.Security.AccessControl.DirectorySecurity directorySecurity = System.IO.Directory.GetAccessControl(fullPath);
            directorySecurity.SetAccessRuleProtection(true, keepPermissions);
            System.IO.Directory.SetAccessControl(fullPath, directorySecurity);
        }

        /// <summary>
        ///     Remove all user permissions from folder.</summary>
        /// <param name="fullPath">
        ///     Full path to the folder on which to remove all user permissions.</param>
        /// <param name="username">
        ///     Name of the user that will keep the permissions.</param>
        public static void removePermissionsForAllUsersButOne(string fullPath, string username)
        {
            // Is the function being run by an administrator?
            if (!Utils.IsAdministrator())
            {
                throw new InvalidOperationException("This method must be run by an administrator.");
            }

            System.IO.DirectoryInfo dirinfo = new System.IO.DirectoryInfo(fullPath);
            System.Security.AccessControl.DirectorySecurity dsec = dirinfo.GetAccessControl(System.Security.AccessControl.AccessControlSections.All);

            AuthorizationRuleCollection rules = dsec.GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount));
            foreach (AccessRule rule in rules)
            {
                if (rule.IdentityReference.Value != username)
                {
                    bool value;
                    dsec.PurgeAccessRules(rule.IdentityReference);
                    dsec.ModifyAccessRule(AccessControlModification.RemoveAll, rule, out value);
                    System.Console.WriteLine("Removed permission from " + fullPath + " for " + username);
                }
            }
        }

        /// <summary>
        ///     Set the ownership on a folder to the specified user.</summary>
        /// <param name="fullPath">
        ///     Full path to the folder on which to set the ownership.</param>
        /// <param name="username">
        ///     Name of the user that will take ownership. It must exist.</param>
        public static void SetOwnership(string fullPath, string username)
        {
            // Is the function being run by an administrator?
            if (!Utils.IsAdministrator())
            {
                throw new InvalidOperationException("This method must be run by an administrator.");
            }

            System.IO.DirectoryInfo dInfo = new System.IO.DirectoryInfo(fullPath);
            SetOwnershipRecursively(dInfo, username);
        }

        /// <summary>
        ///     Set the ownership on a folder to the specified user.</summary>
        /// <param name="fullPath">
        ///     Full path to the folder on which to set the ownership.</param>
        /// <param name="username">
        ///     Name of the user that will take ownership. It must exist.</param>
        private static void SetOwnershipRecursively(System.IO.DirectoryInfo root, string username)
        {
            // Is the function being run by an administrator?
            if (!Utils.IsAdministrator())
            {
                throw new InvalidOperationException("This method must be run by an administrator.");
            }

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
        ///     Set the ownership on an item to the specified user.</summary>
        /// <param name="item">
        ///     A FileInfo object on which to set the ownership.</param>
        /// <param name="username">
        ///     Name of the user that will take ownership. It must exist.</param>
        private static void SetOwnershipOnFile(System.IO.FileInfo item, string username)
        {
            // Is the function being run by an administrator?
            if (!Utils.IsAdministrator())
            {
                throw new InvalidOperationException("This method must be run by an administrator.");
            }

            // Allow this process to circumvent ACL restrictions
            WinAPI.ModifyPrivilege(PrivilegeName.SeRestorePrivilege, true);

            // Sometimes this is required and other times it works without it. Not sure when.
            WinAPI.ModifyPrivilege(PrivilegeName.SeTakeOwnershipPrivilege, true);

            // Set owner to SYSTEM
            var fs = System.IO.File.GetAccessControl(item.FullName);
            fs.SetOwner(new System.Security.Principal.NTAccount(username));
            item.SetAccessControl(fs);
        }

        /// <summary>
        ///     Set the ownership on an item to the specified user.</summary>
        /// <param name="item">
        ///     A FileInfo object on which to set the ownership.</param>
        /// <param name="username">
        ///     Name of the user that will take ownership. It must exist.</param>
        private static void SetOwnershipOnDirectory(System.IO.DirectoryInfo item, string username)
        {
            // Is the function being run by an administrator?
            if (!Utils.IsAdministrator())
            {
                throw new InvalidOperationException("This method must be run by an administrator.");
            }

            // Allow this process to circumvent ACL restrictions
            WinAPI.ModifyPrivilege(PrivilegeName.SeRestorePrivilege, true);

            // Sometimes this is required and other times it works without it. Not sure when.
            WinAPI.ModifyPrivilege(PrivilegeName.SeTakeOwnershipPrivilege, true);

            // Set owner to SYSTEM
            var fs = System.IO.File.GetAccessControl(item.FullName);
            fs.SetOwner(new System.Security.Principal.NTAccount(username));
            System.IO.File.SetAccessControl(item.FullName, fs);
        }

        /// <summary>
        ///     From https://stackoverflow.com/questions/37992462/how-to-set-the-owner-of-a-file-to-system.
        /// </summary>
        private static class WinAPI
        {
            /// <summary>
            ///     Enables or disables the specified privilege on the primary access token of the current process.</summary>
            /// <param name="privilege">
            ///     Privilege to enable or disable.</param>
            /// <param name="enable">
            ///     True to enable the privilege, false to disable it.</param>
            /// <returns>
            ///     True if the privilege was enabled prior to the change, false if it was disabled.</returns>
            public static bool ModifyPrivilege(PrivilegeName privilege, bool enable)
            {
                LUID luid;
                if (!LookupPrivilegeValue(null, privilege.ToString(), out luid))
                    throw new System.ComponentModel.Win32Exception();

                using (var identity = System.Security.Principal.WindowsIdentity.GetCurrent(System.Security.Principal.TokenAccessLevels.AdjustPrivileges |
                    System.Security.Principal.TokenAccessLevels.Query))
                {
                    var newPriv = new TOKEN_PRIVILEGES();
                    newPriv.Privileges = new LUID_AND_ATTRIBUTES[1];
                    newPriv.PrivilegeCount = 1;
                    newPriv.Privileges[0].Luid = luid;
                    newPriv.Privileges[0].Attributes = enable ? SE_PRIVILEGE_ENABLED : 0;

                    var prevPriv = new TOKEN_PRIVILEGES();
                    prevPriv.Privileges = new LUID_AND_ATTRIBUTES[1];
                    prevPriv.PrivilegeCount = 1;
                    uint returnedBytes;

                    if (!AdjustTokenPrivileges(identity.Token, false, ref newPriv, (uint)System.Runtime.InteropServices.Marshal.SizeOf(prevPriv), ref prevPriv, out returnedBytes))
                        throw new System.ComponentModel.Win32Exception();

                    return prevPriv.PrivilegeCount == 0 ? enable /* didn't make a change */ : ((prevPriv.Privileges[0].Attributes & SE_PRIVILEGE_ENABLED) != 0);
                }
            }

            const uint SE_PRIVILEGE_ENABLED = 2;

            [System.Runtime.InteropServices.DllImport("advapi32.dll", SetLastError = true)]
            [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
            static extern bool AdjustTokenPrivileges(IntPtr TokenHandle, 
                [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)] bool DisableAllPrivileges, 
                ref TOKEN_PRIVILEGES NewState,
               UInt32 BufferLengthInBytes, ref TOKEN_PRIVILEGES PreviousState, out UInt32 ReturnLengthInBytes);

            [System.Runtime.InteropServices.DllImport("advapi32.dll", SetLastError = true,
                CharSet = System.Runtime.InteropServices.CharSet.Auto)]
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
