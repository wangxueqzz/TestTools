
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
    partial class EditorComponentControl
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
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.PrinterPortText = new System.Windows.Forms.TextBox();
            this.PrinterAETileText = new System.Windows.Forms.TextBox();
            this.PrinterHostText = new System.Windows.Forms.TextBox();
            this.PrinterNametext = new System.Windows.Forms.TextBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.HighResolution = new Macro.Desktop.View.WinForms.NonEmptyNumericUpDown();
            this.StandardResolution = new Macro.Desktop.View.WinForms.NonEmptyNumericUpDown();
            this.AdancedButton = new System.Windows.Forms.Button();
            this.ConfigPanel = new System.Windows.Forms.Panel();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HighResolution)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StandardResolution)).BeginInit();
            this.SuspendLayout();
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(481, 584);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 15;
            this.OkButton.Text = "确定";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(562, 584);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 16;
            this.CancelButton.Text = "取消";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label6);
            this.groupBox7.Controls.Add(this.label4);
            this.groupBox7.Controls.Add(this.label5);
            this.groupBox7.Controls.Add(this.label3);
            this.groupBox7.Controls.Add(this.PrinterPortText);
            this.groupBox7.Controls.Add(this.PrinterAETileText);
            this.groupBox7.Controls.Add(this.PrinterHostText);
            this.groupBox7.Controls.Add(this.PrinterNametext);
            this.groupBox7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox7.ForeColor = System.Drawing.Color.DodgerBlue;
            this.groupBox7.Location = new System.Drawing.Point(13, 13);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(421, 157);
            this.groupBox7.TabIndex = 19;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "DICOM打印机";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(215, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "Port:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(215, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "AETitle:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "IP:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "名字：";
            // 
            // PrinterPortText
            // 
            this.PrinterPortText.Location = new System.Drawing.Point(217, 112);
            this.PrinterPortText.Name = "PrinterPortText";
            this.PrinterPortText.Size = new System.Drawing.Size(119, 21);
            this.PrinterPortText.TabIndex = 0;
            // 
            // PrinterAETileText
            // 
            this.PrinterAETileText.Location = new System.Drawing.Point(217, 45);
            this.PrinterAETileText.Name = "PrinterAETileText";
            this.PrinterAETileText.Size = new System.Drawing.Size(119, 21);
            this.PrinterAETileText.TabIndex = 0;
            // 
            // PrinterHostText
            // 
            this.PrinterHostText.Location = new System.Drawing.Point(5, 112);
            this.PrinterHostText.Name = "PrinterHostText";
            this.PrinterHostText.Size = new System.Drawing.Size(127, 21);
            this.PrinterHostText.TabIndex = 0;
            // 
            // PrinterNametext
            // 
            this.PrinterNametext.Location = new System.Drawing.Point(5, 45);
            this.PrinterNametext.Name = "PrinterNametext";
            this.PrinterNametext.Size = new System.Drawing.Size(127, 21);
            this.PrinterNametext.TabIndex = 0;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label8);
            this.groupBox8.Controls.Add(this.label7);
            this.groupBox8.Controls.Add(this.HighResolution);
            this.groupBox8.Controls.Add(this.StandardResolution);
            this.groupBox8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox8.ForeColor = System.Drawing.Color.DodgerBlue;
            this.groupBox8.Location = new System.Drawing.Point(442, 13);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(195, 157);
            this.groupBox8.TabIndex = 20;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "分辨率 (DPI) ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 92);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 12);
            this.label8.TabIndex = 3;
            this.label8.Text = "最高分辨率：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 12);
            this.label7.TabIndex = 3;
            this.label7.Text = "标准分辨率：";
            // 
            // HighResolution
            // 
            this.HighResolution.Location = new System.Drawing.Point(10, 112);
            this.HighResolution.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.HighResolution.Name = "HighResolution";
            this.HighResolution.Size = new System.Drawing.Size(147, 21);
            this.HighResolution.TabIndex = 2;
            // 
            // StandardResolution
            // 
            this.StandardResolution.Location = new System.Drawing.Point(10, 45);
            this.StandardResolution.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.StandardResolution.Name = "StandardResolution";
            this.StandardResolution.Size = new System.Drawing.Size(147, 21);
            this.StandardResolution.TabIndex = 2;
            // 
            // AdancedButton
            // 
            this.AdancedButton.Location = new System.Drawing.Point(391, 584);
            this.AdancedButton.Name = "AdancedButton";
            this.AdancedButton.Size = new System.Drawing.Size(75, 23);
            this.AdancedButton.TabIndex = 21;
            this.AdancedButton.Text = "高级选项";
            this.AdancedButton.UseVisualStyleBackColor = true;
            this.AdancedButton.Click += new System.EventHandler(this.AdancedButton_Click);
            // 
            // ConfigPanel
            // 
            this.ConfigPanel.Location = new System.Drawing.Point(13, 176);
            this.ConfigPanel.Name = "ConfigPanel";
            this.ConfigPanel.Size = new System.Drawing.Size(624, 408);
            this.ConfigPanel.TabIndex = 22;
            // 
            // EditorComponentControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ConfigPanel);
            this.Controls.Add(this.AdancedButton);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.CancelButton);
            this.Name = "EditorComponentControl";
            this.Size = new System.Drawing.Size(658, 616);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HighResolution)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StandardResolution)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox PrinterPortText;
        private System.Windows.Forms.TextBox PrinterAETileText;
        private System.Windows.Forms.TextBox PrinterHostText;
        private System.Windows.Forms.TextBox PrinterNametext;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private Desktop.View.WinForms.NonEmptyNumericUpDown HighResolution;
        private Desktop.View.WinForms.NonEmptyNumericUpDown StandardResolution;
        private System.Windows.Forms.Button AdancedButton;
        private System.Windows.Forms.Panel ConfigPanel;
    }
}
