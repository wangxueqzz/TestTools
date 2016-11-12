
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
using Macro.Common;
using Macro.Desktop;
using Macro.Desktop.Validation;


namespace Macro.ImageViewer.Utilities.Print.Dicom
{
    [ExtensionPoint]
    public sealed class DicomPrinterEditorViewExtensionPoint : ExtensionPoint<IApplicationComponentView>
    {
    }


    [AssociateView(typeof(DicomPrinterEditorViewExtensionPoint))]
    internal sealed class DicomPrinterEditorComponent : ApplicationComponent, IDicomPrinterEditorComponent
    {
        private DicomPrinterConfigurationEditorComponent _dicomPrinterConfigurationEditorComponent = null;
        private ChildComponentHost _childComponentHost;
        private readonly DicomPrinter _dicomPrinter;

        public DicomPrinterEditorComponent(DicomPrinter dicomPrinter)
        {
            this._dicomPrinter = dicomPrinter;
        }

        private void LocalPropertyChanged(object sender, EventArgs args)
        {
            this.Modified = true;
        }

        public void Accept()
        {
            if (this.HasValidationErrors)
            {
                this.ShowValidation(true);
            }
            else
            {
                base.Exit(ApplicationComponentExitCode.Accepted);
            }
        }

        public void Cancel()
        {
            base.Exit(ApplicationComponentExitCode.None);
        }

        public override void Start()
        {
            if (_dicomPrinterConfigurationEditorComponent == null)
            {
                _dicomPrinterConfigurationEditorComponent = new DicomPrinterConfigurationEditorComponent();
            }
            _dicomPrinterConfigurationEditorComponent.Configuration = _dicomPrinter.Config;
            this._childComponentHost = new ChildComponentHost(base.Host, this._dicomPrinterConfigurationEditorComponent);
            this._dicomPrinterConfigurationEditorComponent.ModifiedChanged += new EventHandler(this.LocalPropertyChanged);
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
                return this.Modified;
            }
        }

        public override bool Modified
        {
            get
            {
                return base.Modified;
            }
            protected set
            {
                base.Modified = value;
                base.NotifyPropertyChanged("Modified");
            }
        }


        [ValidateLength(1, 0x10, Message = "ValidationAETitleLengthIncorrect"), ValidateRegex(@"[\r\n\e\f\\]+", SuccessOnMatch = false, Message = "ValidationAETitleInvalidCharacters")]
        public string PrinterAETitle
        {
            get
            {
                return this._dicomPrinter.AETitle;
            }
            set
            {
                this._dicomPrinter.AETitle = value;
                this.Modified = true;
            }
        }


        public ApplicationComponentHost PrinterConfigurationEditorComponentHost
        {
            get
            {
                return this._childComponentHost;
            }
        }


        [ValidateNotNull]
        public string PrinterHost
        {
            get
            {
                return this._dicomPrinter.Host;
            }
            set
            {
                this._dicomPrinter.Host = value;
                this.Modified = true;
            }
        }


        [ValidateNotNull]
        public string PrinterName
        {
            get
            {
                return this._dicomPrinter.Name;
            }
            set
            {
                this._dicomPrinter.Name = value;
                this.Modified = true;
            }
        }

        [ValidateLessThan(0x10000, Message = "ValidationPortOutOfRange", Inclusive = false), ValidateGreaterThan(0, Message = "ValidationPortOutOfRange", Inclusive = false)]
        public int PrinterPort
        {
            get
            {
                return this._dicomPrinter.Port;
            }
            set
            {
                this._dicomPrinter.Port = value;
                this.Modified = true;
            }
        }

        public int StandardResolutionDPI
        {
            get
            {

                return this._dicomPrinter.Config.StandardResolutionDPI;
            }
            set
            {
                this._dicomPrinter.Config.StandardResolutionDPI = value;
                this.Modified = true;
            }
        }

        public int HighResolutionDPI
        {
            get
            {

                return this._dicomPrinter.Config.HighResolutionDPI;
            }
            set
            {
                this._dicomPrinter.Config.HighResolutionDPI = value;
                this.Modified = true;
            }
        }
    }
}

