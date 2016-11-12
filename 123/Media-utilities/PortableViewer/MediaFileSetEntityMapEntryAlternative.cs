using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Macro.ImageViewer.Utilities.Media.PortableViewer
{


    [Serializable, GeneratedCode("xsd", "2.0.50727.3038"), DebuggerStepThrough, DesignerCategory("code"), XmlType(AnonymousType=true)]
    public sealed class MediaFileSetEntityMapEntryAlternative
    {
        private string mimeTypeField;
        private string valueField;

        [XmlAttribute]
        public string MimeType
        {
            get
            {
                return this.mimeTypeField;
            }
            set
            {
                this.mimeTypeField = value;
            }
        }

        [XmlText]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }
}

