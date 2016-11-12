
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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Macro.Common;
using Macro.Common.Utilities;
using Macro.Desktop;
using Macro.Dicom.Iod.Modules;
using Macro.Dicom.Network.Scu;

namespace Macro.ImageViewer.Utilities.Print.Dicom
{
    [ExtensionPoint]
    public sealed class DicomPrinterConfigurationEditorViewExtensionPoint : ExtensionPoint<IApplicationComponentView>
    {
    }

    [AssociateView(typeof(DicomPrinterConfigurationEditorViewExtensionPoint))]
    public sealed class DicomPrinterConfigurationEditorComponent : ApplicationComponent, IDicomPrinterConfigurationEditorComponent
    {
        private DicomPrinter.Configuration _printerConfig;

        private void PresentationModeChanged()
        {
            //LocalNotifyPropertyChanged("TrueSize");
            //LocalNotifyPropertyChanged("Wysiwyg");
            //LocalNotifyPropertyChanged("CompleteImage");
        }

        private void ColorModeChanged()
        {

            //LocalNotifyPropertyChanged("Color");
            //LocalNotifyPropertyChanged("Grayscale");
        }

        private void ConfigurationChanged()
        {


            this.PresentationModeChanged();
            this.ColorModeChanged();
            LocalNotifyPropertyChanged("BorderDensity");
            //LocalNotifyPropertyChanged("BorderDensityChoices");
            LocalNotifyPropertyChanged("Configuration");
            LocalNotifyPropertyChanged("ConfigurationEnabled");
            LocalNotifyPropertyChanged("ConfigurationInformation");
            LocalNotifyPropertyChanged("EmptyImageDensity");
            //LocalNotifyPropertyChanged("EmptyImageDensityChoices");
            LocalNotifyPropertyChanged("FilmDestination");
            //LocalNotifyPropertyChanged("FilmDestinationChoices");
            LocalNotifyPropertyChanged("FilmSize");
            //LocalNotifyPropertyChanged("FilmSizeChoices");
            LocalNotifyPropertyChanged("ImageDisplayFormat");
            //LocalNotifyPropertyChanged("ImageDisplayFormatChoices");
            LocalNotifyPropertyChanged("MagnificationType");
            //LocalNotifyPropertyChanged("MagnificationTypeChoices");
            LocalNotifyPropertyChanged("MediumType");
            //LocalNotifyPropertyChanged("MediumTypeChoices");
            LocalNotifyPropertyChanged("NumberOfCopies");
            LocalNotifyPropertyChanged("PrintPriority");
            //LocalNotifyPropertyChanged("PrintPriorityChoices");
            LocalNotifyPropertyChanged("RequestedResolution");
            //LocalNotifyPropertyChanged("RequestedResolutionChoices");
            LocalNotifyPropertyChanged("FilmOrientation");
            //LocalNotifyPropertyChanged("FilmOrientationChoices");
        }

