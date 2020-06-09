using System;
using System.IO;
using NLog;
using obit_manager_api.core;
using obit_manager_settings;

namespace obit_manager_gui.fs
{
    public static class AnnotationToolFileSystemTools
    {
        /// <summary>
        /// Logger
        /// </summary>
        private static readonly Logger sLogger = LogManager.GetCurrentClassLogger();

        public static bool FolderExists(string installationDir)
        {
            string fileName = Path.Combine(installationDir, @"obit_annotation_tool");
            return File.Exists(fileName);
        }

        /// <summary>
        /// Extract the AnnotationTool archive to the provided installation directory.
        /// </summary>
        /// <param name="installationDir">Installation directory.</param>
        /// <param name="archiveName">Full path to the archive name. If not set, the method
        /// will try to extract the file with the expected name for current platform from
        /// the installation directory.</param>
        /// <returns>True if the archive could be extracted successfully, false otherwise.</returns>
        public static bool ExtractArchive(string installationDir, string archiveName = null)
        {
            // Was the archive name passed?
            if (archiveName == null)
            {
                if (Environment.Is64BitOperatingSystem)
                {
                    archiveName = Constants.AnnotationTool64bitArchiveFileName;
                }
                else
                {
                    archiveName = Constants.AnnotationTool32bitArchiveFileName;
                }

                // Build full path
                archiveName = Path.Combine(installationDir, archiveName);
            }

            // Does the archive exist?
            if (!File.Exists(archiveName))
            {
                // Log
                sLogger.Error("Annotation Tool archive file '" + archiveName + "' was not found!");

                // Return failure
                return false;
            }

            // Destination folder
            string destFolder = Path.Combine(installationDir, @"obit_annotation_tool");

            // Extract the zip file
            return FileSystem.ExtractZIPFileToFolder(archiveName, destFolder);
        }
    }
}