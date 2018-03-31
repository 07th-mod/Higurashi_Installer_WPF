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

            Utils.ResizeWindow(this);
            MainImage.Source = new BitmapImage(new Uri("/Resources/header1.jpg", UriKind.Relative));
            Utils.ResetPath(this, true);
            patcher.ChapterName = "Onikakushi";
            patcher.ExeName = "HigurashiEp01.exe";
        }

        private void BtnWatanagashi_Click(object sender, RoutedEventArgs e)
        {
            Utils.ResizeWindow(this);
            MainImage.Source = new BitmapImage(new Uri("/Resources/header2.jpg", UriKind.Relative));
            Utils.ResetPath(this, true);
            patcher.ChapterName = "Watanagashi";
            patcher.ExeName = "HigurashiEp02.exe";

        }

        private void BtnTatarigoroshi_Click(object sender, RoutedEventArgs e)
        {
            Utils.ResizeWindow(this);
            MainImage.Source = new BitmapImage(new Uri("/Resources/header3.jpg", UriKind.Relative));
            Utils.ResetPath(this, true);
            patcher.ChapterName = "Tatarigoroshi";
            patcher.ExeName = "HigurashiEp03.exe";
        }

        private void BtnHimatsubushi_Click(object sender, RoutedEventArgs e)
        {
            Utils.ResizeWindow(this);
            MainImage.Source = new BitmapImage(new Uri("/Resources/header4.jpg", UriKind.Relative));
            Utils.ResetPath(this, true);
            patcher.ChapterName = "Himatsubushi";
            patcher.ExeName = "HigurashiEp04.exe";
        }

        private void BtnMeakashi_Click(object sender, RoutedEventArgs e)
        {
            Utils.ResizeWindow(this);
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
}
