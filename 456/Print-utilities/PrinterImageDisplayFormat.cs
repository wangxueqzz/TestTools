
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
using System.Collections.Generic;
using System.ComponentModel;
using Macro.Common.Utilities;
using Macro.Dicom.Iod.Modules;


namespace Macro.ImageViewer.Utilities.Print.Dicom
{
    public sealed class PrinterImageDisplayFormat : IEquatable<PrinterImageDisplayFormat>
    {

        private ImageDisplayFormat _imageDisplayFormat;
        private static readonly TypeConverter _TypeConverter = new ImageDisplayFormat.DisplayValueConverter();
        public static readonly PrinterImageDisplayFormat Default;
        public static readonly IList<PrinterImageDisplayFormat> Options;

        static PrinterImageDisplayFormat()
        {
            List<PrinterImageDisplayFormat> list = new List<PrinterImageDisplayFormat>();
            list.AddRange(CollectionUtils.Map<ImageDisplayFormat, PrinterImageDisplayFormat>(ImageDisplayFormat.StandardFormats, delegate(ImageDisplayFormat imageDisplay)
            {
                PrinterImageDisplayFormat imageDisplayFormat = new PrinterImageDisplayFormat();
                imageDisplayFormat._imageDisplayFormat = imageDisplay;
                return imageDisplayFormat;
            }));

            Options = list;

            PrinterImageDisplayFormat imageDisplay1 = new PrinterImageDisplayFormat();
            imageDisplay1._imageDisplayFormat = ImageDisplayFormat.Standard_1x1;
            Default = imageDisplay1;
        }


        public bool Equals(PrinterImageDisplayFormat other)
        {

            if (other == null)
            {
                return false;
            }

            if (!string.Equals(this.Value, other.Value))
            {
                return false;
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj.GetType() != typeof(PrinterImageDisplayFormat))
            {
                return false;
            }

            if (!string.Equals(this.Value, ((PrinterImageDisplayFormat)obj).Value))
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return (-647193662 ^ (this.ToString() == null ? 1 : this.ToString().GetHashCode()));
        }

        public static bool operator ==(PrinterImageDisplayFormat a, PrinterImageDisplayFormat b)
        {
            return object.Equals(a, b);
        }

        public static bool operator !=(PrinterImageDisplayFormat a, PrinterImageDisplayFormat b)
        {
            return !object.Equals(a, b);
        }

        public ImageDisplayFormat ToImageDisplayFormat()
        {
            return this._imageDisplayFormat;
        }

        public override string ToString()
        {
            if (this._imageDisplayFormat != null && _TypeConverter != null && _TypeConverter.ConvertToString(this._imageDisplayFormat) != null)
            {
                return _TypeConverter.ConvertToString(this._imageDisplayFormat);
            }
            return "";
        }

        public string Value
        {
            get
            {
                if (this._imageDisplayFormat == null)
                {
                    return string.Empty;
                }
                return this._imageDisplayFormat.DicomString;
            }
            set
            {
                this._imageDisplayFormat = !string.IsNullOrEmpty(value) ? ImageDisplayFormat.FromDicomString(value) : null;
            }
        }
    }
}
