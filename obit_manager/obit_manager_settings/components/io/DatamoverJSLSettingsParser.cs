using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using IniParser;
using IniParser.Model;
using NLog;

namespace obit_manager_settings.components.io
{
    public class DatamoverJSLSettingsParser
    {
        // Logger
        private static Logger sLogger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// File parser.
        /// </summary>
        private FileIniDataParser mParser;

        /// <summary>
        /// Map of all configurations.
        /// </summary>
        private Dictionary<string, IniData> mMap;

        /// <summary>
        /// Parses all DatamoverJSL installations in each installationDir / relativeDatamoverJSLdirs.
        /// </summary>
        /// <param name="installationDir">oBIT root installation dir.</param>
        /// <param name="relativeDatamoverJSLdirs">Relative DatamoverJSL dir.</param>
        public DatamoverJSLSettingsParser(string installationDir, string[] relativeDatamoverJSLdirs)
        {
            // Initialize the mParser
            this.mParser = new FileIniDataParser();

            // Clear all configurations
            this.mMap = new Dictionary<string, IniData>();

            for (int i = 0; i < relativeDatamoverJSLdirs.Length; i++)
            {
                // Store the datamoverJSL parent directory as key
                string path = Path.Combine(installationDir, relativeDatamoverJSLdirs[i]);

                // Store the key with no valid value yet
                this.mMap[path] = null;
            }

            this.Load();
        }

        /// <summary>
        /// Tries to find datamover jsl installations.
        /// </summary>
        /// <param name="installationDir">oBIT root installation dir.</param>
        public DatamoverJSLSettingsParser(string installationDir)
        {
            // Initialize the mParser
            this.mParser = new FileIniDataParser();

            // Clear all configurations
            this.mMap = new Dictionary<string, IniData>();

            // Find candidate DatamoverJSL relative folders
            string[] relativeDatamoverJSLdirs = scanForCandidateRelativeDatamoverJSLDirs(installationDir);

            for (int i = 0; i < relativeDatamoverJSLdirs.Length; i++)
            {
                // Store the datamoverJSL parent directory as key
                string path = Path.Combine(installationDir, relativeDatamoverJSLdirs[i]);

                // Store the key with no valid value yet
                this.mMap[path] = null;
            }

            this.Load();
        }

        /// <summary>
        /// Loads settings from disk
        /// </summary>
        public void Load()
        {
            // Since we will update the dictionary, we cannot iterate 
            // directly over it.
            List<string> keys = new List<string>(this.mMap.Keys);

            // Process all file names
            foreach (string key in keys)
            {
                // Build the file name
                string fileName = Path.Combine(key, @"jsl_static.ini");

                // Does the settings file exist?
                if (File.Exists(fileName))
                {
                    // Load the file and update the dictionary
                    IniData data = mParser.ReadFile(fileName);

                    // Update the dictionary
                    this.mMap[key] = data;

                    // Log
                    sLogger.Info("Successfully parsed DatamoverJSL settings file '" + fileName + "'.");
                }
                else
                {
                    // Log
                    sLogger.Warn("The expected DatamoverJSL settings file '" + fileName + "' was not found.");
                }
            }

            if (this.mMap.Count == 0)
            {
                // Log
                sLogger.Warn("No Datamover JSL settings files found!");

                // Create defaults (but do not save them yet)
                this.CreateDefault();
            }
        }

        /// <summary>
        /// Get the settings with given name.
        /// </summary>
        /// <param name="confName">Configuration name.</param>
        /// <param name="section">Configuration section.</param>
        /// <param name="key">Settings name.</param>
        /// <returns>Settings with given name.</returns>
        public string Get(string confName, string section, string key)
        {
            IniData data = this.mMap[confName];

            string value;
            try
            {
                value = data[section][key];
            }
            catch (Exception)
            {
                // Set to empty string
                value = "";

                // Log
                sLogger.Error("Could not retrieve configuration setting '" + key +
                    "' from section '" + section + "'.");
            }

            return value;
        }

        /// <summary>
        /// Sets the value for the property with given name.
        /// </summary>
        /// <param name="confName">Configuration name.</param>
        /// <param name="section">Configuration section.</param>
        /// <param name="key">Property name.</param>
        /// <param name="value">Property value.</param>
        public void Set(string confName, string section, string key, string value)
        {
            IniData data = this.mMap[confName];

            KeyData keyData = new KeyData(key);
            keyData.Value = value;
            data[section].SetKeyData(keyData);

            // Log
            sLogger.Info("Added configuration setting '" + key + "' with value '" +
                value + "' to section '" + section + "'.");
        }


