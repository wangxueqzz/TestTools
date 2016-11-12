
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
    partial class SummaryComponentControl
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
            this.SuspendLayout();
            // 
            // DicomPrintTable
            // 
            this.DicomPrintTable.ColumnHeaderTooltip = null;
            this.DicomPrintTable.Location = new System.Drawing.Point(0, 4);
            this.DicomPrintTable.Name = "DicomPrintTable";
            this.DicomPrintTable.ReadOnly = false;
            this.DicomPrintTable.Size = new System.Drawing.Size(414, 210);
            this.DicomPrintTable.SortButtonTooltip = null;
            this.DicomPrintTable.TabIndex = 0;
            // 
            // SummaryComponentControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DicomPrintTable);
            this.Name = "SummaryComponentControl";
            this.Size = new System.Drawing.Size(527, 219);
            this.ResumeLayout(false);

        }

        #endregion

        private Desktop.View.WinForms.TableView DicomPrintTable;

    }
}
