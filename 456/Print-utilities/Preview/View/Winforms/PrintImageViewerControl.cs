
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
using System.ComponentModel;
using System.Windows.Forms;
using Macro.Common;
using Macro.Desktop;
using Macro.Common.Utilities;
using Macro.Desktop.Actions;
using Macro.Desktop.Configuration;
using Macro.Desktop.View.WinForms;
using Macro.Dicom.Iod.Modules;
using System.Linq;

namespace Macro.ImageViewer.Utilities.Print.Dicom.Preview.View.Winforms
{
    public partial class PrintImageViewerControl : UserControl
    {
        private Form _parentForm;
        private PrintImageViewerComponent _component;
        private DelayedEventPublisher _delayedEventPublisher;
        private DicomPrintComponent printComponent = null;
        private ImageDisplayFormat displayFormat = ImageDisplayFormat.Standard_1x1;
        private Dictionary<string, Checkable<DicomPrinter>> dicomPrintList = new Dictionary<string, Checkable<DicomPrinter>>();

        internal PrintImageViewerControl(PrintImageViewerComponent component)
        {
            _component = component;
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            _component.Closing += new EventHandler(OnComponentClosing);
            _component.Drawing += new EventHandler(OnPhysicalWorkspaceDrawing);
            _component.LayoutCompleted += new EventHandler(OnLayoutCompleted);
            _component.ScreenRectangleChanged += new EventHandler(OnScreenRectangleChanged);
            _delayedEventPublisher = new DelayedEventPublisher(OnRecalculateImageBoxes, 50);

            #region ��ӡ������
            printComponent = _component.DicomPrintComponent as DicomPrintComponent;
            if (printComponent != null)
            {
                FilmSize fileSize = printComponent.DicomPrinterConfigurationEditorComponent.FilmSize.ToFilmSize();
                FilmOrientation filmOrientation = printComponent.DicomPrinterConfigurationEditorComponent.FilmOrientation;
                _component.Layout(fileSize, filmOrientation);

                this.Load += Loaded;
            }

            #endregion

            #region �˵��͹�����

            this.ToolbarModel = _component.ToolbarModel;
            this.ContextMenuModel = component.ContextMenuModel;

            #endregion

            FilmCount.DataBindings.Add("Text", _component, "FilmCount", true, DataSourceUpdateMode.OnPropertyChanged);
            ImageCount.DataBindings.Add("Text", _component, "ImageCount", true, DataSourceUpdateMode.OnPropertyChanged);

        }

        #region ������Ϣ��



        private void Loaded(object sender, EventArgs e)
        {
            if (printComponent == null)
            {
                return;
            }

            FilmSize fileSize = printComponent.DicomPrinterConfigurationEditorComponent.FilmSize.ToFilmSize();
            FilmOrientation filmOrientation = printComponent.DicomPrinterConfigurationEditorComponent.FilmOrientation;

            if (_component.RootImageBox != null)
            {
                _component.ImageBoxSizeChanged(fileSize, filmOrientation);

            }

            if (filmOrientation == FilmOrientation.Landscape)
            {
                radioHengXiang.Checked = true;
            }
            else if (filmOrientation == FilmOrientation.Portrait)
            {
                radioZongXiang.Checked = true;
            }


            this.NumberOfCopies.DataBindings.Clear();
            this.NumberOfCopies.DataBindings.Add("Value", printComponent.DicomPrinterConfigurationEditorComponent,
                                                 "NumberOfCopies", true,
                                                 DataSourceUpdateMode.OnPropertyChanged);

            this.FilmSizeComBox.DataBindings.Clear();
            this.FilmSizeComBox.DataSource = printComponent.DicomPrinterConfigurationEditorComponent.FilmSizeChoices;
            this.FilmSizeComBox.DataBindings.Add("SelectedItem",
                                                 printComponent.DicomPrinterConfigurationEditorComponent, "FilmSize",
                                                 true,
                                                 DataSourceUpdateMode.Never);

            InitDicomPrintTable();

            ISelection selectedItem = printComponent.SelectedItem;

            if (selectedItem == null || selectedItem.Item == null)
            {
                return;
            }

            var print = (Checkable<DicomPrinter>)selectedItem.Item;
            foreach (object item in DicomPrintTable.Items)
            {
                var name = (string)item;
                if (name == null)
                {
                    continue;
                }

                if (name == print.Item.Name)
                {
                    DicomPrintTable.SelectedItem = item;
                    break;
                }
            }
            printComponent.DicomPrinterConfigurationEditorComponent.ImageDisplayFormat = new PrinterImageDisplayFormat() { Value = displayFormat.DicomString }; ;

        }

