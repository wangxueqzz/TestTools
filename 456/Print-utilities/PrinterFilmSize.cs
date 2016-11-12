
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
    //此类是操作FilmSize类的，封装
    public sealed class PrinterFilmSize : IEquatable<PrinterFilmSize>
    {
        private FilmSize _filmSize;
        private static readonly TypeConverter _filmSizeConverter = new FilmSize.DisplayValueConverter();
        public static readonly PrinterFilmSize AutoSelect;
        public static readonly PrinterFilmSize Default;
        public static readonly IList<PrinterFilmSize> Options;

        static PrinterFilmSize()
        {
            List<PrinterFilmSize> list = new List<PrinterFilmSize>();
            PrinterFilmSize size = new PrinterFilmSize();
            size._filmSize = null;
            //list.Add(AutoSelect = size);
            list.AddRange(CollectionUtils.Map<FilmSize, PrinterFilmSize>(FilmSize.StandardFilmSizes, delegate(FilmSize filmSize)
            {
                PrinterFilmSize psize = new PrinterFilmSize();
                psize._filmSize = filmSize;
                return psize;
            }));
            Options = list.AsReadOnly();
            Default = list[0];
        }

        public bool Equals(PrinterFilmSize other)
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

            if (obj.GetType() != typeof(PrinterFilmSize))
            {
                return false;
            }

            if (!string.Equals(this.Value, ((PrinterFilmSize)obj).Value))
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return (-647193662 ^ (this.ToString() == null ? 1 : this.ToString().GetHashCode()));
        }

        public static bool operator ==(PrinterFilmSize a, PrinterFilmSize b)
        {
            return object.Equals(a, b);
        }

        public static bool operator !=(PrinterFilmSize a, PrinterFilmSize b)
        {
            return !object.Equals(a, b);
        }

        public FilmSize ToFilmSize()
        {
            return this._filmSize;
        }

        public override string ToString()
        {
            if (this._filmSize != null && _filmSizeConverter != null && _filmSizeConverter.ConvertToString(this._filmSize) != null)
            {
                return _filmSizeConverter.ConvertToString(this._filmSize);
            }
            else
            {
                return "Automatic";
            }
        }

        public string Value
        {
            get
            {
                if (this._filmSize == null)
                {
                    return string.Empty;
                }
                return this._filmSize.DicomString;
            }
            set
            {
                this._filmSize = !string.IsNullOrEmpty(value) ? FilmSize.FromDicomString(value) : null;
            }
        }
    }
}

