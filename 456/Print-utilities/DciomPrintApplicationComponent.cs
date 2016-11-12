
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
using Macro.Common;
using Macro.Common.Utilities;
using Macro.Desktop;
using Macro.Desktop.Tables;
using Macro.Desktop.Validation;
using Macro.Dicom.Iod.Modules;
using Macro.ImageViewer.StudyManagement;

namespace Macro.ImageViewer.Utilities.Print.Dicom
{
    [ExtensionPoint]
    public sealed class DciomPrintApplicationComponentViewExtensionPoint : ExtensionPoint<IApplicationComponentView>
    {
    }

    [AssociateView(typeof(DciomPrintApplicationComponentViewExtensionPoint))]
    public class DciomPrintApplicationComponent : ApplicationComponent, IDicomPrintComponent
    {

        private Checkable<DicomPrinter> _dicomPrinter;
        private static Converter<DicomPrinter, Checkable<DicomPrinter>> Convert;
        private SelectPresentationsInformationsCollection _selectPresentations;
        private DicomPrintSession _dicomPrintSession;
        private DicomPrinterConfigurationEditorComponent _dicomPrinterConfigurationEditorComponent;
        private readonly DicomPrinterTable _dicomPrinterTable;
        private ChildComponentHost _childComponentHost;
        private readonly string _warningMessage = null;


        public DciomPrintApplicationComponent(SelectPresentationsInformationsCollection selectPresentations)
        {
            this._selectPresentations = selectPresentations;
            this._dicomPrinterTable = new DicomPrinterTable(true);
            _dicomPrintSession = null;
        }

        private static bool IsSamePatient(IEnumerable<ISelectPresentationsInformation> collection)
        {
            string str = null;
            foreach (ISelectPresentationsInformation local in collection)
            {
                ImageSop imageSop = ((IImageSopProvider)local.Image).ImageSop;
                string str2 = imageSop.PatientId;
                if (str == null)
                {
                    str = str2;
                }
                else if (str != str2)
                {
                    return true;
                }
            }
            return false;
        }

        private ValidationResult GetValidtionResult(IApplicationComponent component)
        {
            string str;
            DicomPrinter.Configuration config = _dicomPrinter.Item.Config;
            bool success = DicomPrintSession.IsHaveModalityPixelSpacing(config, _selectPresentations, out str);
            return new ValidationResult(success, str);
        }

        private static Checkable<DicomPrinter> WrapperDicomPrinterToCheckable(DicomPrinter dicomPrinter)
        {
            return new Checkable<DicomPrinter>(dicomPrinter);
        }

        private void LocalPropertyChanged(object sender, PropertyChangedEventArgs arg)
        {
            if (arg.PropertyName == "SelectedItem")
            {
                base.NotifyPropertyChanged("SelectedItem");
            }

            if (arg.PropertyName == "ImageDisplayFormat")
            {
                base.NotifyPropertyChanged("FilmCount");
                base.NotifyPropertyChanged("ImageCount");
            }
        }

        public void Accept()
        {
            if (this._dicomPrinter == null)
            {
                base.Host.DesktopWindow.ShowMessageBox("请选择打印机", MessageBoxActions.Ok);
            }
            else
            {
                if (this.HasValidationErrors || this._dicomPrinterConfigurationEditorComponent.HasValidationErrors)
                {
                    this.ShowValidation(true);
                    this._dicomPrinterConfigurationEditorComponent.ShowValidation(true);
                }
                else
                {
                    IApplicationComponentHost host = base.Host;
                    DesktopWindow desktopWindow = host.DesktopWindow;
                    _dicomPrintSession = new DicomPrintSession(_dicomPrinter.Item, _selectPresentations);
                    // Platform.Log(LogLevel.Info, _dicomPrinter.Item.Config.ToString());
                    base.Exit(ApplicationComponentExitCode.Accepted);
                }
            }

        }

        public void Cancel()
        {
            base.Exit(ApplicationComponentExitCode.None);
        }

        public override void Start()
        {
            this._dicomPrinterConfigurationEditorComponent = new DicomPrinterConfigurationEditorComponent();
            this._childComponentHost = new ChildComponentHost(base.Host, this._dicomPrinterConfigurationEditorComponent);
            this._dicomPrinterConfigurationEditorComponent.PropertyChanged += new PropertyChangedEventHandler(this.LocalPropertyChanged);
            this._dicomPrinterConfigurationEditorComponent.Validation.Add(new ValidationRule("HaveModalityPixelSpacing", new ValidationRule.ValidationDelegate(this.GetValidtionResult)));
            ItemCollection<Checkable<DicomPrinter>> items = this._dicomPrinterTable.Items;
            if (Convert == null)
            {
                Convert = new Converter<DicomPrinter, Checkable<DicomPrinter>>(DciomPrintApplicationComponent.WrapperDicomPrinterToCheckable);
            }
            items.AddRange(CollectionUtils.Map<DicomPrinter, Checkable<DicomPrinter>>(DicomPrintSettings.LocalDicomPrinterCollection, Convert));
            this._dicomPrinterTable.SelectDicomPrinter(DicomPrintSettings.LocalDefaultPrinterName);
            if (items.Count > 0)
            {
                if (this._dicomPrinterTable.SelectFirstCheckedCheckableDicomPrinter == null)
                {
                    this.SelectedItem = new Selection(items[0]);
                }
            }

            if (this._dicomPrinter == null)
            {
                this._dicomPrinterConfigurationEditorComponent.Configuration = new DicomPrinter.Configuration();
            }
            else
            {
                this._dicomPrinterConfigurationEditorComponent.Configuration = this._dicomPrinter.Item.Config;
            }
            base.Start();
            this._childComponentHost.StartComponent();
        }

        public override void Stop()
        {
            this._childComponentHost.StopComponent();
            base.Stop();
        }


        public bool AcceptEnabled
        {
            get
            {
                return (this._dicomPrinter != null);
            }
        }

        internal DicomPrintSession DicomPrintSession
        {

            get
            {
                return this._dicomPrintSession;
            }

            private set
            {
                this._dicomPrintSession = value;
            }
        }

        public int FilmCount
        {
            get
            {
                int num = _selectPresentations.Count;
                ImageDisplayFormat format = _dicomPrinterConfigurationEditorComponent.ImageDisplayFormat.ToImageDisplayFormat();
                return (int)Math.Ceiling((double)(((float)num) / ((float)format.MaximumImageBoxes)));
            }
        }

        public int ImageCount
        {
            get
            {
                return _selectPresentations.Count;
            }
        }

        public ITable PrintersTable
        {
            get
            {
                return this._dicomPrinterTable;
            }
        }

        public ApplicationComponentHost PrintPreferencesComponentHost
        {
            get
            {
                return this._childComponentHost;
            }
        }

        private void UpdateDicomPrinterConfigurationEditorComponent()
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
                    base.NotifyPropertyChanged("SelectedItem");
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

        #region IDicomPrintComponent 成员


        public IDesktopWindow DesktopWindow
        {
            get { return base.Host.DesktopWindow; }
            set { }
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool flag)
        {
            if (flag)
            {
                _dicomPrinter = null;
                _selectPresentations = null;
                _dicomPrinterConfigurationEditorComponent = null;
                if (_dicomPrintSession != null)
                {
                    _dicomPrintSession.Dispose();
                }

            }
        }
        #endregion
    }

}
