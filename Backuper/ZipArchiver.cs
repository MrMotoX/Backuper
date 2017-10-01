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
        }

        public static void SetTargetDirectory(string folder)
        {
            targetDirectory = folder;
        }

        public static void CreateBackup()
        {
            string targetFile = GetTargetFilePath();
            ZipFile.CreateFromDirectory(baseDirectory, targetFile);
        }

        private static string GetTargetFilePath()
        {
            string baseDirectoryName = new DirectoryInfo(baseDirectory).Name;
            return targetDirectory + @"\" + baseDirectoryName + @".zip";
        }
    }
}
