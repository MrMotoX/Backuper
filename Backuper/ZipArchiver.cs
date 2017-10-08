using System;
using System.IO;
using System.IO.Compression;

namespace Backuper
{
    static class ZipArchiver
    {
        public static void CreateBackup(string baseDirPath, string targetFilePath)
        {
            ZipFile.CreateFromDirectory(baseDirPath, targetFilePath);
        }
    }
}
