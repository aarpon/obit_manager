using obit_manager_api.core;
using obit_manager_settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace obit_manager_api
{
    namespace tools
    {
        public static class Tools
        {
            #region public

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
                }
                if (File.Exists(datamoverFileName))
                {
                    File.Delete(datamoverFileName);
                }
                if (File.Exists(jdkFileName))
                {
                    File.Delete(jdkFileName);
                }
                if (File.Exists(annotationToolFileName))
                {
                    File.Delete(annotationToolFileName);
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

                // Download the file
                await WebUtils.DownloadAsync(downloadURL, targetFileName);
                if (!File.Exists(targetFileName))
                {
                    throw new FileNotFoundException("Could not download Java JRE!");
                }

                // Download the checksum
                await WebUtils.DownloadAsync(downloadMD5URL, targetMD5FileName);
                if (!File.Exists(targetMD5FileName))
                {
                    throw new FileNotFoundException("Could not download Java JRE checksum!");
                }

                // Read  the checksum
                String Jdk64bitMD5Checksum = FileSystem.ReadMD5ChecksumFromFile(targetMD5FileName);

                // Calculate and compare MD5 checksum
                if (!FileSystem.CalculateMD5Checksum(targetFileName).Equals(Jdk64bitMD5Checksum))
                {
                    throw new Exception("The Java JRE's checksum does not match!");
                }

                // Delete the checksum file
                File.Delete(targetMD5FileName);

                // Decompress the file
                FileSystem.ExtractZIPFileToFolder(targetFileName, installationFolder);

                // Check that the extract folder exists
                if (!Directory.Exists(jdkExtractPath))
                {
                    throw new FileNotFoundException("Could not extract Java JRE!");
                }

                // Rename the JRE folder
                Directory.Move(jdkExtractPath, jdkFinalPath);

                // Finally, check if the jre folder is in the final location
                if (!Directory.Exists(jdkFinalPath))
                {
                    throw new FileNotFoundException("Could not extract Java JRE!");
                }

                // Check that the jvm.dll file is in the expected place
                String jvmDllPath = Path.Combine(jdkFinalPath, @"bin\server\jvm.dll");
                if (!File.Exists(jvmDllPath))
                {
                    throw new FileNotFoundException("Could not find jvm.dll!");
                }
            }

            /// <summary>
            /// Download, check and extract the Annotation Tool.
            /// </summary>
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
                    fileName = Path.Combine(installationFolder, "annotationTool64bit.zip");
                    dirName = Path.Combine(installationFolder, Constants.AnnotationTool64bitFinalPath);
                }
                else
                {
                    url = Constants.AnnotationTool32bitURL;
                    fileName = Path.Combine(installationFolder, "annotationTool32bit.zip");
                    dirName = Path.Combine(installationFolder, Constants.AnnotationTool32bitFinalPath);
                }

                // Download the archive
                await WebUtils.DownloadAsync(url, fileName);
                if (!File.Exists(fileName))
                {
                    throw new FileNotFoundException("Could not download Annotation Tool!");
                }

                // Decompress the file
                FileSystem.ExtractZIPFileToFolder(fileName, dirName);
                if (!Directory.Exists(dirName))
                {
                    throw new FileNotFoundException("Could not extract Annotation Tool from archive!");
                }
            }

            /// <summary>
            /// Download, check and extract Datamover JSL.
            /// </summary>
            /// <param name="is64bit">True for the 64-bit version, false for the 32-bit one.</param>
            /// <param name="installationFolder">Installation folder.</param>
            /// <returns></returns>
            public static async Task DownloadCheckAndInstallDatamoverJSL(bool is64bit, string installationFolder)
            {
                // Parameters
                string url = Constants.DatamoverURL;
                string jslUrl = Constants.DatamoverJslURL;
                string jslExtractDirName = Path.Combine(installationFolder, Constants.DatamoverJslExtractDirName);
                string jslDirName = Path.Combine(installationFolder, Constants.DatamoverJslFinalPath);

                string fileName = Path.Combine(installationFolder, "datamover.zip");
                string jslFileName = Path.Combine(installationFolder, Constants.DatamoverJslArchiveFileName);
                string dirName = Path.Combine(installationFolder, Constants.DatamoverFinalPath);

                // Make sure that the installaion directory exists
                Directory.CreateDirectory(installationFolder);

                // Download the DatamoverJSL archive
                await WebUtils.DownloadAsync(jslUrl, jslFileName);
                if (!File.Exists(jslFileName))
                {
                    throw new FileNotFoundException("Could not download Datamover JSL!");
                }

                // Decompress the file
                FileSystem.ExtractZIPFileToFolder(jslFileName, installationFolder);
                if (!Directory.Exists(jslExtractDirName))
                {
                    throw new FileNotFoundException("Could not extract Datamover JSL archive!");
                }

                // Rename the extracted JSL folder
                Directory.Move(jslExtractDirName, jslDirName);

                // Download the Datamover archive
                await WebUtils.DownloadAsync(url, fileName);
                if (!File.Exists(fileName))
                {
                    throw new FileNotFoundException("Could not download Datamover!");
                }

                // Decompress the file
                FileSystem.ExtractZIPFileToFolder(fileName, jslDirName);
                if (!Directory.Exists(dirName))
                {
                    throw new FileNotFoundException("Could not extract Datamover archive!");
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
            }

            #endregion public
        }
    }
}
