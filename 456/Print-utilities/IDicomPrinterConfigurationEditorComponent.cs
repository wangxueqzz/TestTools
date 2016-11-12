
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


using System.Collections;
using System.ComponentModel;
using Macro.Desktop;
using Macro.Dicom.Iod.Modules;

namespace Macro.ImageViewer.Utilities.Print.Dicom
{

    public interface IDicomPrinterConfigurationEditorComponent : IApplicationComponent
    {
        event PropertyChangedEventHandler localPropertyChanged;
        void ShowAdvancedConfigurationOptions();
        BorderDensity BorderDensity { get; set; }
        IList BorderDensityChoices { get; }
        bool Color { get; set; }
        bool CompleteImage { get; set; }
        bool ConfigurationEnabled { get; }
        string ConfigurationInformation { get; set; }
        EmptyImageDensity EmptyImageDensity { get; set; }
        IList EmptyImageDensityChoices { get; }
        FilmDestination FilmDestination { get; set; }
        IList FilmDestinationChoices { get; }
        FilmOrientation FilmOrientation { get; set; }
        IList FilmOrientationChoices { get; }
        PrinterFilmSize FilmSize { get; set; }
        IList FilmSizeChoices { get; }
        bool Grayscale { get; set; }
        PrinterImageDisplayFormat ImageDisplayFormat { get; set; }
        IList ImageDisplayFormatChoices { get; }
        MagnificationType MagnificationType { get; set; }
        IList MagnificationTypeChoices { get; }
        MediumType MediumType { get; set; }
        IList MediumTypeChoices { get; }
        int NumberOfCopies { get; set; }
        PrintPriority PrintPriority { get; set; }
        IList PrintPriorityChoices { get; }
        RequestedResolution RequestedResolution { get; set; }
        IList RequestedResolutionChoices { get; }
        bool TrueSize { get; set; }
        bool Wysiwyg { get; set; }
    }
}

