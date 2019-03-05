// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using AppKit;

namespace AndroidRecorder
{
    public partial class PreferencesViewController : NSViewController
    {
        public PreferencesViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            WindowManager.Instance.GetViewController().DisableButton();
            ApplicationConfig config = ApplicationConfig.Instance;
            AdbPathField.StringValue = config.GetADBPath();
            CacheDirField.StringValue = config.GetCachePath();
            SaveDirField.StringValue = config.GetSavePath();
            RecordingTimeField.IntValue = config.GetRecordingTime();
        }

        partial void SaveSettingButton(NSObject sender)
        {
            Console.WriteLine("push save setting button");
            if (RecordingTimeField.IntValue < 10)
            {
                RecordingTimeField.IntValue = 10;
            }
            else if (RecordingTimeField.IntValue > 180)
            {
                RecordingTimeField.IntValue = 180;
            }
            ApplicationConfig.Instance.SaveSettings(
                new ApplicationConfig.Settings(
                AdbPathField.StringValue,
                CacheDirField.StringValue,
                SaveDirField.StringValue,
                RecordingTimeField.IntValue));
        }

    }
}
