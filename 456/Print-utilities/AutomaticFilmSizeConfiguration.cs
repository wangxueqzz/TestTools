
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
using System.Text;
using Macro.Dicom.Iod.Modules;

namespace Macro.ImageViewer.Utilities.Print.Dicom
{
    /// <summary>
    /// 此类是设置打印SCU根据图像的大小自动选择FilmSize，能被自动选择的FilmSize在高级配置中配置
    /// </summary>
    [Serializable]
    public class AutomaticFilmSizeConfiguration : IEquatable<AutomaticFilmSizeConfiguration>, ICloneable
    {
        private float _bottomMargin = 1;
        private FilmSize[] _filmSizes;
        private float _leftMargin = 1;
        private float _rightMargin = 1;
        private float _topMargin = 1;


        public AutomaticFilmSizeConfiguration()
        {
            _bottomMargin = 1;
            _leftMargin = 1;
            _rightMargin = 1;
            _topMargin = 1;
            _filmSizes = CreateArray<FilmSize>(FilmSize.StandardFilmSizes);
        }
        public object Clone()
        {
            AutomaticFilmSizeConfiguration a = new AutomaticFilmSizeConfiguration();
            a.BottomMargin = this.BottomMargin;
            a.LeftMargin = this.LeftMargin;
            a.RightMargin = this.RightMargin;
            a.TopMargin = this.TopMargin;
            a.FilmSizes = CreateArray<FilmSize>(_filmSizes);
            return a;
        }
        private string FilmSizeString
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                if (_filmSizes != null)
                {
                    foreach (FilmSize size in _filmSizes)
                    {
                        builder.AppendLine(size.DicomString);
                    }
                }
                return builder.ToString();
            }
        }
        private static T[] CreateArray<T>(IEnumerable<T> enumerable)
        {
            if (enumerable == null)
            {
                return new T[0];
            }
            return new List<T>(enumerable).ToArray();
        }
        public bool Equals(AutomaticFilmSizeConfiguration other)
        {
            if (other == null)
            {
                return false;
            }

            if (this.BottomMargin != other.BottomMargin || this.FilmSizes != other.FilmSizes ||
                this.LeftMargin != other.LeftMargin || this.RightMargin != other.RightMargin || this.TopMargin != other.TopMargin)
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
            if (obj.GetType() != typeof(AutomaticFilmSizeConfiguration))
            {
                return false;
            }
            var afsc = (AutomaticFilmSizeConfiguration)obj;
            if (this.BottomMargin != afsc.BottomMargin || this.FilmSizes != afsc.FilmSizes ||
                this.LeftMargin != afsc.LeftMargin || this.RightMargin != afsc.RightMargin || this.TopMargin != afsc.TopMargin)
            {
                return false;
            }

            return true;
        }
        public override int GetHashCode()
        {
            return 0x7a6fb2b4 ^ this._bottomMargin.GetHashCode() ^ this._leftMargin.GetHashCode() ^ this._rightMargin.GetHashCode() ^ this._topMargin.GetHashCode() ^ (this._filmSizes == null ? 1 : this._filmSizes.GetHashCode());
        }
        public float BottomMargin
        {
            get { return _bottomMargin; }
            set { _bottomMargin = value; }
        }
        public FilmSize[] FilmSizes
        {
            get { return _filmSizes; }
            set { _filmSizes = value; }
        }
        public float LeftMargin
        {
            get { return _leftMargin; }
            set { _leftMargin = value; }
        }
        public float RightMargin
        {
            get { return _rightMargin; }
            set { _rightMargin = value; }
        }
        public float TopMargin
        {
            get { return _topMargin; }
            set { _topMargin = value; }
        }
    }
}

