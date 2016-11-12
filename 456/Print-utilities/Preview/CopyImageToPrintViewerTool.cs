
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
using Macro.Common.Utilities;
using Macro.Desktop;
using Macro.Desktop.Actions;
using Macro.Desktop.Tools;

namespace Macro.ImageViewer.Utilities.Print.Dicom.Preview
{
    [ButtonAction("copyImage", "PrintPreview-dropdown/MenuCopyImageToPrintPreview", "CopyImage", KeyStroke = XKeys.Space)]
    [MenuAction("copyImage", "imageviewer-contextmenu/MenuCopyImageToPrintPreview", "CopyImage", KeyStroke = XKeys.Space)]
    [IconSet("copyImage", "Icons.CopyToClipboardToolSmall.png", "Icons.CopyToClipboardToolMedium.png", "Icons.CopyToClipboardToolLarge.png")]
    [EnabledStateObserver("copyImage", "CopyImageEnabled", "CopyImageEnabledChanged")]

    [ButtonAction("copyDisplaySet", "PrintPreview-dropdown/MenuCopyDisplaySetToPrintPreview", "CopyDisplaySet")]
    [MenuAction("copyDisplaySet", "imageviewer-contextmenu/MenuCopyDisplaySetToPrintPreview", "CopyDisplaySet")]
    [IconSet("copyDisplaySet", "Icons.CopyToClipboardToolSmall.png", "Icons.CopyToClipboardToolMedium.png", "Icons.CopyToClipboardToolLarge.png")]
    [EnabledStateObserver("copyDisplaySet", "CopyDisplaySetEnabled", "CopyDisplaySetEnabledChanged")]

    [ButtonAction("copyAll", "PrintPreview-dropdown/MenuCopyAllToPrintPreview", "CopyAll")]
    [MenuAction("copyAll", "imageviewer-contextmenu/MenuCopyAllToPrintPreview", "CopyAll")]
    [IconSet("copyAll", "Icons.CopyToClipboardToolSmall.png", "Icons.CopyToClipboardToolMedium.png", "Icons.CopyToClipboardToolLarge.png")]
    [EnabledStateObserver("copyAll", "CopyAllEnabled", "CopyAllEnabledChanged")]

    [ExtensionOf(typeof(ImageViewerToolExtensionPoint))]
    public class CopyImageToPrintViewerTool : Tool<IImageViewerToolContext>
    {
        private bool _copyDisplaySetEnabled;
        private bool _copyImageEnabled;
        private bool _copyAllEnabled;

        private event EventHandler _copyDisplaySetEnabledChanged;
        private event EventHandler _copyImageEnabledChanged;
        private event EventHandler _copyAllEnabledChanged;

        public CopyImageToPrintViewerTool()
        {
            _copyDisplaySetEnabled = false;
            _copyImageEnabled = false;
            _copyAllEnabled = false;
        }

        public void CopyImage()
        {
            if (PrintPresentationImagesHost.ImageViewerComponent == null)
            {
                ExceptionHandler.Report(new Exception("请启动打印排版界面"), this.Context.DesktopWindow);
                return;
            }
            PrintPresentationImagesHost.AddPresentationImage(this.Context.Viewer.SelectedPresentationImage);
            Draw();
        }

        public void CopyDisplaySet()
        {
            if (PrintPresentationImagesHost.ImageViewerComponent == null)
            {
                ExceptionHandler.Report(new Exception("请启动打印排版界面"), this.Context.DesktopWindow);
                return;
            }

            foreach (IPresentationImage action in this.Context.Viewer.SelectedPresentationImage.ParentDisplaySet.PresentationImages)
            {
                PrintPresentationImagesHost.AddPresentationImage(action);
            }

            Draw();
        }

        public void CopyAll()
        {
            if (PrintPresentationImagesHost.ImageViewerComponent == null)
            {
                ExceptionHandler.Report(new Exception("请启动打印排版界面"), this.Context.DesktopWindow);
                return;
            }
            foreach (IImageSet imageSet in this.Context.Viewer.PhysicalWorkspace.LogicalWorkspace.ImageSets)
            {
                foreach (IDisplaySet displaySet in imageSet.DisplaySets)
                {
                    foreach (IPresentationImage action in displaySet.PresentationImages)
                    {
                        PrintPresentationImagesHost.AddPresentationImage(action);
                    }
                }
            }

            Draw();

        }

