using System;
using System.IO;

namespace obit_manager_settings
{
    public static class Constants
    {

        /**
         * 
         * oBIT Components Download URLs
         * 
         *     * JAVA 32 and 64 bit
         *     * Datamover JSL
         *     * Datamover
         *     * Annotation Tool 32 and 64 bit
         * 
         */

        // OpenJDK 8 from Amazon
        private const string jdk64bitURL = "https://corretto.aws/downloads/resources/8.242.08.1/amazon-corretto-8.242.08.1-windows-x86-jre.zip";
        private const string jdk32bitURL = "https://corretto.aws/downloads/resources/8.242.08.1/amazon-corretto-8.242.08.1-windows-x64-jre.zip";
        private const string jdk64bitMD5Checksum = "f7512093358cc3c5bd96d30c9cd339b2";
        private const string jdk32bitMD5Checksum = "b9aded040819457d04a36b726100e3f6";

        // Datamover_JSL
        private const string datamoverJslURL = "https://github.com/aarpon/obit_datamover_jsl/archive/0.2.0.zip";

        // Datamover
        private const string datamoverURL = "https://wiki-bsse.ethz.ch/download/attachments/21567716/datamover-15.06.0-r34542.zip";

        // Annotation Tool
        private const string annotationTool64bitURL = "https://github.com/aarpon/obit_annotation_tool/releases/download/2.1.0/obit_annotation_tool_2.1.0_64bit.zip";
        private const string annotationTool32bitURL = "https://github.com/aarpon/obit_annotation_tool/releases/download/2.1.0/obit_annotation_tool_2.1.0_32bit.zip";

        /**
         * 
         * oBIT Components Extracted (Sub)Path
         * 
         *     * JAVA 32 and 64 bit
         *     * Datamover JSL
         *     * Datamover
         *     * Annotation Tool 32 and 64 bit
         */

        // OpenJDK 8 from Zulu: the last to support 32 bit machines
        private const string jdk64bitPath = "jre";
        private const string jdk32bitPath = "jre";

        // Datamover_JSL
        private const string datamoverJslPath = "obit_datamover_jsl";

        // Datamover (relative to datamoverJSLPath)
        private const string datamoverPath = "datamover";

        // Annotation Tool
        private const string annotationTool64bitPath = "obit_annotation_tool";
        private const string annotationTool32bitPath = "obit_annotation_tool";


        /**
         * PROPERTIES
         */

        // Java
        public static string Jdk64bitURL => jdk64bitURL;
        public static string Jdk32bitURL => jdk32bitURL;
        public static string Jdk64bitPath => jdk64bitPath;
        public static string Jdk32bitPath => jdk32bitPath;
        public static string Jdk64bitMD5Checksum => jdk64bitMD5Checksum;
        public static string Jdk32bitMD5Checksum => jdk32bitMD5Checksum;
        public static string Jdk64bitFileName => Path.GetFileName((new Uri(Jdk64bitURL)).LocalPath);
        public static string Jdk32bitFileName => Path.GetFileName((new Uri(Jdk32bitURL)).LocalPath);
        public static string Jdk64bitExtractDirName => "jre8";
        public static string Jdk32bitExtractDirName => "jre8";

        // Datamover
        public static string DatamoverJslURL => datamoverJslURL;
        public static string DatamoverURL => datamoverURL;
        public static string DatamoverJslPath => datamoverJslPath;
        public static string DatamoverPath { get => Path.Combine(datamoverJslPath, datamoverPath); }

        // Annotation Tool
        public static string AnnotationTool64bitURL => annotationTool64bitURL;
        public static string AnnotationTool32bitURL => annotationTool32bitURL;
        public static string AnnotationTool32bitPath => annotationTool32bitPath;
        public static string AnnotationTool64bitPath => annotationTool64bitPath;
    }
}
