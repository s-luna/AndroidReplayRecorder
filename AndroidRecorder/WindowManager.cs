using System;
namespace AndroidRecorder
{
    public class WindowManager
    {
        public WindowManager()
        {
        }

        private static WindowManager ins;
        public static WindowManager Instance
        {
            get
            {
                if (ins == null)
                {
                    ins = new WindowManager();
                }
                return ins;
            }
        }

        private ViewController viewController;
        private PreferencesViewController preferencesViewController;

        public void SetViewController(ViewController controller)
        {
            viewController = controller;
        }

        public void SetPreferencesViewController(PreferencesViewController controller)
        {
            preferencesViewController = controller;
        }

        public ViewController GetViewController()
        {
            return viewController;
        }

        public PreferencesViewController GetPreferencesViewController()
        {
            return preferencesViewController;
        }

    }
}
