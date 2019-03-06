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
        }

        private static string GetFileName()
        {
            return DateTime.Now.ToString("yyyyMMdd_HH-mm-ss") + ".mp4";
        }

        private string m_fileName;
        private string m_pid;

        private Process m_process;

        private void PullMovie()
        {
            Thread.Sleep(500);
            Process process = ADBCommand.PullMovie(m_fileName);
            process.WaitForExit();
            //Thread.Sleep(500);
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
            // adb shell screenrecordのpidを出力するので取得する
            m_pid = m_process.StandardOutput.ReadLine();
        }

        public void Interruption()
        {
            Task.Run(() => TaskInterruption());
        }

        private async void TaskInterruption()
        {
            ADBCommand.StopScreenRecord(m_pid);
            await Task.Run(() => PullMovie());
            //ADBCommand.RemoveAll();
            DataManager.Instance.Export();
        }

        public void ExitRecording()
        {
            Task.Run(() => TaskExitRecording());
        }

        private async void TaskExitRecording()
        {
            ADBCommand.StopScreenRecord(m_pid);
            await Task.Run(() => PullMovie());
        }

    }
}
