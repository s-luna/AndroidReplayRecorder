using System;
using System.IO;
using System.Collections.Generic;

namespace AndroidRecorder
{
    public class DataManager
    {
        string cachePath
        {
            get
            {
                return ApplicationConfig.Instance.GetCachePath();
            }
        }
        string exportPath
        {
            get
            {
                return ApplicationConfig.Instance.GetSavePath();
            }
        }
        string homeDir
        {
            get
            {
                return ApplicationConfig.Instance.GetHomePath();
            }
        }
        string screenshotPath
        {
            get
            {
                return ApplicationConfig.Instance.GetScreenshotPath();
            }
        }

        public DataManager()
        {
            Initialize();
        }

        public void Initialize()
        {
            if (Directory.Exists($"{cachePath}") == false)
            {
                Directory.CreateDirectory($"{cachePath}");
            }
            if (Directory.Exists($"{screenshotPath}") == false)
            {
                Directory.CreateDirectory($"{screenshotPath}");
            }
            cacheMovieNames = new Queue<string>();
            Clean();
            isInit = true;
        }

        private static DataManager ins;
        public static DataManager Instance
        {
            get
            {
                if (ins == null)
                {
                    ins = new DataManager();
                }
                return ins;
            }
        }

        private Queue<string> cacheMovieNames;
        private bool isInit;

        private string[] GetAllCache()
        {
            return Directory.GetFiles($"{cachePath}", "*", SearchOption.AllDirectories);
        }

        public void Clean()
        {
            foreach (string file in GetAllCache())
            {
                File.Delete(file);
            }
            cacheMovieNames = new Queue<string>();
        }


        public void SetMovie(string fileName)
        {
            if (isInit)
            {
                cacheMovieNames.Enqueue(fileName);
                while (cacheMovieNames.Count > 10)
                {
                    DeleteCacheMovie(cacheMovieNames.Dequeue());
                }
            }
        }

        public void DeleteCacheMovie(string fileName)
        {
            string path = $"{cachePath}/{fileName}";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public void Export()
        {
            string exportDir = $"{exportPath}{DateTime.Now.ToString("yyyyMMdd_HH-mm-ss")}";
            Directory.CreateDirectory(exportDir);
            while (cacheMovieNames.Count > 0)
            {
                string cacheMovieName = cacheMovieNames.Dequeue();
                if (File.Exists($"{cachePath}/{cacheMovieName}"))
                {
                    File.Move($"{cachePath}/{cacheMovieName}", $"{exportDir}/{cacheMovieName}");
                }
            }
            Clean();
        }

    }
}
