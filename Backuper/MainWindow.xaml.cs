using System;
using System.Windows;
using System.Windows.Forms;
using System.IO;

namespace Backuper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FolderBrowserDialog folderBrowserDialog1;
        private Directory baseDir;
        private TargetFile targetFile;

        public MainWindow()
        {
            InitializeComponent();
            
            Directory.OnPathSet += Directory_PathSet;

            ConfigXml.LoadFromFile();
            baseDir = new Directory("Base");
            if (baseDir.GetName() != "")
            {
                targetFile = new TargetFile(baseDir.GetName());
                if (targetFile.GetPath() != null)
                {
                    UpdateTargetFilePathText();
                }
            }

            this.folderBrowserDialog1 = new FolderBrowserDialog()
            {
                ShowNewFolderButton = true,
                RootFolder = Environment.SpecialFolder.MyComputer
            };
        }

        private void Directory_PathSet(object sender, DirectoryPathSetEventArgs e)
        {
            if (e.Trait == "Base")
            {
                BaseDirectoryString.Text = e.Path;
                StatusString.Text = "Basmapp vald";
            }
        }

        private void PickDirectoryButton_Click(object sender, RoutedEventArgs e)
        {
            string tag = ((System.Windows.Controls.Button)sender).Tag.ToString();
            if (tag == "base")
            {
                folderBrowserDialog1.Description = "Välj den mapp du vill komprimera.";
                folderBrowserDialog1.SelectedPath = baseDir.GetPath();
            }
            else if (tag == "target")
            {
                folderBrowserDialog1.Description = "Välj den mapp du vill skapa den komprimerade filen i.";
                folderBrowserDialog1.SelectedPath = targetFile.GetPath();
            }

            DialogResult result = folderBrowserDialog1.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                if (tag == "base")
                {
                    baseDir.SetPath(folderBrowserDialog1.SelectedPath);
                    if (targetFile == null)
                    {
                        targetFile = new TargetFile(baseDir.GetName());
                    }
                    else
                    {
                        targetFile.SetName(baseDir.GetName());
                    }
                }
                else if (tag == "target")
                {
                    targetFile.SetPath(folderBrowserDialog1.SelectedPath);
                    UpdateTargetFilePathText();
                }
            }
        }

        private void UpdateTargetFilePathText()
        {
            try
            {
                TargetDirectoryString.Text = targetFile.GetFilePath();
                StatusString.Text = "Målmapp vald";
            }
            catch (ArgumentNullException)
            {
                UpdateStatusPickFolder();
            }
        }

        private void CreateZipButton_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(targetFile.GetFilePathWithTimeStamp()))
            {
                StatusString.Text = "Fil finns redan";
                return;
            }

            //StatusString.Text = "Skapar zip-fil";
            try
            {
                //ZipArchiver.CreateBackup(baseDir.GetPath(), targetFile.GetFilePathWithTimeStamp());
                ZipFileWithProgress.CreateFromDirectory(baseDir.GetPath(), targetFile.GetFilePathWithTimeStamp(),
                new BasicProgress<double>(p => StatusString.Text = $"{p:P2} av zip-filen är färdig"));
                StatusString.Text = "Zip-fil skapades";
            }
            catch (ArgumentNullException)
            {
                UpdateStatusPickFolder();
            }
            catch (DirectoryNotFoundException)
            {
                StatusString.Text = "Du har valt en mapp som inte finns";
            }
        }

        private void UpdateStatusPickFolder()
        {
            StatusString.Text = "Du måste välja en bas- och en målmapp";
        }

        private void NewZipFunction(string[] args)
        {
            string sourceDirectory = args[0],
            archive = args[1],
            archiveDirectory = Path.GetDirectoryName(Path.GetFullPath(archive)),
            unpackDirectoryName = Guid.NewGuid().ToString();

            File.Delete(archive);
            ZipFileWithProgress.CreateFromDirectory(sourceDirectory, archive,
                new BasicProgress<double>(p => Console.WriteLine($"{p:P2} archiving complete")));

            ZipFileWithProgress.ExtractToDirectory(archive, unpackDirectoryName,
                new BasicProgress<double>(p => Console.WriteLine($"{p:P0} extracting complete")));
        }
    }
}
