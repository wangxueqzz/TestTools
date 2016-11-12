using Macro.Common;
using Macro.Desktop;
using Macro.Desktop.View.WinForms;

namespace Macro.ImageViewer.Utilities.Media.View.WinForms
{
    [ExtensionOf(typeof(MediaWriteComponentViewExtensionPoint))]
    public class MediaWriteComponentView : WinFormsView, IApplicationComponentView
    {
        private MediaWriterComponent _component;
        private MediaControl _mediaControl = null;

        public void SetComponent(IApplicationComponent component)
        {
            _component = (MediaWriterComponent)component;
        }

        public override object GuiElement
        {
            get
            {

                if (_mediaControl == null)
                {
                    _mediaControl = new MediaControl(_component);
                }

                return _mediaControl;
            }
        }

    }
}
