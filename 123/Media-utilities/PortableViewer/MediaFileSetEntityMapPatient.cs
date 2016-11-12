using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Macro.ImageViewer.Utilities.Media.PortableViewer
{


    [Serializable, DebuggerStepThrough, DesignerCategory("code"), XmlType(AnonymousType=true), GeneratedCode("xsd", "2.0.50727.3038")]
    public sealed class MediaFileSetEntityMapPatient : MediaFileSetEntityMapEntry
    {
        private string idField;

        [XmlAttribute]
        public string ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }
}

