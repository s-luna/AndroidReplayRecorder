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

        private static string cachePath
        {
            get
            {
                return ApplicationConfig.Instance.GetCachePath();
            }
        }

        private static string homeDir
        {
            get
            {
                return ApplicationConfig.Instance.GetHomePath();
            }
        }

        private static string adbPath
        {
            get
            {
                return ApplicationConfig.Instance.GetADBPath();
            }
        }

        const string ANDROID_PATH = "AndroidRecorder/";

        public static Process MakeAndroidMovieDir()
        {
            return DoBashCommand.RunADBCommanc($"shell mkdir /sdcard/{ANDROID_PATH}");
        }

        public static Process StartScreenRecord(string fileName)
        {
            return DoBashCommand.RunBashCommand($"./record.sh {adbPath} {m_timeLimit} {ANDROID_PATH}{fileName}");
        }

        public static Process StopScreenRecord(string pid)
        {
            return DoBashCommand.RunBashCommand($"kill {pid}");
        }

        public static Process PullFile(string fileName)
        {
            return DoBashCommand.RunADBCommanc($"pull /sdcard/{ANDROID_PATH}{fileName} {cachePath}");
        }

        public static Process RemoveFile(string fileName)
        {
            return DoBashCommand.RunADBCommanc($"shell rm /sdcard/{ANDROID_PATH}{fileName}");
        }

        public static Process RemoveAll()
        {
            return DoBashCommand.RunADBCommanc($"shell rm /sdcard/{ANDROID_PATH}*");
        }

        public static string GetHomeDirPath()
        {
            Process process = DoBashCommand.RunBashCommand("cd ; echo $(pwd)");
            return process.StandardOutput.ReadLine();
        }

    }
}
