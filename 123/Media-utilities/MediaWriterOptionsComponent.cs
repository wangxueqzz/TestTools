using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Macro.Common;
using Macro.Desktop;
using System.ComponentModel;

namespace Macro.ImageViewer.Utilities.Media
{
    [ExtensionPoint()]
    public sealed class MediaWriteOptionsComponentViewExtensionPoint : ExtensionPoint<IApplicationComponentView>
    {

    }

    [AssociateView(typeof(MediaWriteOptionsComponentViewExtensionPoint))]
    public class MediaWriterOptionsComponent : ApplicationComponent, IMediaWriterOptionsComponent
    {

        public MediaWriterOptionsComponent()
        {
            propertyChanged = null;
        }

        #region IMediaWriterOptionsComponent 成员

        private event PropertyChangedEventHandler propertyChanged;

        public event PropertyChangedEventHandler PropertyChanged
        {
            add { propertyChanged += value; }
            remove { propertyChanged -= value; }
        }

        public void Cancel()
        {
            this.Exit(ApplicationComponentExitCode.None);
        }

        public void Save()
        {
            MediaWriterSettings.Default.Save();
            propertyChanged(this, new PropertyChangedEventArgs("UserStagingFolder"));
            this.Exit(ApplicationComponentExitCode.Accepted);
        }

        public bool DeleteStagedFilesOnCompleted
        {
            get
            {
                return MediaWriterSettings.Default.DeleteStagedFilesOnCompleted;
            }
            set
            {
                if (MediaWriterSettings.Default.DeleteStagedFilesOnCompleted != value)
                {
                    MediaWriterSettings.Default.DeleteStagedFilesOnCompleted = value;
                    this.Modified = true;
                    NotifyPropertyChanged("DeleteStagedFilesOnCompleted");

                }
            }
        }

        public bool EjectMediaOnCompleted
        {
            get
            {
                return MediaWriterSettings.Default.EjectMediaOnCompleted;
            }
            set
            {
                if (MediaWriterSettings.Default.EjectMediaOnCompleted != value)
                {
                    MediaWriterSettings.Default.EjectMediaOnCompleted = value;
                    this.Modified = true;
                    NotifyPropertyChanged("EjectMediaOnCompleted");

                }
            }
        }

        public bool IncludeIdeographicNames
        {
            get
            {
                return MediaWriterSettings.Default.IncludeIdeographicNames;
            }
            set
            {
                if (MediaWriterSettings.Default.IncludeIdeographicNames != value)
                {
                    MediaWriterSettings.Default.IncludeIdeographicNames = value;
                    this.Modified = true;
                    NotifyPropertyChanged("IncludeIdeographicNames");

                }
            }
        }

        public bool IncludePhoneticNames
        {
            get
            {
                return MediaWriterSettings.Default.IncludePhoneticNames;
            }
            set
            {
                if (MediaWriterSettings.Default.IncludePhoneticNames != value)
                {
                    MediaWriterSettings.Default.IncludePhoneticNames = value;
                    this.Modified = true;
                    NotifyPropertyChanged("IncludePhoneticNames");

                }
            }
        }

        public bool IncludePortableWorkstation
        {
            get
            {
                return MediaWriterSettings.Default.IncludePortableWorkstation;
            }
            set
            {
                if (MediaWriterSettings.Default.IncludePortableWorkstation != value)
                {
                    MediaWriterSettings.Default.IncludePortableWorkstation = value;
                    this.Modified = true;
                    NotifyPropertyChanged("IncludePortableWorkstation");

                }
            }
        }

        public bool StageToTempFolder
        {
            get
            {
                return MediaWriterSettings.Default.StageToTempFolder;
            }
            set
            {
                if (MediaWriterSettings.Default.StageToTempFolder != value)
                {
                    MediaWriterSettings.Default.StageToTempFolder = value;
                    this.Modified = true;
                    propertyChanged(this, new PropertyChangedEventArgs("StageToTempFolder"));
                    NotifyPropertyChanged("StageToTempFolder");

                }
            }
        }

        public string UserStagingFolder
        {
            get
            {
                return MediaWriterSettings.Default.UserStagingFolder;
            }
            set
            {
                if (MediaWriterSettings.Default.UserStagingFolder != value)
                {
                    MediaWriterSettings.Default.UserStagingFolder = value;
                    this.Modified = true;                   
                    MediaWriterSettings.Default.StageToTempFolder = false;
                    NotifyPropertyChanged("UserStagingFolder");

                }
            }
        }

        public bool VerifyMediaOnCompleted
        {
            get
            {
                return MediaWriterSettings.Default.VerifyMediaOnCompleted;
            }
            set
            {
                if (MediaWriterSettings.Default.VerifyMediaOnCompleted != value)
                {
                    MediaWriterSettings.Default.VerifyMediaOnCompleted = value;
                    this.Modified = true;
                    NotifyPropertyChanged("VerifyMediaOnCompleted");

                }
            }
        }

        #endregion
    }
}
