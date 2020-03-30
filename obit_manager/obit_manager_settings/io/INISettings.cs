using IniParser;
using IniParser.Model;
using System;

namespace obit_manager_settings
{
    namespace io
    {
        internal class INISettings
        {
            ///
            /// 
            /// 
            private readonly static string sSettingsDirectory =
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) +
                @"\obit\obit_manager";

            /// <summary>
            /// Settings file name.
            /// </summary>
            private readonly static string sSettingsFileName = sSettingsDirectory + @"\obit_manager_settings.ini";

            /// <summary>
            /// File parser.
            /// </summary>
            private FileIniDataParser mParser;

            /// <summary>
            /// Settings.
            /// </summary>
            IniData mData;

            /// <summary>
            /// Settings version number.
            /// </summary>
            public readonly int mVersion = 1;

            /// <summary>
            /// Constructor.
            /// </summary>
            public INISettings()
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

            }
            /// <summary>
            /// Get the settings with given name.
            /// </summary>
            /// <param name="key">Settings name.</param>
            /// <returns>Settings with given name.</returns>
            public string Get(String section, String key)
            {
                string value;
                try
                {
                    value = mData[section][key];
                }
                catch (Exception)
                {
                    value = "";
                }
                return value;
            }

            /// <summary>
            /// Sets the value for tje property with given name.
            /// </summary>
            /// <param name="key">Property name.</param>
            /// <param name="value">Property value.</param>
            public void Set(String section, String key, string value)
            {
                KeyData data = new KeyData(key);
                data.Value = value;
                this.mData[section].SetKeyData(data);
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

                    // Update if needed
                    UpdateIfNeeded();
                }
                else
                {
                    // Create a default file
                    CreateDefault();

                    // Save it
                    Save();
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

                // Path to local Java Runtime folder
                mData["Application"].AddKey("JavaRuntimePath", @"C:\oBIT\jre");

                // Use already installed Java Runtime?
                mData["Application"].AddKey("UseExistingJavaRuntime", false.ToString());

                // Is the platform 64 bits? Otherwise, it is 32 bits
                mData["Application"].AddKey("IsPlatform64Bits", true.ToString());

            }

            /// <summary>
            /// Check if the loaded settings file is up to date; otherwise update it.
            /// </summary>
            /// <returns>True if the file needed an update; false otherwise.</returns>
            private void UpdateIfNeeded()
            {
                int versionFromFile;
                if (! Int32.TryParse(this.mData["Versions"]["SettingsVersion"], out versionFromFile))
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
        }
    }
}
