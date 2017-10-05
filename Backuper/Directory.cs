using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backuper
{
    class Directory
    {
        public static event EventHandler<DirectoryPathSetEventArgs> OnPathSet;

        private string trait;
        private string path;

        public Directory()
            : this("Base")
        { }

        public Directory(string trait)
        {
            SetTrait(trait);
            if (ConfigXml.GetField(trait + "Directory") != "")
            {
                SetPath(ConfigXml.GetField(trait + "Directory"));
            }
        }

        public Directory(string trait, string path)
        {
            SetTrait(trait);
            SetPath(path);
        }

        private void SetTrait(string trait)
        {
            this.trait = trait;
        }

        public void SetPath(string path)
        {
            this.path = path;
            ConfigXml.SaveToFieldInFile(GetTrait() + "Directory", this.path);
            OnPathSet?.Invoke(this, new DirectoryPathSetEventArgs(GetTrait(), this.path));
        }

        public string GetTrait()
        {
            return this.trait;
        }

        public string GetName()
        {
            string name = new DirectoryInfo(this.path).Name;
            return name;
        }

        public string GetPath()
        {
            return this.path;
        }
    }

    public class DirectoryPathSetEventArgs : EventArgs
    {
        public string Trait;
        public string Path;

        public DirectoryPathSetEventArgs(string trait, string path)
        {
            this.Trait = trait;
            this.Path = path;
        }
    }

    class File : Directory
    {
        private string name;
        private string filePath;

        public File(string name)
            : base("Target")
        {
            SetName(name);
        }

        public File(string name, string dirPath)
            : base("Target", dirPath)
        {
            SetName(name);
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public string GetFilePath()
        {
            return GetPath() + @"\" + this.name + @".zip";
        }
    }
}
