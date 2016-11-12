
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
using System.IO;
using System.Text.RegularExpressions;
using Macro.Common;
using Macro.Desktop;
using Macro.Desktop.View.WinForms;

namespace Macro.ImageViewer.Utilities.Print.Dicom.Preview.View.Winforms
{
    [ExtensionPoint]
    public sealed class SaveCustumGridViewExtensionPoint : ExtensionPoint<IView>
    {
    }

    [ExtensionOf(typeof(SaveCustumGridViewExtensionPoint))]
    public class SaveCustumGridView : WinFormsView, IApplicationComponentView
    {
        private SaveCustumGridControl _control;
        private SaveCustumGridComponent _component;

        public override object GuiElement
        {
            get
            {
                if (_control == null)
                {
                    _control = new SaveCustumGridControl(_component);
                }

                return _control;
            }
        }

        public void SetComponent(IApplicationComponent component)
        {
            _component = (SaveCustumGridComponent)component;
        }
    }

    [AssociateView(typeof(SaveCustumGridViewExtensionPoint))]
    public class SaveCustumGridComponent : ApplicationComponent
    {
        private PrintImageViewerComponent _printImageViewerComponent;
        private string _saveFileName;

        public SaveCustumGridComponent(PrintImageViewerComponent printImageViewerComponent)
        {
            _printImageViewerComponent = printImageViewerComponent;
        }

        public string SaveFileName
        {
            get { return _saveFileName; }
            set
            {
                _saveFileName = value;
                NotifyPropertyChanged("SaveFileName");
            }
        }

        public void Accept()
        {
            try
            {

                if (!(string.IsNullOrEmpty(SaveFileName) || string.IsNullOrWhiteSpace(SaveFileName)))
                {
                    var rx = new Regex(@"([^A-Za-z0-9_])", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    string fileName = rx.Replace(SaveFileName, "");

                    if (string.IsNullOrWhiteSpace(fileName) || string.IsNullOrEmpty(fileName))
                    {
                        this.Host.DesktopWindow.ShowMessageBox("请输入正确的文件名", MessageBoxActions.Ok);
                        return;
                    }

                    fileName = string.Format("{0}.xml", fileName);
                    var file = System.IO.Path.Combine(Platform.ConfigDirectory, fileName);

                    if (File.Exists(file))
                    {
                        var action = this.Host.DesktopWindow.ShowMessageBox("文件已存在，是否替换？", MessageBoxActions.YesNo);
                        if (action == DialogBoxAction.Yes)
                        {
                            _printImageViewerComponent.SaveGrid(fileName);
                        }
                        else
                        {
                            return;
                        }

                    }
                    else
                    {
                        _printImageViewerComponent.SaveGrid(fileName);
                    }
                }

                base.Exit(ApplicationComponentExitCode.Accepted);

            }
            catch (Exception e)
            {
                Platform.Log(LogLevel.Error, e);
            }

        }
    }
}
