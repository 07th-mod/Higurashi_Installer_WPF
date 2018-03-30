using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace Higurashi_Installer_WPF
{

    public partial class MainWindow : Window
    {
        PatcherPOCO patcher = new PatcherPOCO();
        String defaultPathText = "Insert install folder for the chapter";

        public MainWindow()
        {
            InitializeComponent();            
            patcher.IsFull = true;
        }

        private void BtnOnikakushi_Click(object sender, RoutedEventArgs e)
        {
            if (ActualWidth < 950)
            {
                this.AnimateWindowSize(ActualWidth + 500);
                if (InstallGrid.Visibility.Equals(Visibility.Collapsed))
                {
                    InstallGrid.Visibility = Visibility.Visible;
                    
                }
            }
            MainImage.Source = new BitmapImage(new Uri("/Resources/header2.jpg", UriKind.Relative));
            ResetPath(true);
            patcher.ChapterName = "Onikakushi";
            patcher.ExeName = "HigurashiEp02.exe";
        }

        private void BtnWatanagashi_Click(object sender, RoutedEventArgs e)
        {
            if (ActualWidth < 950)
            {
                this.AnimateWindowSize(ActualWidth + 500);
                if (InstallGrid.Visibility.Equals(Visibility.Collapsed))
                {
                    InstallGrid.Visibility = Visibility.Visible;

                }
            }
            MainImage.Source = new BitmapImage(new Uri("/Resources/header1.jpg", UriKind.Relative));
            ResetPath(true);
            patcher.ChapterName = "Watanagashi";
            patcher.ExeName = "HigurashiEp01.exe";
        }

        private void BtnTatarigoroshi_Click(object sender, RoutedEventArgs e)
        {
            if (ActualWidth < 950)
            {
                this.AnimateWindowSize(ActualWidth + 500);
                if (InstallGrid.Visibility.Equals(Visibility.Collapsed))
                {
                    InstallGrid.Visibility = Visibility.Visible;
                    
                }
            }
            MainImage.Source = new BitmapImage(new Uri("/Resources/header3.jpg", UriKind.Relative));
            ResetPath(true);
            patcher.ChapterName = "Tatarigoroshi";
            patcher.ExeName = "HigurashiEp03.exe";
        }

        private void BtnHimatsubushi_Click(object sender, RoutedEventArgs e)
        {
            if (ActualWidth < 950)
            {
                this.AnimateWindowSize(ActualWidth + 500);
                if (InstallGrid.Visibility.Equals(Visibility.Collapsed))
                {
                    InstallGrid.Visibility = Visibility.Visible;
                }
            }
            MainImage.Source = new BitmapImage(new Uri("/Resources/header4.jpg", UriKind.Relative));
            ResetPath(true);
            patcher.ChapterName = "Himatsubushi";
            patcher.ExeName = "HigurashiEp04.exe";
        }

        private void BtnMeakashi_Click(object sender, RoutedEventArgs e)
        {
            if (ActualWidth < 950)
            {
                this.AnimateWindowSize(ActualWidth + 500);
                if (InstallGrid.Visibility.Equals(Visibility.Collapsed))
                {
                    InstallGrid.Visibility = Visibility.Visible;
                }
            }
            MainImage.Source = new BitmapImage(new Uri("/Resources/header5.jpg", UriKind.Relative));
            ResetPath(true);
            patcher.ChapterName = "Meakashi";
            patcher.ExeName = "HigurashiEp05.exe";
        }

        private void InstallCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {          
            switch (InstallCombo.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last()) {
                case "Full":
                    ResetDropBox();
                    patcher.IsFull = true;
                    TreatCheckboxes(false);
                    break;
                case "Custom":
                    ResetDropBox();
                    patcher.IsCustom = true;
                    TreatCheckboxes(true);
                    break;
                case "Voice only":
                    ResetDropBox();
                    patcher.IsVoiceOnly = true;
                    TreatCheckboxes(false);
                    break;
            }
        }

        private void BtnInstall_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ResetDropBox()
        {
            patcher.IsVoiceOnly = false;
            patcher.IsCustom = false;
            patcher.IsFull = false;
        }

        private void TreatCheckboxes(Boolean IsCustom)
        {
            if (IsCustom)
            {
                ChkPatch.Visibility = Visibility.Visible;
                ChkPS3.Visibility = Visibility.Visible;
                ChkSteamSprites.Visibility = Visibility.Visible;
                ChkUI.Visibility = Visibility.Visible;
                ChkVoices.Visibility = Visibility.Visible;
            }
            else
            {
                ChkPatch.Visibility = Visibility.Collapsed;
                ChkPS3.Visibility = Visibility.Collapsed;
                ChkSteamSprites.Visibility = Visibility.Collapsed;
                ChkUI.Visibility = Visibility.Collapsed;
                ChkVoices.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnPath_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result.ToString() == "Ok")
            {
                PathText.Text = dialog.FileName;
                if (!CheckValidFileExists(dialog.FileName))
                {
                    TextWarningPath.Width = 422;
                    TextWarningPath.Text = "Invalid path! Please select a folder with the " + patcher.ChapterName + " .exe file";
                    TextWarningPath.Visibility = Visibility.Visible;
                    BtnInstall.IsEnabled = false;
                    BtnUninstall.IsEnabled = false;
                } else
                {
                    TextWarningPath.Text = patcher.ChapterName + " .exe file found!";
                    TextWarningPath.Visibility = Visibility.Visible;
                    TextWarningPath.Width = 180;
                }

            }
        }

        private Boolean CheckValidFileExists(String path)
        {
            string file = path + "\\" + patcher.ExeName;
            return File.Exists(file);
        }

        public void ResetPath(Boolean ChangedChapter)
        {   
            if (ChangedChapter)
            {
                PathText.Text = defaultPathText;
            }
            TextWarningPath.Width = 422;
            TextWarningPath.Visibility = Visibility.Collapsed;
            BtnInstall.IsEnabled = true;
            BtnUninstall.IsEnabled = true;
        }


    }

    public static class WindowUtilties
    {
        public static void AnimateWindowSize(this Window target, double newWidth)
        {
            
                var sb = new Storyboard { Duration = new Duration(new TimeSpan(0, 0, 0, 0, 200)) };

                var aniWidth = new DoubleAnimationUsingKeyFrames();

                aniWidth.Duration = new Duration(new TimeSpan(0, 0, 0, 0, 200));

                aniWidth.KeyFrames.Add(new EasingDoubleKeyFrame(target.ActualWidth, KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 00))));
                aniWidth.KeyFrames.Add(new EasingDoubleKeyFrame(newWidth, KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 200))));

                Storyboard.SetTarget(aniWidth, target);
                Storyboard.SetTargetProperty(aniWidth, new PropertyPath(Window.WidthProperty));

                sb.Children.Add(aniWidth);

                sb.Begin();
            
        }
    }
}
