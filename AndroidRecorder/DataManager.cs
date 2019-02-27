using System;
using System.IO;
using System.Collections.Generic;

namespace AndroidRecorder
{
    public class DataManager
    {
        const string CACHE_PATH = "cache";
        string EXPORT_PATH
        {
            get
            {
                return $"{homeDir}/Movies/";
            }
        }
        string homeDirins;
        string homeDir
        {
            get
            {
                if(homeDirins == "")
                {
                    homeDirins = ADBCommand.GetHomeDirPath();
                }
                return homeDirins;
            }
        }

        public DataManager()
        {
            if (Directory.Exists($"./{CACHE_PATH}") == false)
            {
                Directory.CreateDirectory($"./{CACHE_PATH}");
            }
            cacheMovieNames = new Queue<string>();
            homeDirins = ADBCommand.GetHomeDirPath();
            Clean();
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

        private string[] GetAllCache ()
        {
            return Directory.GetFiles($"./{CACHE_PATH}", "*", SearchOption.AllDirectories);
        }

        public void Clean()
        {
            foreach (string file in GetAllCache())
            {
                File.Delete(file);
            }
        }


        public void SetMovie(string fileName)
        {
            string moviePath = $"./{fileName}";
            string cacheMoviePath = $"./{CACHE_PATH}/{fileName}";
            File.Move(moviePath, cacheMoviePath);
            cacheMovieNames.Enqueue(fileName);
            while (cacheMovieNames.Count > 10) 
            {
                DeleteCacheMovie(cacheMovieNames.Dequeue());
            }
        }

        private void DeleteCacheMovie(string path)
        {
            File.Delete(path);
        }

        public void Export()
        {
            string exportDir = $"{EXPORT_PATH}{DateTime.Now.ToString("yyyyMMdd_HH-mm-ss")}";
            Directory.CreateDirectory(exportDir);
            while (cacheMovieNames.Count > 0)
            {
                string cacheMovieName = cacheMovieNames.Dequeue();
                File.Move($"./{CACHE_PATH}/{cacheMovieName}", $"{exportDir}/{cacheMovieName}");
            }
            Clean();
        }

    }
}
