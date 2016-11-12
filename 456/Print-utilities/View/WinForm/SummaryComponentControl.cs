
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
    public partial class SummaryComponentControl : ApplicationComponentUserControl
    {
        private IDicomPrinterSummaryComponent _component;

        public SummaryComponentControl(IDicomPrinterSummaryComponent component)
            : base(component)
        {
            InitializeComponent();
            _component = component;
            this.DicomPrintTable.ToolbarModel = _component.TableActionModel;
            this.DicomPrintTable.MenuModel = _component.TableActionModel;
            this.DicomPrintTable.Table = _component.PrintersTable;
            this.DicomPrintTable.DataBindings.Add("Selection", _component, "SelectedItem", true,
                                                  DataSourceUpdateMode.OnPropertyChanged);
            this.DicomPrintTable.ItemDoubleClicked += OnItemDoubleClicked;

        }

        private void OnItemDoubleClicked(object sender, EventArgs args)
        {
            _component.OnItemDoubleClicked();
        }
    }
}
