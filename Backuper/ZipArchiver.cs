using System;
using System.IO;
using System.IO.Compression;

namespace Backuper
{
    static class ZipArchiver
    {
        private static string baseDirectory;
        private static string targetDirectory;

        public static void SetBaseDirectory(string folder)
        {
            baseDirectory = folder;
            OnBaseDirectoryUpdated(EventArgs.Empty);
        }

        public static string GetBaseDirectory()
        {
            return baseDirectory;
        }

        public static void SetTargetDirectory(string folder)
        {
            targetDirectory = folder;
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

        public static event EventHandler BaseDirectoryUpdated;

        private static void OnBaseDirectoryUpdated(EventArgs e)
        {
            BaseDirectoryUpdated?.Invoke(null, e);
        }
    }
}
