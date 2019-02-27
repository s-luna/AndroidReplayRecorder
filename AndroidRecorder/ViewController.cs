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
            //DataManager.Instance.Clean();
            //new RecordingManager(this);
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

            //RecordButtonOutlet.Title = "fuga";
            //Console.WriteLine(ViewController.DoBashCommand("adb devices"));
            //LogLabel.StringValue = "";
            //LogLabel.StringValue += ViewController.DoBashCommand("echo foo\n");
            //LogLabel.StringValue += ViewController.DoBashCommand("pwd \n");
            //LogLabel.StringValue += ViewController.DoBashCommand("ls ../../../ \n");

            //RecordCommand.DoBashCommand("bash ./init.sh");
            //RecordCommand.DoBashCommand("");


            //if (isRecording)
            //{
            //    RecordCommand.StopRecord();
            //    //RecordingManager.Instance.StopRecording();
            //    isRecording = false;
            //    LogLabel.StringValue += "StopRecording...";
            //    RecordButtonOutlet.Title = "record";
            //    AddLog("hogehoge");
            //}
            //else
            //{
            //    RecordCommand.StartRecording();
            //    RecordButtonOutlet.Title = "stop";
            //    //RecordingManager.Instance.StartRecording(LogLabel, this);
            //    isRecording = true;
            //}

            //if (pid != "")
            //{

            //    string output = ViewController.DoBashCommand($"kill {pid}");
            //    LogLabel.StringValue += output + "\n";
            //    pid = "";
            //}
            //else
            //{
            //    //pid = ViewController.DoBashCommand("bash ../../../loop.sh");
            //    //LogLabel.StringValue += pid + "\n";
            //    RecordingManager.Instance.StartRecording(LogLabel);
            //    //LogLabel.StringValue += RecordingManager.Instance.StartRecordingAsync().Result;

            //}

            //LogLabel.StringValue += ViewController.DoBashCommand("curl http://local.com:3000/users");
            //LogLabel.StringValue += ViewController.DoBashCommand("echo foo\n");
            //ViewController.DoBashCommand("adb devices");
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
