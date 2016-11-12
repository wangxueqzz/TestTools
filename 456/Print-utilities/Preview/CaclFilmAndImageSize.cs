
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
using Macro.Common;
using Macro.Dicom.Iod.Modules;

namespace Macro.ImageViewer.Utilities.Print.Dicom.Preview
{
    public class CaclFilmAndImageSize
    {
        private static int InsetWidth = 4;
        private static int parentImageBoxBorderWidth = 1;

        public static Size FilmBoxSize(FilmSize filmSizeId, int filmDPI, FilmOrientation filmOrientation)
        {

            if (filmSizeId == null)
                return Size.Empty;

            var physicalWidthInInches = filmSizeId.GetWidth(FilmSize.FilmSizeUnit.Inch);
            var physicalHeightInInches = filmSizeId.GetHeight(FilmSize.FilmSizeUnit.Inch);

            var width = (int)Math.Ceiling(physicalWidthInInches * filmDPI);
            var height = (int)Math.Ceiling(physicalHeightInInches * filmDPI);

            return filmOrientation == FilmOrientation.Landscape
                ? new Size(height, width)
                : new Size(width, height); // default portrait, even if the value is None

        }

        public static Size ImageBoxSize(Size filmBoxSize, out Point location, RectangleF nrmalizedRectangle)
        {
            Size result;
            SetRectangle(out result, out location, nrmalizedRectangle, new Rectangle(0, 0, filmBoxSize.Width, filmBoxSize.Height));
            return result;
        }


        private static void SetRectangle(out Size result, out Point location, RectangleF nrmalizedRectangle, Rectangle parentRectangle)
        {
            int insetImageBoxWidth = parentRectangle.Width - 2 * parentImageBoxBorderWidth;
            int insetImageBoxHeight = parentRectangle.Height - 2 * parentImageBoxBorderWidth;

            int left = (int)(nrmalizedRectangle.Left * insetImageBoxWidth + InsetWidth);
            int top = (int)(nrmalizedRectangle.Top * insetImageBoxHeight + InsetWidth);
            int right = (int)(nrmalizedRectangle.Right * insetImageBoxWidth - InsetWidth);
            int bottom = (int)(nrmalizedRectangle.Bottom * insetImageBoxHeight - InsetWidth);

            location = new Point(left + parentImageBoxBorderWidth, top + parentImageBoxBorderWidth);
            result = new Size(right - left, bottom - top);

            Platform.Log(LogLevel.Debug, location);
            Platform.Log(LogLevel.Debug, result);
        }
    }
}
