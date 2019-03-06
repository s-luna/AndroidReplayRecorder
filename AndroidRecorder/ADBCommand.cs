﻿using System;
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

        const string ANDROID_PATH = "AndroidRecorder/";

        public static Process StartScreenRecord(string fileName)
        {
            return DoBashCommand.RunADBCommanc($"shell screenrecord --time-limit {m_timeLimit} /sdcard/{ANDROID_PATH}{fileName} & echo $!; sleep {m_timeLimit}");
        }

        public static Process StopScreenRecord(string pid) {
            return DoBashCommand.RunBashCommand($"kill {pid}");
        }

        public static Process PullMovie(string fileName) {
            return DoBashCommand.RunADBCommanc($"pull /sdcard/{ANDROID_PATH}{fileName} {cachePath}");
        }

        public static Process RemoveMovie(string fileName) {
            return DoBashCommand.RunADBCommanc($"shell rm /sdcard/{ANDROID_PATH}{fileName}");
        }

        public static Process RemoveAll() {
            return DoBashCommand.RunADBCommanc($"shell rm /sdcard/{ANDROID_PATH}*");
        }

        public static string GetHomeDirPath ()
        {
            Process process = DoBashCommand.RunBashCommand("bash ./getHome.sh");
            return process.StandardOutput.ReadLine();
        }

    }
}
