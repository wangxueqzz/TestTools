
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
using System.ComponentModel;
using System.Windows.Forms;
using Macro.Desktop.View.WinForms;
using Macro.ImageViewer.Utilities.Print.Dicom;

namespace Macro.ImageViewer.Utilities.Print.View.WinForm
{
    public partial class ConfigurationEditorComponentControl : ApplicationComponentUserControl
    {
        private IDicomPrinterConfigurationEditorComponent _component;
        public ConfigurationEditorComponentControl(IDicomPrinterConfigurationEditorComponent component)
            : base(component)
        {
            InitializeComponent();
            _component = component;

            this.WysiwygRadio.DataBindings.Add("Checked", _component, "Wysiwyg", true,
                                                 DataSourceUpdateMode.Never);
            this.CompleteImageRadio.DataBindings.Add("Checked", _component, "CompleteImage", true,
                                                 DataSourceUpdateMode.Never);
            this.TrueSizeRadio.DataBindings.Add("Checked", _component, "TrueSize", true,
                                                 DataSourceUpdateMode.Never);
            this.GrayscaleRadio.DataBindings.Add("Checked", _component, "Grayscale", true,
                                                 DataSourceUpdateMode.Never);
            this.ColorRadio.DataBindings.Add("Checked", _component, "Color", true,
                                                 DataSourceUpdateMode.Never);
            this.NumberOfCopies.DataBindings.Add("Value", _component, "NumberOfCopies", true,
                                                 DataSourceUpdateMode.OnPropertyChanged);
            this.ConfigInfoText.DataBindings.Add("Text", _component, "ConfigurationInformation", true,
                                                 DataSourceUpdateMode.OnPropertyChanged);

            this.FilmSizeComBox.DataBindings.Clear();
            this.FilmSizeComBox.DataSource = _component.FilmSizeChoices;
            this.FilmSizeComBox.DataBindings.Add("SelectedItem", _component, "FilmSize", true,
                            DataSourceUpdateMode.OnPropertyChanged);

            this.FilmOrientationComBox.DataBindings.Clear();
            this.FilmOrientationComBox.DataSource = _component.FilmOrientationChoices;
            this.FilmOrientationComBox.DataBindings.Add("SelectedItem", _component, "FilmOrientation", true,
                                                 DataSourceUpdateMode.OnPropertyChanged);


            this.FormatComBox.DataBindings.Clear();
            this.FormatComBox.DataSource = _component.ImageDisplayFormatChoices;
            this.FormatComBox.DataBindings.Add("SelectedItem", _component, "ImageDisplayFormat", true,
                                                 DataSourceUpdateMode.Never);

            this.DestinationComBox.DataBindings.Clear();
            this.DestinationComBox.DataSource = _component.FilmDestinationChoices;
            this.DestinationComBox.DataBindings.Add("SelectedItem", _component, "FilmDestination", true,
                                                 DataSourceUpdateMode.OnPropertyChanged);


            this.MediumComBox.DataBindings.Clear();
            this.MediumComBox.DataSource = _component.MediumTypeChoices;
            this.MediumComBox.DataBindings.Add("SelectedItem", _component, "MediumType", true,
                                                 DataSourceUpdateMode.OnPropertyChanged);


            this.RequestResolutionComBox.DataBindings.Clear();
            this.RequestResolutionComBox.DataSource = _component.RequestedResolutionChoices;
            this.RequestResolutionComBox.DataBindings.Add("SelectedItem", _component, "RequestedResolution", true,
                                                 DataSourceUpdateMode.OnPropertyChanged);


            this.BorderDensity.DataBindings.Clear();
            this.BorderDensity.DataSource = _component.BorderDensityChoices;
            this.BorderDensity.DataBindings.Add("SelectedItem", _component, "BorderDensity", true,
                                                 DataSourceUpdateMode.OnPropertyChanged);

            this.ImageDensityComBox.DataBindings.Clear();
            this.ImageDensityComBox.DataSource = _component.EmptyImageDensityChoices;
            this.ImageDensityComBox.DataBindings.Add("SelectedItem", _component, "EmptyImageDensity", true,
                                                 DataSourceUpdateMode.OnPropertyChanged);

            this.MangnificationComBox.DataBindings.Clear();
            this.MangnificationComBox.DataSource = _component.MagnificationTypeChoices;
            this.MangnificationComBox.DataBindings.Add("SelectedItem", _component, "MagnificationType", true,
                                                 DataSourceUpdateMode.OnPropertyChanged);

            this.PriorityComBox.DataBindings.Clear();
            this.PriorityComBox.DataSource = _component.PrintPriorityChoices;
            this.PriorityComBox.DataBindings.Add("SelectedItem", _component, "PrintPriority", true,
                                                 DataSourceUpdateMode.OnPropertyChanged);


            _component.localPropertyChanged += PropertyChanged;
            this.FilmSizeComBox.SelectedValueChanged += VerityEnabled;
        }

