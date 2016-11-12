
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
using System.Collections.Generic;
using Macro.Common;
using Macro.Desktop;
using Macro.Dicom.Iod.Modules;

namespace Macro.ImageViewer.Utilities.Print.Dicom
{
    [ExtensionPoint]
    public sealed class DicomPrinterAdvancedConfigurationViewExtensionPoint : ExtensionPoint<IApplicationComponentView>
    {
    }


    [AssociateView(typeof(DicomPrinterAdvancedConfigurationViewExtensionPoint))]
    internal sealed class DicomPrinterAdvancedConfigurationComponent : ApplicationComponent, IDicomPrinterAdvancedConfigurationComponent, IApplicationComponent
    {
        private float HorizontalMargin;
        private readonly List<FilmSize> _standardFilmSizes = new List<FilmSize>(FilmSize.StandardFilmSizes);
        private float VerticalMargin;
        private readonly List<FilmSize> _listFileSize;

        public DicomPrinterAdvancedConfigurationComponent(AutomaticFilmSizeConfiguration filmSizeConfiguration)
        {
            if (filmSizeConfiguration == null)
            {
                this._listFileSize = new List<FilmSize>(_standardFilmSizes);
            }
            else
            {
                FilmSize[] collection = filmSizeConfiguration.FilmSizes;
                this._listFileSize = new List<FilmSize>(collection);
                this.HorizontalMargin = filmSizeConfiguration.LeftMargin + filmSizeConfiguration.RightMargin;
                this.VerticalMargin = filmSizeConfiguration.TopMargin + filmSizeConfiguration.BottomMargin;
            }
        }

        public AutomaticFilmSizeConfiguration CreateAutomaticFilmSizeConfiguration()
        {

            AutomaticFilmSizeConfiguration config = new AutomaticFilmSizeConfiguration();
            FilmSize[] filmsize = _listFileSize.ToArray();
            config.BottomMargin = this.VerticalMargins / 2f;
            config.TopMargin = this.VerticalMargins / 2f;
            config.LeftMargin = this.HorizontalMargins / 2f;
            config.RightMargin = this.HorizontalMargins / 2f;
            return config;
        }
        public IList AutoSelectFilmSizes
        {
            get
            {
                return this._standardFilmSizes;
            }
        }
        public IList AvailableFilmSizes
        {
            get
            {
                return this._listFileSize;
            }
        }
        public override bool HasValidationErrors
        {
            get
            {
                if (base.HasValidationErrors)
                {
                    return true;
                }
                int count = _listFileSize.Count;
                return (count < 1);
            }
        }
        public float HorizontalMargins
        {
            get
            {
                return this.HorizontalMargin;
            }
            set
            {
                if (this.HorizontalMargin != value)
                {
                    this.HorizontalMargin = value;
                    base.NotifyPropertyChanged("HorizontalMargins");
                }
            }
        }
        public float VerticalMargins
        {
            get
            {
                return this.VerticalMargin;
            }
            set
            {
                if (this.VerticalMargin != value)
                {
                    this.VerticalMargin = value;
                    base.NotifyPropertyChanged("VerticalMargins");
                }
            }
        }
    }
}

