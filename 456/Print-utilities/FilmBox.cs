
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
using Macro.Dicom.Iod.Modules;

namespace Macro.ImageViewer.Utilities.Print.Dicom
{

    public class FilmBox : ICloneable
    {
        private AutomaticFilmSizeConfiguration _automaticFilmSizeConfiguration;
        private BorderDensity _borderDensity;
        private string _configurationInformation;
        private EmptyImageDensity _emptyImageDensity;
        private FilmOrientation _filmOrientation;
        private PrinterFilmSize _filmSize;
        private PrinterImageDisplayFormat _imageDisplayFormat;
        private MagnificationType _magnificationType;
        private RequestedResolution _requestedResolution;

        public FilmBox()
        {
            _imageDisplayFormat = PrinterImageDisplayFormat.Default;
            _filmOrientation = FilmOrientation.Portrait;
            _borderDensity = BorderDensity.None;
            _emptyImageDensity = EmptyImageDensity.None;
            _magnificationType = MagnificationType.None;
            _filmSize = PrinterFilmSize.Default;
            _automaticFilmSizeConfiguration = new AutomaticFilmSizeConfiguration();
            _configurationInformation = null;
            _requestedResolution = RequestedResolution.Standard;
        }

        public FilmBox(PrinterImageDisplayFormat imageDisplayFormat, FilmOrientation filmOrientation, BorderDensity borderDensity, EmptyImageDensity emptyImageDensity, MagnificationType magnificationType, PrinterFilmSize filmSize, AutomaticFilmSizeConfiguration automaticFilmSizeConfiguration, string configurationInformation, RequestedResolution requestedResolution)
        {
            _imageDisplayFormat = imageDisplayFormat;
            _filmOrientation = filmOrientation;
            _borderDensity = borderDensity;
            _emptyImageDensity = emptyImageDensity;
            _magnificationType = magnificationType;
            _filmSize = filmSize;
            _automaticFilmSizeConfiguration = automaticFilmSizeConfiguration;
            _configurationInformation = configurationInformation;
            _requestedResolution = requestedResolution;
        }

        public object Clone()
        {
            FilmBox box = new FilmBox();
            box.ImageDisplayFormat = this.ImageDisplayFormat;
            box.FilmOrientation = this.FilmOrientation;
            box.BorderDensity = this.BorderDensity;
            box.EmptyImageDensity = this.EmptyImageDensity;
            box.MagnificationType = this.MagnificationType;
            box.FilmSize = this.FilmSize;
            box.AutomaticFilmSizeConfiguration = this.AutomaticFilmSizeConfiguration;
            box.ConfigurationInformation = this.ConfigurationInformation;
            box.RequestedResolution = this.RequestedResolution;

            return box;
        }

        /// <summary>
        /// 当FilmSize为空时，使用这个获得FilmSize
        /// </summary>
        public AutomaticFilmSizeConfiguration AutomaticFilmSizeConfiguration
        {
            get { return _automaticFilmSizeConfiguration; }
            set { _automaticFilmSizeConfiguration = value; }
        }
        public BorderDensity BorderDensity
        {
            get { return _borderDensity; }
            set { _borderDensity = value; }
        }
        public string ConfigurationInformation
        {
            get { return _configurationInformation; }
            set { _configurationInformation = value; }
        }
        public EmptyImageDensity EmptyImageDensity
        {
            get { return _emptyImageDensity; }
            set { _emptyImageDensity = value; }
        }
        public FilmOrientation FilmOrientation
        {
            get { return _filmOrientation; }
            set { _filmOrientation = value; }
        }
        public PrinterFilmSize FilmSize
        {
            get { return _filmSize; }
            set { _filmSize = value; }
        }
        public PrinterImageDisplayFormat ImageDisplayFormat
        {
            get { return _imageDisplayFormat; }
            set { _imageDisplayFormat = value; }
        }
        public MagnificationType MagnificationType
        {
            get { return _magnificationType; }
            set { _magnificationType = value; }
        }
        public RequestedResolution RequestedResolution
        {
            get { return _requestedResolution; }
            set { _requestedResolution = value; }
        }
    }
}

