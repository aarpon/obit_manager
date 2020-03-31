using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace obit_manager_settings
{
    namespace io
    {
        internal class AnnotationToolSettingsParser
        {

            List<Dictionary<string, string>> mConfigurations;

            /// <summary>
            /// Annotation Tool settings version.
            /// </summary>
            private readonly static int sAnnotationToolSettingsVersion = 9;

            /// <summary>
            /// Settings directory.
            /// </summary>
            private readonly static string sSettingsDirectory =
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) +
                @"\obit\AnnotationTool";

            /// <summary>
            /// Settings file name.
            /// </summary>
            private readonly static string sSettingsFileName = sSettingsDirectory + @"\settings.xml";

            /// <summary>
            /// XML document.
            /// </summary>
            XmlDocument doc;

            public AnnotationToolSettingsParser()
            {
                // Instantiate XmlDocument parser
                this.doc = new XmlDocument();

                // Load if possible
                this.Load();
            }

            public bool Load()
            {
                if (!File.Exists(AnnotationToolSettingsParser.sSettingsFileName))
                {
                    return false;
                }

                // Load the file
                this.doc.Load(AnnotationToolSettingsParser.sSettingsFileName);

                // Get the root note
                XmlElement rootNode = this.doc.DocumentElement;

                // Check the version
                if (rootNode.Name != "AnnotationTool_App_Settings")
                {
                    return false;
                }

                if (!Int32.TryParse(rootNode.GetAttribute("version"), out int version))
                {
                    return false;
                }

                if (version < AnnotationToolSettingsParser.sAnnotationToolSettingsVersion)
                {
                    // @TODO Implement update!
                    return false;
                }

                // Reset (or instantiate) list of configurations
                this.mConfigurations = new List<Dictionary<string, string>>();

                // Read all configurations
                foreach (XmlNode node in rootNode.ChildNodes)
                {
                    Dictionary<string, string> conf = new Dictionary<string, string>();
                    conf["CreateMarkerFile"] = node.Attributes["CreateMarkerFile"].Value;
                    conf["OpenBISURL"] = node.Attributes["OpenBISURL"].Value;
                    conf["AcceptSelfSignedCertificates"] = node.Attributes["AcceptSelfSignedCertificates"].Value;
                    conf["HumanFriendlyHostName"] = node.Attributes["HumanFriendlyHostName"].Value;
                    conf["AcquisitionStation"] = node.Attributes["AcquisitionStation"].Value;
                    conf["DatamoverIncomingDir"] = node.Attributes["DatamoverIncomingDir"].Value;
                    conf["UserDataDir"] = node.Attributes["UserDataDir"].Value;
                    conf["ConfigurationName"] = node.Attributes["ConfigurationName"].Value;

                    this.mConfigurations.Add(conf);
                }

                return true;
            }

            public bool Save()
            {
                throw new NotImplementedException("Implement me!");
            }
        }
    }
}
