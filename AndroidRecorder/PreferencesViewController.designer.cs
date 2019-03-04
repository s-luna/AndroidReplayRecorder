// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace AndroidRecorder
{
    [Register ("PreferencesViewController")]
    partial class PreferencesViewController
    {
        [Outlet]
        AppKit.NSButton SaveSettingButtonOutlet { get; set; }

        [Action ("SaveSettingButton:")]
        partial void SaveSettingButton (Foundation.NSObject sender);
        
        void ReleaseDesignerOutlets ()
        {
            if (SaveSettingButtonOutlet != null) {
                SaveSettingButtonOutlet.Dispose ();
                SaveSettingButtonOutlet = null;
            }
        }
    }
}
