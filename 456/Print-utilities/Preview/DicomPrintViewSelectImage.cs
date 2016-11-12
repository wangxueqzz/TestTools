
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
using Macro.Common;
using Macro.Desktop;
using Macro.ImageViewer.BaseTools;
using Macro.ImageViewer.InputManagement;

namespace Macro.ImageViewer.Utilities.Print.Dicom.Preview
{
    [DefaultMouseToolButton(XMouseButtons.Left)]
    [ExtensionOf(typeof(PrintImageViewerToolExtensionPoint))]
    public class DicomPrintViewSelectImage : MouseImageViewerTool
    {

        public DicomPrintViewSelectImage()
        {
            //this tool is activated on a double-click
            base.Behaviour &= ~MouseButtonHandlerBehaviour.CancelStartOnDoubleClick;
        }

        public override void Initialize()
        {
            PrintImageViewerComponent printImageViewer = Context.Viewer as PrintImageViewerComponent;
            if (printImageViewer == null)
            {
                return;
            }
            printImageViewer.EventBroker.SelectAll += SelectAll;
            printImageViewer.EventBroker.SelectRever += SelectRever;

            base.Initialize();
        }

        public override bool Start(InputManagement.IMouseInformation mouseInformation)
        {
            if (mouseInformation.ClickCount < 2)
            {
                return false;
            }

            PrintImageViewerComponent printImageViewer = this.Context.Viewer as PrintImageViewerComponent;
            if (printImageViewer == null || printImageViewer.SelectPresentationImages == null)
            {
                return false;
            }

            foreach (var item in printImageViewer.SelectPresentationImages)
            {
                PresentationImage image = item as PresentationImage;
                image.Selected = false;
            }
            printImageViewer.SelectPresentationImages.Clear();

            foreach (PrintViewTile tile in this.Context.Viewer.SelectedImageBox.Tiles)
            {
                if (tile.PresentationImage == null)
                {
                    tile.Deselect();
                    continue;
                }
                PresentationImage image = tile.PresentationImage as PresentationImage;
                image.Selected = true;
                printImageViewer.SelectPresentationImages.Add(image);
            }

            if (printImageViewer.SelectPresentationImages.Count > 0)
            {
                PrintViewImageBox imageBox = printImageViewer.SelectedImageBox as PrintViewImageBox;
                imageBox.SelectedTile = printImageViewer.SelectPresentationImages[0].Tile;

            }
            this.Context.Viewer.SelectedImageBox.Draw();

            return true;

        }

        private void SelectAll(object sender, EventArgs args)
        {


            PrintImageViewerComponent printImageViewer = Context.Viewer as PrintImageViewerComponent;
            if (printImageViewer == null || printImageViewer.SelectPresentationImages == null)
            {
                return;
            }

            foreach (PrintViewTile tile in this.Context.Viewer.SelectedImageBox.Tiles)
            {
                if (tile.PresentationImage == null)
                {
                    tile.Deselect();
                }
            }

            foreach (var item in printImageViewer.SelectPresentationImages)
            {
                PresentationImage image = item as PresentationImage;
                image.Selected = false;
            }
            printImageViewer.SelectPresentationImages.Clear();

            foreach (var tile in printImageViewer.DisplaySet.PresentationImages)
            {

                PresentationImage image = tile as PresentationImage;
                image.Selected = true;
                printImageViewer.SelectPresentationImages.Add(image);
            }

            if (printImageViewer.SelectPresentationImages.Count > 0)
            {
                PrintViewImageBox imageBox = printImageViewer.SelectedImageBox as PrintViewImageBox;
                imageBox.SelectedTile = printImageViewer.SelectPresentationImages[0].Tile;

            }
            this.Context.Viewer.SelectedImageBox.Draw();
        }

        private void SelectRever(object sender, EventArgs args)
        {
            PrintImageViewerComponent printImageViewer = Context.Viewer as PrintImageViewerComponent;
            if (printImageViewer == null || printImageViewer.SelectPresentationImages == null)
            {
                return;
            }

            foreach (PrintViewTile tile in this.Context.Viewer.SelectedImageBox.Tiles)
            {
                if (tile.PresentationImage == null)
                {
                    tile.Deselect();
                }
            }

            List<PresentationImage> temp = new List<PresentationImage>();
            foreach (var tile in printImageViewer.DisplaySet.PresentationImages)
            {
                PresentationImage image = tile as PresentationImage;
                if (printImageViewer.SelectPresentationImages.Contains(image))
                {
                    image.Selected = false;
                }
                else
                {
                    image.Selected = true;
                    temp.Add(image);

                }
            }
            printImageViewer.SelectPresentationImages.Clear();
            printImageViewer.SelectPresentationImages.AddRange(temp);

            if (printImageViewer.SelectPresentationImages.Count > 0)
            {
                PrintViewImageBox imageBox = printImageViewer.SelectedImageBox as PrintViewImageBox;
                imageBox.SelectedTile = printImageViewer.SelectPresentationImages[0].Tile;

            }
            this.Context.Viewer.SelectedImageBox.Draw();
        }

        protected override void Dispose(bool disposing)
        {
            PrintImageViewerComponent printImageViewer = Context.Viewer as PrintImageViewerComponent;
            if (printImageViewer == null)
            {
                return;
            }
            printImageViewer.EventBroker.SelectAll -= SelectAll;
            printImageViewer.EventBroker.SelectRever -= SelectRever;

            base.Dispose(disposing);
        }
    }
}
