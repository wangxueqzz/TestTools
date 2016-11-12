using Macro.Common;
using Macro.Desktop;
using Macro.Desktop.View.WinForms;

namespace Macro.ImageViewer.Utilities.Media.View.WinForms
{
    [ExtensionOf(typeof(MediaWriteOptionsComponentViewExtensionPoint))]
   public class MediaWriteOptionsComponentView:WinFormsView,IApplicationComponentView
    {
        private MediaWriterOptionsComponent _component;
        private MediaWriteOptions _mediaOptionControl;

        public void SetComponent(IApplicationComponent component)
        {
            _component = (MediaWriterOptionsComponent)component;
        }

        public override object GuiElement
        {
            get 
            {
                if (_mediaOptionControl==null)
                {
                    _mediaOptionControl = new MediaWriteOptions(_component);
                }

                return _mediaOptionControl;
            }
        }
    }
}
