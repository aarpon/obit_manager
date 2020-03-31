using obit_manager_settings.components;
using obit_manager_settings.io;
using System.Collections.Generic;
using System.IO;

namespace obit_manager_settings
{
    /// <summary>
    /// Manager of all oBIS settings.
    /// </summary>
    public class SettingsManager
    {
        // Settings parsers
        private ManagerSettingsParser mManagerParser;
        private AnnotationToolSettingsParser mATParser;
        private DatamoverSettingsParser mDatamoverParser;

        // List of instances
        private List<Instance> mInstances;

        public SettingsManager()
        {
            this.mInstances = new List<Instance>();

            // Load (if possible, otherwise instantiate with default values)
            // the oBIT Manager Application Settings
            this.mManagerParser = new ManagerSettingsParser();

            // Load (if possible, otherwise instantiate with default values)
            // the AnnotationTool Settings
            this.mATParser = new AnnotationToolSettingsParser();

            // Load (if possible, otherwise instantiate with default values)
            // the Datamover Settings
            // @TODO Adapt for the case there is more than one configuration!
            this.mDatamoverParser = new DatamoverSettingsParser(
                Path.Combine(
                    this.mManagerParser.InstallationDir,
                    this.mManagerParser.DatamoverRelativeDirList[0])
                );
        }

        public void Reload()
        {
            // @Todo Implement!
            this.mManagerParser.Load();
            this.mATParser.Load();
            this.mDatamoverParser.Load();
        }

        public void Save()
        {
            // @Todo Implement!
            this.mManagerParser.Save();
            this.mATParser.Save();
            this.mDatamoverParser.Save();
        }

        #region properties

        // openBIS Importer Toolset installation dir
        public string InstallationDir
        {
            get
            {
                return this.mManagerParser.InstallationDir;
            }
            set
            {
                this.mManagerParser.InstallationDir = value;
            }
        }

        #endregion properties

    }
}
