using System;
using System.Configuration;
using Foundation;

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
        private int m_recordingTime;
        private int m_pullWaitTime;
        private string m_adbPath;
        private string m_cacheDir;
        private string m_saveDir;
        private string m_screenshotPath;

        const string ADB_PATH_KEY = "adbPath";
        const string CACHE_DIR_KEY = "cacheDir";
        const string SAVE_DIR_KEY = "saveDir";
        const string RECORDING_TIME_KEY = "recordingTime";
        const string PULLWAIT_TIME_KEY = "pullWaitTime";
        const string SAVE_IMAGE_DIR_KIY = "saveImageDir";

        NSUserDefaults userDefault;
        public class Settings
        {
            public string adbPath;
            public string cacheDir;
            public string saveDir;
            public string saveImageDir;
            public int recordingTime;
            public int pullWaitTime;
            public Settings(string adbPath, string cacheDir, string saveDir, string saveImageDir, int recordingTime, int pullWaitTime)
            {
                this.adbPath = adbPath;
                this.cacheDir = cacheDir;
                this.saveDir = saveDir;
                this.recordingTime = recordingTime;
                this.pullWaitTime = pullWaitTime;
            }
        }

        public void Initialize()
        {
            m_homeDirPath = ADBCommand.GetHomeDirPath();
            userDefault = NSUserDefaults.StandardUserDefaults;
            if (userDefault.StringForKey(ADB_PATH_KEY) == null)
            {
                SeveDefaultSettigs();
            }
            RoadSettings();
        }

        public void SeveDefaultSettigs()
        {
            userDefault.SetString($"{m_homeDirPath}/Library/Android/sdk/platform-tools/adb", ADB_PATH_KEY);
            userDefault.SetString($"{m_homeDirPath}/Movies/Cache", CACHE_DIR_KEY);
            userDefault.SetString($"{m_homeDirPath}/Movies/", SAVE_DIR_KEY);
            userDefault.SetString($"{m_homeDirPath}/Pictures/AndroidReplayRecorder", SAVE_IMAGE_DIR_KIY);
            userDefault.SetInt(60, RECORDING_TIME_KEY);
            userDefault.SetInt(500, PULLWAIT_TIME_KEY);
        }

        public void SaveSettings(Settings settings)
        {
            userDefault.SetString(settings.adbPath, ADB_PATH_KEY);
            userDefault.SetString(settings.cacheDir, CACHE_DIR_KEY);
            userDefault.SetString(settings.saveDir, SAVE_DIR_KEY);
            userDefault.SetInt(settings.recordingTime, RECORDING_TIME_KEY);
            userDefault.SetInt(settings.pullWaitTime, PULLWAIT_TIME_KEY);
            userDefault.SetString(settings.saveImageDir, SAVE_IMAGE_DIR_KIY);
            RoadSettings();
        }

        private void RoadSettings()
        {
            m_adbPath = userDefault.StringForKey(ADB_PATH_KEY);
            m_cacheDir = userDefault.StringForKey(CACHE_DIR_KEY);
            m_saveDir = userDefault.StringForKey(SAVE_DIR_KEY);
            m_screenshotPath = userDefault.StringForKey(SAVE_IMAGE_DIR_KIY);
            m_recordingTime = (int)userDefault.IntForKey(RECORDING_TIME_KEY);
            m_pullWaitTime = (int)userDefault.IntForKey(PULLWAIT_TIME_KEY);
        }

        public int GetRecordingTime()
        {
            return m_recordingTime;
        }

        public int GetPullWaitTime()
        {
            return m_pullWaitTime;
        }

        public string GetHomePath()
        {
            return m_homeDirPath;
        }

        public string GetADBPath()
        {
            return m_adbPath;
        }

        public string GetCachePath()
        {
            return m_cacheDir;
        }

        public string GetSavePath()
        {
            return m_saveDir;
        }

        public string GetScreenshotPath()
        {
            return m_screenshotPath;
        }

    }
}
