
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
using Macro.Desktop.Actions;
using Macro.ImageViewer.BaseTools;
using Macro.ImageViewer.ImageExport;

namespace Macro.ImageViewer.Utilities.Print.Dicom.Preview
{
    [ButtonAction("Delete", "global-toolbars/ToolbarStandard/ToolbarPrintPreviewDeleteImage", "Delete", KeyStroke = XKeys.Delete)]
    [EnabledStateObserver("Delete", "Enabled", "EnabledChanged")]
    [Tooltip("Delete", "TooltipPrintPreviewDeleteImage")]
    [IconSet("Delete", "Icons.DeleteImageSmall.png", "Icons.DeleteImageMedium.png", "Icons.DeleteImageLarge.png")]

    [KeyboardAction("PrintPreviewCopy", "Printimageviewer-keyboard/PrintPreviewCopy", "PrintPreviewCopy", KeyStroke = XKeys.Control | XKeys.C)]
    [KeyboardAction("PrintPreviewCut", "Printimageviewer-keyboard/PrintPreviewCut", "PrintPreviewCut", KeyStroke = XKeys.Control | XKeys.X)]
    [KeyboardAction("PrintPreviewPaste", "Printimageviewer-keyboard/PrintPreviewPaste", "PrintPreviewPaste", KeyStroke = XKeys.Control | XKeys.V)]

    [ExtensionOf(typeof(PrintImageViewerToolExtensionPoint))]
    public class PrintPreviewDeleteImageTool : ImageViewerTool
    {
        public override void Initialize()
        {
            var printImageViewer = Context.Viewer as PrintImageViewerComponent;
            if (printImageViewer == null)
            {
                return;
            }
            printImageViewer.EventBroker.CutEvent += CutEvent;
            printImageViewer.EventBroker.CopyEvent += CopyEvent;
            printImageViewer.EventBroker.PasteEvent += PasteEvent;
            printImageViewer.EventBroker.CurrentPageEvent += DeleteCurrentPage;
            printImageViewer.EventBroker.CreateEmptyPage += CreateEmptyPage;

            base.Initialize();
        }

        private List<IPresentationImage> selectPresentationImages = null;

        private void CreateEmptyPage(object sender, EventArgs args)
        {
            if (Context.Viewer.SelectedImageBox == null)
            {
                return;
            }

            PrintViewImageBox imageBox = Context.Viewer.SelectedImageBox as PrintViewImageBox;
            imageBox.TotleImageCount += imageBox.Tiles.Count;
            imageBox.TopLeftPresentationImageIndex = imageBox.TotleImageCount - imageBox.Tiles.Count;
            imageBox.Draw();
            imageBox.SelectDefaultTile();
        }

        public void Delete()
        {
            var imageBox = this.ImageViewer.SelectedImageBox;

            if (this.Context.Viewer == null || imageBox == null || this.Context.Viewer.SelectedPresentationImage == null || imageBox.DisplaySet == null)
            {
                return;
            }
            var memorableCommand = new MemorableUndoableCommand(imageBox) { BeginState = CreateMemento() };
            var printImageViewer = this.Context.Viewer as PrintImageViewerComponent;
            int lastSelectImageIndex = 0;
            if (printImageViewer.SelectPresentationImages.Count > 0)
            {
                var lastImage = printImageViewer.SelectPresentationImages[printImageViewer.SelectPresentationImages.Count - 1];
                if (imageBox.DisplaySet.PresentationImages.Contains(lastImage))
                {
                    lastSelectImageIndex = imageBox.DisplaySet.PresentationImages.IndexOf(lastImage);
                }
            }
            foreach (IPresentationImage selectPresentationImage in printImageViewer.SelectPresentationImages)
            {
                if (imageBox.DisplaySet.PresentationImages.Contains(selectPresentationImage))
                {
                    imageBox.DisplaySet.PresentationImages.Remove(selectPresentationImage);
                    selectPresentationImage.Dispose();
                }
            }
            printImageViewer.SelectPresentationImages.Clear();

            var selectTile = imageBox.SelectedTile as PrintViewTile;
            selectTile.PresentationImage = null;
            int tileimageIsNullCount = 0;
            foreach (PrintViewTile tile in imageBox.Tiles)
            {
                if (tile.PresentationImage == null)
                {
                    tileimageIsNullCount++;
                }

                tile.PresentationImage = null;
                tile.Deselect();
            }

            imageBox.TopLeftPresentationImageIndex = tileimageIsNullCount == imageBox.Tiles.Count
                                                         ? imageBox.TopLeftPresentationImageIndex - imageBox.Tiles.Count
                                                         : imageBox.TopLeftPresentationImageIndex;
            imageBox.Draw();
            if (imageBox.DisplaySet.PresentationImages.Count - 1 >= lastSelectImageIndex)
            {
                var lastImage = imageBox.DisplaySet.PresentationImages[lastSelectImageIndex];
                var presentationImage = lastImage as PresentationImage;
                if (presentationImage != null)
                {
                    if (presentationImage.Tile != null)
                    {
                        presentationImage.Tile.Select();
                    }

                }
            }
            else if (imageBox.DisplaySet.PresentationImages.Count > 0)
            {
                var lastImage = imageBox.DisplaySet.PresentationImages[imageBox.DisplaySet.PresentationImages.Count - 1];
                var presentationImage = lastImage as PresentationImage;
                if (presentationImage != null)
                {
                    if (presentationImage.Tile != null)
                    {
                        presentationImage.Tile.Select();
                    }

                }
            }
            else
            {
                imageBox.SelectDefaultTile();
            }
            memorableCommand.EndState = CreateMemento();
            var historyCommand = new DrawableUndoableCommand(imageBox) { Name = SR.CommandPrintPreviewDeleteImage };
            historyCommand.Enqueue(memorableCommand);
            this.Context.Viewer.CommandHistory.AddCommand(historyCommand);
            PropertyChanged();
        }

