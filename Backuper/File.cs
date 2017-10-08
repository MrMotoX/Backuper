namespace Backuper
{
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
