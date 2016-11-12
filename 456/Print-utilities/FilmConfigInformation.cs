
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
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Macro.Common;
using Macro.Dicom.Iod.Modules;
using Macro.ImageViewer.Graphics;
using Macro.ImageViewer.StudyManagement;

namespace Macro.ImageViewer.Utilities.Print.Dicom
{
    
    internal sealed class FilmConfigInformation
    {

        private Macro.Dicom.Iod.Modules.FilmOrientation _filmOrientation;
        private Macro.Dicom.Iod.Modules.FilmSize _filmSize;

        private FilmConfigInformation()
        {

        }
        private static IEnumerable<FilmConfigInformation> FilmConfigInformationEnumerable(IEnumerable<Macro.Dicom.Iod.Modules.FilmSize> filmSizeList)
        {
            IEnumerator<Macro.Dicom.Iod.Modules.FilmSize> enumerator = filmSizeList.GetEnumerator();
            FilmConfigInformation info;
            while (enumerator.MoveNext())
            {
                info = new FilmConfigInformation();
                FilmSize size = enumerator.Current;
                info.FilmOrientation = Macro.Dicom.Iod.Modules.FilmOrientation.Portrait;
                info.FilmSize = size;
                yield return info;
                info = new FilmConfigInformation();
                info.FilmOrientation = Macro.Dicom.Iod.Modules.FilmOrientation.Landscape;
                info.FilmSize = size;
                yield return info;
            }
        }
        private static SizeF GetPresentationImageSizeF(ISelectPresentationsInformation selectPresentation, int size)
        {
            //1英寸等于25.4毫米
            float num = 25.4f / ((float)size);
            IPresentationImage image1 = selectPresentation.Image;
            Frame frame = ((IImageSopProvider)image1).Frame;
            switch (selectPresentation.PresentationMode)
            {
                case PresentationMode.Wysiwyg:
                    {
                        Rectangle rectangle = selectPresentation.DisplayRectangle;
                        int width = rectangle.Width;
                        Rectangle rectangle2 = selectPresentation.DisplayRectangle;
                        int height = rectangle2.Height;
                        return new SizeF(width * num, height * num);
                    }
                case PresentationMode.CompleteImage:
                    {
                        int columns = frame.Columns;
                        return new SizeF(columns * num, frame.Rows * num);
                    }
                case PresentationMode.TrueSize:
                    {
                        int num1 = frame.Columns;
                        NormalizedPixelSpacing normalizedPixelSpacing = frame.NormalizedPixelSpacing;
                        int rows = frame.Rows;
                        NormalizedPixelSpacing spacing2 = frame.NormalizedPixelSpacing;
                        double row = spacing2.Row;
                        SizeF ef = new SizeF((float)(num1 * normalizedPixelSpacing.Column), (float)(rows * row));
                        IPresentationImage image2 = selectPresentation.Image;
                        ISpatialTransform spatialTransform = ((ISpatialTransformProvider)image2).SpatialTransform;
                        if (((spatialTransform.RotationXY / 90) % 2) == 1)
                        {
                            float single1 = ef.Height;
                            ef = new SizeF(single1, ef.Width);
                        }
                        return ef;
                    }
            }
            return SizeF.Empty;
        }
        private static float verificationPresentationImageSizeF(SizeF A, SizeF B)
        {
            float width = B.Width;
            float single2 = A.Width;
            float num = width - single2;
            float height = B.Height;
            float single4 = A.Height;
            float num2 = height - single4;
            if ((num < 0f) || (num2 < 0f))
            {
                return float.PositiveInfinity;
            }
            float single5 = B.Width;
            float single6 = B.Height;
            float single7 = A.Width;
            return ((single5 * single6) - (single7 * A.Height));
        }


        public static FilmConfigInformation GetFilmInformation(AutomaticFilmSizeConfiguration config, ISelectPresentationsInformation selectPresentation, int sizef)
        {
            List<FilmConfigInformation> list = new List<FilmConfigInformation>();
            float num = 0f;
            float num2 = 0f;
            float num3 = 0f;
            float num4 = 0f;
            if (config != null)
            {
                num = config.LeftMargin;
                num2 = config.RightMargin;
                num3 = config.TopMargin;
                num4 = config.BottomMargin;
                if (config.FilmSizes != null)
                {
                    IEnumerable<FilmConfigInformation> collection = FilmConfigInformation.FilmConfigInformationEnumerable(config.FilmSizes);
                    list.AddRange(collection);
                }
            }
            if (list.Count == 1)
            {
                return list[0];
            }
            if (list.Count == 0)
            {
                IEnumerable<FilmConfigInformation> enumerable2 = FilmConfigInformation.FilmConfigInformationEnumerable(Macro.Dicom.Iod.Modules.FilmSize.StandardFilmSizes);
                list.AddRange(enumerable2);
            }
            try
            {
                SizeF ef1 = FilmConfigInformation.GetPresentationImageSizeF(selectPresentation, sizef);
                SizeF a = ef1 + new SizeF(num + num2, num3 + num4);
                FilmConfigInformation filmInfo = list[0];
                SizeF size = filmInfo.FilmSizeF;
                float num5 = FilmConfigInformation.verificationPresentationImageSizeF(a, size);
                for (int i = 1; i < list.Count; i++)
                {
                    FilmConfigInformation local3 = list[i];
                    float num7 = FilmConfigInformation.verificationPresentationImageSizeF(a, local3.FilmSizeF);
                    if (num7 < num5)
                    {
                        num5 = num7;
                        filmInfo = list[i];
                    }
                }
                return filmInfo;
            }
            catch (Exception exception)
            {
                string message = exception.Message;
                Platform.Log(Macro.Common.LogLevel.Warn, exception, message, new object[0]);
                return list[0];
            }
        }

        public SizeF FilmSizeF
        {
            get
            {
                float width = FilmSize.GetWidth(Macro.Dicom.Iod.Modules.FilmSize.FilmSizeUnit.Centimeter);
                Macro.Dicom.Iod.Modules.FilmSize size2 = FilmSize;
                SizeF ef = new SizeF(10f * width, 10f * size2.GetHeight(Macro.Dicom.Iod.Modules.FilmSize.FilmSizeUnit.Centimeter));
                Macro.Dicom.Iod.Modules.FilmOrientation orientation1 = FilmOrientation;
                if (orientation1 != Macro.Dicom.Iod.Modules.FilmOrientation.Landscape)
                {
                    return ef;
                }
                return new SizeF(ef.Height, ef.Width);
            }
        }
        public Macro.Dicom.Iod.Modules.FilmOrientation FilmOrientation
        {

            get
            {
                return _filmOrientation;
            }

            private set
            {
                this._filmOrientation = value;
            }
        }
        public Macro.Dicom.Iod.Modules.FilmSize FilmSize
        {

            get
            {
                return this._filmSize;
            }

            private set
            {
                this._filmSize = value;
            }
        }
    }
}

