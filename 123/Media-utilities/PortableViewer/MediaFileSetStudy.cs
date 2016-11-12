using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Macro.ImageViewer.Utilities.Media.PortableViewer
{

    [Serializable, DesignerCategory("code"), GeneratedCode("xsd", "2.0.50727.3038"), XmlType(AnonymousType = true), DebuggerStepThrough]
    public sealed class MediaFileSetStudy
    {
        private string uIDField;
        private string valueField;

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

