
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
    partial class ConfigurationEditorComponentControl
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
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.TrueSizeRadio = new System.Windows.Forms.RadioButton();
            this.CompleteImageRadio = new System.Windows.Forms.RadioButton();
            this.WysiwygRadio = new System.Windows.Forms.RadioButton();
            this.NumberOfCopies = new Macro.Desktop.View.WinForms.NonEmptyNumericUpDown();
            this.GrayscaleRadio = new System.Windows.Forms.RadioButton();
            this.ColorRadio = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.ConfigInfoText = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.MangnificationComBox = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.ImageDensityComBox = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.BorderDensity = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RequestResolutionComBox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.MediumComBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.DestinationComBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.PriorityComBox = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.FormatComBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.FilmOrientationComBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.FilmSizeComBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumberOfCopies)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.TrueSizeRadio);
            this.groupBox5.Controls.Add(this.CompleteImageRadio);
            this.groupBox5.Controls.Add(this.WysiwygRadio);
            this.groupBox5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox5.ForeColor = System.Drawing.Color.DodgerBlue;
            this.groupBox5.Location = new System.Drawing.Point(204, 197);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(193, 188);
            this.groupBox5.TabIndex = 25;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "缩放选项：";
            // 
            // TrueSizeRadio
            // 
            this.TrueSizeRadio.AutoSize = true;
            this.TrueSizeRadio.Location = new System.Drawing.Point(14, 130);
            this.TrueSizeRadio.Name = "TrueSizeRadio";
            this.TrueSizeRadio.Size = new System.Drawing.Size(86, 16);
            this.TrueSizeRadio.TabIndex = 0;
            this.TrueSizeRadio.TabStop = true;
            this.TrueSizeRadio.Text = "True Size";
            this.TrueSizeRadio.UseVisualStyleBackColor = true;
            this.TrueSizeRadio.CheckedChanged += new System.EventHandler(this.TrueSizeRadio_CheckedChanged);
            // 
            // CompleteImageRadio
            // 
            this.CompleteImageRadio.AutoSize = true;
            this.CompleteImageRadio.Location = new System.Drawing.Point(14, 92);
            this.CompleteImageRadio.Name = "CompleteImageRadio";
            this.CompleteImageRadio.Size = new System.Drawing.Size(49, 16);
            this.CompleteImageRadio.TabIndex = 0;
            this.CompleteImageRadio.TabStop = true;
            this.CompleteImageRadio.Text = "标准";
            this.CompleteImageRadio.UseVisualStyleBackColor = true;
            this.CompleteImageRadio.CheckedChanged += new System.EventHandler(this.CompleteImageRadio_CheckedChanged);
            // 
            // WysiwygRadio
            // 
            this.WysiwygRadio.AutoSize = true;
            this.WysiwygRadio.Location = new System.Drawing.Point(14, 51);
            this.WysiwygRadio.Name = "WysiwygRadio";
            this.WysiwygRadio.Size = new System.Drawing.Size(72, 16);
            this.WysiwygRadio.TabIndex = 0;
            this.WysiwygRadio.TabStop = true;
            this.WysiwygRadio.Text = "WYSIWYG";
            this.WysiwygRadio.UseVisualStyleBackColor = true;
            this.WysiwygRadio.CheckedChanged += new System.EventHandler(this.WysiwygRadio_CheckedChanged);
            // 
            // NumberOfCopies
            // 
            this.NumberOfCopies.Location = new System.Drawing.Point(5, 116);
            this.NumberOfCopies.Name = "NumberOfCopies";
            this.NumberOfCopies.Size = new System.Drawing.Size(140, 21);
            this.NumberOfCopies.TabIndex = 2;
            // 
            // GrayscaleRadio
            // 
            this.GrayscaleRadio.AutoSize = true;
            this.GrayscaleRadio.Location = new System.Drawing.Point(9, 38);
            this.GrayscaleRadio.Name = "GrayscaleRadio";
            this.GrayscaleRadio.Size = new System.Drawing.Size(49, 16);
            this.GrayscaleRadio.TabIndex = 0;
            this.GrayscaleRadio.TabStop = true;
            this.GrayscaleRadio.Text = "灰度";
            this.GrayscaleRadio.UseVisualStyleBackColor = true;
            this.GrayscaleRadio.CheckedChanged += new System.EventHandler(this.GrayscaleRadio_CheckedChanged);
            // 
            // ColorRadio
            // 
            this.ColorRadio.AutoSize = true;
            this.ColorRadio.Location = new System.Drawing.Point(9, 92);
            this.ColorRadio.Name = "ColorRadio";
            this.ColorRadio.Size = new System.Drawing.Size(49, 16);
            this.ColorRadio.TabIndex = 0;
            this.ColorRadio.TabStop = true;
            this.ColorRadio.Text = "彩色";
            this.ColorRadio.UseVisualStyleBackColor = true;
            this.ColorRadio.CheckedChanged += new System.EventHandler(this.ColorRadio_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label1.Location = new System.Drawing.Point(397, 341);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 12);
            this.label1.TabIndex = 31;
            this.label1.Text = "配置信息：";
            // 
            // ConfigInfoText
            // 
            this.ConfigInfoText.Location = new System.Drawing.Point(399, 364);
            this.ConfigInfoText.Name = "ConfigInfoText";
            this.ConfigInfoText.Size = new System.Drawing.Size(153, 21);
            this.ConfigInfoText.TabIndex = 30;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.MangnificationComBox);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.ImageDensityComBox);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.BorderDensity);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.ForeColor = System.Drawing.Color.DodgerBlue;
            this.groupBox3.Location = new System.Drawing.Point(399, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(197, 188);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "图像选项";
            // 
            // MangnificationComBox
            // 
            this.MangnificationComBox.FormattingEnabled = true;
            this.MangnificationComBox.Location = new System.Drawing.Point(9, 149);
            this.MangnificationComBox.Name = "MangnificationComBox";
            this.MangnificationComBox.Size = new System.Drawing.Size(144, 20);
            this.MangnificationComBox.TabIndex = 5;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 134);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(57, 12);
            this.label11.TabIndex = 4;
            this.label11.Text = "放大率：";
            // 
            // ImageDensityComBox
            // 
            this.ImageDensityComBox.FormattingEnabled = true;
            this.ImageDensityComBox.Location = new System.Drawing.Point(9, 96);
            this.ImageDensityComBox.Name = "ImageDensityComBox";
            this.ImageDensityComBox.Size = new System.Drawing.Size(144, 20);
            this.ImageDensityComBox.TabIndex = 5;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 81);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 12);
            this.label10.TabIndex = 4;
            this.label10.Text = "图像密度：";
            // 
            // BorderDensity
            // 
            this.BorderDensity.FormattingEnabled = true;
            this.BorderDensity.Location = new System.Drawing.Point(9, 44);
            this.BorderDensity.Name = "BorderDensity";
            this.BorderDensity.Size = new System.Drawing.Size(144, 20);
            this.BorderDensity.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 12);
            this.label9.TabIndex = 4;
            this.label9.Text = "边框密度:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RequestResolutionComBox);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.MediumComBox);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.DestinationComBox);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.ForeColor = System.Drawing.Color.DodgerBlue;
            this.groupBox2.Location = new System.Drawing.Point(204, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(193, 188);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "胶片/打印质量";
            // 
            // RequestResolutionComBox
            // 
            this.RequestResolutionComBox.FormattingEnabled = true;
            this.RequestResolutionComBox.Location = new System.Drawing.Point(11, 149);
            this.RequestResolutionComBox.Name = "RequestResolutionComBox";
            this.RequestResolutionComBox.Size = new System.Drawing.Size(124, 20);
            this.RequestResolutionComBox.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 134);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 12);
            this.label8.TabIndex = 4;
            this.label8.Text = "分辨率：";
            // 
            // MediumComBox
            // 
            this.MediumComBox.FormattingEnabled = true;
            this.MediumComBox.Location = new System.Drawing.Point(11, 96);
            this.MediumComBox.Name = "MediumComBox";
            this.MediumComBox.Size = new System.Drawing.Size(124, 20);
            this.MediumComBox.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 81);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 12);
            this.label7.TabIndex = 4;
            this.label7.Text = " 媒介：";
            // 
            // DestinationComBox
            // 
            this.DestinationComBox.FormattingEnabled = true;
            this.DestinationComBox.Location = new System.Drawing.Point(11, 44);
            this.DestinationComBox.Name = "DestinationComBox";
            this.DestinationComBox.Size = new System.Drawing.Size(124, 20);
            this.DestinationComBox.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "打印目标：";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.GrayscaleRadio);
            this.groupBox6.Controls.Add(this.ColorRadio);
            this.groupBox6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox6.ForeColor = System.Drawing.Color.DodgerBlue;
            this.groupBox6.Location = new System.Drawing.Point(399, 197);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(197, 137);
            this.groupBox6.TabIndex = 24;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "颜色模式";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.PriorityComBox);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.NumberOfCopies);
            this.groupBox4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox4.ForeColor = System.Drawing.Color.DodgerBlue;
            this.groupBox4.Location = new System.Drawing.Point(1, 197);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(197, 188);
            this.groupBox4.TabIndex = 26;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "打印队列选项";
            // 
            // PriorityComBox
            // 
            this.PriorityComBox.FormattingEnabled = true;
            this.PriorityComBox.Location = new System.Drawing.Point(9, 37);
            this.PriorityComBox.Name = "PriorityComBox";
            this.PriorityComBox.Size = new System.Drawing.Size(136, 20);
            this.PriorityComBox.TabIndex = 5;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(57, 12);
            this.label12.TabIndex = 4;
            this.label12.Text = "优先级：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "份数：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.FormatComBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.FilmOrientationComBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.FilmSizeComBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.groupBox1.Location = new System.Drawing.Point(1, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(197, 188);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "布局";
            // 
            // FormatComBox
            // 
            this.FormatComBox.FormattingEnabled = true;
            this.FormatComBox.Location = new System.Drawing.Point(9, 149);
            this.FormatComBox.Name = "FormatComBox";
            this.FormatComBox.Size = new System.Drawing.Size(136, 20);
            this.FormatComBox.TabIndex = 5;
            this.FormatComBox.DropDownClosed += new System.EventHandler(this.FormatComBox_DropDownClosed);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "分格：";
            // 
            // FilmOrientationComBox
            // 
            this.FilmOrientationComBox.FormattingEnabled = true;
            this.FilmOrientationComBox.Location = new System.Drawing.Point(9, 96);
            this.FilmOrientationComBox.Name = "FilmOrientationComBox";
            this.FilmOrientationComBox.Size = new System.Drawing.Size(136, 20);
            this.FilmOrientationComBox.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "胶片方向：";
            // 
            // FilmSizeComBox
            // 
            this.FilmSizeComBox.FormattingEnabled = true;
            this.FilmSizeComBox.Location = new System.Drawing.Point(9, 44);
            this.FilmSizeComBox.Name = "FilmSizeComBox";
            this.FilmSizeComBox.Size = new System.Drawing.Size(136, 20);
            this.FilmSizeComBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "胶片大小：";
            // 
            // ConfigurationEditorComponentControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ConfigInfoText);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ConfigurationEditorComponentControl";
            this.Size = new System.Drawing.Size(604, 391);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumberOfCopies)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton TrueSizeRadio;
        private System.Windows.Forms.RadioButton CompleteImageRadio;
        private System.Windows.Forms.RadioButton WysiwygRadio;
        private Desktop.View.WinForms.NonEmptyNumericUpDown NumberOfCopies;
        private System.Windows.Forms.RadioButton GrayscaleRadio;
        private System.Windows.Forms.RadioButton ColorRadio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ConfigInfoText;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox FilmSizeComBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox FormatComBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox FilmOrientationComBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox DestinationComBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox MediumComBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox RequestResolutionComBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox BorderDensity;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox ImageDensityComBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox MangnificationComBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox PriorityComBox;
        private System.Windows.Forms.Label label12;

    }
}
