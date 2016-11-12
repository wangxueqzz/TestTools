
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
using System.IO;
using System.Windows.Forms;
using Macro.Common;
using Macro.Desktop;
using Macro.Desktop.View.WinForms;

namespace Macro.ImageViewer.Utilities.Print.Dicom.Preview.View.Winforms
{
    public sealed class LayoutSelectExtensionPoint : ExtensionPoint<IView>
    {

    }

    [ExtensionOf(typeof(LayoutSelectExtensionPoint))]
    public class LayoutSelectView : WinFormsView, IApplicationComponentView
    {
        private LayoutSelectComponent _component;
        private LayoutSelectControl _control;

        public override object GuiElement
        {
            get
            {
                if (_control == null)
                {
                    _control = new LayoutSelectControl(_component);
                }
                return _control;
            }
        }

        public void SetComponent(IApplicationComponent component)
        {
            _component = (LayoutSelectComponent)component;
        }
    }

    [AssociateView(typeof(LayoutSelectExtensionPoint))]
    public class LayoutSelectComponent : ApplicationComponent
    {
        private Button _selectLayout;
        private PrintImageViewerComponent _component;

        public LayoutSelectComponent(PrintImageViewerComponent component)
        {
            _component = component;
        }

        public List<Button> Layouts
        {
            get
            {
                var layouts = new List<Button>();
                string[] files = Directory.GetFiles(Platform.ConfigDirectory);
                for (int i = 0; i < files.Length; i++)
                {
                    var info = new FileInfo(files[i]);
                    var button = new Button();
                    button.Click += Select;
                    button.Text = info.Name.Replace(".xml", "");
                    button.Tag = info.Name;
                    layouts.Add(button);
                }
                return layouts;
            }
        }

        private void Select(object sender, EventArgs args)
        {
            _selectLayout = (Button)sender;
        }

        public void Accept()
        {
            if (_selectLayout == null)
            {
                return;
            }
            _component.SetGrid((string)_selectLayout.Tag);
            base.Exit(ApplicationComponentExitCode.Accepted);
        }

        public void Delete()
        {
            if (_selectLayout == null)
            {
                return;
            }
            string fileName = System.IO.Path.Combine(Platform.ConfigDirectory, (string)_selectLayout.Tag);
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }

        public void Cancel()
        {
            base.Exit(ApplicationComponentExitCode.Accepted);
        }
    }
}
