using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Higurashi_Installer_WPF
{

    public partial class MainWindow : Window
    {
        PatcherPOCO patcher = new PatcherPOCO();
        WebClient downloader = new WebClient();

        public MainWindow()
        {
            InitializeComponent();
            patcher.IsFull = true;
        }

        private void BtnOnikakushi_Click(object sender, RoutedEventArgs e)
        {

            Utils.ResizeWindow(this);
            Utils.ResetPath(this, true);
            MainImage.Source = new BitmapImage(new Uri("/Resources/header1.jpg", UriKind.Relative));
            patcher.ChapterName = "onikakushi";
            patcher.DataFolder = "HigurashiEp01_Data";
            patcher.ExeName = "HigurashiEp01.exe";
        }

        private void BtnWatanagashi_Click(object sender, RoutedEventArgs e)
        {
            Utils.ResizeWindow(this);
            Utils.ResetPath(this, true);
            MainImage.Source = new BitmapImage(new Uri("/Resources/header2.jpg", UriKind.Relative));
            patcher.ChapterName = "watanagashi";
            patcher.ExeName = "HigurashiEp02.exe";

        }

        private void BtnTatarigoroshi_Click(object sender, RoutedEventArgs e)
        {
            Utils.ResizeWindow(this);
            Utils.ResetPath(this, true);
            MainImage.Source = new BitmapImage(new Uri("/Resources/header3.jpg", UriKind.Relative));
            patcher.ChapterName = "tatarigoroshi";
            patcher.ExeName = "HigurashiEp03.exe";
        }

        private void BtnHimatsubushi_Click(object sender, RoutedEventArgs e)
        {
            Utils.ResizeWindow(this);
            Utils.ResetPath(this, true);
            MainImage.Source = new BitmapImage(new Uri("/Resources/header4.jpg", UriKind.Relative));
            patcher.ChapterName = "himatsubushi";
            patcher.ExeName = "HigurashiEp04.exe";
        }

        private void BtnMeakashi_Click(object sender, RoutedEventArgs e)
        {
            Utils.ResizeWindow(this);
            Utils.ResetPath(this, true);
            MainImage.Source = new BitmapImage(new Uri("/Resources/header5.jpg", UriKind.Relative));
            patcher.ChapterName = "meakashi";
            patcher.DataFolder = "HigurashiEp05_Data";
            patcher.ExeName = "HigurashiEp05.exe";
        }

        private void InstallCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Utils.InstallComboChoose(this, patcher);
        }

        private void BtnInstall_Click(object sender, RoutedEventArgs e)
        {
            Utils.CheckValidFilePath(this, patcher);
        }


        private void ConfirmBack_Click(object sender, RoutedEventArgs e)
        {
            ConfirmationGrid.Visibility = Visibility.Collapsed;
            InstallGrid.Visibility = Visibility.Visible;
            IconGrid.IsEnabled = true;
        }

        private void BtnPath_Click(object sender, RoutedEventArgs e)
        {
            Utils.ValidateFilePath(this, patcher);
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            
            ConfirmationGrid.Visibility = Visibility.Collapsed;
            InstallerGrid.Visibility = Visibility.Visible;

            //get the latest .bat from github

            if (!System.IO.Directory.Exists(patcher.InstallPath))
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile("https://raw.githubusercontent.com/07th-mod/resources/master/" + patcher.ChapterName + "/install.bat", patcher.InstallPath + "\\install.bat");
                    client.DownloadFile("https://transfer.sh/6jBrM/temp.zip", patcher.InstallPath + "\\resources.zip");            
                }
                    System.IO.Compression.ZipFile.ExtractToDirectory(patcher.InstallPath + "\\resources.zip", patcher.InstallPath);
            }

            // If you don't do this, the InstallerGrid won't be visible
            Utils.DelayAction(5000, new Action(() => {
                Utils.runInstaller(this, "install.bat", patcher.InstallPath);

           }));
        }
    }
}
