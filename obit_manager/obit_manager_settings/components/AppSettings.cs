using obit_manager_settings.io;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace obit_manager_settings.components
{
    public class AppSettings
    {
        // INISettigs object
        private INISettings mINISettings;

        // Minimum accepted Java Runtime vMersion
        public int SettingsVersion
        {
            get
            {
                int value;
                if (Int32.TryParse(this.mINISettings.Get("Versions", "SettingsVersion"), out value))
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
                this.mINISettings.Set("Application", "InstallationDir", value.ToString());
            }
        }

        // openBIS Importer Toolset installation dir
        public string InstallationDir
        {
            get
            {
                return this.mINISettings.Get("Application", "InstallationDir");
            }
            set
            {
                this.mINISettings.Set("Application", "InstallationDir", value);
            }
        }

        // Minimum accepted Java Runtime version
        public int MinJavaMajorVersion
        {
            get
            {
                int value;
                if (Int32.TryParse(this.mINISettings.Get("Versions", "MinJavaMajorVersion"), out value))
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
                this.mINISettings.Set("Application", "InstallationDir", value.ToString());
            }
        }

        // Is the platform 64 bits? Otherwise, it is 32 bits
        public bool IsPlatform64Bits
        {
            get
            {
                bool value;
                if (Boolean.TryParse(this.mINISettings.Get("Application", "IsPlatform64Bits"), out value))
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
                this.mINISettings.Set("Application", "IsPlatform64Bits", value.ToString());
            }
        }

        // Use already installed Java Runtime?
        public bool UseExistingJavaRuntime
        {
            get
            {
                bool value;
                if (Boolean.TryParse(this.mINISettings.Get("Application", "UseExistingJavaRuntime"), out value))
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
                this.mINISettings.Set("Application", "UseExistingJavaRuntime", value.ToString());
            }
        }

        // Path to local Java Runtime folder
        public string JavaRuntimePath
        {
            get
            {
                return this.mINISettings.Get("Application", "JavaRuntimePath");
            }
            set
            {
                this.mINISettings.Set("Application", "JavaRuntimePath", value);
            }
        }

        public AppSettings()
        {
            this.mINISettings = new INISettings();
        }

        /// <summary>
        /// Reload settings from disk.
        /// </summary>
        public void Reload()
        {
            this.mINISettings.Load();
        }

        public void Save()
        {
            this.mINISettings.Save();
        }
    }
}
