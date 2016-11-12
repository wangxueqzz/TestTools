
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
using System.Drawing;
using System.Globalization;
using System.Threading;
using Macro.Common;
using Macro.Dicom;
using Macro.Dicom.Iod.Modules;
using Macro.Dicom.Network.Scu;
using Macro.ImageViewer.Annotations;
using Macro.ImageViewer.Common;
using Macro.ImageViewer.ImageExport;
using Macro.ImageViewer.Mathematics;
using Macro.ImageViewer.StudyManagement;

namespace Macro.ImageViewer.Utilities.Print.Dicom.Preview
{
    public class MergerDicomImageToBitmap
    {
        public static List<IPresentationImage> Merger(FilmSize filmSizeId,
            int filmDPI,
            FilmOrientation filmOrientation,
            List<ISelectPresentationsInformation> selects,
            PresentationMode presentationMode,
            ColorMode colorMode,
            int tileCount)
        {
            if (selects.Count == 0)
            {
                return null;
            }

            List<IPresentationImage> results = new List<IPresentationImage>();

            Size filmSize = CaclFilmAndImageSize.FilmBoxSize(filmSizeId, filmDPI, filmOrientation);
            if (filmSize == Size.Empty)
            {
                return null;
            }

            Bitmap result = null;
            System.Drawing.Graphics graphics = null;
            string savePath = null;

            foreach (var temp in selects)
            {
                var imageSopProvider = temp.Image as IImageSopProvider;
                if (imageSopProvider == null)
                    continue;
                
                var localSource = imageSopProvider.ImageSop.DataSource as ILocalSopDataSource;
                if (localSource != null)
                {
                    int index = localSource.Filename.LastIndexOf("\\");
                    savePath = localSource.Filename.Substring(0, index);
                    break;
                }
            }

            for (int i = 0; i < selects.Count; i++)
            {
                var select = selects[i];
                if (i != 0 && i % tileCount == 0) //翻页时进入此处
                {
                    IPresentationImage presentationImage = CreateResultImage(result, colorMode, savePath);
                    graphics.Dispose();
                    results.Add(presentationImage);
                    result.Dispose();
                    result = null;
                }

                if (result == null)
                {
                    result = new Bitmap(filmSize.Width, filmSize.Height);
                    graphics = System.Drawing.Graphics.FromImage(result);
                }

                Point location;
                Size imageBoxSize = CaclFilmAndImageSize.ImageBoxSize(filmSize, out location, select.NormalizedRectangle);
                Bitmap bitmap = GetPrintImageBitmap(select, imageBoxSize, presentationMode);
                if (bitmap == null)
                {
                    continue;
                }
                System.Drawing.Graphics borderGraphics = System.Drawing.Graphics.FromImage(bitmap);
                Platform.Log(LogLevel.Debug, new Size(bitmap.Width, bitmap.Height));
                borderGraphics.DrawRectangle(new Pen(Color.White, 2), 0, 0, bitmap.Width, bitmap.Height);
                graphics.DrawImageUnscaled(bitmap, location.X, location.Y);
                bitmap.Dispose();
                borderGraphics.Dispose();
            }

            var image = CreateResultImage(result, colorMode, savePath);
            graphics.Dispose();
            results.Add(image);
            result.Dispose();
            return results;
        }

        private static float ScaleToFit(Size source, SizeF destination)
        {
            if (source.Width == 0 || source.Height == 0)
                return 1;

            float aW = destination.Width / source.Width;
            float aH = destination.Height / source.Height;
            if (!FloatComparer.IsGreaterThan(aW * source.Height, destination.Height))
                return aW;
            else
                return aH;
        }

