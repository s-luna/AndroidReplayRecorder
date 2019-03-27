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
        AppKit.NSTextField PullWaitTimeField { get; set; }

        [Outlet]
        AppKit.NSTextField RecordingTimeField { get; set; }

        [Outlet]
        AppKit.NSButton ResetSettingButtonOutlet { get; set; }

        [Outlet]
        AppKit.NSTextField SaveDirField { get; set; }

        [Outlet]
        AppKit.NSButton SaveSettingButtonOutlet { get; set; }

        [Outlet]
        AppKit.NSTextField ScreenshotPath { get; set; }

        [Action ("ResetSettingButton:")]
        partial void ResetSettingButton (Foundation.NSObject sender);

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

            if (PullWaitTimeField != null) {
                PullWaitTimeField.Dispose ();
                PullWaitTimeField = null;
            }

            if (RecordingTimeField != null) {
                RecordingTimeField.Dispose ();
                RecordingTimeField = null;
            }

            if (ResetSettingButtonOutlet != null) {
                ResetSettingButtonOutlet.Dispose ();
                ResetSettingButtonOutlet = null;
            }

            if (SaveDirField != null) {
                SaveDirField.Dispose ();
                SaveDirField = null;
            }

            if (SaveSettingButtonOutlet != null) {
                SaveSettingButtonOutlet.Dispose ();
                SaveSettingButtonOutlet = null;
            }

            if (ScreenshotPath != null) {
                ScreenshotPath.Dispose ();
                ScreenshotPath = null;
            }
        }
    }
}
