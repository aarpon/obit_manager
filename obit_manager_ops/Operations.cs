using System;
using System.IO;
using System.Threading.Tasks;
using NLog;
using obit_manager_api.core;
using obit_manager_config;


namespace obit_manager_gui.tools
{
    public static class Operations
    {
        #region public

        /// <summary>
        /// Logger
        /// </summary>
        private static readonly Logger sLogger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Delete downloaded archives from the installation directory.
        /// </summary>
        /// <param name="is64bit">True for a 64-bit JRE, false for a 32-bit JRE.</param>
        /// <param name="installationFolder">Installation folder.</param>
        /// <returns></returns>
        public static void DeleteDownloadedArchives(bool is64bit, string installationFolder)
        {
            // File names
            string datamoverJSLFileName = Path.Combine(installationFolder, Constants.DatamoverJslArchiveFileName);
            string datamoverFileName = Path.Combine(installationFolder, Constants.DatamoverArchiveFileName);
            string jdkFileName;
            string annotationToolFileName;

            if (is64bit)
            {
                jdkFileName = Path.Combine(installationFolder, Constants.Jdk64bitArchiveFileName);
                annotationToolFileName = Path.Combine(installationFolder, Constants.AnnotationTool64bitArchiveFileName);
            }
            else
            {
                jdkFileName = Path.Combine(installationFolder, Constants.Jdk32bitArchiveFileName);
                annotationToolFileName = Path.Combine(installationFolder, Constants.AnnotationTool32bitArchiveFileName);
            }

            if (File.Exists(datamoverJSLFileName))
            {
                File.Delete(datamoverJSLFileName);

                // Inform
                sLogger.Info("Deleted file '" + datamoverJSLFileName + "'.");
            }
            if (File.Exists(datamoverFileName))
            {
                File.Delete(datamoverFileName);

                // Inform
                sLogger.Info("Deleted file '" + datamoverFileName + "'.");
            }
            if (File.Exists(jdkFileName))
            {
                File.Delete(jdkFileName);

                // Inform
                sLogger.Info("Deleted file '" + jdkFileName + "'.");
            }
            if (File.Exists(annotationToolFileName))
            {
                File.Delete(annotationToolFileName);

                // Inform
                sLogger.Info("Deleted file '" + annotationToolFileName + "'.");
            }
        }