        private static Bitmap GetPrintImageBitmap(ISelectPresentationsInformation presentationsInformation, Size outputSize, PresentationMode presentationMode)
        {
            presentationsInformation.PresentationMode = presentationMode;
            var currentUiCulture = Thread.CurrentThread.CurrentUICulture;
            var printItem = new PrintItem(presentationsInformation, currentUiCulture);
            var exportImageItem = new PrintItem.ExportImageItem
               {
                   printItem = printItem
               };

            float scale = ScaleToFit(printItem.DisplayRectangle.Size, outputSize);
            var exportImageParams = new ExportImageParams();
            exportImageParams.Dpi = 96;
            exportImageParams.SizeMode = SizeMode.ScaleToFit;
            exportImageParams.DisplayRectangle = new Rectangle(0, 0, (int)(outputSize.Width / scale), (int)(outputSize.Height / scale));
            exportImageParams.OutputSize = outputSize;
            exportImageParams.ExportOption = ExportOption.Wysiwyg;
            exportImageItem.exportImageParams = exportImageParams;

            exportImageItem.bitmap = null;
            MemoryManager.Execute(exportImageItem.ExportImage);
            return exportImageItem.bitmap;
        }

        private static IPresentationImage CreateResultImage(Bitmap bitmap, ColorMode colorMode, string filePath)
        {
            DicomFile file = new DicomFile();
            file.DataSet[DicomTags.BitsAllocated].SetInt32(0, 8);
            file.DataSet[DicomTags.BitsStored].SetInt32(0, 8);
            file.DataSet[DicomTags.HighBit].SetInt32(0, 7);
            file.DataSet.RemoveAttribute(DicomTags.WindowCenter);
            file.DataSet.RemoveAttribute(DicomTags.WindowWidth);
            file.DataSet[DicomTags.PixelSpacing].SetInt32(0, 0);
            file.DataSet[DicomTags.RescaleIntercept].SetInt32(0, 0);
            file.DataSet[DicomTags.RescaleSlope].SetInt32(0, 1);
            file.DataSet[DicomTags.Rows].SetInt32(0, bitmap.Height);
            file.DataSet[DicomTags.Columns].SetInt32(0, bitmap.Width);
            file.DataSet[DicomTags.PixelRepresentation].SetInt32(0, 0);
            file.DataSet[DicomTags.NumberOfFrames].SetInt32(0, 1);
            file.DataSet[DicomTags.PhotometricInterpretation].SetStringValue("MONOCHROME2");
            file.DataSet[DicomTags.SopClassUid].SetStringValue("1.2.840.10008.5.1.4.1.1.7");
            file.DataSet[DicomTags.Modality].SetStringValue("OT");
            file.DataSet[DicomTags.TransferSyntaxUid].SetStringValue("1.2.840.10008.1.2");
            file.DataSet[DicomTags.StudyId].SetStringValue("1");
            file.DataSet[DicomTags.StudyInstanceUid].SetStringValue("1.2.276.0.7230010.3.1.2.2866517296.296.1377417571.2");
            file.DataSet[DicomTags.SeriesNumber].SetInt32(0, 1);
            file.DataSet[DicomTags.SamplesPerPixel].SetInt32(0, 1);
            file.DataSet[DicomTags.SeriesInstanceUid].SetStringValue(" 1.2.276.0.7230010.3.1.3.2866517296.296.1377417571.3");
            file.DataSet[DicomTags.SopInstanceUid].SetStringValue(DicomUid.GenerateUid().UID);
            file.DataSet[DicomTags.PixelData].Values = null;
            byte[] pixelData = BitMapUtility.GetBitmap(bitmap, colorMode);
            file.DataSet[DicomTags.PixelData].Values = pixelData;
            file.MediaStorageSopClassUid = file.DataSet[DicomTags.SopClassUid];
            file.MediaStorageSopInstanceUid = file.DataSet[DicomTags.SopInstanceUid];
            string fileName = string.Format("{0}\\{1}", filePath, DateTime.Now.Ticks.ToString());
            file.Save(fileName);
            var dataSource = new LocalSopDataSource(file);
            Sop dstSop = Sop.Create(dataSource);
            IPresentationImage presentation = (PresentationImageFactory.Create((ImageSop)dstSop))[0];
            if (presentation is IAnnotationLayoutProvider)
            {
                foreach (AnnotationBox box in ((IAnnotationLayoutProvider)presentation).AnnotationLayout.AnnotationBoxes)
                    box.Visible = false;
            }

            return presentation;

        }
    }
}
