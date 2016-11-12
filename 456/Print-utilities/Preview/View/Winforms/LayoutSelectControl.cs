
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

namespace Macro.ImageViewer.Utilities.Print.Dicom.Preview.View.Winforms
{
    public partial class LayoutSelectControl : UserControl
    {
        private LayoutSelectComponent _component;

        public LayoutSelectControl(LayoutSelectComponent component)
        {
            InitializeComponent();
            _component = component;
            this.Load += new EventHandler(LayoutSelectControl_Load);
        }

        void LayoutSelectControl_Load(object sender, EventArgs e)
        {
            setLayoutPanel();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _component.Accept();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _component.Delete();
            setLayoutPanel();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _component.Cancel();
        }

        private void setLayoutPanel()
        {
            flowLayoutPanel.Controls.Clear();
            flowLayoutPanel.Controls.AddRange(_component.Layouts.ToArray());
        }
    }
}
