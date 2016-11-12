using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Macro.ImageViewer.Utilities.Media.PortableViewer
{


    [Serializable, GeneratedCode("xsd", "2.0.50727.3038"), DebuggerStepThrough, DesignerCategory("code"), XmlType(AnonymousType=true)]
    public sealed class MediaFileSetEntityMapSeries : MediaFileSetEntityMapEntry
    {
        private string studyInstanceUIDField;
        private string uIDField;

        [XmlAttribute]
        public string StudyInstanceUID
        {
            get
            {
                return this.studyInstanceUIDField;
            }
            set
            {
                this.studyInstanceUIDField = value;
            }
        }

        [XmlAttribute]
        public string UID
        {
            get
            {
                return this.uIDField;
            }
            set
            {
                this.uIDField = value;
            }
        }
    }
}

