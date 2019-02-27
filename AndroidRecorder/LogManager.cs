using System;
namespace AndroidRecorder
{
    public class LogManager
    {
        public LogManager()
        {
        }

        private static LogManager ins;
        public static LogManager Instance
        {
            get
            {
                if (ins == null)
                {
                    ins = new LogManager();
                }
                return ins;
            }
        }

        private ViewController viewController;

        public void SetViewController(ViewController controller) {
            viewController = controller;
        }

        public void OutputLog (string text)
        {
            viewController.AddLog(text);
        }


    }
}
