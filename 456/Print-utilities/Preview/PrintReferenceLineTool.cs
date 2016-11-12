
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
using System.Drawing;
using Macro.Common;
using Macro.Common.Utilities;
using Macro.Desktop;
using Macro.Desktop.Actions;
using Macro.ImageViewer.BaseTools;
using Macro.ImageViewer.Graphics;
using Macro.ImageViewer.Mathematics;

namespace Macro.ImageViewer.Utilities.Print.Dicom.Preview
{
    [MenuAction("PrintReferenceLineTool", "Printimageviewer-contextmenu/MenuReferenceLines", "Toggle", InitiallyAvailable = false)]
    [DropDownButtonAction("PrintReferenceLineTool", "global-toolbars/ToolbarStandard/ToolbarReferenceLines", "Toggle", "ReferenceLineDropDownMenuModel", Flags = ClickActionFlags.CheckAction)]
    [EnabledStateObserver("PrintReferenceLineTool", "Enabled", "EnabledChanged")]
    [Tooltip("PrintReferenceLineTool", "TooltipReferenceLines")]
    [IconSet("PrintReferenceLineTool", "Icons.CurrentReferenceLineToolSmall.png", "Icons.CurrentReferenceLineToolMedium.png", "Icons.CurrentReferenceLineToolLarge.png")]

    [ExtensionOf(typeof(PrintImageViewerToolExtensionPoint))]
    public class PrintReferenceLineTool : ImageViewerTool
    {
        #region ReferenceLine class

        private class ReferenceLine
        {
            public readonly PointF StartPoint;
            public readonly PointF EndPoint;
            public readonly string Label;

            public ReferenceLine(PointF startPoint, PointF endPoint, string label)
            {
                this.StartPoint = startPoint;
                this.EndPoint = endPoint;
                this.Label = label;
            }
        }

        #endregion

        private ActionModelRoot _dropDownMenuModel;
        private IResourceResolver _resolver = new ApplicationThemeResourceResolver(typeof(PrintReferenceLineTool).Assembly);
        private int jgCount = 0;

        public void Toggle()
        {

            if (this.SelectedOverlayGraphicsProvider == null)
                return;

            if (this.SelectedPresentationImage == null)
            {
                return;
            }

            try
            {
                var selectPresentationImage = this.SelectedPresentationImage;
                IPrintViewImageViewer printViewImageViewer = this.ImageViewer as IPrintViewImageViewer;
                if (printViewImageViewer.ReferenceLines.ContainsKey(selectPresentationImage))
                {
                    foreach (var referenceLine in printViewImageViewer.ReferenceLines[selectPresentationImage])
                    {
                        this.SelectedOverlayGraphicsProvider.OverlayGraphics.Remove(referenceLine);
                    }
                }
                else
                {
                    printViewImageViewer.ReferenceLines.Add(selectPresentationImage, new List<IGraphic>());
                }

                DicomImagePlane targetImagePlane = DicomImagePlane.FromImage(selectPresentationImage);
                if (targetImagePlane == null) return;
                IDisplaySet displaySet = this.Context.Viewer.SelectedImageBox.DisplaySet;
                List<ReferenceLine> lines = new List<ReferenceLine>();
                int index = 0;
                foreach (var referenceImage in displaySet.PresentationImages)
                {
                    index++;
                    if (referenceImage == selectPresentationImage) continue;
                    if (jgCount != 0 && index % jgCount != 0)
                    {
                        continue;
                    }
                    DicomImagePlane referenceImagePlane = DicomImagePlane.FromImage(referenceImage);
                    if (referenceImagePlane == null) continue;
                    if (referenceImagePlane.StudyInstanceUid == targetImagePlane.StudyInstanceUid)
                    {
                        ReferenceLine line = GetReferenceLine(referenceImagePlane, targetImagePlane);
                        // CompositeGraphic lineGraphic = new CompositeGraphic();
                        LinePrimitive linePrimitive = new LinePrimitive();
                        linePrimitive.LineStyle = LineStyle.Dash;
                        linePrimitive.Point1 = line.StartPoint;
                        linePrimitive.Point2 = line.EndPoint;
                        InvariantTextPrimitive text = new InvariantTextPrimitive(line.Label);
                        text.Location = new PointF(line.EndPoint.X + 10, line.EndPoint.Y);

                        //lineGraphic.Graphics.Add(linePrimitive);
                        //lineGraphic.Graphics.Add(text);
                        bool isHaveSameLine = false;
                        foreach (var referenceLine in lines)
                        {
                            if (line.StartPoint == referenceLine.StartPoint && line.EndPoint == referenceLine.EndPoint)
                            {
                                isHaveSameLine = true;
                                break;
                            }
                        }
                        if (!isHaveSameLine)
                        {
                            lines.Add(line);
                            this.SelectedOverlayGraphicsProvider.OverlayGraphics.Add(linePrimitive);
                            this.SelectedOverlayGraphicsProvider.OverlayGraphics.Add(text);
                            printViewImageViewer.ReferenceLines[selectPresentationImage].Add(linePrimitive);
                            printViewImageViewer.ReferenceLines[selectPresentationImage].Add(text);
                        }
                    }
                }
                selectPresentationImage.Tile.Draw();
            }
            catch (Exception)
            {
                IPrintViewImageViewer printViewImageViewer = this.ImageViewer as IPrintViewImageViewer;
                if (printViewImageViewer.ReferenceLines.ContainsKey(this.SelectedPresentationImage))
                {
                    printViewImageViewer.ReferenceLines.Remove(this.SelectedPresentationImage);
                }
            }
        }

