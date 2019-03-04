using System;
namespace AndroidRecorder
{
    public class ApplicationConfig
    {
        public ApplicationConfig()
        {
        }

        private static ApplicationConfig ins;
        public static ApplicationConfig Instance
        {
            get
            {
                if (ins == null)
                {
                    ins = new ApplicationConfig();
                }
                return ins;
            }
        }

        private string m_homeDirPath;
        private const int m_recordigTime = 180;

        public void Initialize()
        {
            m_homeDirPath = ADBCommand.GetHomeDirPath();
        }

        public int GetRecordingTime ()
        {
            return m_recordigTime;
        }

        public string GetHomePath()
        {
            return m_homeDirPath;
        }

    }
}
