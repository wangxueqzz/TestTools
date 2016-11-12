using Macro.Common.Media.IMAPI2;

namespace Macro.ImageViewer.Utilities.Media
{
    using Macro.Desktop;
    using Macro.Desktop.Trees;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public interface IMediaWriterComponent 
    {
        void ClearStudies();
        void EjectMedia();
        void OpenOptions();
        void WriteMedia();
        void Cancel();
        void DetectMedia();
        void EreaseDisc();

        bool CanCancel { get; }

        bool CanWrite { get; }

        bool IsWriting { get; set; }

        string CurrentMediaDescription { get; }

        int CurrentMediaSpacePercent { get;  }

        string CurrentWriteStageName { get; set; }

        int CurrentWriteStagePercent { get; set; }

        bool EjectOnCompleted { get; }

        IList<IDiscRecorder2> MediaWriters { get; }

        string NumberOfStudies { get; }

        string RequiredMediaSpace { get; set; }

        IDiscRecorder2 SelectedMediaWriter { get; set; }

        string StagingFolderPath { get;}

        ITree Tree { get; set; }

        string VolumeName { get; set; }
    }
}

