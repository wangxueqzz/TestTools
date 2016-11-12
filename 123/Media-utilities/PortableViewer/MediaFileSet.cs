﻿using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;


namespace Macro.ImageViewer.Utilities.Media.PortableViewer
{
    [Serializable, DesignerCategory("code"), DebuggerStepThrough, XmlRoot(Namespace = "", IsNullable = false), GeneratedCode("xsd", "2.0.50727.3038"), XmlType(AnonymousType = true)]
    public sealed class MediaFileSet
    {
        private MediaFileSetEntityMapEntry[] entityMapField;
        private string idField;
        private string implementationNameField;
        private string implementationUIDField;
        private MediaFileSetPrivateInformation privateInformationField;
        private string sourceAETitleField;
        private MediaFileSetStudy[] studyIndexField;

        [XmlArrayItem("EntityMapSop", typeof(MediaFileSetEntityMapSop), IsNullable = false), XmlArrayItem("EntityMapStudy", typeof(MediaFileSetEntityMapStudy), IsNullable = false), XmlArrayItem("EntityMapSeries", typeof(MediaFileSetEntityMapSeries), IsNullable = false), XmlArrayItem("EntityMapPatient", typeof(MediaFileSetEntityMapPatient), IsNullable = false)]
        public MediaFileSetEntityMapEntry[] EntityMap
        {
            get
            {
                return this.entityMapField;
            }
            set
            {
                this.entityMapField = value;
            }
        }

        [XmlAttribute]
        public string Id
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

        [XmlAttribute]
        public string ImplementationName
        {
            get
            {
                return this.implementationNameField;
            }
            set
            {
                this.implementationNameField = value;
            }
        }

        [XmlAttribute]
        public string ImplementationUID
        {
            get
            {
                return this.implementationUIDField;
            }
            set
            {
                this.implementationUIDField = value;
            }
        }

        public MediaFileSetPrivateInformation PrivateInformation
        {
            get
            {
                return this.privateInformationField;
            }
            set
            {
                this.privateInformationField = value;
            }
        }

        [XmlAttribute]
        public string SourceAETitle
        {
            get
            {
                return this.sourceAETitleField;
            }
            set
            {
                this.sourceAETitleField = value;
            }
        }

        [XmlArrayItem("Study", IsNullable = false)]
        public MediaFileSetStudy[] StudyIndex
        {
            get
            {
                return this.studyIndexField;
            }
            set
            {
                this.studyIndexField = value;
            }
        }
    }
}

