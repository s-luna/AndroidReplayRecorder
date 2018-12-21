using System;
using System.Threading;
using System.Threading.Tasks;

namespace AndroidRecorder
{
    public class RecordingManager
    {

        private static RecordingManager ins;
        public static RecordingManager Instance
        {
            get
            {
                return ins;
            }
        }

        ViewController viewController;

        AppKit.NSTextField textField;
        CancellationTokenSource tokenSource;

        public RecordingManager(ViewController viewController)
        {
            this.viewController = viewController;
            ins = this;
        }


        public void StopRecording()
        {
            viewController.AddLog("piyo");
            tokenSource.Cancel();
        }

        public void StartRecording(AppKit.NSTextField button, ViewController viewController)
        {
            this.viewController = viewController;
            this.textField = button;
            tokenSource = new CancellationTokenSource();
            StartRecordingAsync(tokenSource.Token);
        }

        public async Task<string> StartRecordingAsync(CancellationToken cancelToken)
        {

            //この処理は待つ
            var task = await Task.Run(() => Recording(cancelToken));


            //return "fugafuga";
            //textField.StringValue += task + "\n";
            return task;

        }

        async Task<string> Recording(CancellationToken cancelToken)
        {

            int count = 0;
            string outText = "";
            int waitTime = 180;
            while (true)
            {
                outText += count.ToString();
                count++;
                Thread.Sleep(100);
                if (cancelToken.IsCancellationRequested)
                {
                    //録画停止時処理
                    //textField.StringValue = outText;
                    RecordCommand.DoBashCommand("curl http://local.com:3000/users");
                    viewController.AddLog("fugafuga");
                    break;
                }
            }

            //}
            return outText;
        }

        async Task<string> Recording(int waitTIme, CancellationToken cancelToken)
        {
            Thread.Sleep(waitTIme);
            if (!cancelToken.IsCancellationRequested)
            {

            }
            return "";
        }

        //public async void Recordiong()
        //{
        //}

    }
}