        public ActionModelNode ReferenceLineDropDownMenuModel
        {
            get
            {
                if (_dropDownMenuModel == null)
                {
                    _dropDownMenuModel = new ActionModelRoot();

                    ClickAction action = new ClickAction("JG1Line",
                        new ActionPath("reference-line-dropdown/jiange1", _resolver),
                        ClickActionFlags.None, _resolver);
                    action.Label = "���1";
                    action.SetClickHandler(delegate { JG1Line(); });

                    _dropDownMenuModel.InsertAction(action);

                    action = new ClickAction("JG2Line",
                     new ActionPath("reference-line-dropdown/jiange2", _resolver),
                     ClickActionFlags.None, _resolver);
                    action.Label = "���2";
                    action.SetClickHandler(delegate { JG2Line(); });

                    _dropDownMenuModel.InsertAction(action);

                    action = new ClickAction("JG3Line",
                     new ActionPath("reference-line-dropdown/jiange3", _resolver),
                     ClickActionFlags.None, _resolver);
                    action.Label = "���3";
                    action.SetClickHandler(delegate { JG3Line(); });

                    _dropDownMenuModel.InsertAction(action);

                    action = new ClickAction("JG4Line",
                     new ActionPath("reference-line-dropdown/jiange4", _resolver),
                     ClickActionFlags.None, _resolver);
                    action.Label = "���4";
                    action.SetClickHandler(delegate { JG4Line(); });

                    _dropDownMenuModel.InsertAction(action);
                }

                return _dropDownMenuModel;
            }
        }

        private void JG1Line()
        {
            jgCount = 1;
            Toggle();
        }

        private void JG2Line()
        {
            jgCount = 2;
            Toggle();
        }

        private void JG3Line()
        {
            jgCount = 3;
            Toggle();
        }

        private void JG4Line()
        {
            jgCount = 4;
            Toggle();
        }

        #region Private Methods

        private static ReferenceLine GetReferenceLine(DicomImagePlane referenceImagePlane, DicomImagePlane targetImagePlane)
        {

            Vector3D intersectionPatient1, intersectionPatient2;
            if (!referenceImagePlane.GetIntersectionPoints(targetImagePlane, out intersectionPatient1, out intersectionPatient2))
                return null;

            Vector3D intersectionImagePlane1 = targetImagePlane.ConvertToImagePlane(intersectionPatient1);
            Vector3D intersectionImagePlane2 = targetImagePlane.ConvertToImagePlane(intersectionPatient2);

            //The coordinates need to be converted to pixel coordinates because right now they are in mm.
            PointF intersectionImage1 = targetImagePlane.ConvertToImage(new PointF(intersectionImagePlane1.X, intersectionImagePlane1.Y));
            PointF intersectionImage2 = targetImagePlane.ConvertToImage(new PointF(intersectionImagePlane2.X, intersectionImagePlane2.Y));
            string label = referenceImagePlane.InstanceNumber.ToString();

            return new ReferenceLine(intersectionImage1, intersectionImage2, label);
        }


        #endregion
    }
}
