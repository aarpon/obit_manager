using System;

namespace obit_manager_settings.components
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class SettingAttribute : Attribute
    {
        /// <summary>
        /// Defines which configuration file stores this Property.
        /// </summary>
        ///
        /// One of AnnotationTool, Datamover_JSL, or Datamover;
        /// combinations are also possile, e.g. "AnnotationTool;Datamover"
        public virtual string Configuration { get; set; }

        /// <summary>
        /// Defines which object owns this property.
        /// <summary>
        ///
        /// One of Client, Datamover or Server.
        /// 
        /// This object is the only one that should accept changes to the underlying
        /// Property (e.g. when exposed via some configuration dialog).
        public virtual string Component { get; set; }
    }
}
