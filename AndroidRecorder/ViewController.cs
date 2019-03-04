using System;
using System.Diagnostics;
using AppKit;
using Foundation;

namespace AndroidRecorder
{
    public partial class ViewController : NSViewController
    {

        public ViewController(IntPtr handle) : base(handle)
        {

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
            LogLabel.StringValue = "";
            LogManager.Instance.SetViewController(this);
            ApplicationConfig.Instance.Initialize();
        }

        partial void RecordButton(Foundation.NSObject sender)
        {
            //AddLog(DoBashCommand.RunBashCommand("pwd").StandardOutput.ReadLine());
            SequenceManager sequenceManager = SequenceManager.Instance;
            if (sequenceManager.GetSequenceStatus() == SequenceManager.SequenceStatus.Idle) {
                sequenceManager.Start();
                RecordButtonOutlet.Title = "Stop";
            }
            else if (sequenceManager.GetSequenceStatus() == SequenceManager.SequenceStatus.Recording)
            {
                sequenceManager.Stop();
                RecordButtonOutlet.Title = "Record";
            }

        }

        partial void SaveButton(NSObject sender)
        {
            if (SequenceManager.Instance.GetSequenceStatus() == SequenceManager.SequenceStatus.Recording) {
                SequenceManager sequenceManager = SequenceManager.Instance;
                sequenceManager.Export();
            }
        }

        public void AddLog(string text)
        {
            LogLabel.StringValue += text + "\n";
        }

    }
}
