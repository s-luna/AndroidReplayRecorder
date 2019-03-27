using System;
using System.Threading.Tasks;

namespace AndroidRecorder
{
    public class ScreenshotController
    {
        public ScreenshotController()
        {
        }

        private static ScreenshotController ins;
        public static ScreenshotController Instance
        {
            get
            {
                if (ins == null)
                {
                    ins = new ScreenshotController();
                }
                return ins;
            }
        }

        private static string GetFileName()
        {
            return DateTime.Now.ToString("yyyyMMdd_HH-mm-ss") + ".png";
        }

        public void Capture()
        {
            ADBCommand.MakeAndroidMovieDir();
            Task.Run(() => ScreenCapture());
        }

        private async void ScreenCapture()
        {
            var fileName = GetFileName();
            await Task.Run(() => RunScreenshot(fileName));
            await Task.Run(() => PullImage(fileName));
            await Task.Run(() => deleteImage(fileName));
        }

        private void RunScreenshot(string fileName)
        {
            var process = ADBCommand.ScreenCapture(fileName);
            process.WaitForExit();
        }

        private void PullImage(string fileName)
        {
            var process = ADBCommand.PullImage(fileName);
            process.WaitForExit();
        }

        private void deleteImage(string fileName)
        {
            ADBCommand.RemoveFile(fileName);
        }

    }
}
