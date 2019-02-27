using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace AndroidRecorder
{
    public class RecordingManager
    {
        public RecordingManager()
        {
            m_status = RecordingStatus.None;
        }

        public enum RecordingStatus
        {
            None,
            Start,
            Recording,
            Stop,
            Pull,
            Exit,
        }

        private static string GetFileName()
        {
            return DateTime.Now.ToString("yyyyMMdd_HH-mm-ss") + ".mp4";
        }

        private string m_fileName;
        private string m_pid;

        private Process m_process;
        private RecordingStatus m_status;
        public RecordingStatus GetRecordingStatus() { return m_status; }

        private void SetRecordingStatus(RecordingStatus status)
        {
            // Pre change
            switch (status)
            {
                case RecordingStatus.Start:
                    break;
                case RecordingStatus.Recording:
                    //LogManager.Instance.OutputLog("StartRecording...");
                    // この辺に録画終了待ち入れる...？
                    break;
                case RecordingStatus.Stop:
                    // この辺に録画中断待ち入れる...？
                    break;
                case RecordingStatus.Pull:
                    // Pull待ち入れる
                    break;
                case RecordingStatus.Exit:
                    //LogManager.Instance.OutputLog("StopRecording...");
                    // ガチ終了
                    break;
                default:
                    break;
            }
            m_status = status;
        }

        private void PullMovie()
        {
            //LogManager.Instance.OutputLog("Pull Start...");
            //Process process = ADBCommand.PullMovie(m_fileName);
            Process process = ADBCommand.PullMovie(m_fileName);
            SetRecordingStatus(RecordingStatus.Pull);
            process.WaitForExit();
            RemoveMovie();
            DataManager.Instance.SetMovie($"{m_fileName}");
        }

        private void RemoveMovie()
        {
            ADBCommand.RemoveMovie(m_fileName);
        }

        public void StartRecording()
        {
            Task.Run(() => TaskStartRecording());
        }

        private async void TaskStartRecording()
        {
            m_fileName = GetFileName();
            m_process = ADBCommand.StartScreenRecord(m_fileName);
            // pidのセットするもしかしたらProcessから取れるかも
            m_pid = m_process.StandardOutput.ReadLine();
            //LogManager.Instance.OutputLog(m_process.StandardOutput.ReadLine());
            //LogManager.Instance.OutputLog(m_process.StandardError.ReadLine());
            //Task.Run(() => RecordingProcessExited(m_process));
            SetRecordingStatus(RecordingStatus.Recording);
        }

        public void Interruption()
        {
            Task.Run(() => TaskInterruption());
        }

        private async void TaskInterruption()
        {
            ADBCommand.StopScreenRecord(m_pid);
            PullMovie();
            ADBCommand.RemoveAll();
            DataManager.Instance.Export();
            SetRecordingStatus(RecordingStatus.Exit);
        }

        public void ExitRecording()
        {
            Task.Run(() => TaskExitRecording());
        }

        private async void TaskExitRecording()
        {
            ADBCommand.StopScreenRecord(m_pid);
            PullMovie();

            SetRecordingStatus(RecordingStatus.Stop);
        }

        public void GetMoviePath()
        {
            // ???
        }

        public void ProcessTextMethod()
        {
            Process process = ADBCommand.RemoveAll();
            //process.Exited += TestExited;
            //process.WaitForExit();
            //TestExited(null,null);
            LogManager.Instance.OutputLog("Command Start");
            process.WaitForExit();
            TestExited(null,null);
        }

        void TestExited(object sender, EventArgs e)
        {
            LogManager.Instance.OutputLog("Process End.");
        }

    }
}
