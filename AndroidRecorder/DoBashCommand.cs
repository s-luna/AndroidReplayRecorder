using System;
using System.Diagnostics;

namespace AndroidRecorder
{
    public static class DoBashCommand
    {
        public static Process RunBashCommand(string command)
        {
            Process process = new Process();
            process.StartInfo.FileName = "/bin/bash";
            process.StartInfo.Arguments = "-c \"" + command + "\" &";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.Verb = "RunAs";
            process.Start();

            return process;
        }

        private static string m_homeDir
        {
            get
            {
                return ApplicationConfig.Instance.GetHomePath();
            }
        }

        public static Process RunADBCommanc(string command)
        {
            Process process = new Process();
            process.StartInfo.FileName = $"{m_homeDir}/Library/Android/sdk/platform-tools/adb";
            process.StartInfo.Arguments = command;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();

            return process;
        }

}
}
