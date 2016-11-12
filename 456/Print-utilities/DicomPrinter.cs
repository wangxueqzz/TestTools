
#region License

// Copyright (c) 2013, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This file is part of the ClearCanvas RIS/PACS open source project.
//
// The ClearCanvas RIS/PACS open source project is free software: you can
// redistribute it and/or modify it under the terms of the GNU General Public
// License as published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// The ClearCanvas RIS/PACS open source project is distributed in the hope that it
// will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
// Public License for more details.
//
// You should have received a copy of the GNU General Public License along with
// the ClearCanvas RIS/PACS open source project.  If not, see
// <http://www.gnu.org/licenses/>.

#endregion

using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Macro.Dicom.Network.Scu;


namespace Macro.ImageViewer.Utilities.Print.Dicom
{
    public class DicomPrinter
    {
        private string _aeTitle = "PRINT_SCP";
        private Configuration _config = null;
        private string _host = "127.0.0.1";
        private string _name = "LOCAL";
        private int _port = 104;

        public DicomPrinter()
        {
            _config = new Configuration();
        }
        public DicomPrinter(string name, string aeTitle, string host, int port, Configuration configuration)
        {
            this._config = configuration;
            this._aeTitle = aeTitle;
            this._name = name;
            this._host = host;
            this._port = port;
        }
        public object Clone()
        {
            DicomPrinter printer = new DicomPrinter(this.Name, this.AETitle, this.Host, this.Port, this.Config);
            return printer;

        }
        public string AETitle
        {
            get { return _aeTitle; }
            set
            {
                _aeTitle = value;
            }
        }
        public Configuration Config
        {
            get { return _config; }
            set { _config = value; }
        }
        public string Host
        {
            get { return _host; }
            set { _host = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }
        public class Configuration : ICloneable
        {
            protected Macro.ImageViewer.Utilities.Print.Dicom.FilmBox _filmBox;
            protected Macro.ImageViewer.Utilities.Print.Dicom.FilmSession _filmSession;
            private Macro.Dicom.Network.Scu.ColorMode _colorMode = ColorMode.Grayscale;
            private int _highResolutionDPI;
            private Macro.ImageViewer.Utilities.Print.Dicom.PresentationMode _presentationMode = PresentationMode.CompleteImage;
            private int _standardResolutionDPI;

            public Configuration()
            {
                this._filmSession = new Macro.ImageViewer.Utilities.Print.Dicom.FilmSession();
                this._filmBox = new Macro.ImageViewer.Utilities.Print.Dicom.FilmBox();
                _standardResolutionDPI = 300;
            }

            public Configuration(Macro.ImageViewer.Utilities.Print.Dicom.FilmSession filmSession, Macro.ImageViewer.Utilities.Print.Dicom.FilmBox filmBox, Macro.ImageViewer.Utilities.Print.Dicom.PresentationMode presentationMode, int standardResDPI, int highResDPI, Macro.Dicom.Network.Scu.ColorMode colorMode)
            {
                this._filmSession = filmSession;
                this._filmBox = filmBox;
                _presentationMode = presentationMode;
                _standardResolutionDPI = standardResDPI;
                _highResolutionDPI = highResDPI;
                _colorMode = colorMode;
            }

            public object Clone()
            {
                Configuration config = new Configuration(this.Session, this.FilmBox, this.PresentationMode, this.StandardResolutionDPI, this.HighResolutionDPI, this.ColorMode);
                return config;
            }

            public Macro.Dicom.Network.Scu.ColorMode ColorMode
            {
                get { return _colorMode; }
                set { _colorMode = value; }
            }

            public Macro.ImageViewer.Utilities.Print.Dicom.FilmBox FilmBox
            {
                get
                {
                    return this._filmBox;
                }
                set
                {
                    this._filmBox = value;
                }
            }

            public int HighResolutionDPI
            {
                get { return _highResolutionDPI; }
                set { _highResolutionDPI = value; }
            }

            public Macro.ImageViewer.Utilities.Print.Dicom.PresentationMode PresentationMode
            {
                get { return _presentationMode; }
                set { _presentationMode = value; }
            }

            public Macro.ImageViewer.Utilities.Print.Dicom.FilmSession Session
            {
                get
                {
                    return this._filmSession;
                }
                set
                {
                    this._filmSession = value;
                }
            }

            public int StandardResolutionDPI
            {
                get { return _standardResolutionDPI; }
                set { _standardResolutionDPI = value; }
            }

            public string XmlSerialize()
            {

                string xmlString = string.Empty;

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Configuration));

                using (MemoryStream ms = new MemoryStream())
                {

                    xmlSerializer.Serialize(ms, this);
                    xmlString = Encoding.UTF8.GetString(ms.ToArray());
                }
                return xmlString;

            }

            public override string ToString()
            {
                return XmlSerialize();
            }
        }
    }
}