        private void InitDicomPrintTable()
        {
            dicomPrintList.Clear();
            DicomPrintTable.DataSource = null;
            foreach (Checkable<DicomPrinter> item in printComponent.PrintersTable.Items)
            {
                if (!dicomPrintList.ContainsKey(item.Item.Name))
                {
                    dicomPrintList.Add(item.Item.Name, item);
                }
            }

            DicomPrintTable.DataSource = dicomPrintList.Keys.ToList();

        }

        #endregion

        internal void Draw()
        {
            foreach (PrintImageBoxControl control in this._layoutImages.Controls)
                control.Draw();

            Invalidate();
        }

        #region Protected members

        protected override void OnParentChanged(EventArgs e)
        {
            SetParentForm(base.ParentForm);

            base.OnParentChanged(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            AddImageBoxControls(_component.RootImageBox);
            base.OnLoad(e);

            Draw();
            this.Invalidate();
        }

        #endregion

        #region Private members

        private void OnParentMoved(object sender, EventArgs e)
        {
            UpdateScreenRectangle();
        }

        private void OnScreenRectangleChanged(object sender, EventArgs e)
        {
            _delayedEventPublisher.Publish(this, EventArgs.Empty);
        }

        private void OnRecalculateImageBoxes(object sender, EventArgs e)
        {
            this.SuspendLayout();

            foreach (PrintImageBoxControl control in this._layoutImages.Controls)
                control.ParentRectangle = this._layoutImages.ClientRectangle;

            this.ResumeLayout(false);

            Invalidate();
        }

        private void OnPhysicalWorkspaceDrawing(object sender, EventArgs e)
        {
            Draw();
        }

        void OnComponentClosing(object sender, EventArgs e)
        {
            while (this._layoutImages.Controls.Count > 0)
                this._layoutImages.Controls[0].Dispose();
        }

        private void OnLayoutCompleted(object sender, EventArgs e)
        {
            List<Control> oldControlList = new List<Control>();

            foreach (Control control in this._layoutImages.Controls)
                oldControlList.Add(control);

            // We add all the new tile controls to the image box control first,
            // then we remove the old ones. Removing them first then adding them
            // results in flickering, which we don't want.
            AddImageBoxControls(_component.RootImageBox);

            foreach (Control control in oldControlList)
            {
                this._layoutImages.Controls.Remove(control);
                control.Dispose();
            }
        }

        private void UpdateScreenRectangle()
        {
            _component.WorkRectangle = this.RectangleToScreen(this._layoutImages.ClientRectangle);
        }

        private void AddImageBoxControls(PrintViewImageBox rootImageBox)
        {
            if (rootImageBox == null)
            {
                return;
            }

            AddImageBoxControl(rootImageBox);
            PrintViewImageBox printViewImageBox = rootImageBox;
            if (printViewImageBox.ChildImageBox.Count > 0)
            {
                foreach (PrintViewImageBox imageBox in printViewImageBox.ChildImageBox)
                {
                    AddImageBoxControls(imageBox);
                }
            }
        }

        private void AddImageBoxControl(PrintViewImageBox imageBox)
        {
            PrintImageBoxView view = ViewFactory.CreateAssociatedView(typeof(PrintViewImageBox)) as PrintImageBoxView;

            view.ImageBox = imageBox;
            view.ParentRectangle = this._layoutImages.ClientRectangle;

            PrintImageBoxControl control = view.GuiElement as PrintImageBoxControl;
            control.Dock = DockStyle.None;
            control.SuspendLayout();
            this._layoutImages.Controls.Add(control);
            control.ResumeLayout(false);

        }

        private void SetParentForm(Form value)
        {
            if (_parentForm != value)
            {
                if (_parentForm != null)
                    _parentForm.Move -= OnParentMoved;

                _parentForm = value;

                if (_parentForm != null)
                    _parentForm.Move += OnParentMoved;
            }
        }

        #endregion

        #region �����¼�
        private void SubGrid(object sender, EventArgs rArgs)
        {

            Button button = sender as Button;
            if (button == null)
            {
                return;
            }
            string imageDisplaySet = (string)button.Tag;
            if (imageDisplaySet == null || imageDisplaySet == "")
            {
                return;
            }

            if (imageDisplaySet == "CUSTOM")
            {
                int row = int.Parse(this.RowNumeric.Text);
                int col = int.Parse(this.ColumnNumeric.Text);
                if (row == 0 || col == 0)
                {
                    return;
                }
                displayFormat = LayoutFactory.TileFactory(row, col);
            }
            else
            {
                displayFormat = ImageDisplayFormat.FromDicomString(imageDisplaySet);
            }

            if (this.fenge.Checked)
            {
                _component.SetTileGrid(displayFormat);
                if (printComponent != null)
                {
                    printComponent.DicomPrinterConfigurationEditorComponent.ImageDisplayFormat = new PrinterImageDisplayFormat() { Value = displayFormat.DicomString }; ;

                }
            }
            else
            {
                _component.SubGrid(displayFormat);
            }

        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            _component.ClearAllImages();
        }

        private void PrintButton_Click(object sender, EventArgs e)
        {
            if (printComponent != null)
            {
                printComponent.DicomPrinterConfigurationEditorComponent.ImageDisplayFormat =
                    new PrinterImageDisplayFormat() { Value = displayFormat.DicomString };
                _component.Accept(true, this.PrintedDeleteImage.Checked);
            }
            else
            {
                _component.DesktopWindow.ShowMessageBox("��ӡ���ΪNULL������ϵ����Ա", MessageBoxActions.Ok);
            }
        }
        #endregion

        #region ������
        private ActionModelNode _toolbarModel;
        private ActionModelNode _contextMenuModel;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private ActionModelNode ToolbarModel
        {
            set
            {
                _toolbarModel = value;

                // Turn on the toolbar only if a toolbar model exists
                _toolStrip.Visible = true;

                InitializeToolStrip();
            }
        }

        private void InitializeToolStrip()
        {
            ToolStripBuilder.Clear(_toolStrip.Items);

            if (_toolbarModel != null)
            {
                ToolStripBuilder.BuildToolbar(_toolStrip.Items, _toolbarModel.ChildNodes);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private ActionModelNode ContextMenuModel
        {
            set
            {
                _contextMenuModel = value;
                ToolStripBuilder.Clear(_contextMenu.Items);
                if (_contextMenuModel != null)
                {
                    ToolStripBuilder.BuildMenu(_contextMenu.Items, _contextMenuModel.ChildNodes);
                }
            }
        }

        #endregion

        private void _layoutImages_SizeChanged(object sender, EventArgs e)
        {
            UpdateScreenRectangle();

        }

        private void ImageBoxSizeChanged()
        {
            FilmSize fileSize =
                 printComponent.DicomPrinterConfigurationEditorComponent.FilmSize.ToFilmSize();
            FilmOrientation filmOrientation =
                printComponent.DicomPrinterConfigurationEditorComponent.FilmOrientation;

            _component.ImageBoxSizeChanged(fileSize, filmOrientation);
        }

        private void FilmSizeComBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            printComponent.DicomPrinterConfigurationEditorComponent.FilmSize =
                (PrinterFilmSize)this.FilmSizeComBox.SelectedItem;
            ImageBoxSizeChanged();
        }

        private void radioHengXiang_CheckedChanged(object sender, EventArgs e)
        {
            printComponent.DicomPrinterConfigurationEditorComponent.FilmOrientation = FilmOrientation.Landscape;
            ImageBoxSizeChanged();
        }

        private void radioZongXiang_CheckedChanged(object sender, EventArgs e)
        {
            printComponent.DicomPrinterConfigurationEditorComponent.FilmOrientation = FilmOrientation.Portrait;
            ImageBoxSizeChanged();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            EventsHelper.Fire(_component.EventBroker.DelegatePasteEvent, this, null);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            EventsHelper.Fire(_component.EventBroker.DelegateSelectAll, this, null);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            EventsHelper.Fire(_component.EventBroker.DelegateSelectRever, this, null);

        }

        private void button7_Click(object sender, EventArgs e)
        {
            EventsHelper.Fire(_component.EventBroker.DelegateCutEvent, this, null);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            EventsHelper.Fire(_component.EventBroker.DelegateCopyEvent, this, null);

        }

        private void button9_Click(object sender, EventArgs e)
        {
            EventsHelper.Fire(_component.EventBroker.DelegateDeleteCurrentPage, this, null);

        }

        private void button10_Click(object sender, EventArgs e)
        {
            EventsHelper.Fire(_component.EventBroker.DelegateCreateEmptyPage, this, null);

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (printComponent != null)
            {
                printComponent.DicomPrinterConfigurationEditorComponent.ImageDisplayFormat =
                    new PrinterImageDisplayFormat() { Value = displayFormat.DicomString };
                _component.Accept(false, this.PrintedDeleteImage.Checked);
            }
            else
            {
                _component.DesktopWindow.ShowMessageBox("��ӡ���ΪNULL������ϵ����Ա", MessageBoxActions.Ok);
            }
        }

        private void DicomPrintTable_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (DicomPrintTable.SelectedItem != null)
            {
                string key = (string)DicomPrintTable.SelectedItem;
                if (dicomPrintList.ContainsKey(key))
                {
                    printComponent.SelectedItem = new Selection(dicomPrintList[key]);
                }

                FilmSize fileSize = printComponent.DicomPrinterConfigurationEditorComponent.FilmSize.ToFilmSize();
                FilmOrientation filmOrientation = printComponent.DicomPrinterConfigurationEditorComponent.FilmOrientation;

                if (_component.RootImageBox != null)
                {
                    _component.ImageBoxSizeChanged(fileSize, filmOrientation);

                }

                if (filmOrientation == FilmOrientation.Landscape)
                {
                    radioHengXiang.Checked = true;
                }
                else if (filmOrientation == FilmOrientation.Portrait)
                {
                    radioZongXiang.Checked = true;
                }


                this.NumberOfCopies.DataBindings.Clear();
                this.NumberOfCopies.DataBindings.Add("Value", printComponent.DicomPrinterConfigurationEditorComponent,
                                                     "NumberOfCopies", true,
                                                     DataSourceUpdateMode.OnPropertyChanged);

                this.FilmSizeComBox.DataBindings.Clear();
                this.FilmSizeComBox.DataSource = printComponent.DicomPrinterConfigurationEditorComponent.FilmSizeChoices;
                this.FilmSizeComBox.DataBindings.Add("SelectedItem",
                                                     printComponent.DicomPrinterConfigurationEditorComponent, "FilmSize",
                                                     true,
                                                     DataSourceUpdateMode.Never);
            }

        }

        private void setPrintButton_Click(object sender, EventArgs e)
        {
            if (printComponent == null)
            {
                return;
            }
            ApplicationComponentExitCode exitCode = ConfigurationDialog.Show(printComponent.DesktopWindow, null);
            if (exitCode == ApplicationComponentExitCode.Accepted)
            {
                printComponent.InitDicomPrinterConfig();
                Loaded(this, null);
            }

        }

        private void button36_Click(object sender, EventArgs e)
        {
            EventsHelper.Fire(_component.EventBroker.DelegateFirstPage, this, null);
        }

        private void button38_Click(object sender, EventArgs e)
        {
            EventsHelper.Fire(_component.EventBroker.DelegateUpPage, this, null);
        }

        private void button41_Click(object sender, EventArgs e)
        {
            EventsHelper.Fire(_component.EventBroker.DelegateDownPage, this, null);
        }

        private void button40_Click(object sender, EventArgs e)
        {
            EventsHelper.Fire(_component.EventBroker.DelegateLastPage, this, null);
        }

        private void SaveGridToConfig_Click(object sender, EventArgs e)
        {
            var savegridComponent = new SaveCustumGridComponent(_component);
            ApplicationComponent.LaunchAsDialog(_component.DesktopWindow, savegridComponent, "�����Ű�");
        }

        private void LoadGridFromConfig_Click(object sender, EventArgs e)
        {
            var layoutsSelectCompont = new LayoutSelectComponent(_component);
            ApplicationComponent.LaunchAsDialog(_component.DesktopWindow, layoutsSelectCompont, "ѡ���Ű�");
        }

        private void Mergerbutton42_Click(object sender, EventArgs e)
        {
            _component.MergerGrid();
        }






    }
}
