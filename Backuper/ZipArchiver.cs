using System;
using System.IO;
using System.IO.Compression;

namespace Backuper
{
    static class ZipArchiver
    {
        private static string baseDirectory;
        private static string targetDirectory;
        //private static Directory baseDir;
        //private static Directory targetDir;

        //public static void LoadConfig()
        //{
        //    ConfigXml.LoadFromFile();

        //    baseDir = new Directory("Base");
        //    targetDir = new Directory("Target");
        //}

        //public static void SetBaseDirectory(string path)
        //{
        //    baseDir = new Directory(path);
        //}

        //public static string GetBaseDirectory()
        //{
        //    return baseDirectory;
        //}

        //public static void SetTargetDirectory(string folder)
        //{
        //    targetDirectory = folder;
        //    OnDirectoryUpdated?.Invoke(null, new DirectoryPathSetEventArgs("Target", folder));
        //}

        public static void CreateBackup(string baseDirPath, string targetFilePath)
        {
            ZipFile.CreateFromDirectory(baseDirPath, targetFilePath);
        }

        public static string GetTargetFileName()
        {
            string baseDirectoryName = new DirectoryInfo(baseDirectory).Name;
            return targetDirectory + @"\" + baseDirectoryName + @".zip";
        }
    }
}
