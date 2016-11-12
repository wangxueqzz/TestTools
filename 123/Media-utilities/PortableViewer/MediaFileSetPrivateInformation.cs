using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Macro.ImageViewer.Utilities.Media.PortableViewer
{


    [Serializable, DesignerCategory("code"), XmlType(AnonymousType=true), DebuggerStepThrough, GeneratedCode("xsd", "2.0.50727.3038")]
    public sealed class MediaFileSetPrivateInformation
    {
        private XmlElement anyField;
        private string creatorUIDField;

        [XmlAnyElement]
        public XmlElement Any
        {
            get
            {
                return this.anyField;
            }
            set
            {
                this.anyField = value;
            }
        }

        [XmlAttribute]
        public string CreatorUID
        {
            get
            {
                return this.creatorUIDField;
            }
            set
            {
                this.creatorUIDField = value;
            }
        }
    }
}

