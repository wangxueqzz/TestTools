
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

using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Macro.ImageViewer.Utilities.Print.Dicom
{
    public class DicomPrinterCollection : ICollection<DicomPrinter>, IEnumerable<DicomPrinter>, IEnumerable, IXmlSerializable
    {
        private readonly List<DicomPrinter> _printers;

        public DicomPrinterCollection()
        {
            this._printers = new List<DicomPrinter>();
        }
        public DicomPrinterCollection(IEnumerable<DicomPrinter> printers)
        {
            this._printers = new List<DicomPrinter>(printers);
        }
        public void Add(DicomPrinter item)
        {
            this._printers.Add(item);
        }
        public void Clear()
        {
            this._printers.Clear();
        }
        public bool Contains(DicomPrinter item)
        {
            return this._printers.Contains(item);
        }
        public void CopyTo(DicomPrinter[] array, int arrayIndex)
        {
            this._printers.CopyTo(array, arrayIndex);
        }
        public IEnumerator<DicomPrinter> GetEnumerator()
        {
            return this._printers.GetEnumerator();
        }
        public XmlSchema GetSchema()
        {
            return null;
        }
        public void ReadXml(XmlReader reader)
        {
            List<DicomPrinter> collection = new List<DicomPrinter>();
            this._printers.Clear();
            if ((reader.MoveToContent() == XmlNodeType.Element) && !reader.IsEmptyElement)
            {
                reader.ReadStartElement();
                while (reader.MoveToContent() == XmlNodeType.Element)
                {
                    DicomPrinter item = null;
                    if (reader.IsEmptyElement)
                    {
                        reader.ReadStartElement();
                    }
                    else
                    {
                        XmlReader xmlReader = reader.ReadSubtree();
                        xmlReader.MoveToContent();
                        item = (DicomPrinter)new XmlSerializer(typeof(DicomPrinter)).Deserialize(xmlReader);
                        xmlReader.Close();
                        reader.ReadEndElement();
                    }
                    if (item != null)
                    {
                        collection.Add(item);
                    }
                }
                reader.ReadEndElement();
                this._printers.AddRange(collection);
            }
        }
        public bool Remove(DicomPrinter item)
        {
            return this._printers.Remove(item);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _printers.GetEnumerator();
        }
        public void WriteXml(XmlWriter writer)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(DicomPrinter));
            using (IEnumerator<DicomPrinter> enumerator = GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    DicomPrinter current = enumerator.Current;
                    serializer.Serialize(writer, current);
                }
            }
        }
        public int Count
        {
            get
            {
                return this._printers.Count;
            }
        }
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }
    }
}

