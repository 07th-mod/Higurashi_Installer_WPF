using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows.Media.Animation;


//Util class for all methods related to the grid and general flow of the layout

namespace Higurashi_Installer_WPF
{
    static class Utils
    {
        public static void TreatCheckboxes(MainWindow window, Boolean IsCustom)
        {
            if (IsCustom)
            {
                window.ChkPatch.Visibility = Visibility.Visible;
                window.ChkPS3.Visibility = Visibility.Visible;
                window.ChkSteamSprites.Visibility = Visibility.Visible;
                window.ChkUI.Visibility = Visibility.Visible;
                window.ChkVoices.Visibility = Visibility.Visible;
            }
            else
            {
                window.ChkPatch.Visibility = Visibility.Collapsed;
                window.ChkPS3.Visibility = Visibility.Collapsed;
                window.ChkSteamSprites.Visibility = Visibility.Collapsed;
                window.ChkUI.Visibility = Visibility.Collapsed;
                window.ChkVoices.Visibility = Visibility.Collapsed;
            }
        }


        public static void ResetPath(MainWindow window, Boolean ChangedChapter)
        {
            if (ChangedChapter)
            {
                window.PathText.Text = "Insert install folder for the chapter";
            }
            window.TextWarningPath.Width = 422;
            window.TextWarningPath.Visibility = Visibility.Collapsed;
            window.BtnInstall.IsEnabled = true;
            window.BtnUninstall.IsEnabled = true;
        }

        public static void ResetDropBox(PatcherPOCO patcher)
        {
            patcher.IsVoiceOnly = false;
            patcher.IsCustom = false;
            patcher.IsFull = false;
        }

        /* Populates the object for installation
           And fills the list in the grid for user confirmation */
        public static void ConstructPatcher(MainWindow window, PatcherPOCO patcher)
        {
            patcher.InstallPath = window.PathText.Text;
            patcher.IsBackup = (Boolean)window.ChkBackup.IsChecked;
            patcher.InstallUpdate = "Installation";

            window.List1.Content = "Chapter: " + patcher.ChapterName;
            window.List2.Content = "Path: " + window.PathText.Text;
            window.List3.Content = "Process: Installation";
            window.List5.Content = "Backup: " + (patcher.IsBackup ? "Yes" : "No");

            if (patcher.IsCustom)
            {
                patcher.InstallType = "Custom";
                window.List4.Content = "Installation Type: Custom";
            }
            else if (patcher.IsFull)
            {
                patcher.InstallType = "Full";
                window.List4.Content = "Installation Type: Full";
            }
            else
            {
                patcher.InstallType = "Voice Only";
                window.List4.Content = "Installation Type: Voice Only";
            }
        }

        public static Boolean CheckValidFileExists(String path, PatcherPOCO patcher)
        {
            string file = path + "\\" + patcher.ExeName;
            return File.Exists(file);
        }

        public static void CheckValidFilePath(MainWindow window, PatcherPOCO patcher)
        {
            if (window.PathText.Text != "Insert install folder for the chapter")
            {
                window.InstallGrid.Visibility = Visibility.Collapsed;
                window.ConfirmationGrid.Visibility = Visibility.Visible;
                window.IconGrid.IsEnabled = false;
                ConstructPatcher(window, patcher);
            }
            else
            {
                window.TextWarningPath.Width = 250;
                window.TextWarningPath.Text = "Please select a folder before installing!";
                window.TextWarningPath.Visibility = Visibility.Visible;
            }
        }

        public static void ValidateFilePath(MainWindow window, PatcherPOCO patcher)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result.ToString() == "Ok")
            {
                window.PathText.Text = dialog.FileName;
                if (!CheckValidFileExists(dialog.FileName, patcher))
                {
                    window.TextWarningPath.Width = 422;
                    window.TextWarningPath.Text = "Invalid path! Please select a folder with the " + patcher.ChapterName + " .exe file";
                    window.TextWarningPath.Visibility = Visibility.Visible;
                    window.BtnInstall.IsEnabled = false;
                    window.BtnUninstall.IsEnabled = false;
                }
                else
                {
                    window.TextWarningPath.Text = patcher.ChapterName + " .exe file found!";
                    window.TextWarningPath.Visibility = Visibility.Visible;
                    window.BtnInstall.IsEnabled = true;
                    window.BtnUninstall.IsEnabled = true;
                    window.TextWarningPath.Width = 180;
                }
            }
        }

        public static void InstallComboChoose(MainWindow window, PatcherPOCO patcher)
        {
            switch (window.InstallCombo.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last())
            {
                case "Full":
                    ResetDropBox(patcher);
                    patcher.IsFull = true;
                    TreatCheckboxes(window, false);
                    break;
                case "Custom":
                    ResetDropBox(patcher);
                    patcher.IsCustom = true;
                    TreatCheckboxes(window, true);
                    break;
                case "Voice only":
                    ResetDropBox(patcher);
                    patcher.IsVoiceOnly = true;
                    TreatCheckboxes(window, false);
                    break;
            }
        }

        public static void ResizeWindow(MainWindow window)
        {
            if (window.ActualWidth < 950)
            {
                window.AnimateWindowSize(window.ActualWidth + 500);
                if (window.InstallGrid.Visibility.Equals(Visibility.Collapsed))
                {
                    window.InstallGrid.Visibility = Visibility.Visible;

                }
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




