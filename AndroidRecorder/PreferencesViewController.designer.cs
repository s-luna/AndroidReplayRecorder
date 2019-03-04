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
        AppKit.NSTextField AdbPathField { get; set; }

        [Outlet]
        AppKit.NSTextField CacheDirField { get; set; }

        [Outlet]
        AppKit.NSTextField RecordingTimeField { get; set; }

        [Outlet]
        AppKit.NSTextField SaveDirField { get; set; }

        [Outlet]
        AppKit.NSButton SaveSettingButtonOutlet { get; set; }

        [Action ("SaveSettingButton:")]
        partial void SaveSettingButton (Foundation.NSObject sender);
        
        void ReleaseDesignerOutlets ()
        {
            if (AdbPathField != null) {
                AdbPathField.Dispose ();
                AdbPathField = null;
            }

            if (CacheDirField != null) {
                CacheDirField.Dispose ();
                CacheDirField = null;
            }

            if (SaveDirField != null) {
                SaveDirField.Dispose ();
                SaveDirField = null;
            }

            if (RecordingTimeField != null) {
                RecordingTimeField.Dispose ();
                RecordingTimeField = null;
            }

            if (SaveSettingButtonOutlet != null) {
                SaveSettingButtonOutlet.Dispose ();
                SaveSettingButtonOutlet = null;
            }
        }
    }
}