        public void ShowAdvancedConfigurationOptions()
        {
            try
            {
                if (_printerConfig != null)
                {
                    DicomPrinterAdvancedConfigurationComponent component = new DicomPrinterAdvancedConfigurationComponent(_printerConfig.FilmBox.AutomaticFilmSizeConfiguration);
                    DialogBoxAction action = base.Host.DesktopWindow.ShowDialogBox(new SimpleComponentContainer(component), "Advanced Configuration");
                    if (action == DialogBoxAction.Ok)
                    {
                        AutomaticFilmSizeConfiguration objB = component.CreateAutomaticFilmSizeConfiguration();
                        AutomaticFilmSizeConfiguration objA = _printerConfig.FilmBox.AutomaticFilmSizeConfiguration;
                        if (!object.Equals(objA, objB))
                        {
                            _printerConfig.FilmBox.AutomaticFilmSizeConfiguration = objB;
                            this.Modified = true;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Platform.Log(Macro.Common.LogLevel.Debug, exception);
            }
        }

        public BorderDensity BorderDensity
        {
            get
            {
                if (_printerConfig.FilmBox.BorderDensity == BorderDensity.None)
                {
                    return BorderDensity.None;
                }
                return _printerConfig.FilmBox.BorderDensity;
            }
            set
            {
                if (_printerConfig.FilmBox.BorderDensity != value)
                {
                    _printerConfig.FilmBox.BorderDensity = value;
                    this.Modified = true;
                    //LocalNotifyPropertyChanged("BorderDensity");
                }
            }
        }

        public IList BorderDensityChoices
        {
            get
            {
                Type enumType = typeof(BorderDensity);
                return Enum.GetValues(enumType);
            }
        }

        public bool Color
        {
            get
            {
                return (_printerConfig.ColorMode == ColorMode.Color);
            }
            set
            {
                _printerConfig.ColorMode = ColorMode.Color;
                this.Modified = true;
                this.ColorModeChanged();
            }
        }

        public bool Grayscale
        {
            get
            {
                return _printerConfig.ColorMode == ColorMode.Grayscale;
            }
            set
            {
                _printerConfig.ColorMode = ColorMode.Grayscale;
                this.Modified = true;
            }
        }

        public bool CompleteImage
        {
            get
            {
                return (_printerConfig.PresentationMode == PresentationMode.CompleteImage);
            }
            set
            {
                _printerConfig.PresentationMode = PresentationMode.CompleteImage;
                this.Modified = true;
                PresentationModeChanged();
            }
        }

        public bool TrueSize
        {
            get
            {
                return (_printerConfig.PresentationMode == PresentationMode.TrueSize);
            }
            set
            {
                _printerConfig.PresentationMode = PresentationMode.TrueSize;
                this.Modified = true;
                PresentationModeChanged();
            }
        }

        public bool Wysiwyg
        {
            get
            {
                return (_printerConfig.PresentationMode == PresentationMode.Wysiwyg);
            }
            set
            {

                _printerConfig.PresentationMode = PresentationMode.Wysiwyg;
                this.Modified = true;
                PresentationModeChanged();
            }
        }

        public DicomPrinter.Configuration Configuration
        {
            get
            {
                return _printerConfig;
            }
            set
            {
                _printerConfig = value;
                this.Modified = false;
                this.ConfigurationChanged();
            }
        }

        public bool ConfigurationEnabled
        {
            get
            {
                return (this._printerConfig != null);
            }
        }

        public string ConfigurationInformation
        {
            get
            {
                if (_printerConfig.FilmBox.ConfigurationInformation == null)
                {
                    return null;
                }

                return _printerConfig.FilmBox.ConfigurationInformation;
            }
            set
            {
                if (_printerConfig.FilmBox.ConfigurationInformation != value)
                {
                    _printerConfig.FilmBox.ConfigurationInformation = value;
                    this.Modified = true;
                    LocalNotifyPropertyChanged("ConfigurationInformation");
                }
            }
        }

        public EmptyImageDensity EmptyImageDensity
        {
            get
            {
                if (_printerConfig.FilmBox.EmptyImageDensity == EmptyImageDensity.None)
                {
                    return EmptyImageDensity.None;
                }

                return _printerConfig.FilmBox.EmptyImageDensity;
            }
            set
            {
                if (_printerConfig.FilmBox.EmptyImageDensity != value)
                {
                    _printerConfig.FilmBox.EmptyImageDensity = value;
                    this.Modified = true;
                    //LocalNotifyPropertyChanged("EmptyImageDensity");
                }
            }
        }

        public IList EmptyImageDensityChoices
        {
            get
            {
                Type enumType = typeof(EmptyImageDensity);
                return Enum.GetValues(enumType);
            }
        }

        public FilmDestination FilmDestination
        {
            get
            {
                if (_printerConfig.Session.FilmDestination == FilmDestination.None)
                {
                    return FilmDestination.None;
                }

                return _printerConfig.Session.FilmDestination;
            }
            set
            {
                if (_printerConfig.Session.FilmDestination != value)
                {
                    _printerConfig.Session.FilmDestination = value;
                    this.Modified = true;
                    // LocalNotifyPropertyChanged("FilmDestination");
                }
            }
        }

        public IList FilmDestinationChoices
        {
            get
            {
                Type enumType = typeof(FilmDestination);
                return Enum.GetValues(enumType);
            }
        }

        public FilmOrientation FilmOrientation
        {
            get
            {
                if (_printerConfig.FilmBox.FilmOrientation == FilmOrientation.None)
                {
                    return FilmOrientation.None;
                }
                return _printerConfig.FilmBox.FilmOrientation;
            }
            set
            {
                if (_printerConfig.FilmBox.FilmOrientation != value)
                {
                    _printerConfig.FilmBox.FilmOrientation = value;
                    //LocalNotifyPropertyChanged("FilmOrientation");
                }
            }
        }

        public IList FilmOrientationChoices
        {
            get
            {
                FilmOrientationCollection gih = new FilmOrientationCollection
                {
                    FilmOrientationList = new List<FilmOrientation>()
                };
                Type enumType = typeof(FilmOrientation);
                CollectionUtils.ForEach<FilmOrientation>(Enum.GetValues(enumType), new Action<FilmOrientation>(gih.Add));
                return gih.FilmOrientationList;
            }
        }

        public PrinterFilmSize FilmSize
        {
            get
            {
                if (_printerConfig.FilmBox.FilmSize == PrinterFilmSize.Default)
                {
                    return PrinterFilmSize.Default;
                }

                return _printerConfig.FilmBox.FilmSize;
            }
            set
            {
                if (_printerConfig.FilmBox.FilmSize != value)
                {
                    _printerConfig.FilmBox.FilmSize = value;
                    // LocalNotifyPropertyChanged("FilmSize");
                    this.Modified = true;
                }
            }
        }

        public IList FilmSizeChoices
        {
            get
            {
                List<PrinterFilmSize> list = new List<PrinterFilmSize>(PrinterFilmSize.Options);
                if (_printerConfig.FilmBox.FilmSize != null)
                {
                    PrinterFilmSize item = _printerConfig.FilmBox.FilmSize;
                    if (!list.Contains(item))
                    {
                        list.Add(item);
                    }
                }
                return list;
            }
        }

        public PrinterImageDisplayFormat ImageDisplayFormat
        {
            get
            {
                if (_printerConfig.FilmBox.ImageDisplayFormat == PrinterImageDisplayFormat.Default)
                {
                    return PrinterImageDisplayFormat.Default;
                }

                return _printerConfig.FilmBox.ImageDisplayFormat;
            }
            set
            {
                if (_printerConfig.FilmBox.ImageDisplayFormat != value)
                {
                    _printerConfig.FilmBox.ImageDisplayFormat = value;
                    this.Modified = true;
                    LocalNotifyPropertyChanged("ImageDisplayFormat");
                }
            }
        }

        public IList ImageDisplayFormatChoices
        {
            get
            {
                List<PrinterImageDisplayFormat> list = new List<PrinterImageDisplayFormat>(PrinterImageDisplayFormat.Options);

                if (_printerConfig.FilmBox.ImageDisplayFormat != null)
                {
                    ImageDisplayFormatCollection fih = new ImageDisplayFormatCollection
                        {
                            EditorComponent = this
                        };
                    fih.ImageDisplay = _printerConfig.FilmBox.ImageDisplayFormat;
                    PrinterImageDisplayFormat item = CollectionUtils.SelectFirst(list,
                                                                                 new Predicate
                                                                                     <PrinterImageDisplayFormat>(
                                                                                     fih.Equals));
                    if (item == null)
                    {
                        list.Insert(0, fih.ImageDisplay);
                        return list;
                    }
                    int num = list.IndexOf(item);
                    list.Remove(item);
                    list.Insert(num, fih.ImageDisplay);
                }
                return list;
            }
        }

        public MagnificationType MagnificationType
        {
            get
            {
                if (_printerConfig.FilmBox.MagnificationType == MagnificationType.None)
                {
                    return MagnificationType.None;
                }
                return _printerConfig.FilmBox.MagnificationType;
            }
            set
            {
                if (_printerConfig.FilmBox.MagnificationType != value)
                {
                    _printerConfig.FilmBox.MagnificationType = value;
                    this.Modified = true;
                    // LocalNotifyPropertyChanged("MagnificationType");
                }
            }
        }

        public IList MagnificationTypeChoices
        {
            get
            {
                return Enum.GetValues(typeof(MagnificationType));
            }
        }

        public MediumType MediumType
        {
            get
            {
                if (_printerConfig.Session.MediumType == MediumType.None)
                {
                    return MediumType.None;
                }
                return _printerConfig.Session.MediumType;
            }
            set
            {
                if (_printerConfig.Session.MediumType != value)
                {
                    _printerConfig.Session.MediumType = value;
                    this.Modified = true;
                    // LocalNotifyPropertyChanged("MediumType");
                }
            }
        }

        public IList MediumTypeChoices
        {
            get
            {
                return Enum.GetValues(typeof(MediumType));
            }
        }

        public int NumberOfCopies
        {
            get
            {
                return _printerConfig.Session.NumberOfCopies;
            }
            set
            {
                if (_printerConfig.Session.NumberOfCopies != value)
                {
                    _printerConfig.Session.NumberOfCopies = value;
                    this.Modified = true;
                    //LocalNotifyPropertyChanged("NumberOfCopies");
                }
            }
        }

        public PrintPriority PrintPriority
        {
            get
            {
                if (PrintPriority.None == _printerConfig.Session.PrintPriority)
                {
                    return PrintPriority.None;
                }
                return _printerConfig.Session.PrintPriority;
            }
            set
            {
                if (_printerConfig.Session.PrintPriority != value)
                {
                    _printerConfig.Session.PrintPriority = value;
                    this.Modified = true;
                    //LocalNotifyPropertyChanged("PrintPriority");
                }
            }
        }

        public IList PrintPriorityChoices
        {
            get
            {
                return Enum.GetValues(typeof(PrintPriority));
            }
        }

        public RequestedResolution RequestedResolution
        {
            get
            {
                if (_printerConfig.FilmBox.RequestedResolution == RequestedResolution.None)
                {
                    return RequestedResolution.None;
                }
                return _printerConfig.FilmBox.RequestedResolution;
            }
            set
            {
                if (_printerConfig.FilmBox.RequestedResolution != value)
                {
                    _printerConfig.FilmBox.RequestedResolution = value;
                    this.Modified = true;
                    //LocalNotifyPropertyChanged("RequestedResolution");
                }
            }
        }
        public IList RequestedResolutionChoices
        {
            get
            {
                RequestedResolutionCollection hih = new RequestedResolutionCollection
                {
                    requestedResolutionList = new List<RequestedResolution>()
                };
                Type enumType = typeof(RequestedResolution);
                Array values = Enum.GetValues(enumType);
                CollectionUtils.ForEach(values, new Action<RequestedResolution>(hih.Add));
                return hih.requestedResolutionList;
            }
        }

        private sealed class ImageDisplayFormatCollection
        {
            public DicomPrinterConfigurationEditorComponent EditorComponent;
            public PrinterImageDisplayFormat ImageDisplay;

            public bool Equals(PrinterImageDisplayFormat imageDisplay)
            {
                return this.ImageDisplay == imageDisplay;
            }
        }
        private sealed class FilmOrientationCollection
        {
            public List<FilmOrientation> FilmOrientationList;

            public void Add(FilmOrientation filmOrientation)
            {
                if (filmOrientation != FilmOrientation.None)
                {
                    this.FilmOrientationList.Add(filmOrientation);
                }
            }
        }
        private sealed class RequestedResolutionCollection
        {
            public List<RequestedResolution> requestedResolutionList;

            public void Add(RequestedResolution requestedResolution)
            {
                if (requestedResolution != RequestedResolution.None)
                {
                    this.requestedResolutionList.Add(requestedResolution);
                }
            }
        }

        private event PropertyChangedEventHandler _lcalPropertyChanged;

        public event PropertyChangedEventHandler localPropertyChanged
        {
            add { _lcalPropertyChanged += value; }
            remove { _lcalPropertyChanged -= value; }
        }

        /// <summary>
        /// Notifies subscribers of the <see cref="PropertyChanged"/> event that the specified property has changed.
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed.</param>
        protected void LocalNotifyPropertyChanged(string propertyName)
        {
            EventsHelper.Fire(_lcalPropertyChanged, this, new PropertyChangedEventArgs(propertyName));
            NotifyPropertyChanged(propertyName);
        }
    }
}

