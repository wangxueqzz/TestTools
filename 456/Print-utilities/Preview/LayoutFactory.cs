
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

using Macro.Dicom.Iod.Modules;

namespace Macro.ImageViewer.Utilities.Print.Dicom.Preview
{
    public class LayoutFactory
    {
        /// <summary>
        /// 构造一个ImageDisplayFormat实例
        /// </summary>
        /// <param name="row">行</param>
        /// <param name="column">列</param>
        public static ImageDisplayFormat TileFactory(int row, int column)
        {
            ImageDisplayFormat imageDisplayFormat = ImageDisplayFormat.FromDicomString(string.Format(@"STANDARD\{0},{1}", column, row));
            return imageDisplayFormat;
        }

        /// <summary>
        /// 获得打印格式的行
        /// </summary>
        public static int GetRowNumber(ImageDisplayFormat imageDisplayFormat)
        {
            return imageDisplayFormat.Modifiers[1];
        }

        /// <summary>
        /// 获得打印格式的列
        /// </summary>
        public static int GetColumnNumber(ImageDisplayFormat imageDisplayFormat)
        {
            return imageDisplayFormat.Modifiers[0];
        }

    }
}
