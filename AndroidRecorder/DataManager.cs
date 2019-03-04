using System;
using System.IO;
using System.Collections.Generic;

namespace AndroidRecorder
{
    public class DataManager
    {
        string CACHE_PATH
        {
            get
            {
                return $"{homeDir}/Movies/Cache";
            }
        }
        string EXPORT_PATH
        {
            get
            {
                return $"{homeDir}/Movies/";
            }
        }
        string homeDir
        {
            get
            {
                return ApplicationConfig.Instance.GetHomePath();
            }
        }

        public DataManager()
        {
            string getHoveDir = homeDir;
            Initialize();
        }

        public void Initialize()
        {
            if (homeDir != null) {
                if (Directory.Exists($"{CACHE_PATH}") == false)
                {
                    Directory.CreateDirectory($"{CACHE_PATH}");
                }
                cacheMovieNames = new Queue<string>();
                Clean();
                isInit = true;
            }
        }

        private static DataManager ins;
        public static DataManager Instance
        {
            get
            {
                if(ins == null)
                {
                    ins = new DataManager();
                }
                return ins;
            }
        }

        private Queue<string> cacheMovieNames;
        private bool isInit;

        private string[] GetAllCache ()
        {
            return Directory.GetFiles($"{CACHE_PATH}", "*", SearchOption.AllDirectories);
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
            if (isInit) {
                cacheMovieNames.Enqueue(fileName);
                while (cacheMovieNames.Count > 10)
                {
                    DeleteCacheMovie(cacheMovieNames.Dequeue());
                }
            }
        }

        public void DeleteCacheMovie(string fileName)
        {
            string path = $"{CACHE_PATH}/{fileName}";
            if (File.Exists(path)) {
                File.Delete(path);
            }
        }

        public void Export()
        {
            string exportDir = $"{EXPORT_PATH}{DateTime.Now.ToString("yyyyMMdd_HH-mm-ss")}";
            Directory.CreateDirectory(exportDir);
            while (cacheMovieNames.Count > 0)
            {
                string cacheMovieName = cacheMovieNames.Dequeue();
                if (File.Exists($"{CACHE_PATH}/{cacheMovieName}")) {
                    File.Move($"{CACHE_PATH}/{cacheMovieName}", $"{exportDir}/{cacheMovieName}");
                }
            }
            Clean();
        }

    }
}
