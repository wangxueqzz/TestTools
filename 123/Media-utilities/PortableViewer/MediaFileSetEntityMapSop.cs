using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Macro.ImageViewer.Utilities.Media.PortableViewer
{


    [Serializable, GeneratedCode("xsd", "2.0.50727.3038"), DebuggerStepThrough, DesignerCategory("code"), XmlType(AnonymousType=true)]
    public sealed class MediaFileSetEntityMapSop : MediaFileSetEntityMapEntry
    {
        private string seriesInstanceUIDField;
        private string studyInstanceUIDField;
        private string uIDField;

        [XmlAttribute]
        public string SeriesInstanceUID
        {
            get
            {
                return this.seriesInstanceUIDField;
            }
            set
            {
                this.seriesInstanceUIDField = value;
            }
        }

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

