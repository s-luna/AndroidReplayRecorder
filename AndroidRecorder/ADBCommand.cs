using System;
using System.Diagnostics;

namespace AndroidRecorder
{
    public static class ADBCommand
    {
        private static int m_timeLimit
        {
            get
            {
                return ApplicationConfig.Instance.GetRecordingTime();
            }
        }

        private static string homeDir
        {
            get
            {
                return ApplicationConfig.Instance.GetHomePath();
            }
        }

        const string ANDROID_PATH = "AndroidRecorder/";

        private static string GetPullCommandStr (string fileName) {
            return $"bash ./pull.sh {ANDROID_PATH}{fileName}";
        }

        private static string GetRecordCommandStr (string fileName)
        {
            return $"bash ./record.sh {m_timeLimit.ToString()} {ANDROID_PATH}{fileName}";
        }

        private static string GetRemoveCommandStr(string fileName)
        {
            return $"bash ./remove.sh {ANDROID_PATH}{fileName}";
        }

        private static string GetRemoveAllCommanndStr() {
            return $"bash ./removeAll.sh";
        }

        public static Process StartScreenRecord(string fileName) {
            //return DoBashCommand.RunADBCommanc($"shell screenrecord --time-limit {TIMELIMIT} /sdcard/{ANDROID_PATH}{fileName} &");
            return DoBashCommand.RunBashCommand(GetRecordCommandStr(fileName));
        }

        public static Process StopScreenRecord(string pid) {
            return DoBashCommand.RunBashCommand($"kill {pid}");
        }

        public static Process PullMovie(string fileName) {
            return DoBashCommand.RunADBCommanc($"pull /sdcard/{ANDROID_PATH}{fileName} {homeDir}/Movies/Cache");
            //return DoBashCommand.RunBashCommand(GetPullCommandStr(fileName));
        }

        public static Process RemoveMovie(string fileName) {
            return DoBashCommand.RunBashCommand(GetRemoveCommandStr(fileName));
        }

        public static Process RemoveAll() {
            return DoBashCommand.RunBashCommand(GetRemoveAllCommanndStr());
        }

        public static string GetHomeDirPath ()
        {
            Process process = DoBashCommand.RunBashCommand("bash ./getHome.sh");
            return process.StandardOutput.ReadLine();
        }

    }
}
