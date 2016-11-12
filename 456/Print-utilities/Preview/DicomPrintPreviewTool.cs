
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
using Macro.Desktop.Actions;
using Macro.Desktop;
using Macro.ImageViewer.BaseTools;

namespace Macro.ImageViewer.Utilities.Print.Dicom.Preview
{

    [DropDownButtonAction("PrintPreview", "global-toolbars/ToolbarStandard/ToolbarDicomPrintPreview", "Print", "DropDownActionModel", KeyStroke = XKeys.Control | XKeys.P)]
    [EnabledStateObserver("PrintPreview", "Enabled", "EnabledChanged")]
    [Tooltip("PrintPreview", "TooltipDicomPrintPreview")]
    [IconSet("PrintPreview", "Icons.PrintPreviewSmall.png", "Icons.PrintPreviewMedium.png", "Icons.PrintPreviewLarge.png")]

    [ViewerActionPermission("PrintPreview", "Viewer/PrintPreview")]
    [ExtensionOf(typeof(ImageViewerToolExtensionPoint), FeatureToken = "Workstation.PrintPreview")]
    public class DicomPrintPreviewTool : ImageViewerTool
    {
        private ActionModelNode _mainDropDownActionModel;

        public ActionModelNode DropDownActionModel
        {
            get
            {
                if (_mainDropDownActionModel == null)
                {
                    _mainDropDownActionModel = ActionModelRoot.CreateModel("Macro.ImageViewer.Tools.Standard", "PrintPreview-dropdown", this.ImageViewer.ExportedActions);
                }
                return _mainDropDownActionModel;
            }
        }

        [ThreadStatic]
        private static Shelf _shelf = null;
        private static IPrintViewImageViewer _printViewImageViewer = null;

        internal static Shelf Launch(IDesktopWindow window)
        {

            var component = new PrintImageViewerComponent(window);
            _printViewImageViewer = component;
            var shelf = ApplicationComponent.LaunchAsShelf(
                 window,
                 component,
                 "打印预览",
                  ShelfDisplayHint.DockRight
                 );
            return shelf;
        }

        internal static void InitDicomPrintPreviewComponent(IDesktopWindow desktopWindow)
        {
            try
            {
                if (_shelf == null)
                {
                    _shelf = Launch(desktopWindow);
                    _shelf.Closed += CloseWorkspace;
                }
                else
                {
                    _shelf.Show();
                }
            }
            catch (Exception e)
            {
                CloseWorkspace(null, null);
                ExceptionHandler.Report(e, desktopWindow);
            }
        }

        protected override void OnPresentationImageSelected(object sender, PresentationImageSelectedEventArgs e)
        {
            base.OnPresentationImageSelected(sender, e);
            IPresentationImage selectedPresentationImage = base.SelectedPresentationImage;
            IDisplaySet set = (selectedPresentationImage != null) ? selectedPresentationImage.ParentDisplaySet : null;
            base.Enabled = set != null;
        }

        public void Print()
        {
            IDesktopWindow desktopWindow = base.Context.DesktopWindow;
            InitDicomPrintPreviewComponent(desktopWindow);
        }

        private static void CloseWorkspace(object sender, EventArgs Args)
        {
            if (_printViewImageViewer != null)
            {
                _printViewImageViewer.Dispose();
            }

            if (_shelf != null)
            {
                _shelf = null;
            }
        }

    }
}
