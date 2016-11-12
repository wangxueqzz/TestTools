
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
using System.Linq;
using Macro.Common;
using Macro.Desktop;
using Macro.Desktop.Actions;
using Macro.ImageViewer.Automation;
using Macro.ImageViewer.BaseTools;
using Macro.ImageViewer.InputManagement;
using Macro.ImageViewer.StudyManagement;

namespace Macro.ImageViewer.Utilities.Print.Dicom.Preview
{
    [MouseWheelHandler(ModifierFlags.None)]
    [MouseToolButton(XMouseButtons.Left, true)]

    #region 打印
    [ButtonAction("activate", "global-toolbars/ToolbarStandard/ToolbarStack", "Select", KeyStroke = XKeys.S)]
    [CheckedStateObserver("activate", "Active", "ActivationChanged")]
    [EnabledStateObserver("activate", "Enabled", "EnabledChanged")]
    [TooltipValueObserver("activate", "Tooltip", "TooltipChanged")]
    [MouseButtonIconSet("activate", "Icons.StackToolSmall.png", "Icons.StackToolMedium.png", "Icons.StackToolLarge.png")]
    //[MouseButtonIconSet("activate", "Icons.SelectSmall.png", "Icons.SelectMedium.png", "Icons.SelectLarge.png")]


    [KeyboardAction("stackup", "Printimageviewer-keyboard/ToolsStandardStack/StackUp", "StackUp", KeyStroke = XKeys.PageUp)]
    [KeyboardAction("stackdown", "Printimageviewer-keyboard/ToolsStandardStack/StackDown", "StackDown", KeyStroke = XKeys.PageDown)]
    [KeyboardAction("jumptobeginning", "Printimageviewer-keyboard/ToolsStandardStack/JumpToBeginning", "JumpToBeginning", KeyStroke = XKeys.Home)]
    [KeyboardAction("jumptoend", "Printimageviewer-keyboard/ToolsStandardStack/JumpToEnd", "JumpToEnd", KeyStroke = XKeys.End)]
    #endregion


    [ExtensionOf(typeof(PrintImageViewerToolExtensionPoint))]
    public partial class PrintPreviewStackTool : MouseImageViewerTool
    {
        private MemorableUndoableCommand _memorableCommand;
        private int _initialLeftTopTileIndex;
        private IImageBox _currentImageBox;

        public PrintPreviewStackTool()
            : base(SR.TooltipStack)
        {
            CursorToken = new CursorToken("Icons.StackToolSmall.png", GetType().Assembly);
        }

        public override void Initialize()
        {
            PrintImageViewerComponent printImageViewer = Context.Viewer as PrintImageViewerComponent;
            if (printImageViewer == null)
            {
                return;
            }
            printImageViewer.EventBroker.FirstPageEvent += FirstPage;
            printImageViewer.EventBroker.LastPageEvent += LastPage;
            printImageViewer.EventBroker.UpPageEvent += UpPage;
            printImageViewer.EventBroker.DownPageEvent += DownPage;
            this.Context.Viewer.EventBroker.SelectToolEvent += SelectTool;

            base.Initialize();
        }

        private void SelectTool(object sender, EventArgs args)
        {
            this.Select();
        }

        private void FirstPage(object sender, EventArgs e)
        {
            JumpToBeginning();

        }

        private void LastPage(object sender, EventArgs e)
        {
            JumpToEnd();
        }

        private void UpPage(object sender, EventArgs e)
        {
            StackUp();
        }

        private void DownPage(object sender, EventArgs e)
        {
            StackDown();
        }

        public override event EventHandler TooltipChanged
        {
            add { base.TooltipChanged += value; }
            remove { base.TooltipChanged -= value; }
        }

        private void CaptureBeginState(IImageBox imageBox)
        {
            _memorableCommand = new MemorableUndoableCommand(imageBox) { BeginState = imageBox.CreateMemento() };
            // Capture state before stack
            _currentImageBox = imageBox;

            _initialLeftTopTileIndex = imageBox.TopLeftPresentationImageIndex;
        }

        private bool CaptureEndState()
        {
            if (_memorableCommand == null || _currentImageBox == null)
            {
                _currentImageBox = null;
                return false;
            }

            bool commandAdded = false;

            // If nothing's changed then just return
            if (_initialLeftTopTileIndex != _currentImageBox.TopLeftPresentationImageIndex)
            {
                // Capture state after stack
                _memorableCommand.EndState = _currentImageBox.CreateMemento();
                if (!_memorableCommand.EndState.Equals(_memorableCommand.BeginState))
                {
                    var historyCommand = new DrawableUndoableCommand(_currentImageBox) { Name = SR.CommandStack };
                    historyCommand.Enqueue(_memorableCommand);
                    Context.Viewer.CommandHistory.AddCommand(historyCommand);
                    commandAdded = true;
                }
            }

            _memorableCommand = null;
            _currentImageBox = null;

            return commandAdded;
        }

        private void JumpToBeginning()
        {
            if (Context.Viewer.SelectedTile == null)
                return;
            
            IImageBox imageBox = Context.Viewer.SelectedTile.ParentImageBox;

            CaptureBeginState(imageBox);
            imageBox.TopLeftPresentationImageIndex = 0;
            if (CaptureEndState())
                imageBox.Draw();
        }

        private void JumpToEnd()
        {
            if (Context.Viewer.SelectedTile == null)
                return;

            PrintViewImageBox imageBox = Context.Viewer.SelectedTile.ParentImageBox as PrintViewImageBox;

            if (imageBox.DisplaySet == null)
                return;

            CaptureBeginState(imageBox);

            imageBox.TopLeftPresentationImageIndex = imageBox.TotleImageCount - imageBox.Tiles.Count;
            if (CaptureEndState())
                imageBox.Draw();
        }