        private void PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            this.PropertyValueChanged(args.PropertyName);
        }

        private void VerityEnabled(object sender, EventArgs args)
        {

        }


        private void PropertyValueChanged(string PropertyName)
        {
            bool flag = string.IsNullOrEmpty(PropertyName);
            if (!flag)
            {
                switch (PropertyName)
                {
                    case "FilmSize":

                        this.FilmSizeComBox.DataBindings.Clear();
                        this.FilmSizeComBox.DataSource = _component.FilmSizeChoices;
                        this.FilmSizeComBox.DataBindings.Add("SelectedItem", _component, "FilmSize", true,
                                        DataSourceUpdateMode.OnPropertyChanged);
                        break;
                    case "FilmOrientation":
                        this.FilmOrientationComBox.DataBindings.Clear();
                        this.FilmOrientationComBox.DataSource = _component.FilmOrientationChoices;
                        this.FilmOrientationComBox.DataBindings.Add("SelectedItem", _component, "FilmOrientation", true,
                                                             DataSourceUpdateMode.OnPropertyChanged);
                        break;
                    case "ImageDisplayFormat":
                        this.FormatComBox.DataBindings.Clear();
                        this.FormatComBox.DataSource = _component.ImageDisplayFormatChoices;
                        this.FormatComBox.DataBindings.Add("SelectedItem", _component, "ImageDisplayFormat", true,
                                                             DataSourceUpdateMode.Never);
                        break;
                    case "FilmDestination":
                        this.DestinationComBox.DataBindings.Clear();
                        this.DestinationComBox.DataSource = _component.FilmDestinationChoices;
                        this.DestinationComBox.DataBindings.Add("SelectedItem", _component, "FilmDestination", true,
                                                             DataSourceUpdateMode.OnPropertyChanged);
                        break;
                    case "MediumType":

                        this.MediumComBox.DataBindings.Clear();
                        this.MediumComBox.DataSource = _component.MediumTypeChoices;
                        this.MediumComBox.DataBindings.Add("SelectedItem", _component, "MediumType", true,
                                                             DataSourceUpdateMode.OnPropertyChanged);
                        break;
                    case "RequestedResolution":

                        this.RequestResolutionComBox.DataBindings.Clear();
                        this.RequestResolutionComBox.DataSource = _component.RequestedResolutionChoices;
                        this.RequestResolutionComBox.DataBindings.Add("SelectedItem", _component, "RequestedResolution", true,
                                                             DataSourceUpdateMode.OnPropertyChanged);
                        break;
                    case "BorderDensity":

                        this.BorderDensity.DataBindings.Clear();
                        this.BorderDensity.DataSource = _component.BorderDensityChoices;
                        this.BorderDensity.DataBindings.Add("SelectedItem", _component, "BorderDensity", true,
                                                             DataSourceUpdateMode.OnPropertyChanged);
                        break;
                    case "EmptyImageDensity":
                        this.ImageDensityComBox.DataBindings.Clear();
                        this.ImageDensityComBox.DataSource = _component.EmptyImageDensityChoices;
                        this.ImageDensityComBox.DataBindings.Add("SelectedItem", _component, "EmptyImageDensity", true,
                                                             DataSourceUpdateMode.OnPropertyChanged);
                        break;
                    case "MagnificationType":
                        this.MangnificationComBox.DataBindings.Clear();
                        this.MangnificationComBox.DataSource = _component.MagnificationTypeChoices;
                        this.MangnificationComBox.DataBindings.Add("SelectedItem", _component, "MagnificationType", true,
                                                             DataSourceUpdateMode.OnPropertyChanged);
                        break;
                    case "PrintPriority":
                        this.PriorityComBox.DataBindings.Clear();
                        this.PriorityComBox.DataSource = _component.PrintPriorityChoices;
                        this.PriorityComBox.DataBindings.Add("SelectedItem", _component, "PrintPriority", true,
                                                             DataSourceUpdateMode.OnPropertyChanged);
                        break;
                }
            }
        }

        private void WysiwygRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (WysiwygRadio.Checked)
            {
                _component.Wysiwyg=true;
            }
        }

        private void CompleteImageRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (CompleteImageRadio.Checked)
            {
                _component.CompleteImage = true;
            }
        }

        private void TrueSizeRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (TrueSizeRadio.Checked)
            {
                _component.TrueSize = true;
            }
        }

        private void GrayscaleRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (GrayscaleRadio.Checked)
            {
                _component.Grayscale = true;
            }
        }

        private void ColorRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (ColorRadio.Checked)
            {
                _component.Color = true;
            }
        }
        
        private void FormatComBox_DropDownClosed(object sender, EventArgs e)
        {
            _component.ImageDisplayFormat = (PrinterImageDisplayFormat)this.FormatComBox.SelectedItem;
        }
    }
}
