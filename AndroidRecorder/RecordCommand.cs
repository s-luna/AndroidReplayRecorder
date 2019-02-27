using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace AndroidRecorder
{
    public static class RecordCommand
    {

        static string GetRecordCommandStr(string fileName)
        {
            return $"bash ./record.sh {timeLimit.ToString()} {fileName}";
        }

        //const string RecordCommandStr = "adb shell screenrecord --time-limit 10 /sdcard/hogehoge.mp4";
        static string GetPullCommandStr(string fileName)
        {
            return $"bash ./pull.sh {fileName}";
        }
        static string GetRemoveCommandStr(string fileName)
        {
            return $"bash ./remove.sh {fileName}";
        }

        static int timeLimit = 10;

        static List<RecordingProcess> recordingProcesses = new List<RecordingProcess>();
        static CancellationTokenSource cancellationTokenSource;

        static string GetFileName()
        {
            return DateTime.Now.ToString("yyyyMMdd_HH-mm-ss") + ".mp4";
        }

        public static string DoBashCommand(string cmd)
        {
            var p = new Process();
            p.StartInfo.FileName = "/bin/sh";
            p.StartInfo.Arguments = "-c \"" + cmd + "\"";
            //p.StartInfo.Arguments = "-c \"bash ../../../main.sh\" &";
            //p.StartInfo.Arguments = "-c " + cmd;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.Start();

            var output = "";
            var readLine = p.StandardOutput.ReadLine();
            //var readLine = p.StandardOutput.ReadLineAsync();
            if (readLine != null)
            {
                output += readLine.ToString();
            }
            //p.Kill();
            //p.WaitForExit();
            //p.Close();
            return output;
        }

        static Process StartBashCommand(string command)
        {
            Process process = new Process();
            process.StartInfo.FileName = "/bin/sh";
            process.StartInfo.Arguments = "-c \"" + command + "\" &";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();

            return process;
        }

        public static async void StartRecording()
        {
            Record();
            cancellationTokenSource = new CancellationTokenSource();
            await Task.Run(() => RecordingLoop(cancellationTokenSource.Token));
        }

        public static async Task Record()
        {
            string fileName = GetFileName();
            Process process = StartBashCommand(GetRecordCommandStr(fileName));
            string pid = process.StandardOutput.ReadLine();
            RecordingProcess recordingProcess = new RecordingProcess(process, fileName, pid, new CancellationTokenSource());
            recordingProcesses.Add(recordingProcess);
            await Task.Run(() => Recording(recordingProcess));
        }

        public static void StopRecord()
        {
            cancellationTokenSource.Cancel();
        }

        public static void SaveRecord()
        {
            RecordingProcess lastProcess = recordingProcesses.Last();
            StopRecord();
            StartRecording();
            DoBashCommand($"kill {lastProcess.pid}");
            lastProcess.tokenSource.Cancel();
            //DoBashCommand($"kill {recordingPID}");
        }

        public static string Pull(string fileName)
        {
            DoBashCommand(GetPullCommandStr(fileName));
            return "";
        }

        public static string Remove(string fileName)
        {
            DoBashCommand(GetRemoveCommandStr(fileName));
            return "";
        }

        static async Task RecordingLoop(CancellationToken cancellationToken)
        {
            int timeCount = 0;
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }
                Thread.Sleep(1000);
                timeCount++;
                if (timeCount >= timeLimit * 1)
                {
                    Record();
                    timeCount = 0;
                }
            }
        }


        public static async Task Recording(RecordingProcess recordingProcess)
        {
            while (true)
            {
                if (recordingProcess.tokenSource.Token.IsCancellationRequested || recordingProcess.process.HasExited)
                {
                    recordingProcesses.Remove(recordingProcess);
                    Thread.Sleep(100);
                    Pull(recordingProcess.fileName);
                    Remove(recordingProcess.fileName);
                    break;
                }
            }
        }

    }

    public class RecordingProcess
    {
        public Process process;
        public string fileName;
        public string pid;
        public CancellationTokenSource tokenSource;

        public RecordingProcess(Process process, string fileName, string pid, CancellationTokenSource tokenSource)
        {
            this.process = process;
            this.fileName = fileName;
            this.pid = pid;
            this.tokenSource = tokenSource;
        }
    }
}
