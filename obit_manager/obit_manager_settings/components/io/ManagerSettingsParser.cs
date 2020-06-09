using System;
using IniParser;
using IniParser.Model;
using NLog;

namespace obit_manager_settings.components.io
{
    public class ManagerSettingsParser
    {
        /// <summary>
        /// Logger
        /// </summary>
        private static Logger sLogger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Settings directory.
        /// </summary>
        private static readonly string sSettingsDirectory =
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) +
            @"\obit\obit_manager";

        /// <summary>
        /// Settings file name.
        /// </summary>
        private static readonly string sSettingsFileName = sSettingsDirectory + @"\obit_manager_settings.ini";

        /// <summary>
        /// File parser.
        /// </summary>
        private FileIniDataParser mParser;

        /// <summary>
        /// Settings.
        /// </summary>
        private IniData mData;

        /// <summary>
        /// Settings version number.
        /// </summary>
        private readonly int mVersion = 1;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ManagerSettingsParser()
        {
            // Initialize the mParser
            this.mParser = new FileIniDataParser();

            // Load or create the settings file
            Load();
        }

        /// <summary>
        /// Save current settings to disk.
        /// </summary>
        public void Save()
        {
            // Make sure the path is valid
            System.IO.Directory.CreateDirectory(sSettingsDirectory);
            mParser.WriteFile(sSettingsFileName, mData);

            // Log
            sLogger.Info("Successfully saved settings to '" + sSettingsFileName + "'.");
        }

        /// <summary>
        /// Get the settings with given name.
        /// </summary>
        /// <param name="key">Settings name.</param>
        /// <returns>Settings with given name.</returns>
        private string Get(String section, String key)
        {
            string value;
            try
            {
                value = mData[section][key];
            }
            catch (Exception)
            {
                // Set to empty string
                value = "";

                // Log
                sLogger.Error("Could not retrieve configuration setting '" + key + "' from section '" + section + "'.");

            }
            return value;
        }

        /// <summary>
        /// Sets the value for tje property with given name.
        /// </summary>
        /// <param name="key">Property name.</param>
        /// <param name="value">Property value.</param>
        private void Set(String section, String key, string value)
        {
            KeyData data = new KeyData(key);
            data.Value = value;
            this.mData[section].SetKeyData(data);

            // Log
            sLogger.Info("Added configuration setting '" + key + "' with value '" +
                value + "' to section '" + section + "'.");
        }

        /// <summary>
        /// Loads settings from disk
        /// </summary>
        public void Load()
        {
            // Does the settings file exist?
            if (System.IO.File.Exists(sSettingsFileName))
            {
                // Load the file
                mData = mParser.ReadFile(sSettingsFileName);

                // Log
                sLogger.Info("Successfully parsed settings file '" + sSettingsFileName + "'.");

                // Update if needed
                UpdateIfNeeded();
            }
            else
            {
                // Create a default file
                CreateDefault();

                // Save it
                Save();

                // Log
                sLogger.Info("Settings file '" + sSettingsFileName + "' did not exist. Created default.");
            }
        }

        /// <summary>
        /// Create default settings.
        /// </summary>
        private void CreateDefault()
        {
            // Create a new data object
            mData = new IniData();

            // Versions
            mData.Sections.AddSection("Versions");

            // oBIT_Manager settings file version
            mData["Versions"].AddKey("SettingsVersion", mVersion.ToString());

            // Minimum accepted Java Runtime vMersion
            mData["Versions"].AddKey("MinJavaMajorVersion", "8");

            // Application
            mData.Sections.AddSection("Application");

            // openBIS Importer Toolset installation dir
            mData["Application"].AddKey("InstallationDir", @"C:\oBIT");

            // List of relative paths to (multiple) Datamover installations (;-separated)
            mData["Application"].AddKey("DatamoverRelativeDirList", @"obit_datamover_jsl");

            // Path to local Java Runtime folder
            mData["Application"].AddKey("JavaRuntimePath", @"C:\oBIT\jre");

            // Use already installed Java Runtime?
            mData["Application"].AddKey("UseExistingJavaRuntime", false.ToString());

            // Is the platform 64 bits? Otherwise, it is 32 bits
            mData["Application"].AddKey("IsPlatform64Bits", true.ToString());

            // Log
            sLogger.Info("Created default settings file.");
        }

        /// <summary>
        /// Check if the loaded settings file is up to date; otherwise update it.
        /// </summary>
        /// <returns>True if the file needed an update; false otherwise.</returns>
        private void UpdateIfNeeded()
        {
            int versionFromFile;
            if (!Int32.TryParse(this.mData["Versions"]["SettingsVersion"], out versionFromFile))
            {
                CreateDefault();
                Save();
                Load();
            }
            else
            {
                if (this.mVersion == versionFromFile)
                {
                    // Nothing to do.
                }
                else
                {
                    // @TODO
                    throw new NotImplementedException("Implement Application Settings update!");
                }
            }
        }

        #region properties

        // Minimum accepted Java Runtime vMersion
        public int SettingsVersion
        {
            get
            {
                int value;
                if (Int32.TryParse(this.Get("Versions", "SettingsVersion"), out value))
                {
                    return value;
                }
                else
                {
                    return -1;
                }

            }
            set
            {
                this.Set("Application", "InstallationDir", value.ToString());
            }
        }

        // openBIS Importer Toolset installation dir
        public string InstallationDir
        {
            get
            {
                return this.Get("Application", "InstallationDir");
            }
            set
            {
                this.Set("Application", "InstallationDir", value);
            }
        }

        // Minimum accepted Java Runtime version
        public int MinJavaMajorVersion
        {
            get
            {
                int value;
                if (Int32.TryParse(this.Get("Versions", "MinJavaMajorVersion"), out value))
                {
                    return value;
                }
                else
                {
                    return -1;
                }

            }
            set
            {
                this.Set("Application", "InstallationDir", value.ToString());
            }
        }

        // Is the platform 64 bits? Otherwise, it is 32 bits
        public bool IsPlatform64Bits
        {
            get
            {
                bool value;
                if (Boolean.TryParse(this.Get("Application", "IsPlatform64Bits"), out value))
                {
                    return value;
                }
                else
                {
                    return true;
                }

            }
            set
            {
                this.Set("Application", "IsPlatform64Bits", value.ToString());
            }
        }

        // Use already installed Java Runtime?
        public bool UseExistingJavaRuntime
        {
            get
            {
                bool value;
                if (Boolean.TryParse(this.Get("Application", "UseExistingJavaRuntime"), out value))
                {
                    return value;
                }
                else
                {
                    return true;
                }

            }
            set
            {
                this.Set("Application", "UseExistingJavaRuntime", value.ToString());
            }
        }

        // Path to local Java Runtime folder
        public string JavaRuntimePath
        {
            get
            {
                return this.Get("Application", "JavaRuntimePath");
            }
            set
            {
                this.Set("Application", "JavaRuntimePath", value);
            }
        }

        // List of relative paths to (multiple) Datamover installations (;-separated)
        public string[] DatamoverRelativeDirList
        {
            get
            {
                string dirs = this.Get("Application", "DatamoverRelativeDirList");
                string[] dirList = dirs.Split(';');
                for (int i = 0; i < dirList.Length; i++)
                {
                    dirList[i] = dirList[i].Trim();
                }
                return dirList;
            }
            set
            {
                for (int i = 0; i < value.Length; i++)
                {
                    value[i] = value[i].Trim();
                }
                string dirs = string.Join(";", value);
                this.Set("Application", "DatamoverRelativeDirList", dirs);
            }
        }

        #endregion properties
    }
}
