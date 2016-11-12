
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
using System.Drawing;
using Macro.Common;
using Macro.Common.Utilities;
using Macro.Desktop;
using Macro.Desktop.Tables;
using Macro.ImageViewer.ImageExport;

namespace Macro.ImageViewer.Utilities.Print.Dicom.Preview
{

    [ExtensionOf(typeof(DicomPrintPreviewComponentExtensionPoint))]
    public class DicomPrintComponent : IDicomPrintPreviewComponent
    {
        private Checkable<DicomPrinter> _dicomPrinter;
        private static Converter<DicomPrinter, Checkable<DicomPrinter>> Convert;
        private DicomPrinterConfigurationEditorComponent _dicomPrinterConfigurationEditorComponent;
        private DicomPrinterTable _dicomPrinterTable;
        private readonly string _warningMessage = null;
        private event PropertyChangedEventHandler _propertyChanged;
        private IDesktopWindow _desktopWindow;
        private DicomPrintManager _dicomPrintManager = null;
        private PrintImageViewerComponent _printImageViewerComponent = null;

        public DicomPrintComponent()
        {
            InitDicomPrinterConfig();
        }

        public void InitDicomPrinterConfig()
        {
            if (_dicomPrinterConfigurationEditorComponent != null)
            {
                _dicomPrinterConfigurationEditorComponent.PropertyChanged -= LocalPropertyChanged;
            }

            _dicomPrinter = null;

            _dicomPrinterTable = new DicomPrinterTable(true);
            _dicomPrinterConfigurationEditorComponent = new DicomPrinterConfigurationEditorComponent();
            _dicomPrinterConfigurationEditorComponent.PropertyChanged += LocalPropertyChanged;
            if (Convert == null)
            {
                Convert = WrapperDicomPrinterToCheckable;
            }
            _dicomPrinterTable.Items.AddRange(CollectionUtils.Map(DicomPrintSettings.LocalDicomPrinterCollection, Convert));
            _dicomPrinter = _dicomPrinterTable.SelectDicomPrinter(DicomPrintSettings.LocalDefaultPrinterName);

            if (_dicomPrinterTable.Items.Count > 0)
            {
                if (_dicomPrinterTable.SelectFirstCheckedCheckableDicomPrinter == null)
                {
                    _dicomPrinter = _dicomPrinterTable.Items[0];
                }
            }

            if (_dicomPrinter == null)
            {
                _dicomPrinterConfigurationEditorComponent.Configuration = new DicomPrinter.Configuration();
            }
            else
            {
                _dicomPrinterConfigurationEditorComponent.Configuration = this._dicomPrinter.Item.Config;
            }
        }

        private static Checkable<DicomPrinter> WrapperDicomPrinterToCheckable(DicomPrinter dicomPrinter)
        {
            return new Checkable<DicomPrinter>(dicomPrinter);
        }

        private void LocalPropertyChanged(object sender, PropertyChangedEventArgs arg)
        {
            if (arg.PropertyName == "SelectedItem")
            {
                NotifyPropertyChanged("SelectedItem");
            }

            if (arg.PropertyName == "ImageDisplayFormat")
            {
                NotifyPropertyChanged("FilmCount");
                NotifyPropertyChanged("ImageCount");
            }
        }

        public void UpdateDicomPrinterConfigurationEditorComponent()
        {
            if (this._dicomPrinter == null)
            {
                this._dicomPrinterConfigurationEditorComponent.Configuration = null;
            }
            else
            {
                this._dicomPrinterConfigurationEditorComponent.Configuration = this._dicomPrinter.Item.Config;
            }
        }

        public IDesktopWindow DesktopWindow
        {
            get { return _desktopWindow; }
            set { _desktopWindow = value; }
        }

        public DicomPrinterConfigurationEditorComponent DicomPrinterConfigurationEditorComponent
        {
            get { return _dicomPrinterConfigurationEditorComponent; }
        }

        private static IPresentationImage ClonePresentationImage(IPresentationImage presentationImage)
        {
            return ImageExporter.ClonePresentationImage(presentationImage);
        }

        private void InitDicomPrint(
            IDesktopWindow deskTopWindow,
            List<ISelectPresentationsInformation> selectPresentations,
            int tilecount,
            bool isAllPages,
            bool isDelete)
        {

            try
            {

                if (_dicomPrintManager == null)
                {
                    _dicomPrintManager = new DicomPrintManager(deskTopWindow, this);
                    _dicomPrintManager.CloseShelf += ClosePrintManager;
                }

                if (_dicomPrintManager.IsPrinting)
                {
                    DesktopWindow.ShowMessageBox("正在打印....，请稍后", MessageBoxActions.Ok);
                    return;
                }
                _dicomPrintManager.Show();
                _dicomPrintManager.Print(selectPresentations, this.DicomPrinter.Item, tilecount, isAllPages, isDelete);
            }
            catch (Exception exception)
            {
                ExceptionHandler.Report(exception, deskTopWindow);
            }
        }

