
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
using Macro.Common;
using Macro.Dicom;
using Macro.Dicom.Iod;
using Macro.Dicom.Iod.Modules;
using Macro.Dicom.Network.Scu;
using Macro.ImageViewer.Common;
using Macro.ImageViewer.ImageExport;
using Macro.ImageViewer.StudyManagement;
using AuditedInstances = Macro.ImageViewer.Common.Auditing.AuditedInstances;

namespace Macro.ImageViewer.Utilities.Print.Dicom
{
    public sealed class DicomPrintSession : IDisposable
    {
        private SelectPresentationsInformationsCollection _selectPresentationsCollection;
        private readonly DicomPrinter _dicomPrinter;
        public DicomPrintSession(DicomPrinter dicomPrinter, SelectPresentationsInformationsCollection selectPresentationsCollection)
        {
            this._dicomPrinter = dicomPrinter;
            this._selectPresentationsCollection = selectPresentationsCollection;
        }

        internal static ExportOption GetPresentationMode(PresentationMode mode)
        {
            switch (mode)
            {
                case PresentationMode.Wysiwyg:
                    return ExportOption.Wysiwyg;

                case PresentationMode.CompleteImage:
                    return ExportOption.CompleteImage;

                case PresentationMode.TrueSize:
                    return ExportOption.TrueSize;
            }
            return ExportOption.Wysiwyg;
        }

        internal static AuditedInstances GetAuditedInstances(IEnumerable<ISelectPresentationsInformation> collection)
        {
            AuditedInstances instances = new AuditedInstances();
            IEnumerator<ISelectPresentationsInformation> enumerator1 = collection.GetEnumerator();
            using (IEnumerator<ISelectPresentationsInformation> enumerator = enumerator1)
            {
                while (enumerator.MoveNext())
                {
                    ISelectPresentationsInformation selectitem = enumerator.Current;
                    IPresentationImage image = selectitem.Image;
                    IImageSopProvider provider = image as IImageSopProvider;
                    if (provider != null)
                    {
                        ImageSop imageSop = provider.ImageSop;
                        PersonName patientsName = imageSop.PatientsName;
                        string studyInstanceUid = imageSop.StudyInstanceUid;
                        instances.AddInstance(imageSop.PatientId, patientsName.ToString(), studyInstanceUid);
                    }
                }
            }
            return instances;
        }

        private static T GetT<T>(IEnumerable<T> Collection) where T : class
        {
            if (Collection == null)
            {
                return default(T);
            }
            IEnumerator<T> enumerator = Collection.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return default(T);
            }
            return enumerator.Current;
        }

        private static PrintScu.FilmBox GetFilmBox(DicomPrinter.Configuration config, IList<PrintScu.IPrintItem> itemList)
        {
            // This item is obfuscated and can not be translated.
            PrintScu.FilmBox box;
            if (config.FilmBox.FilmSize != PrinterFilmSize.AutoSelect)
            {

                box = new PrintScu.FilmBox(config.StandardResolutionDPI, config.HighResolutionDPI)
                {
                    FilmSizeId = config.FilmBox.FilmSize.ToFilmSize(),
                    ImageDisplayFormat = config.FilmBox.ImageDisplayFormat.ToImageDisplayFormat()
                };

                if (config.FilmBox.FilmOrientation != FilmOrientation.None)
                {
                    box.FilmOrientation = config.FilmBox.FilmOrientation;
                }

                if (config.FilmBox.BorderDensity != BorderDensity.None)
                {
                    box.BorderDensity = config.FilmBox.BorderDensity;
                }

                if (config.FilmBox.EmptyImageDensity != EmptyImageDensity.None)
                {
                    box.EmptyImageDensity = config.FilmBox.EmptyImageDensity;
                }

                if (config.FilmBox.MagnificationType != MagnificationType.None)
                {
                    box.MagnificationType = config.FilmBox.MagnificationType;
                }

                if (!string.IsNullOrEmpty(config.FilmBox.ConfigurationInformation))
                {
                    box.ConfigurationInformation = config.FilmBox.ConfigurationInformation;
                }

                if (config.FilmBox.RequestedResolution != RequestedResolution.None)
                {
                    box.RequestedResolutionId = config.FilmBox.RequestedResolution;
                }
            }
            else
            {

                box = new PrintScu.FilmBox(config.StandardResolutionDPI, config.HighResolutionDPI)
                {
                    FilmSizeId = config.FilmBox.FilmSize.ToFilmSize(),
                    ImageDisplayFormat = config.FilmBox.ImageDisplayFormat.ToImageDisplayFormat()
                };

                if (config.FilmBox.RequestedResolution == RequestedResolution.High)
                {
                    int numberOfCopies = config.Session.NumberOfCopies;
                    AutomaticFilmSizeConfiguration automaticFilmSizeConfiguration = config.FilmBox.AutomaticFilmSizeConfiguration;
                    PrintScu.IPrintItem printItem = DicomPrintSession.GetT<PrintScu.IPrintItem>(itemList);
                    FilmConfigInformation fileConfigInformation = FilmConfigInformation.GetFilmInformation(automaticFilmSizeConfiguration, (ISelectPresentationsInformation)printItem, numberOfCopies);
                    box.ImageDisplayFormat = ImageDisplayFormat.Standard_1x1;
                    box.FilmSizeId = fileConfigInformation.FilmSize;
                    box.FilmOrientation = fileConfigInformation.FilmOrientation;
                }
            }
            return box;
        }

