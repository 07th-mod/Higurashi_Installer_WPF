using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            EpisodeImage.Visibility = Visibility.Visible;
            EpisodeImage.Source = new BitmapImage(new Uri("/Resources/header1.jpg", UriKind.Relative));
            patcher.ChapterName = "onikakushi";
            patcher.DataFolder = "HigurashiEp01_Data";
            patcher.ExeName = "HigurashiEp01.exe";
            patcher.ImagePath = "/Resources/header1.jpg";
        }

        private void BtnWatanagashi_Click(object sender, RoutedEventArgs e)
        {
            Utils.ResizeWindow(this);
            Utils.ResetPath(this, true);
            EpisodeImage.Visibility = Visibility.Visible;
            EpisodeImage.Source = new BitmapImage(new Uri("/Resources/header2.jpg", UriKind.Relative));
            patcher.ChapterName = "watanagashi";
            patcher.DataFolder = "HigurashiEp02_Data";
            patcher.ExeName = "HigurashiEp02.exe";
            patcher.ImagePath = "/Resources/header2.jpg";
        }

        private void BtnTatarigoroshi_Click(object sender, RoutedEventArgs e)
        {
            Utils.ResizeWindow(this);
            Utils.ResetPath(this, true);
            EpisodeImage.Visibility = Visibility.Visible;
            EpisodeImage.Source = new BitmapImage(new Uri("/Resources/header3.jpg", UriKind.Relative));
            patcher.ChapterName = "tatarigoroshi";
            patcher.DataFolder = "HigurashiEp03_Data";
            patcher.ExeName = "HigurashiEp03.exe";
            patcher.ImagePath = "/Resources/header3.jpg";
        }

        private void BtnHimatsubushi_Click(object sender, RoutedEventArgs e)
        {
            Utils.ResizeWindow(this);
            Utils.ResetPath(this, true);
            EpisodeImage.Visibility = Visibility.Visible;
            EpisodeImage.Source = new BitmapImage(new Uri("/Resources/header4.jpg", UriKind.Relative));
            patcher.ChapterName = "himatsubushi";
            patcher.DataFolder = "HigurashiEp04_Data";
            patcher.ExeName = "HigurashiEp04.exe";
            patcher.ImagePath = "/Resources/header4.jpg";
        }

        private void BtnMeakashi_Click(object sender, RoutedEventArgs e)
        {
            Utils.ResizeWindow(this);
            Utils.ResetPath(this, true);
            EpisodeImage.Visibility = Visibility.Visible;
            EpisodeImage.Source = new BitmapImage(new Uri("/Resources/header5.jpg", UriKind.Relative));
            patcher.ChapterName = "meakashi";
            patcher.DataFolder = "HigurashiEp05_Data";
            patcher.ExeName = "HigurashiEp05.exe";
            patcher.ImagePath = "/Resources/header5.jpg";
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

        private void BtnInstallerFinish_Click(object sender, RoutedEventArgs e)
        {
            MainImage.Source = null;
            Utils.FinishInstallation(this);
        }


        //Main install logic starts here after the confirmation button is clicked
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            
            ConfirmationGrid.Visibility = Visibility.Collapsed;
            InstallerGrid.Visibility = Visibility.Visible;

            //get the latest .bat from github

           
                using (var client = new WebClient())
                {                                       
                    client.DownloadFile("https://raw.githubusercontent.com/07th-mod/resources/master/" + patcher.ChapterName + "/install.bat", patcher.InstallPath + "\\install.bat");                  
                    client.DownloadFile("https://github.com/07th-mod/resources/raw/master/dependencies.zip", patcher.InstallPath + "\\resources.zip");                                
                }
                    System.IO.Compression.ZipFile.ExtractToDirectory(patcher.InstallPath + "\\resources.zip", patcher.InstallPath);
            

            // If you don't do this, the InstallerGrid won't be visible
            Utils.DelayAction(5000, new Action(() => {
                Utils.runInstaller(this, "install.bat", patcher.InstallPath);

           }));
        }

        //Listeners for mouse
        private void MouseEnterOni(object sender, MouseEventArgs e)
        {
            EpisodeImage.Visibility = Visibility.Visible;
            EpisodeImage.Source = new BitmapImage(new Uri("/Resources/header1.jpg", UriKind.Relative));
        }

        private void MouseEnterWata(object sender, MouseEventArgs e)
        {
            EpisodeImage.Visibility = Visibility.Visible;
            EpisodeImage.Source = new BitmapImage(new Uri("/Resources/header2.jpg", UriKind.Relative));
        }

        private void MouseEnterTata(object sender, MouseEventArgs e)
        {
            EpisodeImage.Visibility = Visibility.Visible;
            EpisodeImage.Source = new BitmapImage(new Uri("/Resources/header3.jpg", UriKind.Relative));
        }

        private void MouseEnterHima(object sender, MouseEventArgs e)
        {
            EpisodeImage.Visibility = Visibility.Visible;
            EpisodeImage.Source = new BitmapImage(new Uri("/Resources/header4.jpg", UriKind.Relative));
        }

        private void MouseEnterMea(object sender, MouseEventArgs e)
        {
            EpisodeImage.Visibility = Visibility.Visible;
            EpisodeImage.Source = new BitmapImage(new Uri("/Resources/header5.jpg", UriKind.Relative));
        }

        private void MouseLeaveEpisode(object sender, MouseEventArgs e)
        {
            if (ActualWidth < 950)
            {
                EpisodeImage.Visibility = Visibility.Collapsed;
            }else
            {
                EpisodeImage.Source = new BitmapImage(new Uri(patcher.ImagePath, UriKind.Relative));
            }
        }
    }
}
