
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
using System.Drawing;

namespace Macro.ImageViewer.Utilities.Print.Dicom
{

    public class SelectPresentionInformation : ISelectPresentationsInformation
    {
        private IPresentationImage _presentationImage;
        private PresentationMode _presentationMode;
        private Rectangle _rectangle;
        private RectangleF _location;

        public SelectPresentionInformation(IPresentationImage presentationImage, Rectangle rectangle)
        {
            _presentationImage = presentationImage;
            _rectangle = rectangle;
        }

        public System.Drawing.Rectangle DisplayRectangle
        {
            get { return _rectangle; }
        }

        public IPresentationImage Image
        {
            get { return _presentationImage; }
        }

        public PresentationMode PresentationMode
        {
            get
            {
                return _presentationMode;
            }
            set
            {
                _presentationMode = value;
            }
        }

        public RectangleF NormalizedRectangle
        {
            get { return _location; }
            set { _location = value; }
        }

        #region IDisposable 成员

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool flag)
        {
            if (_presentationImage != null)
            {
                _presentationImage.Dispose();
            }
        }
        #endregion
    }
}