        private void DeleteCurrentPage(object sender, EventArgs args)
        {

            if (this.Context.Viewer == null || this.Context.Viewer.SelectedImageBox == null || this.Context.Viewer.SelectedImageBox.DisplaySet == null)
            {
                return;
            }
            PrintViewImageBox imageBox = this.ImageViewer.SelectedImageBox as PrintViewImageBox;
            var memorableCommand = new MemorableUndoableCommand(imageBox) { BeginState = CreateMemento() };
            var printImageViewer = Context.Viewer as PrintImageViewerComponent;
            foreach (PrintViewTile tile in imageBox.Tiles)
            {
                if (tile.PresentationImage == null)
                {
                    int number1 = imageBox.TotleImageCount - imageBox.DisplaySet.PresentationImages.Count;
                    if (number1 > 0)
                    {
                        imageBox.TotleImageCount = imageBox.TotleImageCount - 1;
                    }
                    tile.Deselect();
                    continue;
                }

                if (imageBox.DisplaySet.PresentationImages.Contains(tile.PresentationImage))
                {
                    if (printImageViewer.SelectPresentationImages.Contains(tile.PresentationImage as PresentationImage))
                    {
                        var pi = tile.PresentationImage as PresentationImage;
                        pi.Selected = false;
                        printImageViewer.SelectPresentationImages.Remove(pi);
                    }
                    imageBox.DisplaySet.PresentationImages.Remove(tile.PresentationImage);
                    //tile.PresentationImage.Dispose();
                    tile.PresentationImage = null;
                    tile.Deselect();
                }
            }

            imageBox.TopLeftPresentationImageIndex = imageBox.TopLeftPresentationImageIndex - imageBox.Tiles.Count;
            imageBox.Draw();
            imageBox.SelectDefaultTile();
            memorableCommand.EndState = CreateMemento();
            var historyCommand = new DrawableUndoableCommand(imageBox) { Name = SR.CommandPrintPreviewDeleteImage };
            historyCommand.Enqueue(memorableCommand);
            this.Context.Viewer.CommandHistory.AddCommand(historyCommand);
            PropertyChanged();

        }

        private void CutEvent(object sender, EventArgs args)
        {
            PrintPreviewCut();
        }

        private void CopyEvent(object sender, EventArgs args)
        {
            PrintPreviewCopy();
        }

        private void PasteEvent(object sender, EventArgs args)
        {
            PrintPreviewPaste();
        }

        public void PrintPreviewCopy()
        {
            if (this.ImageViewer.SelectedImageBox == null || this.ImageViewer.SelectedImageBox.DisplaySet == null || this.Context.Viewer.SelectedPresentationImage == null)
            {
                return;
            }
            var imageBox = this.ImageViewer.SelectedImageBox;
            PrintImageViewerComponent component = this.Context.Viewer as PrintImageViewerComponent;
            if (component == null)
            {
                return;
            }

            if (selectPresentationImages == null)
            {
                selectPresentationImages = new List<IPresentationImage>();
            }
            else
            {
                selectPresentationImages.Clear();
            }

            foreach (var selectPresentationImage in component.SelectPresentationImages)
            {
                PresentationImage image = ImageExporter.ClonePresentationImage(selectPresentationImage) as PresentationImage;
                if (image == null)
                {
                    continue;
                }
                image.Selected = false;
                selectPresentationImages.Add(image);
            }

            foreach (PrintViewTile tile in imageBox.Tiles)
            {
                if (tile.PresentationImage == null)
                {
                    tile.Deselect();
                }
            }
        }

