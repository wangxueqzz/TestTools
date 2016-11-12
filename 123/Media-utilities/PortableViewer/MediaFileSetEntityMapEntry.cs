using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Macro.ImageViewer.Utilities.Media.PortableViewer
{


    [Serializable, DebuggerStepThrough, DesignerCategory("code"), GeneratedCode("xsd", "2.0.50727.3038")]
    public abstract class MediaFileSetEntityMapEntry
    {
        private MediaFileSetEntityMapEntryAlternative[] alternativeField;

        protected MediaFileSetEntityMapEntry()
        {
        }

        [XmlElement("Alternative")]
        public MediaFileSetEntityMapEntryAlternative[] Alternative
        {
            get
            {
                return this.alternativeField;
            }
            set
            {
                this.alternativeField = value;
            }
        }
    }
}