        /// <summary>
        /// Download, check and extract the requested Java JDK.
        /// </summary>
        /// <param name="is64bit">True for a 64-bit JRE, false for a 32-bit JRE.</param>
        /// <param name="installationFolder">Installation folder.</param>
        /// <returns></returns>
        public static async Task DownloadCheckAndInstallJDKAsync(bool is64bit, string installationFolder)
        {
            // Check that the folder exists
            FileInfo file = new FileInfo(installationFolder);
            file.Directory.Create();

            // Declare needed variables
            string downloadURL, targetFileName, jdkExtractPath, jdkFinalPath;
            string downloadMD5URL, targetMD5FileName;

            // Set the correct URLs and paths
            if (is64bit)
            {
                // Java 64 bit JRE
                downloadURL = Constants.Jdk64bitURL;
                targetFileName = Path.Combine(installationFolder, Constants.Jdk64bitArchiveFileName);
                jdkExtractPath = Path.Combine(installationFolder, Constants.Jdk64bitExtractDirName);
                jdkFinalPath = Path.Combine(installationFolder, Constants.Jdk64bitFinalPath);

                // Jave 64 bit JRE MD5 checksum
                downloadMD5URL = Constants.Jdk64bitMD5URL;
                targetMD5FileName = Path.Combine(installationFolder, Constants.Jdk64bitMD5FileName);
            }
            else
            {
                // Java 32 bit JRE
                downloadURL = Constants.Jdk32bitURL;
                targetFileName = Path.Combine(installationFolder, Constants.Jdk32bitArchiveFileName);
                jdkExtractPath = Path.Combine(installationFolder, Constants.Jdk32bitExtractDirName);
                jdkFinalPath = Path.Combine(installationFolder, Constants.Jdk32bitFinalPath);

                // Jave 32 bit JRE MD5 checksum
                downloadMD5URL = Constants.Jdk32bitMD5URL;
                targetMD5FileName = Path.Combine(installationFolder, Constants.Jdk32bitMD5FileName);
            }

            // Make sure that the installation directory exists
            Directory.CreateDirectory(installationFolder);

            // Download the file
            await WebUtils.DownloadAsync(downloadURL, targetFileName);
            if (!File.Exists(targetFileName))
            {
                // Inform
                string msg = "Could not download Java JRE!";

                sLogger.Error(msg);
                throw new FileNotFoundException(msg);
            }

            // Download the checksum
            await WebUtils.DownloadAsync(downloadMD5URL, targetMD5FileName);
            if (!File.Exists(targetMD5FileName))
            {
                // Inform
                string msg = "Could not download Java JRE checksum file!";

                sLogger.Error(msg);
                throw new FileNotFoundException(msg);
            }

            // Read  the checksum
            String Jdk64bitMD5Checksum = FileSystem.ReadMD5ChecksumFromFile(targetMD5FileName);

            // Calculate and compare MD5 checksum
            if (!FileSystem.CalculateMD5Checksum(targetFileName).Equals(Jdk64bitMD5Checksum))
            {
                // Inform
                string msg = "The Java JRE's checksum does not match!";

                sLogger.Error(msg);
                throw new Exception(msg);
            }

            // Delete the checksum file
            File.Delete(targetMD5FileName);

            // Decompress the file
            FileSystem.ExtractZIPFileToFolder(targetFileName, installationFolder);

            // Check that the extract folder exists
            if (!Directory.Exists(jdkExtractPath))
            {
                // Inform
                string msg = "Could not extract Java JRE!";

                sLogger.Error(msg);
                throw new FileNotFoundException(msg);
            }

            // Rename the JRE folder
            try
            {
                Directory.Move(jdkExtractPath, jdkFinalPath);
            }
            catch (Exception)
            {
                // Inform
                string msg = "Could not rename folder '" + jdkExtractPath + "' to '" + jdkFinalPath + "'!";

                sLogger.Error(msg);
                throw new FileNotFoundException(msg);

            }

            // Finally, check if the jre folder is in the final location
            if (!Directory.Exists(jdkFinalPath))
            {
                // Inform
                string msg = "Could not find expected Java JRE path '" + jdkFinalPath + "'!";

                sLogger.Error(msg);
                throw new FileNotFoundException(msg);
            }

            // Check that the jvm.dll file is in the expected place
            String jvmDllPath = Path.Combine(jdkFinalPath, @"bin\server\jvm.dll");
            if (!File.Exists(jvmDllPath))
            {
                // Inform
                string msg = "Could not find jvm.dll!";

                sLogger.Error(msg);
                throw new FileNotFoundException(msg);
            }
        }

        /// <summary>
        /// Download, check and extract the Annotation Tool.
        /// </summary>
        /// 
        /// This Task will perform all necessary checks and immediately return if no work is 
        /// necessary. This way, all DownloadCheckAndInstall* Tasks can safely be added to a 
        /// Task.WhenAll() composite Task without bothering about figuring out which of the
        /// Tasks needs to be run and which won't.
        /// 
        /// <param name="is64bit">True for the 64-bit version, false for the 32-bit one.</param>
        /// <param name="installationFolder">Installation folder.</param>
        /// <returns></returns>
        public static async Task DownloadCheckAndInstallAnnotationToolAsync(bool is64bit, string installationFolder)
        {
            // Declare needed variables
            string url, fileName, dirName;

            // Set the correct URLs and paths
            if (is64bit)
            {
                url = Constants.AnnotationTool64bitURL;
                fileName = Path.Combine(installationFolder, Constants.AnnotationTool64bitArchiveFileName);
                dirName = Path.Combine(installationFolder, Constants.AnnotationTool64bitFinalPath);
            }
            else
            {
                url = Constants.AnnotationTool32bitURL;
                fileName = Path.Combine(installationFolder, Constants.AnnotationTool32bitArchiveFileName);
                dirName = Path.Combine(installationFolder, Constants.AnnotationTool32bitFinalPath);
            }

            // Make sure that the installation directory exists
            Directory.CreateDirectory(installationFolder);

            // First, check if the final (extracted) directory already exists. If it does, return.
            if (Directory.Exists(dirName))
            {
                return;
            }

            // Then check if the expected archive file exists. If it exists, skip the download
            if (! File.Exists(fileName))
            {
                // Download the archive
                await WebUtils.DownloadAsync(url, fileName);
                if (!File.Exists(fileName))
                {
                    // Inform
                    string msg = "Could not download Annotation Tool!";

                    sLogger.Error(msg);
                    throw new FileNotFoundException(msg);
                }
            }

            // Decompress the file
            FileSystem.ExtractZIPFileToFolder(fileName, dirName);
            if (!Directory.Exists(dirName))
            {
                // Inform
                string msg = "Could not extract Annotation Tool from archive!";

                sLogger.Error(msg);
                throw new FileNotFoundException(msg);
            }
        }