        internal static PrintScu.FilmSession GetFilmSession(SelectPresentationsInformationsCollection selects, DicomPrinter.Configuration config)
        {
            FilmBoxConfig fileBoxConfig = new FilmBoxConfig
            {
                config = config
            };

            List<PrintScu.IPrintItem> printItems = new List<PrintScu.IPrintItem>();
            using (IEnumerator<ISelectPresentationsInformation> enumerator = selects.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    ISelectPresentationsInformation presentationsInformation = enumerator.Current;
                    IImageSopProvider imageSopProvider = (IImageSopProvider)presentationsInformation.Image;
                    if (imageSopProvider != null)
                        printItems.Add(new DicomPrintItem(imageSopProvider.ImageSop));
                }
            }

            PrintScu.FilmSession session = new PrintScu.FilmSession(printItems, new PrintScu.CreateFilmBoxDelegate(fileBoxConfig.GetFilmBox));
            if (fileBoxConfig.config.Session.FilmDestination != FilmDestination.None)
            {
                session.FilmDestination = fileBoxConfig.config.Session.FilmDestination;
            }

            if (fileBoxConfig.config.Session.PrintPriority != PrintPriority.None)
            {
                session.PrintPriority = fileBoxConfig.config.Session.PrintPriority;
            }

            if (fileBoxConfig.config.Session.MediumType != MediumType.None)
            {
                session.MediumType = fileBoxConfig.config.Session.MediumType;
            }
            session.NumberOfCopies = fileBoxConfig.config.Session.NumberOfCopies;

