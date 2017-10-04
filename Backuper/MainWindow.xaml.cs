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
        
        public MainWindow()
        {
            InitializeComponent();

            ZipArchiver.OnDirectoryUpdated += ZipArchiver_BaseDirectoryUpdated;
            ZipArchiver.LoadConfig();

            this.folderBrowserDialog1 = new FolderBrowserDialog()
            {
                ShowNewFolderButton = true,
                RootFolder = Environment.SpecialFolder.MyComputer
            };
        }

        private void ZipArchiver_BaseDirectoryUpdated(object sender, DirectoryUpdatedEventArgs e)
        {
            if (e.Directory == "Base")
            {
                ConfigXml.SaveToFieldInFile("BaseDirectory", e.Path);
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
                folderBrowserDialog1.SelectedPath = GetBaseDirectoryPath();
            }
            else if (tag == "target")
            {
                folderBrowserDialog1.Description = "Välj den mapp du vill skapa den komprimerade filen i.";
            }

            DialogResult result = folderBrowserDialog1.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                if (tag == "base")
                {
                    ZipArchiver.SetBaseDirectory(folderBrowserDialog1.SelectedPath);
                    //BaseDirectoryString.Text = folderBrowserDialog1.SelectedPath;
                    //StatusString.Text = "Basmapp vald";
                }
                else if (tag == "target")
                {
                    ZipArchiver.SetTargetDirectory(folderBrowserDialog1.SelectedPath);
                    try
                    {
                        TargetDirectoryString.Text = ZipArchiver.GetTargetFileName();
                        StatusString.Text = "Målmapp vald";
                    }
                    catch (ArgumentNullException)
                    {
                        UpdateStatusPickFolder();
                    }
                }
            }
        }

        private string GetBaseDirectoryPath()
        {
            string directoryPath = ConfigXml.GetField("BaseDirectory");
            if (directoryPath == "")
            {
                directoryPath = ZipArchiver.GetBaseDirectory();
            }
            return directoryPath;
        }

        private void CreateZipButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateStatusCreatingArchive();
            try
            {
                ZipArchiver.CreateBackup();
                UpdateStatusArchiveCreated();
            }
            catch (ArgumentNullException)
            {
                UpdateStatusPickFolder();
            }
            catch (DirectoryNotFoundException)
            {
                UpdateStatusNonExistingFolder();
            }
        }

        private void UpdateStatusCreatingArchive()
        {
            StatusString.Text = "Skapar zip-fil";
        }

        private void UpdateStatusArchiveCreated()
        {
            StatusString.Text = "Zip-fil skapades";
        }

        private void UpdateStatusPickFolder()
        {
            StatusString.Text = "Du måste välja en bas- och en målmapp";
        }

        private void UpdateStatusNonExistingFolder()
        {
            StatusString.Text = "Du har valt en mapp som inte finns";
        }
    }
}
