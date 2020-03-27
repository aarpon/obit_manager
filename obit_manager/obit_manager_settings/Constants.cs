﻿using System;
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
        public static string Jdk64bitURL => Properties.Settings.Default.Jdk64bitURL;
        public static string Jdk32bitURL => Properties.Settings.Default.Jdk32bitURL;
        public static string Jdk64bitMD5URL => Properties.Settings.Default.Jdk64bitMD5URL;
        public static string Jdk32bitMD5URL => Properties.Settings.Default.Jdk32bitMD5URL;

        // Datamover_JSL
        public static string DatamoverJslURL => Properties.Settings.Default.DatamoverJslURL;

        // Datamover
        public static string DatamoverURL = Properties.Settings.Default.DatamoverURL;

        // Annotation Tool
        public static string AnnotationTool64bitURL = Properties.Settings.Default.AnnotationTool64bitURL;
        public static string AnnotationTool32bitURL = Properties.Settings.Default.AnnotationTool32bitURL;

        /**
         * 
         * oBIT Components Extracted (Sub)Path
         * 
         *     * JAVA 32 and 64 bit
         *     * Datamover JSL
         *     * Datamover
         *     * Annotation Tool 32 and 64 bit
         */

        // Amazon Corretto OpenJDK 8
        public static string Jdk64bitFinalPath = "jre";
        public static string Jdk32bitFinalPath = "jre";
        public static string Jdk64bitExtractDirName => "jre8";
        public static string Jdk32bitExtractDirName => "jre8";
        public static string Jdk64bitArchiveFileName => Path.GetFileName((new Uri(Jdk64bitURL)).LocalPath);
        public static string Jdk32bitArchiveFileName => Path.GetFileName((new Uri(Jdk32bitURL)).LocalPath);
        public static string Jdk64bitMD5FileName => Path.GetFileName((new Uri(Jdk64bitMD5URL)).LocalPath + ".md5");
        public static string Jdk32bitMD5FileName => Path.GetFileName((new Uri(Jdk32bitMD5URL)).LocalPath + ".md5");

        // Datamover_JSL
        public static string DatamoverJslFinalPath = "obit_datamover_jsl";
        public static string DatamoverJslArchiveFileName => Path.GetFileName((new Uri(DatamoverJslURL)).LocalPath);

        public static string DatamoverJslExtractDirName
        {
            get
            {
                int index = DatamoverJslArchiveFileName.IndexOf(".zip");
                return DatamoverJslFinalPath + DatamoverJslArchiveFileName.Substring(0, index);
            }
        }

        // Datamover (relative to DatamoverJSLPath)
        public static string DatamoverFinalPath => Path.Combine(DatamoverJslFinalPath, "datamover");
        public static string DatamoverArchiveFileName => Path.GetFileName((new Uri(DatamoverURL)).LocalPath);


        // Annotation Tool
        public static string AnnotationTool64bitArchiveFileName => Path.GetFileName((new Uri(AnnotationTool64bitURL)).LocalPath);
        public static string AnnotationTool32bitArchiveFileName => Path.GetFileName((new Uri(AnnotationTool32bitURL)).LocalPath);
        public static string AnnotationTool64bitFinalPath = "obit_annotation_tool";
        public static string AnnotationTool32bitFinalPath = "obit_annotation_tool";

        // Other constants
        public enum AcquisitionStationType { FLOW_CYTOMETRY, MICROSCOPY };
    }
}