        public void PrintPreviewCut()
        {
            if (this.ImageViewer.SelectedImageBox == null || this.ImageViewer.SelectedImageBox.DisplaySet == null || this.Context.Viewer.SelectedPresentationImage == null)
            {
                return;
            }
            var imageBox = this.ImageViewer.SelectedImageBox;

            var memorableCommand = new MemorableUndoableCommand(imageBox) { BeginState = CreateMemento() };
            PrintImageViewerComponent component = this.Context.Viewer as PrintImageViewerComponent;
            if (component == null)
            {
                return;
            }

            if (selectPresentationImages == null)
            {
                selectPresentationImages = new List<IPresentationImage>();
            }
            else
            {
                selectPresentationImages.Clear();
            }

            foreach (var selectPresentationImage in component.SelectPresentationImages)
            {
                var image = selectPresentationImage as PresentationImage;
                if (image == null)
                {
                    continue;
                }
                image.Selected = false;
                selectPresentationImages.Add(image);
                if (imageBox.DisplaySet.PresentationImages.Contains(selectPresentationImage))
                {
                    imageBox.DisplaySet.PresentationImages.Remove(selectPresentationImage);
                }

            }
            component.SelectPresentationImages.Clear();

            int tileimageIsNullCount = 0;
            foreach (PrintViewTile tile in imageBox.Tiles)
            {
                foreach (var selectPresentationImage in selectPresentationImages)
                {
                    if (tile.PresentationImage == selectPresentationImage)
                    {
                        tile.PresentationImage = null;
                    }
                    if (tile.PresentationImage == null)
                    {
                        tileimageIsNullCount++;
                    }
                }

                tile.Deselect();
            }

            imageBox.TopLeftPresentationImageIndex = tileimageIsNullCount == imageBox.Tiles.Count
                                                         ? imageBox.TopLeftPresentationImageIndex - imageBox.Tiles.Count
                                                         : imageBox.TopLeftPresentationImageIndex;
            imageBox.Draw();
            imageBox.SelectDefaultTile();
            memorableCommand.EndState = CreateMemento();
            var historyCommand = new DrawableUndoableCommand(imageBox) { Name = SR.CommandPrintPreviewCut };
            historyCommand.Enqueue(memorableCommand);
            this.Context.Viewer.CommandHistory.AddCommand(historyCommand);
            PropertyChanged();
        }

        public void PrintPreviewPaste()
        {
            var imageBox = this.ImageViewer.SelectedImageBox;

            if (imageBox == null || imageBox.DisplaySet == null || imageBox.SelectedTile == null || selectPresentationImages == null)
            {
                return;
            }

            var memorableCommand = new MemorableUndoableCommand(imageBox) { BeginState = CreateMemento() };
            if (this.Context.Viewer.SelectedPresentationImage == null)
            {
                foreach (var selectPresentationImage in selectPresentationImages)
                {
                    imageBox.DisplaySet.PresentationImages.Add(ImageExporter.ClonePresentationImage(selectPresentationImage));
                }
            }
            else
            {
                int index = imageBox.DisplaySet.PresentationImages.IndexOf(this.Context.Viewer.SelectedPresentationImage);

                for (int i = selectPresentationImages.Count - 1; i >= 0; i--)
                {
                    imageBox.DisplaySet.PresentationImages.Insert(index, ImageExporter.ClonePresentationImage(selectPresentationImages[i]));
                }

            }
            imageBox.TopLeftPresentationImageIndex = imageBox.TopLeftPresentationImageIndex;
            imageBox.Draw();
            imageBox.SelectDefaultTile();
            memorableCommand.EndState = CreateMemento();
            var historyCommand = new DrawableUndoableCommand(imageBox) { Name = SR.CommandPrintPreviewPaste };
            historyCommand.Enqueue(memorableCommand);
            this.Context.Viewer.CommandHistory.AddCommand(historyCommand);
            PropertyChanged();
        }

        public virtual object CreateMemento()
        {
            var imageBox = this.ImageViewer.SelectedImageBox as PrintViewImageBox;

            object displaySetMemento = null;
            if (imageBox.DisplaySet != null)
                displaySetMemento = imageBox.DisplaySet.CreateMemento();

            int IndexOfSelectedTile;
            if (imageBox.SelectedTile == null)
                IndexOfSelectedTile = -1;
            else
                IndexOfSelectedTile = imageBox.Tiles.IndexOf(imageBox.SelectedTile);

            TileCollection tileCollection = new TileCollection();
            tileCollection.AddRange(imageBox.Tiles);

            PrintImageBoxMemento imageBoxMemento =
                new PrintImageBoxMemento(imageBox.DisplaySet.Clone(),
                                    imageBox.DisplaySetLocked,
                                    displaySetMemento,
                                    tileCollection,
                                    imageBox.TopLeftPresentationImageIndex,
                                    imageBox.NormalizedRectangle,
                                    IndexOfSelectedTile,
                                    imageBox.TotleImageCount);

            return imageBoxMemento;
        }

        private void PropertyChanged()
        {
            PrintImageViewerComponent imageViewerComponent = this.ImageViewer as PrintImageViewerComponent;
            imageViewerComponent.PropertyValueChanged();
        }

        protected override void Dispose(bool disposing)
        {
            PrintImageViewerComponent printImageViewer = Context.Viewer as PrintImageViewerComponent;
            if (printImageViewer == null)
            {
                return;
            }
            printImageViewer.EventBroker.CutEvent -= CutEvent;
            printImageViewer.EventBroker.CopyEvent -= CopyEvent;
            printImageViewer.EventBroker.PasteEvent -= PasteEvent;
            printImageViewer.EventBroker.CurrentPageEvent -= DeleteCurrentPage;
            printImageViewer.EventBroker.CreateEmptyPage -= CreateEmptyPage;


            base.Dispose(disposing);
        }
    }
}