        private void StackUp()
        {
            if (Context.Viewer.SelectedTile == null)
                return;

            IImageBox imageBox = Context.Viewer.SelectedTile.ParentImageBox;
            CaptureBeginState(imageBox);
            AdvanceImage(-imageBox.Tiles.Count, imageBox);
            CaptureEndState();
        }

        private void StackDown()
        {
            if (Context.Viewer.SelectedTile == null)
                return;

            IImageBox imageBox = Context.Viewer.SelectedTile.ParentImageBox;
            CaptureBeginState(imageBox);
            AdvanceImage(+imageBox.Tiles.Count, imageBox);
            CaptureEndState();
        }

        private static void AdvanceImage(int increment, IImageBox selectedImageBox)
        {
            int prevTopLeftPresentationImageIndex = selectedImageBox.TopLeftPresentationImageIndex;
            selectedImageBox.TopLeftPresentationImageIndex += increment;

            if (selectedImageBox.TopLeftPresentationImageIndex != prevTopLeftPresentationImageIndex)
                selectedImageBox.Draw();
        }

        public override bool Start(IMouseInformation mouseInformation)
        {
            if (Context.Viewer.SelectedTile == null)
                return false;
          
            base.Start(mouseInformation);

            if (mouseInformation.Tile == null)
                return false;

            CaptureBeginState(mouseInformation.Tile.ParentImageBox);

            return true;
        }

        public override bool Track(IMouseInformation mouseInformation)
        {
            if (Context.Viewer.SelectedTile == null)
                return false;
           
            base.Track(mouseInformation);

            if (mouseInformation.Tile == null)
                return false;

            if (DeltaY == 0)
                return true;

            int increment;

            if (DeltaY > 0)
                increment = 1;
            else
                increment = -1;

            AdvanceImage(increment * mouseInformation.Tile.ParentImageBox.Tiles.Count, mouseInformation.Tile.ParentImageBox);

            return true;
        }

        public override bool Stop(IMouseInformation mouseInformation)
        {
            if (Context.Viewer.SelectedTile == null)
                return false;

            base.Stop(mouseInformation);

            CaptureEndState();

            return false;
        }

        public override void Cancel()
        {
            if (Context.Viewer.SelectedTile == null)
                return;
           
            CaptureEndState();
        }

        public override void StartWheel()
        {
            if (Context.Viewer.SelectedTile == null)
                return;

           
            IImageBox imageBox = Context.Viewer.SelectedTile.ParentImageBox;
            if (imageBox == null)
                return;

            CaptureBeginState(imageBox);
        }

        protected override void WheelBack()
        {
           
            if (Context.Viewer.SelectedTile == null)
                return;

            AdvanceImage(Context.Viewer.SelectedTile.ParentImageBox.Tiles.Count, Context.Viewer.SelectedTile.ParentImageBox);
        }

        protected override void WheelForward()
        {
            if (Context.Viewer.SelectedTile == null)
                return;

            AdvanceImage(-Context.Viewer.SelectedTile.ParentImageBox.Tiles.Count, Context.Viewer.SelectedTile.ParentImageBox);
        }

        public override void StopWheel()
        {
            if (Context.Viewer.SelectedTile == null)
                return;
            
            CaptureEndState();
        }

        protected override void Dispose(bool disposing)
        {
            PrintImageViewerComponent printImageViewer = Context.Viewer as PrintImageViewerComponent;
            if (printImageViewer == null)
            {
                return;
            }
            printImageViewer.EventBroker.FirstPageEvent -= FirstPage;
            printImageViewer.EventBroker.LastPageEvent -= LastPage;
            printImageViewer.EventBroker.UpPageEvent -= UpPage;
            printImageViewer.EventBroker.DownPageEvent -= DownPage;
            this.Context.Viewer.EventBroker.SelectToolEvent -= SelectTool;


            base.Dispose(disposing);
        }
    }

    #region Oto
    partial class PrintPreviewStackTool : IStack
    {
        #region IViewerStackService Members

        void IStack.StackBy(int delta)
        {
            if (Context.Viewer.SelectedTile == null)
                throw new InvalidOperationException("No tile selected.");

            if (this.SelectedPresentationImage == null)
                throw new InvalidOperationException("No image selected.");

            IImageBox imageBox = Context.Viewer.SelectedTile.ParentImageBox;
            CaptureBeginState(imageBox);
            AdvanceImage(delta, imageBox);
            //No draw - AdvanceImage has already done it.
            CaptureEndState();
        }

        void IStack.StackTo(int instanceNumber, int? frameNumber)
        {
            if (Context.Viewer.SelectedTile == null)
                throw new InvalidOperationException("No tile selected.");

            if (this.SelectedPresentationImage == null)
                throw new InvalidOperationException("No image selected.");

            var displaySet = Context.Viewer.SelectedPresentationImage.ParentDisplaySet;

            //First will throw if no such image exists.
            var image = (IPresentationImage)displaySet.PresentationImages.OfType<IImageSopProvider>().First(
                i => i.ImageSop.InstanceNumber == instanceNumber
                        && (!frameNumber.HasValue || (i.ImageSop.NumberOfFrames > 1 && frameNumber.Value == i.Frame.FrameNumber)));

            IImageBox imageBox = Context.Viewer.SelectedTile.ParentImageBox;
            CaptureBeginState(imageBox);
            imageBox.TopLeftPresentationImage = image;
            if (!CaptureEndState())
                return; /// TODO (CR Dec 2011): Should we still select the top-left??

            imageBox.Draw();
            imageBox.Tiles[0].Select();
        }

        #endregion
    }
    #endregion
}
