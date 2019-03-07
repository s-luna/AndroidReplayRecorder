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
    [Register("ViewController")]
    partial class ViewController
    {
        [Outlet]
        AppKit.NSButton RecordButtonOutlet { get; set; }

        [Outlet]
        AppKit.NSButton SaveButtonOutlet { get; set; }

        [Action("RecordButton:")]
        partial void RecordButton(Foundation.NSObject sender);

        [Action("SaveButton:")]
        partial void SaveButton(Foundation.NSObject sender);

        void ReleaseDesignerOutlets()
        {
            if (RecordButtonOutlet != null)
            {
                RecordButtonOutlet.Dispose();
                RecordButtonOutlet = null;
            }

            if (SaveButtonOutlet != null)
            {
                SaveButtonOutlet.Dispose();
                SaveButtonOutlet = null;
            }
        }
    }
}