        #region IDicomPrintComponent 成员

        public void Accept(IDisplaySet displaySet, TileCollection tiles, bool isAllPage, bool printedDeleteImage)
        {
            if (_dicomPrinter == null)
            {
                DesktopWindow.ShowMessageBox("请选择打印机", MessageBoxActions.Ok);
                return;
            }

            if (displaySet != null && displaySet.PresentationImages.Count != 0)
            {

                List<ISelectPresentationsInformation> selectPresentations =
                    new List<ISelectPresentationsInformation>();
                if (isAllPage)
                {
                    for (int i = 0; i < displaySet.PresentationImages.Count; i++)
                    {
                        var image = ClonePresentationImage(displaySet.PresentationImages[i]);

                        int index = i % tiles.Count;
                        Rectangle clientRectangle = tiles[index].ClientRectangle;
                        var select = new SelectPresentionInformation(image, clientRectangle);
                        select.NormalizedRectangle = tiles[index].NormalizedRectangle;
                        selectPresentations.Add(select);

                    }

                }
                else
                {
                    foreach (PrintViewTile tile in tiles)
                    {
                        if (tile.PresentationImage == null)
                        {
                            continue;
                        }
                        var image = ClonePresentationImage(tile.PresentationImage);
                        Rectangle clientRectangle = tile.ClientRectangle;
                        var select = new SelectPresentionInformation(image, clientRectangle);
                        select.NormalizedRectangle = tile.NormalizedRectangle;
                        selectPresentations.Add(select);
                    }
                }

                InitDicomPrint(DesktopWindow, selectPresentations, tiles.Count, isAllPage, printedDeleteImage);
            }
        }

        public bool AcceptEnabled
        {
            get
            {
                return (this._dicomPrinter != null);
            }
        }

        public ITable PrintersTable
        {
            get
            {
                return this._dicomPrinterTable;
            }
        }

        public ISelection SelectedItem
        {
            get
            {
                return new Selection(this._dicomPrinter);
            }
            set
            {
                Selection selection = new Selection(this._dicomPrinter);
                if (!selection.Equals(value))
                {
                    object item = value.Item;
                    this._dicomPrinter = (Checkable<DicomPrinter>)item;
                    UpdateDicomPrinterConfigurationEditorComponent();
                    NotifyPropertyChanged("SelectedItem");
                }
            }
        }

        public string WarningMessage
        {
            get
            {
                return this._warningMessage;
            }
        }

        public bool WarningVisible
        {
            get
            {
                bool flag = string.IsNullOrEmpty(this._warningMessage);
                return !flag;
            }
        }

        public Checkable<DicomPrinter> DicomPrinter
        {
            get { return _dicomPrinter; }
        }

        public PrintImageViewerComponent PrintImageViewerComponent
        {
            set { _printImageViewerComponent = value; }
        }

        #endregion

        #region INotifyPropertyChanged 成员

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged
        {
            add { _propertyChanged += value; }
            remove { _propertyChanged -= value; }
        }

        /// <summary>
        /// Notifies subscribers of the <see cref="PropertyChanged"/> event that the specified property has changed.
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed.</param>
        protected void NotifyPropertyChanged(string propertyName)
        {
            EventsHelper.Fire(_propertyChanged, this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

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
                if (_dicomPrintManager != null && _dicomPrintManager.ProgressShelf != null)
                {
                    _dicomPrintManager.CloseShelf -= ClosePrintManager;
                    _dicomPrintManager.ProgressShelf.Close(UserInteraction.Allowed);
                    _dicomPrintManager = null;
                }
                _dicomPrinter = null;
                _dicomPrinterConfigurationEditorComponent = null;
                _desktopWindow = null;
            }
        }

        private void ClosePrintManager(object sender, EventArgs args)
        {
            if (_dicomPrintManager != null)
            {
                _dicomPrintManager.CloseShelf -= ClosePrintManager;
                _dicomPrintManager = null;
            }
        }
        #endregion

        internal void PrintedDeleteImage(object dicomPrintManger)
        {
            if (dicomPrintManger == null)
            {
                return;
            }
            DicomPrintManager manager = (DicomPrintManager)dicomPrintManger;
            if (!manager.IsPrintedDelete || _printImageViewerComponent == null)
            {
                return;
            }

            if (manager.IsAllPages)
            {
                if (_printImageViewerComponent != null)
                {
                    _printImageViewerComponent.ClearAllImages();
                }
            }
            else
            {
                EventsHelper.Fire(_printImageViewerComponent.EventBroker.DelegateDeleteCurrentPage, this, null);
            }
        }

    }
}
