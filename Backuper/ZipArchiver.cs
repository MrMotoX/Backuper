using System;
using System.IO;
using System.IO.Compression;

namespace Backuper
{
    static class ZipArchiver
    {
        //public static event EventHandler<DirectorySetEventArgs> OnDirectoryUpdated;

        private static string baseDirectory;
        private static string targetDirectory;
        private static Directory baseDir;
        private static Directory targetDir;

        public static void LoadConfig()
        {
            ConfigXml.LoadFromFile();

            if (ConfigXml.GetField("BaseDirectory") != "")
            {
                SetBaseDirectory(ConfigXml.GetField("BaseDirectory"));
            }
            if (ConfigXml.GetField("TargetDirectory") != "")
            {
                SetTargetDirectory(ConfigXml.GetField("TargetDirectory"));
            }
        }

        public static void SetBaseDirectory(string path)
        {
            baseDir = new Directory(path);
            //baseDirectory = folder;
            //OnDirectoryUpdated?.Invoke(null, new DirectorySetEventArgs("Base", path));
        }

        public static string GetBaseDirectory()
        {
            return baseDirectory;
        }

        public static void SetTargetDirectory(string folder)
        {
            targetDirectory = folder;
            OnDirectoryUpdated?.Invoke(null, new DirectorySetEventArgs("Target", folder));
        }

        public static void CreateBackup()
        {
            ZipFile.CreateFromDirectory(baseDirectory, GetTargetFileName());
        }

        public static string GetTargetFileName()
        {
            string baseDirectoryName = new DirectoryInfo(baseDirectory).Name;
            return targetDirectory + @"\" + baseDirectoryName + @".zip";
        }
    }

    //public class DirectoryUpdatedEventArgs : EventArgs
    //{
    //    public string Directory;
    //    public string Path;

    //    public DirectoryUpdatedEventArgs(string directory, string path)
    //    {
    //        Directory = trait;
    //        Path = path;
    //    }
    //}
}
