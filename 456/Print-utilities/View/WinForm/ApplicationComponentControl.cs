
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
using Macro.Desktop;
using Macro.Desktop.View.WinForms;
using Macro.ImageViewer.Utilities.Print.Dicom;

namespace Macro.ImageViewer.Utilities.Print.View.WinForm
{
    public partial class ApplicationComponentControl : ApplicationComponentUserControl
    {
        private DciomPrintApplicationComponent _component;

        public ApplicationComponentControl(DciomPrintApplicationComponent component)
            : base(component)
        {
            InitializeComponent();
            _component = component;
            this.DicomPrintTable.Table = _component.PrintersTable;
            this.DicomPrintTable.DataBindings.Add("Selection", _component, "SelectedItem", true,
                                                  DataSourceUpdateMode.OnPropertyChanged);

            this.StatusMessage.DataBindings.Add("Text", _component, "WarningMessage", true,
                                                DataSourceUpdateMode.OnPropertyChanged);
            this.FileCountLabel.DataBindings.Add("Text", _component, "FilmCount", true,
                                                DataSourceUpdateMode.OnPropertyChanged);
            this.ImageCountLabel.DataBindings.Add("Text", _component, "ImageCount", true,
                                                DataSourceUpdateMode.OnPropertyChanged);

            Control guiElement = (Control)_component.PrintPreferencesComponentHost.ComponentView.GuiElement;
            this.DicomPrinterConfigPanel.Controls.Add(guiElement);
            guiElement.Dock = DockStyle.Fill;
            this.Load += Loaded;
        }

        private void Loaded(object sender, EventArgs e)
        {
            ISelection selectedItem = _component.SelectedItem;
            this.DicomPrintTable.Selection = selectedItem;
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
