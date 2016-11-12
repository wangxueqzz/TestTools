
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

#region License

// Copyright (c) 2006-2008, Macro Inc.
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without modification, 
// are permitted provided that the following conditions are met:
//
//    * Redistributions of source code must retain the above copyright notice, 
//      this list of conditions and the following disclaimer.
//    * Redistributions in binary form must reproduce the above copyright notice, 
//      this list of conditions and the following disclaimer in the documentation 
//      and/or other materials provided with the distribution.
//    * Neither the name of Macro Inc. nor the names of its contributors 
//      may be used to endorse or promote products derived from this software without 
//      specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" 
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, 
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR 
// PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR 
// CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, 
// OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE 
// GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) 
// HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, 
// STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN 
// ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY 
// OF SUCH DAMAGE.

#endregion

using System;

namespace Macro.ImageViewer.Utilities.Print.Dicom.Preview.View.Winforms
{
	partial class PrintImageViewerControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_parentForm != null)
				{
					_parentForm.Move -= new EventHandler(OnParentMoved);
					_parentForm = null;
				}
				
				if (components != null)
					components.Dispose();

				if (_component != null)
				{
					_component.Closing -= new EventHandler(OnComponentClosing);
					_component = null;
				}

				if (_delayedEventPublisher != null)
				{
					_delayedEventPublisher.Dispose();
					_delayedEventPublisher = null;
				}

                if (_component != null)
				{
                    _component.Drawing -= new EventHandler(OnPhysicalWorkspaceDrawing);
                    _component.LayoutCompleted -= new EventHandler(OnLayoutCompleted);
                    _component.ScreenRectangleChanged -= new EventHandler(OnScreenRectangleChanged);

                    _component = null;
				}
			}

			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupboximage = new System.Windows.Forms.GroupBox();
            this._layoutImages = new System.Windows.Forms.Panel();
            this._toolStrip = new System.Windows.Forms.ToolStrip();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Printlable = new System.Windows.Forms.Label();
            this.DicomPrintTable = new System.Windows.Forms.ComboBox();
            this.setPrintButton = new System.Windows.Forms.Button();
            this.radioZongXiang = new System.Windows.Forms.RadioButton();
            this.radioHengXiang = new System.Windows.Forms.RadioButton();
            this.NumberOfCopies = new Macro.Desktop.View.WinForms.NonEmptyNumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.FilmSizeComBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.PrintedDeleteImage = new System.Windows.Forms.CheckBox();
            this.button8 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.PrintButton = new System.Windows.Forms.Button();
            this.ImageCount = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.FilmCount = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button40 = new System.Windows.Forms.Button();
            this.button41 = new System.Windows.Forms.Button();
            this.button38 = new System.Windows.Forms.Button();
            this.button36 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.button30 = new System.Windows.Forms.Button();
            this.button20 = new System.Windows.Forms.Button();
            this.button39 = new System.Windows.Forms.Button();
            this.button29 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.button28 = new System.Windows.Forms.Button();
            this.button19 = new System.Windows.Forms.Button();
            this.button37 = new System.Windows.Forms.Button();
            this.button27 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button26 = new System.Windows.Forms.Button();
            this.button18 = new System.Windows.Forms.Button();
            this.button35 = new System.Windows.Forms.Button();
            this.button25 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button24 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.button33 = new System.Windows.Forms.Button();
            this.button23 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button22 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.button31 = new System.Windows.Forms.Button();
            this.button21 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button34 = new System.Windows.Forms.Button();
            this.zifenge = new System.Windows.Forms.RadioButton();
            this.fenge = new System.Windows.Forms.RadioButton();
            this.SaveGridToConfig = new System.Windows.Forms.Button();
            this.LoadGridFromConfig = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.ColumnNumeric = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.button32 = new System.Windows.Forms.Button();
            this.RowNumeric = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this._contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Mergerbutton42 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupboximage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumberOfCopies)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ColumnNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RowNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Black;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(10);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Black;
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.Black;
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(726, 801);
            this.splitContainer1.SplitterDistance = 471;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BackColor = System.Drawing.Color.Black;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackColor = System.Drawing.Color.Black;
            this.splitContainer2.Panel1.Controls.Add(this.groupboximage);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this._toolStrip);
            this.splitContainer2.Panel2.Margin = new System.Windows.Forms.Padding(1);
            this.splitContainer2.Panel2MinSize = 35;
            this.splitContainer2.Size = new System.Drawing.Size(471, 801);
            this.splitContainer2.SplitterDistance = 422;
            this.splitContainer2.TabIndex = 2;
            // 
            // groupboximage
            // 
            this.groupboximage.Controls.Add(this._layoutImages);
            this.groupboximage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupboximage.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupboximage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.groupboximage.Location = new System.Drawing.Point(0, 0);
            this.groupboximage.Name = "groupboximage";
            this.groupboximage.Size = new System.Drawing.Size(422, 801);
            this.groupboximage.TabIndex = 3;
            this.groupboximage.TabStop = false;
            this.groupboximage.Text = "��ӡ�Ű�";
            // 
            // _layoutImages
            // 
            this._layoutImages.Dock = System.Windows.Forms.DockStyle.Fill;
            this._layoutImages.Location = new System.Drawing.Point(3, 17);
            this._layoutImages.Margin = new System.Windows.Forms.Padding(0);
            this._layoutImages.Name = "_layoutImages";
            this._layoutImages.Size = new System.Drawing.Size(416, 781);
            this._layoutImages.TabIndex = 0;
            this._layoutImages.SizeChanged += new System.EventHandler(this._layoutImages_SizeChanged);
            // 
            // _toolStrip
            // 
            this._toolStrip.AllowItemReorder = true;
            this._toolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(194)))), ((int)(((byte)(211)))));
            this._toolStrip.CanOverflow = false;
            this._toolStrip.Dock = System.Windows.Forms.DockStyle.Right;
            this._toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._toolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this._toolStrip.Location = new System.Drawing.Point(19, 0);
            this._toolStrip.Name = "_toolStrip";
            this._toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this._toolStrip.Size = new System.Drawing.Size(26, 801);
            this._toolStrip.Stretch = true;
            this._toolStrip.TabIndex = 0;
            this._toolStrip.Text = "toolStrip1";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer3.Panel1.Padding = new System.Windows.Forms.Padding(3);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.AutoScroll = true;
            this.splitContainer3.Panel2.Controls.Add(this.panel1);
            this.splitContainer3.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer3.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer3.Panel2.Padding = new System.Windows.Forms.Padding(3);
            this.splitContainer3.Size = new System.Drawing.Size(251, 801);
            this.splitContainer3.SplitterDistance = 167;
            this.splitContainer3.SplitterWidth = 1;
            this.splitContainer3.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Black;
            this.groupBox2.Controls.Add(this.Mergerbutton42);
            this.groupBox2.Controls.Add(this.Printlable);
            this.groupBox2.Controls.Add(this.DicomPrintTable);
            this.groupBox2.Controls.Add(this.setPrintButton);
            this.groupBox2.Controls.Add(this.radioZongXiang);
            this.groupBox2.Controls.Add(this.radioHengXiang);
            this.groupBox2.Controls.Add(this.NumberOfCopies);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.FilmSizeComBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(1);
            this.groupBox2.Size = new System.Drawing.Size(245, 161);
            this.groupBox2.TabIndex = 33;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "���ò���";
            // 
            // Printlable
            // 
            this.Printlable.AutoSize = true;
            this.Printlable.Location = new System.Drawing.Point(6, 20);
            this.Printlable.Name = "Printlable";
            this.Printlable.Size = new System.Drawing.Size(57, 12);
            this.Printlable.TabIndex = 17;
            this.Printlable.Text = "��ӡ����";
            // 
            // DicomPrintTable
            // 
            this.DicomPrintTable.FormattingEnabled = true;
            this.DicomPrintTable.Location = new System.Drawing.Point(77, 17);
            this.DicomPrintTable.Name = "DicomPrintTable";
            this.DicomPrintTable.Size = new System.Drawing.Size(153, 20);
            this.DicomPrintTable.TabIndex = 16;
            this.DicomPrintTable.SelectionChangeCommitted += new System.EventHandler(this.DicomPrintTable_SelectionChangeCommitted);
            // 
            // setPrintButton
            // 
            this.setPrintButton.BackColor = System.Drawing.SystemColors.Control;
            this.setPrintButton.ForeColor = System.Drawing.Color.Black;
            this.setPrintButton.Location = new System.Drawing.Point(77, 134);
            this.setPrintButton.Name = "setPrintButton";
            this.setPrintButton.Size = new System.Drawing.Size(71, 23);
            this.setPrintButton.TabIndex = 15;
            this.setPrintButton.Text = "���ô�ӡ��";
            this.setPrintButton.UseVisualStyleBackColor = false;
            this.setPrintButton.Click += new System.EventHandler(this.setPrintButton_Click);
            // 
            // radioZongXiang
            // 
            this.radioZongXiang.AutoSize = true;
            this.radioZongXiang.Location = new System.Drawing.Point(165, 111);
            this.radioZongXiang.Name = "radioZongXiang";
            this.radioZongXiang.Size = new System.Drawing.Size(49, 16);
            this.radioZongXiang.TabIndex = 14;
            this.radioZongXiang.Text = "����";
            this.radioZongXiang.UseVisualStyleBackColor = true;
            this.radioZongXiang.CheckedChanged += new System.EventHandler(this.radioZongXiang_CheckedChanged);
            // 
            // radioHengXiang
            // 
            this.radioHengXiang.AutoSize = true;
            this.radioHengXiang.Location = new System.Drawing.Point(84, 109);
            this.radioHengXiang.Name = "radioHengXiang";
            this.radioHengXiang.Size = new System.Drawing.Size(49, 16);
            this.radioHengXiang.TabIndex = 13;
            this.radioHengXiang.Text = "����";
            this.radioHengXiang.UseVisualStyleBackColor = true;
            this.radioHengXiang.CheckedChanged += new System.EventHandler(this.radioHengXiang_CheckedChanged);
            // 
            // NumberOfCopies
            // 
            this.NumberOfCopies.Location = new System.Drawing.Point(77, 76);
            this.NumberOfCopies.Name = "NumberOfCopies";
            this.NumberOfCopies.Size = new System.Drawing.Size(153, 21);
            this.NumberOfCopies.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "��ӡ������";
            // 
            // FilmSizeComBox
            // 
            this.FilmSizeComBox.FormattingEnabled = true;
            this.FilmSizeComBox.Location = new System.Drawing.Point(77, 46);
            this.FilmSizeComBox.Name = "FilmSizeComBox";
            this.FilmSizeComBox.Size = new System.Drawing.Size(153, 20);
            this.FilmSizeComBox.TabIndex = 3;
            this.FilmSizeComBox.SelectionChangeCommitted += new System.EventHandler(this.FilmSizeComBox_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "��Ƭ��С��";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "��Ƭ����";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.PrintedDeleteImage);
            this.panel1.Controls.Add(this.button8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.PrintButton);
            this.panel1.Controls.Add(this.ImageCount);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.FilmCount);
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(3, 570);
            this.panel1.Margin = new System.Windows.Forms.Padding(1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(242, 78);
            this.panel1.TabIndex = 30;
            // 
            // PrintedDeleteImage
            // 
            this.PrintedDeleteImage.AutoSize = true;
            this.PrintedDeleteImage.BackColor = System.Drawing.Color.Black;
            this.PrintedDeleteImage.Checked = true;
            this.PrintedDeleteImage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.PrintedDeleteImage.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PrintedDeleteImage.ForeColor = System.Drawing.Color.Red;
            this.PrintedDeleteImage.Location = new System.Drawing.Point(111, 9);
            this.PrintedDeleteImage.Name = "PrintedDeleteImage";
            this.PrintedDeleteImage.Size = new System.Drawing.Size(128, 16);
            this.PrintedDeleteImage.TabIndex = 16;
            this.PrintedDeleteImage.Text = "ɾ����ӡ����ͼ��";
            this.PrintedDeleteImage.UseVisualStyleBackColor = false;
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.SystemColors.Control;
            this.button8.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button8.ForeColor = System.Drawing.Color.Black;
            this.button8.Location = new System.Drawing.Point(107, 31);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(65, 39);
            this.button8.TabIndex = 7;
            this.button8.Text = "��ӡ��ǰҳ";
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(1, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 29;
            this.label7.Text = "ͼ������";
            // 
            // PrintButton
            // 
            this.PrintButton.BackColor = System.Drawing.SystemColors.Control;
            this.PrintButton.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PrintButton.ForeColor = System.Drawing.Color.Black;
            this.PrintButton.Location = new System.Drawing.Point(178, 31);
            this.PrintButton.Name = "PrintButton";
            this.PrintButton.Size = new System.Drawing.Size(62, 39);
            this.PrintButton.TabIndex = 26;
            this.PrintButton.Text = "��ӡ����";
            this.PrintButton.UseVisualStyleBackColor = false;
            this.PrintButton.Click += new System.EventHandler(this.PrintButton_Click);
            // 
            // ImageCount
            // 
            this.ImageCount.AutoSize = true;
            this.ImageCount.ForeColor = System.Drawing.Color.Red;
            this.ImageCount.Location = new System.Drawing.Point(61, 53);
            this.ImageCount.Name = "ImageCount";
            this.ImageCount.Size = new System.Drawing.Size(41, 12);
            this.ImageCount.TabIndex = 35;
            this.ImageCount.Text = "label9";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(1, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 33;
            this.label6.Text = "��Ƭ����";
            // 
            // FilmCount
            // 
            this.FilmCount.AutoSize = true;
            this.FilmCount.ForeColor = System.Drawing.Color.Red;
            this.FilmCount.Location = new System.Drawing.Point(60, 31);
            this.FilmCount.Name = "FilmCount";
            this.FilmCount.Size = new System.Drawing.Size(41, 12);
            this.FilmCount.TabIndex = 34;
            this.FilmCount.Text = "label8";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button40);
            this.groupBox4.Controls.Add(this.button41);
            this.groupBox4.Controls.Add(this.button38);
            this.groupBox4.Controls.Add(this.button36);
            this.groupBox4.Controls.Add(this.button9);
            this.groupBox4.Controls.Add(this.button10);
            this.groupBox4.Controls.Add(this.button11);
            this.groupBox4.Controls.Add(this.ClearButton);
            this.groupBox4.Controls.Add(this.button6);
            this.groupBox4.Controls.Add(this.button7);
            this.groupBox4.Controls.Add(this.button5);
            this.groupBox4.Controls.Add(this.button4);
            this.groupBox4.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.groupBox4.Location = new System.Drawing.Point(1, 419);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(245, 147);
            this.groupBox4.TabIndex = 35;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "����";
            // 
            // button40
            // 
            this.button40.BackColor = System.Drawing.SystemColors.Control;
            this.button40.ForeColor = System.Drawing.Color.Black;
            this.button40.Location = new System.Drawing.Point(186, 104);
            this.button40.Name = "button40";
            this.button40.Size = new System.Drawing.Size(42, 37);
            this.button40.TabIndex = 36;
            this.button40.Text = "βҳ";
            this.button40.UseVisualStyleBackColor = false;
            this.button40.Click += new System.EventHandler(this.button40_Click);
            // 
            // button41
            // 
            this.button41.BackColor = System.Drawing.SystemColors.Control;
            this.button41.ForeColor = System.Drawing.Color.Black;
            this.button41.Location = new System.Drawing.Point(124, 104);
            this.button41.Name = "button41";
            this.button41.Size = new System.Drawing.Size(55, 37);
            this.button41.TabIndex = 35;
            this.button41.Text = "��һҳ";
            this.button41.UseVisualStyleBackColor = false;
            this.button41.Click += new System.EventHandler(this.button41_Click);
            // 
            // button38
            // 
            this.button38.BackColor = System.Drawing.SystemColors.Control;
            this.button38.ForeColor = System.Drawing.Color.Black;
            this.button38.Location = new System.Drawing.Point(65, 104);
            this.button38.Name = "button38";
            this.button38.Size = new System.Drawing.Size(53, 37);
            this.button38.TabIndex = 34;
            this.button38.Text = "��һҳ";
            this.button38.UseVisualStyleBackColor = false;
            this.button38.Click += new System.EventHandler(this.button38_Click);
            // 
            // button36
            // 
            this.button36.BackColor = System.Drawing.SystemColors.Control;
            this.button36.ForeColor = System.Drawing.Color.Black;
            this.button36.Location = new System.Drawing.Point(13, 104);
            this.button36.Name = "button36";
            this.button36.Size = new System.Drawing.Size(42, 37);
            this.button36.TabIndex = 33;
            this.button36.Text = "��ҳ";
            this.button36.UseVisualStyleBackColor = false;
            this.button36.Click += new System.EventHandler(this.button36_Click);
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.SystemColors.Control;
            this.button9.ForeColor = System.Drawing.Color.Black;
            this.button9.Location = new System.Drawing.Point(124, 53);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(55, 41);
            this.button9.TabIndex = 6;
            this.button9.Text = "ɾ����ǰҳ";
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.BackColor = System.Drawing.SystemColors.Control;
            this.button10.ForeColor = System.Drawing.Color.Black;
            this.button10.Location = new System.Drawing.Point(181, 53);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(58, 41);
            this.button10.TabIndex = 5;
            this.button10.Text = "�½��հ�ҳ";
            this.button10.UseVisualStyleBackColor = false;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.BackColor = System.Drawing.SystemColors.Control;
            this.button11.ForeColor = System.Drawing.Color.Black;
            this.button11.Location = new System.Drawing.Point(10, 58);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(45, 30);
            this.button11.TabIndex = 4;
            this.button11.Text = "ճ��";
            this.button11.UseVisualStyleBackColor = false;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.BackColor = System.Drawing.SystemColors.Control;
            this.ClearButton.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClearButton.ForeColor = System.Drawing.Color.Black;
            this.ClearButton.Location = new System.Drawing.Point(64, 53);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(54, 41);
            this.ClearButton.TabIndex = 32;
            this.ClearButton.Text = "ɾ��ȫ��ҳ";
            this.ClearButton.UseVisualStyleBackColor = false;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.SystemColors.Control;
            this.button6.ForeColor = System.Drawing.Color.Black;
            this.button6.Location = new System.Drawing.Point(180, 17);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(45, 30);
            this.button6.TabIndex = 3;
            this.button6.Text = "����";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.SystemColors.Control;
            this.button7.ForeColor = System.Drawing.Color.Black;
            this.button7.Location = new System.Drawing.Point(123, 17);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(45, 30);
            this.button7.TabIndex = 2;
            this.button7.Text = "����";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.SystemColors.Control;
            this.button5.ForeColor = System.Drawing.Color.Black;
            this.button5.Location = new System.Drawing.Point(64, 17);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(45, 30);
            this.button5.TabIndex = 1;
            this.button5.Text = "��ѡ";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.Control;
            this.button4.ForeColor = System.Drawing.Color.Black;
            this.button4.Location = new System.Drawing.Point(10, 17);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(45, 30);
            this.button4.TabIndex = 0;
            this.button4.Text = "ȫѡ";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Black;
            this.groupBox3.Controls.Add(this.flowLayoutPanel2);
            this.groupBox3.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.groupBox3.Location = new System.Drawing.Point(2, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(1);
            this.groupBox3.Size = new System.Drawing.Size(244, 407);
            this.groupBox3.TabIndex = 34;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "��������";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.BackColor = System.Drawing.Color.Black;
            this.flowLayoutPanel2.Controls.Add(this.button30);
            this.flowLayoutPanel2.Controls.Add(this.button20);
            this.flowLayoutPanel2.Controls.Add(this.button39);
            this.flowLayoutPanel2.Controls.Add(this.button29);
            this.flowLayoutPanel2.Controls.Add(this.button15);
            this.flowLayoutPanel2.Controls.Add(this.button28);
            this.flowLayoutPanel2.Controls.Add(this.button19);
            this.flowLayoutPanel2.Controls.Add(this.button37);
            this.flowLayoutPanel2.Controls.Add(this.button27);
            this.flowLayoutPanel2.Controls.Add(this.button14);
            this.flowLayoutPanel2.Controls.Add(this.button26);
            this.flowLayoutPanel2.Controls.Add(this.button18);
            this.flowLayoutPanel2.Controls.Add(this.button35);
            this.flowLayoutPanel2.Controls.Add(this.button25);
            this.flowLayoutPanel2.Controls.Add(this.button13);
            this.flowLayoutPanel2.Controls.Add(this.button24);
            this.flowLayoutPanel2.Controls.Add(this.button17);
            this.flowLayoutPanel2.Controls.Add(this.button33);
            this.flowLayoutPanel2.Controls.Add(this.button23);
            this.flowLayoutPanel2.Controls.Add(this.button12);
            this.flowLayoutPanel2.Controls.Add(this.button22);
            this.flowLayoutPanel2.Controls.Add(this.button16);
            this.flowLayoutPanel2.Controls.Add(this.button31);
            this.flowLayoutPanel2.Controls.Add(this.button21);
            this.flowLayoutPanel2.Controls.Add(this.button2);
            this.flowLayoutPanel2.Controls.Add(this.button1);
            this.flowLayoutPanel2.Controls.Add(this.button3);
            this.flowLayoutPanel2.Controls.Add(this.button34);
            this.flowLayoutPanel2.Controls.Add(this.zifenge);
            this.flowLayoutPanel2.Controls.Add(this.fenge);
            this.flowLayoutPanel2.Controls.Add(this.SaveGridToConfig);
            this.flowLayoutPanel2.Controls.Add(this.LoadGridFromConfig);
            this.flowLayoutPanel2.Controls.Add(this.groupBox5);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(1, 15);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(242, 391);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // button30
            // 
            this.button30.BackColor = System.Drawing.SystemColors.Control;
            this.button30.ForeColor = System.Drawing.Color.Black;
            this.button30.Location = new System.Drawing.Point(3, 3);
            this.button30.Name = "button30";
            this.button30.Size = new System.Drawing.Size(50, 36);
            this.button30.TabIndex = 19;
            this.button30.Tag = "STANDARD\\1,1";
            this.button30.Text = "1X1";
            this.button30.UseVisualStyleBackColor = false;
            this.button30.Click += new System.EventHandler(this.SubGrid);
            // 
            // button20
            // 
            this.button20.BackColor = System.Drawing.SystemColors.Control;
            this.button20.ForeColor = System.Drawing.Color.Black;
            this.button20.Location = new System.Drawing.Point(59, 3);
            this.button20.Name = "button20";
            this.button20.Size = new System.Drawing.Size(52, 36);
            this.button20.TabIndex = 20;
            this.button20.Tag = "STANDARD\\1,2";
            this.button20.Text = "1X2";
            this.button20.UseVisualStyleBackColor = false;
            this.button20.Click += new System.EventHandler(this.SubGrid);
            // 
            // button39
            // 
            this.button39.BackColor = System.Drawing.SystemColors.Control;
            this.button39.ForeColor = System.Drawing.Color.Black;
            this.button39.Location = new System.Drawing.Point(117, 3);
            this.button39.Name = "button39";
            this.button39.Size = new System.Drawing.Size(52, 36);
            this.button39.TabIndex = 15;
            this.button39.Tag = "STANDARD\\2,1";
            this.button39.Text = "2X1";
            this.button39.UseVisualStyleBackColor = false;
            this.button39.Click += new System.EventHandler(this.SubGrid);
            // 
            // button29
            // 
            this.button29.BackColor = System.Drawing.SystemColors.Control;
            this.button29.ForeColor = System.Drawing.Color.Black;
            this.button29.Location = new System.Drawing.Point(175, 3);
            this.button29.Name = "button29";
            this.button29.Size = new System.Drawing.Size(53, 36);
            this.button29.TabIndex = 16;
            this.button29.Tag = "STANDARD\\2,2";
            this.button29.Text = "2X2";
            this.button29.UseVisualStyleBackColor = false;
            this.button29.Click += new System.EventHandler(this.SubGrid);
            // 
            // button15
            // 
            this.button15.BackColor = System.Drawing.SystemColors.Control;
            this.button15.ForeColor = System.Drawing.Color.Black;
            this.button15.Location = new System.Drawing.Point(3, 45);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(52, 36);
            this.button15.TabIndex = 17;
            this.button15.Tag = "STANDARD\\2,3";
            this.button15.Text = "2X3";
            this.button15.UseVisualStyleBackColor = false;
            this.button15.Click += new System.EventHandler(this.SubGrid);
            // 
            // button28
            // 
            this.button28.BackColor = System.Drawing.SystemColors.Control;
            this.button28.ForeColor = System.Drawing.Color.Black;
            this.button28.Location = new System.Drawing.Point(61, 45);
            this.button28.Name = "button28";
            this.button28.Size = new System.Drawing.Size(52, 36);
            this.button28.TabIndex = 24;
            this.button28.Tag = "STANDARD\\2,4";
            this.button28.Text = "2X4";
            this.button28.UseVisualStyleBackColor = false;
            this.button28.Click += new System.EventHandler(this.SubGrid);
            // 
            // button19
            // 
            this.button19.BackColor = System.Drawing.SystemColors.Control;
            this.button19.ForeColor = System.Drawing.Color.Black;
            this.button19.Location = new System.Drawing.Point(119, 45);
            this.button19.Name = "button19";
            this.button19.Size = new System.Drawing.Size(52, 36);
            this.button19.TabIndex = 25;
            this.button19.Tag = "STANDARD\\3,2";
            this.button19.Text = "3X2";
            this.button19.UseVisualStyleBackColor = false;
            this.button19.Click += new System.EventHandler(this.SubGrid);
            // 
            // button37
            // 
            this.button37.BackColor = System.Drawing.SystemColors.Control;
            this.button37.ForeColor = System.Drawing.Color.Black;
            this.button37.Location = new System.Drawing.Point(177, 45);
            this.button37.Name = "button37";
            this.button37.Size = new System.Drawing.Size(52, 36);
            this.button37.TabIndex = 26;
            this.button37.Tag = "STANDARD\\3,3";
            this.button37.Text = "3X3";
            this.button37.UseVisualStyleBackColor = false;
            this.button37.Click += new System.EventHandler(this.SubGrid);
            // 
            // button27
            // 
            this.button27.BackColor = System.Drawing.SystemColors.Control;
            this.button27.ForeColor = System.Drawing.Color.Black;
            this.button27.Location = new System.Drawing.Point(3, 87);
            this.button27.Name = "button27";
            this.button27.Size = new System.Drawing.Size(52, 36);
            this.button27.TabIndex = 21;
            this.button27.Tag = "STANDARD\\3,4";
            this.button27.Text = "3X4";
            this.button27.UseVisualStyleBackColor = false;
            this.button27.Click += new System.EventHandler(this.SubGrid);
            // 
            // button14
            // 
            this.button14.BackColor = System.Drawing.SystemColors.Control;
            this.button14.ForeColor = System.Drawing.Color.Black;
            this.button14.Location = new System.Drawing.Point(61, 87);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(52, 36);
            this.button14.TabIndex = 22;
            this.button14.Tag = "STANDARD\\3,5";
            this.button14.Text = "3X5";
            this.button14.UseVisualStyleBackColor = false;
            this.button14.Click += new System.EventHandler(this.SubGrid);
            // 
            // button26
            // 
            this.button26.BackColor = System.Drawing.SystemColors.Control;
            this.button26.ForeColor = System.Drawing.Color.Black;
            this.button26.Location = new System.Drawing.Point(119, 87);
            this.button26.Name = "button26";
            this.button26.Size = new System.Drawing.Size(52, 36);
            this.button26.TabIndex = 23;
            this.button26.Tag = "STANDARD\\4,1";
            this.button26.Text = "4X1";
            this.button26.UseVisualStyleBackColor = false;
            this.button26.Click += new System.EventHandler(this.SubGrid);
            // 
            // button18
            // 
            this.button18.BackColor = System.Drawing.SystemColors.Control;
            this.button18.ForeColor = System.Drawing.Color.Black;
            this.button18.Location = new System.Drawing.Point(177, 87);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(52, 36);
            this.button18.TabIndex = 18;
            this.button18.Tag = "STANDARD\\4,2";
            this.button18.Text = "4X2";
            this.button18.UseVisualStyleBackColor = false;
            this.button18.Click += new System.EventHandler(this.SubGrid);
            // 
            // button35
            // 
            this.button35.BackColor = System.Drawing.SystemColors.Control;
            this.button35.ForeColor = System.Drawing.Color.Black;
            this.button35.Location = new System.Drawing.Point(3, 129);
            this.button35.Name = "button35";
            this.button35.Size = new System.Drawing.Size(52, 36);
            this.button35.TabIndex = 5;
            this.button35.Tag = "STANDARD\\4,3";
            this.button35.Text = "4X3";
            this.button35.UseVisualStyleBackColor = false;
            this.button35.Click += new System.EventHandler(this.SubGrid);
            // 
            // button25
            // 
            this.button25.BackColor = System.Drawing.SystemColors.Control;
            this.button25.ForeColor = System.Drawing.Color.Black;
            this.button25.Location = new System.Drawing.Point(61, 129);
            this.button25.Name = "button25";
            this.button25.Size = new System.Drawing.Size(52, 36);
            this.button25.TabIndex = 6;
            this.button25.Tag = "STANDARD\\4,4";
            this.button25.Text = "4X4";
            this.button25.UseVisualStyleBackColor = false;
            this.button25.Click += new System.EventHandler(this.SubGrid);
            // 
            // button13
            // 
            this.button13.BackColor = System.Drawing.SystemColors.Control;
            this.button13.ForeColor = System.Drawing.Color.Black;
            this.button13.Location = new System.Drawing.Point(119, 129);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(52, 36);
            this.button13.TabIndex = 7;
            this.button13.Tag = "STANDARD\\4,5";
            this.button13.Text = "4X5";
            this.button13.UseVisualStyleBackColor = false;
            this.button13.Click += new System.EventHandler(this.SubGrid);
            // 
            // button24
            // 
            this.button24.BackColor = System.Drawing.SystemColors.Control;
            this.button24.ForeColor = System.Drawing.Color.Black;
            this.button24.Location = new System.Drawing.Point(177, 129);
            this.button24.Name = "button24";
            this.button24.Size = new System.Drawing.Size(52, 36);
            this.button24.TabIndex = 2;
            this.button24.Tag = "STANDARD\\4,6";
            this.button24.Text = "4X6";
            this.button24.UseVisualStyleBackColor = false;
            this.button24.Click += new System.EventHandler(this.SubGrid);
            // 
            // button17
            // 
            this.button17.BackColor = System.Drawing.SystemColors.Control;
            this.button17.ForeColor = System.Drawing.Color.Black;
            this.button17.Location = new System.Drawing.Point(3, 171);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(52, 36);
            this.button17.TabIndex = 3;
            this.button17.Tag = "STANDARD\\5,3";
            this.button17.Text = "5X3";
            this.button17.UseVisualStyleBackColor = false;
            this.button17.Click += new System.EventHandler(this.SubGrid);
            // 
            // button33
            // 
            this.button33.BackColor = System.Drawing.SystemColors.Control;
            this.button33.ForeColor = System.Drawing.Color.Black;
            this.button33.Location = new System.Drawing.Point(61, 171);
            this.button33.Name = "button33";
            this.button33.Size = new System.Drawing.Size(52, 36);
            this.button33.TabIndex = 4;
            this.button33.Tag = "STANDARD\\5,4";
            this.button33.Text = "5X4";
            this.button33.UseVisualStyleBackColor = false;
            this.button33.Click += new System.EventHandler(this.SubGrid);
            // 
            // button23
            // 
            this.button23.BackColor = System.Drawing.SystemColors.Control;
            this.button23.ForeColor = System.Drawing.Color.Black;
            this.button23.Location = new System.Drawing.Point(119, 171);
            this.button23.Name = "button23";
            this.button23.Size = new System.Drawing.Size(52, 36);
            this.button23.TabIndex = 11;
            this.button23.Tag = "STANDARD\\5,5";
            this.button23.Text = "5X5";
            this.button23.UseVisualStyleBackColor = false;
            this.button23.Click += new System.EventHandler(this.SubGrid);
            // 
            // button12
            // 
            this.button12.BackColor = System.Drawing.SystemColors.Control;
            this.button12.ForeColor = System.Drawing.Color.Black;
            this.button12.Location = new System.Drawing.Point(177, 171);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(52, 36);
            this.button12.TabIndex = 12;
            this.button12.Tag = "STANDARD\\5,6";
            this.button12.Text = "5X6";
            this.button12.UseVisualStyleBackColor = false;
            this.button12.Click += new System.EventHandler(this.SubGrid);
            // 
            // button22
            // 
            this.button22.BackColor = System.Drawing.SystemColors.Control;
            this.button22.ForeColor = System.Drawing.Color.Black;
            this.button22.Location = new System.Drawing.Point(3, 213);
            this.button22.Name = "button22";
            this.button22.Size = new System.Drawing.Size(52, 36);
            this.button22.TabIndex = 13;
            this.button22.Tag = "STANDARD\\5,7";
            this.button22.Text = "5X7";
            this.button22.UseVisualStyleBackColor = false;
            this.button22.Click += new System.EventHandler(this.SubGrid);
            // 
            // button16
            // 
            this.button16.BackColor = System.Drawing.SystemColors.Control;
            this.button16.ForeColor = System.Drawing.Color.Black;
            this.button16.Location = new System.Drawing.Point(61, 213);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(52, 36);
            this.button16.TabIndex = 8;
            this.button16.Tag = "STANDARD\\6,4";
            this.button16.Text = "6X4";
            this.button16.UseVisualStyleBackColor = false;
            this.button16.Click += new System.EventHandler(this.SubGrid);
            // 
            // button31
            // 
            this.button31.BackColor = System.Drawing.SystemColors.Control;
            this.button31.ForeColor = System.Drawing.Color.Black;
            this.button31.Location = new System.Drawing.Point(119, 213);
            this.button31.Name = "button31";
            this.button31.Size = new System.Drawing.Size(52, 36);
            this.button31.TabIndex = 9;
            this.button31.Tag = "STANDARD\\6,5";
            this.button31.Text = "6X5";
            this.button31.UseVisualStyleBackColor = false;
            this.button31.Click += new System.EventHandler(this.SubGrid);
            // 
            // button21
            // 
            this.button21.BackColor = System.Drawing.SystemColors.Control;
            this.button21.ForeColor = System.Drawing.Color.Black;
            this.button21.Location = new System.Drawing.Point(177, 213);
            this.button21.Name = "button21";
            this.button21.Size = new System.Drawing.Size(52, 36);
            this.button21.TabIndex = 10;
            this.button21.Tag = "STANDARD\\6,6";
            this.button21.Text = "6X6";
            this.button21.UseVisualStyleBackColor = false;
            this.button21.Click += new System.EventHandler(this.SubGrid);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.Control;
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(3, 255);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(52, 36);
            this.button2.TabIndex = 28;
            this.button2.Tag = "STANDARD\\6,8";
            this.button2.Text = "6X8";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.SubGrid);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(61, 255);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(52, 36);
            this.button1.TabIndex = 14;
            this.button1.Tag = "STANDARD\\6,7";
            this.button1.Text = "6X7";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.SubGrid);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.Control;
            this.button3.ForeColor = System.Drawing.Color.Black;
            this.button3.Location = new System.Drawing.Point(119, 255);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(52, 36);
            this.button3.TabIndex = 29;
            this.button3.Tag = "ROW\\1,2";
            this.button3.Text = "Row 1.2";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.SubGrid);
            // 
            // button34
            // 
            this.button34.BackColor = System.Drawing.SystemColors.Control;
            this.button34.ForeColor = System.Drawing.Color.Black;
            this.button34.Location = new System.Drawing.Point(177, 255);
            this.button34.Name = "button34";
            this.button34.Size = new System.Drawing.Size(52, 36);
            this.button34.TabIndex = 27;
            this.button34.Tag = "COL\\1,2";
            this.button34.Text = "Col 1.2";
            this.button34.UseVisualStyleBackColor = false;
            this.button34.Click += new System.EventHandler(this.SubGrid);
            // 
            // zifenge
            // 
            this.zifenge.AutoSize = true;
            this.zifenge.Location = new System.Drawing.Point(3, 297);
            this.zifenge.Name = "zifenge";
            this.zifenge.Size = new System.Drawing.Size(62, 16);
            this.zifenge.TabIndex = 31;
            this.zifenge.TabStop = true;
            this.zifenge.Text = "�ӷָ�";
            this.zifenge.UseVisualStyleBackColor = true;
            // 
            // fenge
            // 
            this.fenge.AutoSize = true;
            this.fenge.Checked = true;
            this.fenge.Location = new System.Drawing.Point(71, 297);
            this.fenge.Name = "fenge";
            this.fenge.Size = new System.Drawing.Size(49, 16);
            this.fenge.TabIndex = 30;
            this.fenge.TabStop = true;
            this.fenge.Text = "�ָ�";
            this.fenge.UseVisualStyleBackColor = true;
            // 
            // SaveGridToConfig
            // 
            this.SaveGridToConfig.ForeColor = System.Drawing.Color.Black;
            this.SaveGridToConfig.Location = new System.Drawing.Point(126, 297);
            this.SaveGridToConfig.Name = "SaveGridToConfig";
            this.SaveGridToConfig.Size = new System.Drawing.Size(49, 23);
            this.SaveGridToConfig.TabIndex = 32;
            this.SaveGridToConfig.Text = "����";
            this.SaveGridToConfig.UseVisualStyleBackColor = true;
            this.SaveGridToConfig.Click += new System.EventHandler(this.SaveGridToConfig_Click);
            // 
            // LoadGridFromConfig
            // 
            this.LoadGridFromConfig.ForeColor = System.Drawing.Color.Black;
            this.LoadGridFromConfig.Location = new System.Drawing.Point(181, 297);
            this.LoadGridFromConfig.Name = "LoadGridFromConfig";
            this.LoadGridFromConfig.Size = new System.Drawing.Size(49, 23);
            this.LoadGridFromConfig.TabIndex = 32;
            this.LoadGridFromConfig.Tag = "CustemGridConfig.xml";
            this.LoadGridFromConfig.Text = "�Ű�";
            this.LoadGridFromConfig.UseVisualStyleBackColor = true;
            this.LoadGridFromConfig.Click += new System.EventHandler(this.LoadGridFromConfig_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.Black;
            this.groupBox5.Controls.Add(this.ColumnNumeric);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.button32);
            this.groupBox5.Controls.Add(this.RowNumeric);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.groupBox5.Location = new System.Drawing.Point(3, 326);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(233, 62);
            this.groupBox5.TabIndex = 28;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "�Զ���";
            // 
            // ColumnNumeric
            // 
            this.ColumnNumeric.Location = new System.Drawing.Point(29, 22);
            this.ColumnNumeric.Name = "ColumnNumeric";
            this.ColumnNumeric.Size = new System.Drawing.Size(53, 21);
            this.ColumnNumeric.TabIndex = 13;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(2, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 12);
            this.label10.TabIndex = 12;
            this.label10.Text = "�У�";
            // 
            // button32
            // 
            this.button32.BackColor = System.Drawing.SystemColors.Control;
            this.button32.ForeColor = System.Drawing.Color.Black;
            this.button32.Location = new System.Drawing.Point(167, 20);
            this.button32.Name = "button32";
            this.button32.Size = new System.Drawing.Size(60, 23);
            this.button32.TabIndex = 11;
            this.button32.Tag = "CUSTOM";
            this.button32.Text = "ȷ��";
            this.button32.UseVisualStyleBackColor = false;
            this.button32.Click += new System.EventHandler(this.SubGrid);
            // 
            // RowNumeric
            // 
            this.RowNumeric.Location = new System.Drawing.Point(107, 21);
            this.RowNumeric.Name = "RowNumeric";
            this.RowNumeric.Size = new System.Drawing.Size(53, 21);
            this.RowNumeric.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(86, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 12);
            this.label9.TabIndex = 7;
            this.label9.Text = "�У�";
            // 
            // _contextMenu
            // 
            this._contextMenu.Name = "_contextMenu";
            this._contextMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // Mergerbutton42
            // 
            this.Mergerbutton42.ForeColor = System.Drawing.Color.Black;
            this.Mergerbutton42.Location = new System.Drawing.Point(11, 128);
            this.Mergerbutton42.Name = "Mergerbutton42";
            this.Mergerbutton42.Size = new System.Drawing.Size(52, 23);
            this.Mergerbutton42.TabIndex = 18;
            this.Mergerbutton42.Text = "�ϲ�";
            this.Mergerbutton42.UseVisualStyleBackColor = true;
            this.Mergerbutton42.Click += new System.EventHandler(this.Mergerbutton42_Click);
            // 
            // PrintImageViewerControl
            // 
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.splitContainer1);
            this.Name = "PrintImageViewerControl";
            this.Size = new System.Drawing.Size(726, 801);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupboximage.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumberOfCopies)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ColumnNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RowNumeric)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button PrintButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button button30;
        private System.Windows.Forms.Button button20;
        private System.Windows.Forms.Button button39;
        private System.Windows.Forms.Button button29;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button button28;
        private System.Windows.Forms.Button button19;
        private System.Windows.Forms.Button button37;
        private System.Windows.Forms.Button button27;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button26;
        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.Button button35;
        private System.Windows.Forms.Button button25;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button24;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.Button button33;
        private System.Windows.Forms.Button button23;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button22;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.Button button31;
        private System.Windows.Forms.Button button21;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button34;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button32;
        private System.Windows.Forms.NumericUpDown RowNumeric;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Label ImageCount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label FilmCount;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ToolStrip _toolStrip;
        private System.Windows.Forms.GroupBox groupboximage;
        private System.Windows.Forms.ContextMenuStrip _contextMenu;
        private System.Windows.Forms.NumericUpDown ColumnNumeric;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel _layoutImages;
        private System.Windows.Forms.RadioButton fenge;
        private System.Windows.Forms.RadioButton zifenge;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button setPrintButton;
        private System.Windows.Forms.RadioButton radioZongXiang;
        private System.Windows.Forms.RadioButton radioHengXiang;
        private Desktop.View.WinForms.NonEmptyNumericUpDown NumberOfCopies;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox FilmSizeComBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox PrintedDeleteImage;
        private System.Windows.Forms.Label Printlable;
        private System.Windows.Forms.ComboBox DicomPrintTable;
        private System.Windows.Forms.Button button40;
        private System.Windows.Forms.Button button41;
        private System.Windows.Forms.Button button38;
        private System.Windows.Forms.Button button36;
        private System.Windows.Forms.Button SaveGridToConfig;
        private System.Windows.Forms.Button LoadGridFromConfig;
        private System.Windows.Forms.Button Mergerbutton42;

	}
}
