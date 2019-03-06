using System;
using System.Diagnostics;
using AppKit;
using Foundation;

namespace AndroidRecorder
{
    public partial class ViewController : NSViewController
    {
        private SequenceManager sequenceManager;

        public ViewController(IntPtr handle) : base(handle)
        {
            sequenceManager = SequenceManager.Instance;
        }
        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }

        public override void ViewDidLoad()
        {
            //base.ViewDidLoad();

            // Do any additional setup after loading the view.
            base.AwakeFromNib();
            RecordButtonOutlet.Title = "Record";
            WindowManager.Instance.SetViewController(this);
            ApplicationConfig.Instance.Initialize();
            DataManager.Instance.Initialize();
        }

        partial void RecordButton(Foundation.NSObject sender)
        {
            RecordButtonAction();
        }

        partial void SaveButton(NSObject sender)
        {
            if (SequenceManager.Instance.GetSequenceStatus() == SequenceManager.SequenceStatus.Recording)
            {
                sequenceManager.Export();
            }
        }

        private void RecordButtonAction()
        {
            if (sequenceManager.GetSequenceStatus() == SequenceManager.SequenceStatus.Idle)
            {
                StartRecordingAction();
            }
            else if (sequenceManager.GetSequenceStatus() == SequenceManager.SequenceStatus.Recording)
            {
                StopRecordingAction();
            }
        }

        private void StartRecordingAction()
        {
            DataManager.Instance.Initialize();
            sequenceManager.Start();
            RecordButtonOutlet.Title = "Stop";
        }

        private void StopRecordingAction()
        {
            sequenceManager.Stop();
            RecordButtonOutlet.Title = "Record";
        }

        public void DisableButton()
        {
            if (sequenceManager.GetSequenceStatus() == SequenceManager.SequenceStatus.Recording)
            {
                StopRecordingAction();
            }
            RecordButtonOutlet.Enabled = false;
            SaveButtonOutlet.Enabled = false;
        }

        public void EnableButton()
        {
            RecordButtonOutlet.Enabled = true;
            SaveButtonOutlet.Enabled = true;
        }

    }
}
