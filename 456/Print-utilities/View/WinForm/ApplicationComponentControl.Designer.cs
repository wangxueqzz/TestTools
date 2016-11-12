
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

namespace Macro.ImageViewer.Utilities.Print.View.WinForm
{
    partial class ApplicationComponentControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.DicomPrintTable = new Macro.Desktop.View.WinForms.TableView();
            this.label1 = new System.Windows.Forms.Label();
            this.StatusMessage = new System.Windows.Forms.Label();
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.FileCountLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ImageCountLabel = new System.Windows.Forms.Label();
            this.DicomPrinterConfigPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // DicomPrintTable
            // 
            this.DicomPrintTable.AccessibleDescription = "";
            this.DicomPrintTable.AutoScroll = true;
            this.DicomPrintTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.DicomPrintTable.BackColor = System.Drawing.Color.DimGray;
            this.DicomPrintTable.ColumnHeaderTooltip = "";
            this.DicomPrintTable.FilterTextBoxWidth = 116;
            this.DicomPrintTable.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.DicomPrintTable.Location = new System.Drawing.Point(11, 30);
            this.DicomPrintTable.MultiSelect = false;
            this.DicomPrintTable.Name = "DicomPrintTable";
            this.DicomPrintTable.ReadOnly = false;
            this.DicomPrintTable.ShowToolbar = false;
            this.DicomPrintTable.Size = new System.Drawing.Size(624, 212);
            this.DicomPrintTable.SortButtonTooltip = null;
            this.DicomPrintTable.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "选择一个DICOM打印机";
            // 
            // StatusMessage
            // 
            this.StatusMessage.AutoSize = true;
            this.StatusMessage.ForeColor = System.Drawing.Color.Red;
            this.StatusMessage.Location = new System.Drawing.Point(26, 685);
            this.StatusMessage.Name = "StatusMessage";
            this.StatusMessage.Size = new System.Drawing.Size(96, 12);
            this.StatusMessage.TabIndex = 4;
            this.StatusMessage.Text = "StatusMessage";
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(478, 685);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 5;
            this.OkButton.Text = "确定";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(566, 685);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 6;
            this.CancelButton.Text = "取消";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(452, 661);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "胶片数：";
            // 
            // FileCountLabel
            // 
            this.FileCountLabel.AutoSize = true;
            this.FileCountLabel.ForeColor = System.Drawing.Color.Red;
            this.FileCountLabel.Location = new System.Drawing.Point(515, 661);
            this.FileCountLabel.Name = "FileCountLabel";
            this.FileCountLabel.Size = new System.Drawing.Size(12, 12);
            this.FileCountLabel.TabIndex = 9;
            this.FileCountLabel.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(547, 661);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "图像数：";
            // 
            // ImageCountLabel
            // 
            this.ImageCountLabel.AutoSize = true;
            this.ImageCountLabel.ForeColor = System.Drawing.Color.Red;
            this.ImageCountLabel.Location = new System.Drawing.Point(623, 661);
            this.ImageCountLabel.Name = "ImageCountLabel";
            this.ImageCountLabel.Size = new System.Drawing.Size(12, 12);
            this.ImageCountLabel.TabIndex = 9;
            this.ImageCountLabel.Text = "0";
            // 
            // DicomPrinterConfigPanel
            // 
            this.DicomPrinterConfigPanel.Location = new System.Drawing.Point(11, 245);
            this.DicomPrinterConfigPanel.Margin = new System.Windows.Forms.Padding(0);
            this.DicomPrinterConfigPanel.Name = "DicomPrinterConfigPanel";
            this.DicomPrinterConfigPanel.Size = new System.Drawing.Size(624, 407);
            this.DicomPrinterConfigPanel.TabIndex = 10;
            // 
            // ApplicationComponentControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DicomPrinterConfigPanel);
            this.Controls.Add(this.ImageCountLabel);
            this.Controls.Add(this.FileCountLabel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.StatusMessage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DicomPrintTable);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "ApplicationComponentControl";
            this.Size = new System.Drawing.Size(653, 723);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Desktop.View.WinForms.TableView DicomPrintTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label StatusMessage;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label FileCountLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label ImageCountLabel;
        private System.Windows.Forms.Panel DicomPrinterConfigPanel;
    }
}
