using System;
using System.IO;
using System.IO.Compression;

namespace Backuper
{
    static class ZipArchiver
    {
        public static event EventHandler<DirectoryUpdatedEventArgs> OnDirectoryUpdated;

        private static string baseDirectory;
        private static string targetDirectory;

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

        public static void SetBaseDirectory(string folder)
        {
            baseDirectory = folder;
            OnDirectoryUpdated?.Invoke(null, new DirectoryUpdatedEventArgs("Base", folder));
        }

        public static string GetBaseDirectory()
        {
            return baseDirectory;
        }

        public static void SetTargetDirectory(string folder)
        {
            targetDirectory = folder;
            OnDirectoryUpdated?.Invoke(null, new DirectoryUpdatedEventArgs("Target", folder));
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

    public class DirectoryUpdatedEventArgs : EventArgs
    {
        public string Directory;
        public string Path;

        public DirectoryUpdatedEventArgs(string directory, string path)
        {
            Directory = directory;
            Path = path;
        }
    }
}
