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
        PatcherUtils Utils = new PatcherUtils();

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
            MainImage.Source = new BitmapImage(new Uri("/Resources/header1.jpg", UriKind.Relative));
            Utils.ResetPath(this, true);
            patcher.ChapterName = "Onikakushi";
            patcher.ExeName = "HigurashiEp01.exe";
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
            MainImage.Source = new BitmapImage(new Uri("/Resources/header2.jpg", UriKind.Relative));
            Utils.ResetPath(this, true);
            patcher.ChapterName = "Watanagashi";
            patcher.ExeName = "HigurashiEp02.exe";

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
            Utils.ResetPath(this, true);
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
            Utils.ResetPath(this, true);
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
            Utils.ResetPath(this, true);
            patcher.ChapterName = "Meakashi";
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
