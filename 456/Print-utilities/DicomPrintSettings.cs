
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
using System.Configuration;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Macro.Common;
using Macro.Common.Configuration;
using Macro.Desktop;

namespace Macro.ImageViewer.Utilities.Print.Dicom
{
    [SettingsGroupDescription("Stores settings for DicomPrintSettings.")]
    [SettingsProvider(typeof(StandardSettingsProvider))]
    public partial class DicomPrintSettings
    {
        public DicomPrintSettings()
        {
            ApplicationSettingsRegistry.Instance.RegisterInstance(this);
        }

        public static string LocalDefaultPrinterName
        {
            get
            {
                return Default.GetSharedPropertyValue("DefaultPrinterName").ToString();
            }
            set
            {
                Default.SetSharedPropertyValue("DefaultPrinterName", value);
            }
        }

        public static DicomPrinterCollection LocalDicomPrinterCollection
        {
            get
            {
                string xmlcollection = (string)Default.GetSharedPropertyValue("DicomPrinterCollection");
                DicomPrinterCollection sharedPropertyValue = null;
                try
                {
                    if (xmlcollection != null && xmlcollection != "")
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(DicomPrinterCollection));
                        using (TextReader reader = new StringReader(xmlcollection))
                        {
                            sharedPropertyValue = (DicomPrinterCollection)serializer.Deserialize(reader);
                        }
                    }
                }
                catch (Exception e)
                {
                    Platform.Log(LogLevel.Error, e);
                }
                return (sharedPropertyValue ?? new DicomPrinterCollection());
            }
            set
            {
                string result = "";
                try
                {
                    StringBuilder buffer = new StringBuilder();
                    XmlSerializer serializer = new XmlSerializer(typeof(DicomPrinterCollection));
                    using (TextWriter writer = new StringWriter(buffer))
                    {
                        serializer.Serialize(writer, value);
                    }
                    result = buffer.ToString().Substring(buffer.ToString().IndexOf("?>") + 4);
                }
                catch (Exception e)
                {
                    Platform.Log(LogLevel.Error, e);
                }

                Default.SetSharedPropertyValue("DicomPrinterCollection", result);
            }
        }



    }
}