        /// <summary>
        /// Download, check and extract Datamover JSL.
        /// </summary>
        /// 
        /// This Task will perform all necessary checks and immediately return if no work is 
        /// necessary. This way, all DownloadCheckAndInstall* Tasks can safely be added to a 
        /// Task.WhenAll() composite Task without bothering about figuring out which of the
        /// Tasks needs to be run and which won't.
        /// 
        /// <param name="is64bit">True for the 64-bit version, false for the 32-bit one.</param>
        /// <param name="installationFolder">Installation folder.</param>
        /// <param name="relFolderName">Final (relative) Datamover JSL folder name. Skip to pick the default name (Constants.DatamoverJslFinalPath).</param>
        /// <returns></returns>
        public static async Task DownloadCheckAndInstallDatamoverJSL(bool is64bit, string installationFolder, string relFolderName = null)
        {
            // Parameters
            string url = Constants.DatamoverURL;
            string jslUrl = Constants.DatamoverJslURL;
            string jslExtractDirName = Path.Combine(installationFolder, Constants.DatamoverJslExtractDirName);
            string jslDirName = Path.Combine(installationFolder, Constants.DatamoverJslFinalPath);

            string fileName = Path.Combine(installationFolder, Constants.DatamoverArchiveFileName);
            string jslFileName = Path.Combine(installationFolder, Constants.DatamoverJslArchiveFileName);
            string dirName = Path.Combine(installationFolder, Constants.DatamoverFinalPath);

            // Make sure that the installation directory exists
            Directory.CreateDirectory(installationFolder);

            // First, check if the final (extracted) directories already exist. If they do, return.
            if (Directory.Exists(jslDirName))
            {
                if (Directory.Exists(dirName))
                {
                    return;
                }
            }

            // Then check if the expected archive file exists. If it exists, skip the download
            if (! File.Exists(jslFileName))
            {

                // Download the DatamoverJSL archive
                await WebUtils.DownloadAsync(jslUrl, jslFileName);
                if (!File.Exists(jslFileName))
                {
                    // Inform
                    string msg = "Could not download Datamover JSL!";

                    sLogger.Error(msg);
                    throw new FileNotFoundException(msg);
                }
            }

            // Decompress the file
            FileSystem.ExtractZIPFileToFolder(jslFileName, installationFolder);
            if (!Directory.Exists(jslExtractDirName))
            {
                // Inform
                string msg = "Could not extract Datamover JSL archive!";

                sLogger.Error(msg);
                throw new FileNotFoundException(msg);
            }

            // Rename the extracted JSL folder
            Directory.Move(jslExtractDirName, jslDirName);

            // Download the Datamover archive
            await WebUtils.DownloadAsync(url, fileName);
            if (!File.Exists(fileName))
            {
                // Inform
                string msg = "Could not download Datamover!";

                sLogger.Error(msg);
                throw new FileNotFoundException(msg);
            }

            // Decompress the file
            FileSystem.ExtractZIPFileToFolder(fileName, jslDirName);
            if (!Directory.Exists(dirName))
            {
                // Inform
                string msg = "Could not extract Datamover archive!";

                sLogger.Error(msg);
                throw new FileNotFoundException(msg);
            }

            // Copy the scripts folder for the right platform
            DirectoryInfo scriptsDir;
            if (is64bit)
            {
                scriptsDir = new DirectoryInfo(Path.Combine(jslDirName, @"scripts\dist\64bit"));
            }
            else
            {
                scriptsDir = new DirectoryInfo(Path.Combine(jslDirName, @"scripts\dist\32bit"));
            }
            string targetScriptsDir = Path.Combine(dirName, "scripts").ToString();
            FileSystem.CopyFolderRecursively(scriptsDir.ToString(), targetScriptsDir);

            // If relFolderName was specified (and is therefore not null), rename it
            if (relFolderName != null)
            {
                string finalFolderName = Path.Combine(installationFolder, relFolderName);
                Directory.Move(jslDirName, finalFolderName);

                if (!Directory.Exists(finalFolderName))
                {
                    // Inform
                    string msg = "Could not rename '" + jslDirName + "' to '" + finalFolderName + "'!";

                    sLogger.Error(msg);
                    throw new FileNotFoundException(msg);
                }
            }
        }

        #endregion public
    }
}
