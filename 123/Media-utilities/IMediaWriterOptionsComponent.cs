namespace Macro.ImageViewer.Utilities.Media
{
    using Macro.Desktop;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public interface IMediaWriterOptionsComponent
    {
        event PropertyChangedEventHandler PropertyChanged;

        void Cancel();
        void Save();

        bool DeleteStagedFilesOnCompleted { get; set; }

        bool EjectMediaOnCompleted { get; set; }

        bool IncludeIdeographicNames { get; set; }

        bool IncludePhoneticNames { get; set; }

        bool IncludePortableWorkstation { get; set; }

        bool StageToTempFolder { get; set; }

        string UserStagingFolder { get; set; }

        bool VerifyMediaOnCompleted { get; set; }
    }
}