        private void Draw()
        {
            int presentationImageCount =
               PrintPresentationImagesHost.ImageViewerComponent.RootImageBox.DisplaySet.PresentationImages.Count;
            int tileCount = PrintPresentationImagesHost.ImageViewerComponent.RootImageBox.Tiles.Count;
            int pageCount = CaclPageCountHelper.CaclPageCount(presentationImageCount, tileCount);
            PrintPresentationImagesHost.ImageViewerComponent.RootImageBox.TopLeftPresentationImageIndex = (pageCount - 1) * tileCount;
            int imageCount = PrintPresentationImagesHost.ImageViewerComponent.DisplaySet.PresentationImages.Count;
            if (imageCount > 1)
            {
                var lastImage = PrintPresentationImagesHost.ImageViewerComponent.DisplaySet.PresentationImages[imageCount - 1];
                if (lastImage.Tile != null)
                {
                    lastImage.Tile.Select();
                }
            }
            PrintPresentationImagesHost.ImageViewerComponent.RootImageBox.Draw();
            PrintPresentationImagesHost.ImageViewerComponent.PropertyValueChanged();
        }

        public bool CopyDisplaySetEnabled
        {
            get { return _copyDisplaySetEnabled; }
            set
            {
                if (value == _copyDisplaySetEnabled)
                    return;

                _copyDisplaySetEnabled = value;
                EventsHelper.Fire(_copyDisplaySetEnabledChanged, this, EventArgs.Empty);
            }
        }

        public event EventHandler CopyDisplaySetEnabledChanged
        {
            add { _copyDisplaySetEnabledChanged += value; }
            remove { _copyDisplaySetEnabledChanged -= value; }
        }

        public bool CopyImageEnabled
        {
            get { return _copyImageEnabled; }
            set
            {
                if (value == _copyImageEnabled)
                    return;

                _copyImageEnabled = value;
                EventsHelper.Fire(_copyImageEnabledChanged, this, EventArgs.Empty);
            }
        }

        public event EventHandler CopyImageEnabledChanged
        {
            add { _copyImageEnabledChanged += value; }
            remove { _copyImageEnabledChanged -= value; }
        }

        public bool CopyAllEnabled
        {
            get { return _copyAllEnabled; }
            set
            {
                if (value == _copyAllEnabled)
                    return;

                _copyAllEnabled = value;
                EventsHelper.Fire(_copyAllEnabledChanged, this, EventArgs.Empty);
            }
        }

        public event EventHandler CopyAllEnabledChanged
        {
            add { _copyAllEnabledChanged += value; }
            remove { _copyAllEnabledChanged -= value; }
        }

        public override void Initialize()
        {
            base.Initialize();

            base.Context.Viewer.EventBroker.ImageBoxSelected += OnImageBoxSelected;
            base.Context.Viewer.EventBroker.DisplaySetSelected += OnDisplaySetSelected;
        }

        protected override void Dispose(bool disposing)
        {
            base.Context.Viewer.EventBroker.ImageBoxSelected -= OnImageBoxSelected;
            base.Context.Viewer.EventBroker.DisplaySetSelected -= OnDisplaySetSelected;

            base.Dispose(disposing);
        }

        private void OnImageBoxSelected(object sender, ImageBoxSelectedEventArgs e)
        {
            if (e.SelectedImageBox.DisplaySet == null)
                UpdateEnabled(null);
        }

        private void OnDisplaySetSelected(object sender, DisplaySetSelectedEventArgs e)
        {
            UpdateEnabled(e.SelectedDisplaySet);
        }

        private void UpdateEnabled(IDisplaySet selectedDisplaySet)
        {
            if (selectedDisplaySet == null || selectedDisplaySet.PresentationImages.Count < 1)
            {
                CopyDisplaySetEnabled = false;
                CopyAllEnabled = false;
                CopyImageEnabled = false;
            }
            else if (selectedDisplaySet.PresentationImages.Count == 1)
            {
                CopyDisplaySetEnabled = false;
                CopyAllEnabled = false;
                CopyImageEnabled = true;
            }
            else
            {
                CopyDisplaySetEnabled = true;
                CopyAllEnabled = true;
                CopyImageEnabled = true;
            }
        }

    }
}
