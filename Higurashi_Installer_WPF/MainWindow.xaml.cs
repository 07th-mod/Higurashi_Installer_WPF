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
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

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
        }

        private void ResetLayout()
        {
            MainGrid.Visibility = Visibility.Visible;
            InstallGrid.Visibility = Visibility.Collapsed;
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
