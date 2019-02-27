using System;
using System.Diagnostics;

namespace AndroidRecorder
{
    public static class DoBashCommand
    {
        public static Process RunBashCommand(string command)
        {
            Process process = new Process();
            process.StartInfo.FileName = "/bin/sh";
            process.StartInfo.Arguments = "-c \"" + command + "\" &";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();

            return process;
        }

        public static Process RunADBCommanc(string command)
        {
            Process process = new Process();
            process.StartInfo.FileName = "/Users/luna.suzuki/Library/Android/sdk/platform-tools/adb";
            process.StartInfo.Arguments = command;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();

            return process;
        }

}
}
