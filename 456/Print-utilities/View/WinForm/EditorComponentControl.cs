
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
using System.Windows.Forms;
using Macro.Desktop.View.WinForms;
using Macro.ImageViewer.Utilities.Print.Dicom;

namespace Macro.ImageViewer.Utilities.Print.View.WinForm
{
    public partial class EditorComponentControl : ApplicationComponentUserControl
    {
        private IDicomPrinterEditorComponent _component;

        public EditorComponentControl(IDicomPrinterEditorComponent component):
            base(component)
        {
            InitializeComponent();
            _component = component;
            PrinterNametext.DataBindings.Add("Text", _component, "PrinterName", true,
                                             DataSourceUpdateMode.OnPropertyChanged);
            PrinterAETileText.DataBindings.Add("Text", _component, "PrinterAETitle", true,
                                             DataSourceUpdateMode.OnPropertyChanged);
            PrinterHostText.DataBindings.Add("Text", _component, "PrinterHost", true,
                                             DataSourceUpdateMode.OnPropertyChanged);
            PrinterPortText.DataBindings.Add("Text", _component, "PrinterPort", true,
                                             DataSourceUpdateMode.OnPropertyChanged);

            StandardResolution.DataBindings.Add("Value", _component, "StandardResolutionDPI", true,
                                             DataSourceUpdateMode.OnPropertyChanged);

            HighResolution.DataBindings.Add("Value", _component, "HighResolutionDPI", true,
                                             DataSourceUpdateMode.OnPropertyChanged);

            Control guiElement = (Control)_component.PrinterConfigurationEditorComponentHost.ComponentView.GuiElement;
            this.ConfigPanel.Controls.Add(guiElement);
            guiElement.Dock = DockStyle.Fill;
        }

        private void AdancedButton_Click(object sender, EventArgs e)
        {
            IDicomPrinterConfigurationEditorComponent advancedConfigurationComponent =
              (IDicomPrinterConfigurationEditorComponent)_component.PrinterConfigurationEditorComponentHost.Component;
            advancedConfigurationComponent.ShowAdvancedConfigurationOptions();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
             _component.Accept();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            _component.Cancel();
        }
    }
}
