using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Macro.ImageViewer.Utilities.Media.PortableViewer
{


    [Serializable, XmlType(AnonymousType=true), DebuggerStepThrough, GeneratedCode("xsd", "2.0.50727.3038"), DesignerCategory("code")]
    public sealed class MediaFileSetEntityMapStudy : MediaFileSetEntityMapEntry
    {
        private string uIDField;

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

