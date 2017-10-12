namespace Backuper
{
    class TargetFile : Directory
    {
        private string name;
        private string filePath;

        public TargetFile(string name)
            : base("Target")
        {
            SetName(name);
        }

        public TargetFile(string name, string dirPath)
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
            return GetFilePathNoExtension() + @".zip";
        }

        public string GetFilePathWithTimeStamp()
        {
            return GetFilePathNoExtension() + Time.GetYearMonthDayHourMinute() + @".zip";
        }

        private string GetFilePathNoExtension()
        {
            return GetPath() + @"\" + this.name;
        }
    }
}
