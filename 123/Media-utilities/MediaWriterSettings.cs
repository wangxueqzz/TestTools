using System;
using System.Configuration;
using Macro.Common;
using Macro.Common.Configuration;
using Macro.Desktop;

namespace Macro.ImageViewer.Utilities.Media
{
    [SettingsGroupDescription("Stores settings for MediaWriterSettings.")]
    [SettingsProvider(typeof(StandardSettingsProvider))]
   internal  sealed  partial class MediaWriterSettings
    {
        public MediaWriterSettings()
		{
			ApplicationSettingsRegistry.Instance.RegisterInstance(this);
		}
    }
}
