using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Higurashi_Installer_WPF
{

    public partial class MainWindow : Window
    {
        PatcherPOCO patcher = new PatcherPOCO();

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
            patcher.ChapterName = "Onikakushi";
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
            patcher.ChapterName = "Watanagashi";
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
            patcher.ChapterName = "Tatarigoroshi";
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
            patcher.ChapterName = "Himatsubushi";
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
            patcher.ChapterName = "Meakashi";
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