        /// <summary>
        /// Save all settings to disk.
        /// </summary>
        public void Save()
        {
            // Process all file names
            foreach (KeyValuePair<string, IniData> entry in this.mMap)
            {
                // Build the file name
                string fileName = Path.Combine(entry.Key, @"jsl_static.ini");

                // Save the settings to file
                this.Save(fileName, entry.Value);
            }
        }

        /// <summary>
        /// Save current settings to disk.
        /// </summary>
        private void Save(string settingsFileName, IniData data)
        {
            // Write settings to file
            mParser.WriteFile(settingsFileName, data);

            // Log
            sLogger.Info("Written DatamoverJSL configuration file to '" + settingsFileName + "'.");
        }

        /// <summary>
        /// Scan for candidate relative DatamoverJSL directories.
        /// </summary>
        private string[] scanForCandidateRelativeDatamoverJSLDirs(string installationDir)
        {
            string[] directories = Directory.GetDirectories(installationDir);
            bool[] toBeKept = new bool[directories.Length];

            for (int i = 0; i < directories.Length; i++)
            {
                string dir = directories[i];

                string fileName = Path.Combine(installationDir, @"datamover\etc\services.properties");
                if (File.Exists(fileName))
                {
                    // Mark for deletion
                    toBeKept[i] = true;
                }
            }

            // Return 
            var query = (
                from d in directories
                from t in toBeKept
                where t == true
                select d
                ).ToArray();

            return query;
        }

        /// <summary>
        /// Return the list of relative DatamoverJSL directories.
        /// </summary>
        /// <returns></returns>
        public string[] GetRelativeDatamoverJSLDirs()
        {
            return this.mMap.Keys.ToArray<string>();
        }

        /// <summary>
        /// Create default settings.
        /// </summary>
        private void CreateDefault()
        {
            // Clear all configurations
            this.mMap = new Dictionary<string, IniData>();

            // New data
            IniData data = new IniData();

            // Add to the map
            this.mMap["obit_datamover_jsl"] = data;

            // Section: "defines" (empty)
            data.Sections.AddSection("defines");

            // Section: "service"
            data.Sections.AddSection("service");

            // Add all "service" settings
            this.Set("obit_datamover_jsl", "service", "appname", "Datamover");
            this.Set("obit_datamover_jsl", "service", "servicename", "Datamover");
            this.Set("obit_datamover_jsl", "service", "displayname", "Datamover");
            this.Set("obit_datamover_jsl", "service", "servicedescription", "Datamover as Windows Service");
            this.Set("obit_datamover_jsl", "service", "stringbuffer", "16000");
            this.Set("obit_datamover_jsl", "service", "starttype", "auto");
            this.Set("obit_datamover_jsl", "service", "loadordergroup", "someorder");
            this.Set("obit_datamover_jsl", "service", "useconsolehandler", "false");
            this.Set("obit_datamover_jsl", "service", "stopclass", "java/lang/System");
            this.Set("obit_datamover_jsl", "service", "stopmethod", "exit");
            this.Set("obit_datamover_jsl", "service", "stopsignature", "(I)V");
            this.Set("obit_datamover_jsl", "service", "account", @".\openbis");

            // Section: "java"
            data.Sections.AddSection("java");

            // Add all "java" settings
            this.Set("obit_datamover_jsl", "java", "jrepath", @"C:\oBIT\jre");
            this.Set("obit_datamover_jsl", "java", "jvmtype", "server");
            this.Set("obit_datamover_jsl", "java", "wrkdir", @"C:\oBIT\obit_datamover_jsl\datamover");
            this.Set("obit_datamover_jsl", "java", "cmdline",
                @"-cp lib\datamover.jar; lib\log4j.jar; lib\cisd - base.jar; lib\cisd - args4j.jar; lib\commons - lang.jar; " +
                @"lib\commons - io.jar; lib\activation.jar; lib\mail.jar ch.systemsx.cisd.datamover.Main--rsync - executable = " +
                @"bin\win\rsync.exe--ssh - executable = bin\win\ssh.exe--ln - executable = bin\win\ln.exe");
        }
    }
}