            return session;
        }

        public static bool IsHaveModalityPixelSpacing(DicomPrinter.Configuration config, IEnumerable<ISelectPresentationsInformation> collection, out string OutMessage)
        {
            OutMessage = null;
            if (config.PresentationMode == PresentationMode.TrueSize)
            {
                using (IEnumerator<ISelectPresentationsInformation> enumerator = collection.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        ISelectPresentationsInformation selectinfo = enumerator.Current;
                        IPresentationImage image1 = selectinfo.Image;
                        NormalizedPixelSpacing spacing = ((IImageSopProvider)image1).Frame.NormalizedPixelSpacing;
                        if ((spacing == null) || spacing.IsNull)
                        {
                            OutMessage = "NormalizedPixelSpacing为空";
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public DicomPrinter DicomPrinter
        {
            get { return _dicomPrinter; }
        }

        public SelectPresentationsInformationsCollection SelectPresentationsCollection
        {
            get { return _selectPresentationsCollection; }
            set { _selectPresentationsCollection = value; }
        }

        private sealed class FilmBoxConfig
        {

            public DicomPrinter.Configuration config;

            public PrintScu.FilmBox GetFilmBox(IList<PrintScu.IPrintItem> pirntItemList)
            {
                return DicomPrintSession.GetFilmBox(config, pirntItemList);
            }
        }

        private sealed class DicomPrintItem : PrintScu.IPrintItem
        {
            private Sop _file = null;

            public DicomPrintItem(Sop file)
            {
                _file = file;
            }


            #region IPrintItem 成员

            public void GetPixelData(PrintScu.ImageBox imageBox, ColorMode colorMode, out ushort rows, out ushort columns, out byte[] pixelData)
            {
                try
                {
                    LocalSopDataSource dataSource = _file.DataSource as LocalSopDataSource;
                    dataSource.File.Load(DicomReadOptions.Default | DicomReadOptions.StorePixelDataReferences);
                    rows = dataSource.File.DataSet[DicomTags.Rows].GetUInt16(0, 0);
                    columns = dataSource.File.DataSet[DicomTags.Columns].GetUInt16(0, 0);
                    DicomUncompressedPixelData uncompressedPixelData = new DicomUncompressedPixelData(dataSource.File);
                    pixelData = uncompressedPixelData.GetFrame(0);
                }
                catch (Exception e)
                {
                    Platform.Log(LogLevel.Error, string.Format("获得像素数据失败：{0}", e.Message));
                    throw e;
                }

            }
            #endregion
        }

        #region IDisposable 成员

        public void Dispose()
        {
            this.Dispose(true);
            GC.Collect();
        }

        private void Dispose(bool flag)
        {
            if (flag)
            {
                _selectPresentationsCollection = null;
            }
        }

        #endregion
    }

    public sealed class PrintItem : ISelectPresentationsInformation, PrintScu.IPrintItem
    {
        // Fields
        private ExportOption _exportOption;
        private IPresentationImage _presentationImage;
        private PresentationMode _mode;
        private Rectangle _rectangle = Rectangle.Empty;
        private CultureInfo _cultureInfo;
        // Methods
        public PrintItem(ISelectPresentationsInformation selectPresentation, CultureInfo cultureInfo)
        {
            _rectangle = selectPresentation.DisplayRectangle;
            _mode = selectPresentation.PresentationMode;
            _presentationImage = selectPresentation.Image;
            _exportOption = DicomPrintSession.GetPresentationMode(_mode);
            _cultureInfo = cultureInfo;
        }

        public void Dispose()
        {
            _presentationImage = null;
            GC.SuppressFinalize(this);
        }

        public void GetPixelData(PrintScu.ImageBox imageBox, ColorMode colorMode, out ushort rows, out ushort columns, out byte[] pixelData)
        {
            try
            {
                ExportImageItem exportImageItem = new ExportImageItem
                {
                    printItem = this
                };

                Frame frame = ((IImageSopProvider)this.PresentationImage).Frame;
                object[] args = new object[3];
                args[0] = imageBox.ImageBoxPosition;
                args[1] = colorMode;
                args[2] = frame.SopInstanceUid;

                Platform.Log(LogLevel.Debug, "PresentationImage", args);

                if (this.ExportOption == ExportOption.TrueSize)
                {
                    imageBox.RequestedDecimateCropBehavior = DecimateCropBehavior.Crop;
                }
                else
                {
                    imageBox.RequestedDecimateCropBehavior = DecimateCropBehavior.Decimate;
                }

                ExportImageParams exportImageParams = new ExportImageParams();
                exportImageParams.Dpi = imageBox.FilmBox.FilmDPI;
                exportImageParams.SizeMode = SizeMode.ScaleToFit;
                exportImageParams.DisplayRectangle = this.DisplayRectangle;
                exportImageParams.OutputSize = imageBox.SizeInPixels;
                exportImageParams.ExportOption = ExportOption.Wysiwyg;
                exportImageItem.exportImageParams = exportImageParams;

                Platform.Log(LogLevel.Debug, "OutputSize", exportImageItem.exportImageParams.OutputSize);

                exportImageItem.bitmap = null;
                MemoryManager.Execute(exportImageItem.ExportImage);
                rows = (ushort)exportImageItem.bitmap.Size.Height;
                columns = (ushort)exportImageItem.bitmap.Size.Width;

                if (this.ExportOption == ExportOption.TrueSize)
                {
                    imageBox.RequestedImageSize = (((float)columns) * 25.4f) / ((float)imageBox.FilmBox.FilmDPI);

                    Platform.Log(LogLevel.Debug, "RequestedImageSize", imageBox.RequestedImageSize);
                }
                pixelData = BitMapUtility.GetBitmap(exportImageItem.bitmap, colorMode);

            }
            catch (Exception exception)
            {
                Platform.Log(LogLevel.Error, exception);
                throw;
            }
            finally
            {
                Platform.Log(LogLevel.Debug, "finally", new object[] { "完成" });
            }
        }


        private IPresentationImage PresentationImage
        {
            get { return Image; }
        }

        private ExportOption ExportOption
        {
            get { return _exportOption; }
            set { _exportOption = value; }
        }

        private CultureInfo OutputUICulture
        {
            get { return _cultureInfo; }
            set { _cultureInfo = value; }
        }

        public Rectangle DisplayRectangle
        {
            get { return _rectangle; }
            set { _rectangle = value; }
        }

        public PresentationMode PresentationMode
        {
            get { return _mode; }
            set { _mode = value; }
        }

        public IPresentationImage Image
        {
            get { return _presentationImage; }
        }

        public RectangleF NormalizedRectangle
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        // Nested Types
        internal sealed class ExportImageItem
        {
            // Fields
            public PrintItem printItem;
            public ExportImageParams exportImageParams;
            public Bitmap bitmap;

            // Methods                
            public void ExportImage()
            {
                this.bitmap = ImageExporter.DrawToBitmap(this.printItem.PresentationImage, this.exportImageParams);
            }
        }
    }


}

