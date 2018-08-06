using log4net;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Higurashi_Installer_WPF
{

    public partial class MainWindow : Window
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public ExecutionModeComboViewModel ExecutionModeComboViewModel { get; set; } = new ExecutionModeComboViewModel();
        public ExpanderListViewModel ExpanderListViewModel { get; set; } = new ExpanderListViewModel();
        /* The patcher is the main object that will be used to store
         * all the information necessary for the installer to operate, making it easier to add new chapters
         * since you just need to populate it and the rest will be automatic. */
        public PatcherPOCO patcher;

        //Console window for displaying raw text output of installer.
        public DebugConsole consoleWindow;

        public MainWindow()
        {
            GlobalExceptionHandler.Setup();

            patcher = new PatcherPOCO(ExecutionModeComboViewModel);

            // Set the default installation method according to windows version
            // (see https://stackoverflow.com/questions/2819934/detect-windows-version-in-net for versions)
            if (System.Environment.OSVersion.Version.Major < 10)
            {
                ExecutionModeComboViewModel.BatchFileExecutionMode = PatcherPOCO.BatchFileExecutionModeEnum.ShellExecuteWithLogging;
            }

            DataContext = this;

            InitializeComponent();
            Logger.Setup();
            patcher.IsFull = true;

            //Set title window
            this.Title = $"07th Mod Patcher v1.19  |  LogFile: [{Logger.GetFullLogFilePath()}]";

            //Old .Net versions will crash when creating the JobManagement class - tell users to update .Net
            //They are missing the function "System.Runtime.InteropServices.Marshal.StructureToPtr()"
            try
            {
                Utils.InitJobManagement();
            }
            catch (Exception exception)
            {
                _log.Error(exception);
                MessageBox.Show("You probably have a very old version of .Net. Although the installer should work anyway, upgrading is highly recommended");
                if (MessageBox.Show("Do you want to open the .Net download Page?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    MessageBox.Show("Remember, click '.Net Framework [X.X.X] Runtime', NOT the SDK versions!");
                    Process.Start("https://www.microsoft.com/net/download/windows");
                    System.Threading.Thread.Sleep(2000);
                    MessageBox.Show("Remember, click '.Net Framework [X.X.X] Runtime', NOT the SDK versions!");
                }
                else
                {
                    MessageBox.Show("The installer will try to run anyway, but if you have issues, please try to update your .net version!");
                }
            }
        }

        private void BtnOnikakushi_Click(object sender, RoutedEventArgs e)
        {
            _log.Info("clicked onikakushi");
            Utils.ResizeWindow(this);
            Utils.ResetPath(this, true);

            EpisodeImage.Visibility = Visibility.Visible;
            EpisodeImage.Source = new BitmapImage(new Uri("/Resources/header1.jpg", UriKind.Relative));

            patcher.ChapterName = "onikakushi";            
            patcher.SetExeNames("HigurashiEp01.exe");
            patcher.ImagePath = "/Resources/header1.jpg";
            InstallCombo.IsEnabled = true;
        }

        private void BtnWatanagashi_Click(object sender, RoutedEventArgs e)
        {
            _log.Info("Clicked Watanagashi");
            Utils.ResizeWindow(this);
            Utils.ResetPath(this, true);

            EpisodeImage.Visibility = Visibility.Visible;
            EpisodeImage.Source = new BitmapImage(new Uri("/Resources/header2.jpg", UriKind.Relative));

            patcher.ChapterName = "watanagashi";
            patcher.SetExeNames("HigurashiEp02.exe");
            patcher.ImagePath = "/Resources/header2.jpg";
            InstallCombo.IsEnabled = true;
        }

        private void BtnTatarigoroshi_Click(object sender, RoutedEventArgs e)
        {
            _log.Info("Clicked Tatarigoroshi");
            Utils.ResizeWindow(this);
            Utils.ResetPath(this, true);

            EpisodeImage.Visibility = Visibility.Visible;
            EpisodeImage.Source = new BitmapImage(new Uri("/Resources/header3.jpg", UriKind.Relative));

            patcher.ChapterName = "tatarigoroshi";
            patcher.SetExeNames("HigurashiEp03.exe");
            patcher.ImagePath = "/Resources/header3.jpg";
            InstallCombo.IsEnabled = true;
        }

        private void BtnHimatsubushi_Click(object sender, RoutedEventArgs e)
        {
            _log.Info("Clicked Himatsubushi");
            Utils.ResizeWindow(this);
            Utils.ResetPath(this, true);

            EpisodeImage.Visibility = Visibility.Visible;
            EpisodeImage.Source = new BitmapImage(new Uri("/Resources/header4.jpg", UriKind.Relative));

            patcher.ChapterName = "himatsubushi";
            patcher.SetExeNames("HigurashiEp04.exe");
            patcher.ImagePath = "/Resources/header4.jpg";
            InstallCombo.IsEnabled = true;
        }

        private void BtnMeakashi_Click(object sender, RoutedEventArgs e)
        {
            _log.Info("Clicked Meakashi");
            Utils.ResizeWindow(this);
            Utils.ResetPath(this, true);

            EpisodeImage.Visibility = Visibility.Visible;
            EpisodeImage.Source = new BitmapImage(new Uri("/Resources/header5.jpg", UriKind.Relative));

            patcher.ChapterName = "meakashi";
            patcher.SetExeNames("HigurashiEp05.exe");
            patcher.ImagePath = "/Resources/header5.jpg";
            InstallCombo.IsEnabled = true;
        }

        private void BtnTsumihoroboshi_Click(object sender, RoutedEventArgs e)
        {
            _log.Info("Clicked Tsumihoroboshi");

            Utils.ResizeWindow(this);
            Utils.ResetPath(this, true);

            EpisodeImage.Visibility = Visibility.Visible;
            EpisodeImage.Source = new BitmapImage(new Uri("/Resources/header6.jpg", UriKind.Relative));

            patcher.ChapterName = "tsumihoroboshi";
            patcher.SetExeNames("HigurashiEp06.exe");
            patcher.ImagePath = "/Resources/header6.jpg";
            InstallCombo.IsEnabled = true;
        }

        private void BtnConsole_Click(object sender, RoutedEventArgs e)
        {
            _log.Info("Clicked Console Arcs");
            Utils.ResizeWindow(this);
            Utils.ResetPath(this, true);

            EpisodeImage.Visibility = Visibility.Visible;
            EpisodeImage.Source = new BitmapImage(new Uri("/Resources/console.png", UriKind.Relative));

            patcher.ChapterName = "console0arcs";
            patcher.SetExeNames("HigurashiEp04.exe");
            patcher.ImagePath = "/Resources/console.png";
            InstallCombo.IsEnabled = false;
        }

        private void BtnUminekoQuestion_Click(object sender, RoutedEventArgs e)
        {
            _log.Info("Clicked Umineko Question");
            Utils.ResizeWindow(this);
            Utils.ResetPath(this, true);

            EpisodeImage.Visibility = Visibility.Visible;
            EpisodeImage.Source = new BitmapImage(new Uri(@"/Resources/header_umineko_question.jpg", UriKind.Relative));

            patcher.ChapterName = "umineko-question";
            patcher.SetExeNames("Umineko1to4.exe", "Umineko1to4", "Umineko1to4.app");
            patcher.ImagePath = "/Resources/header_umineko_question.jpg";
            //Since Umineko dosn't have the voice patch only option, the user can't select it.
            InstallCombo.IsEnabled = false;
        }

        private void BtnUminekoAnswer_Click(object sender, RoutedEventArgs e)
        {
            _log.Info("Clicked Umineko Answer");
            Utils.ResizeWindow(this);
            Utils.ResetPath(this, true);

            EpisodeImage.Visibility = Visibility.Visible;
            EpisodeImage.Source = new BitmapImage(new Uri(@"/Resources/header_umineko_answer.jpg", UriKind.Relative));

            patcher.ChapterName = "umineko-answer";
            patcher.SetExeNames("Umineko5to8.exe", "umineko5to8", "Umineko5to8.app");
            patcher.ImagePath = "/Resources/header_umineko_answer.jpg";
            //Since Umineko dosn't have the voice patchonly option, the user can't select it.
            InstallCombo.IsEnabled = false;
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
            patcher.BatName = "higurashi-delete.bat";
            /* Higurashi now has a separate bat file to clean the Temp folder to avoid errors in the script */
            if (patcher.InstallPath.Contains("Higurashi"))
            {
                try
                {
                    Utils.runInstaller(this, "higurashi-delete.bat", patcher.InstallPath);
                }
                catch (Exception error)
                {
                    string errormsg = "(DelayAction) An error has occured while executing the" + patcher.BatName + ": " + error;
                    _log.Error(errormsg);
                }
            }
        }

        //Main install logic starts here after the confirmation button is clicked
        private async void Confirm_Click(object sender, RoutedEventArgs e)
        {
            _log.Info("Starting the installer");
            ConfirmationGrid.Visibility = Visibility.Collapsed;
            InstallerGrid.Visibility = Visibility.Visible;

            //Download the latest install.bat/voice.bat and .zip. If this fails, stop the installer. 
            if (!await Utils.DownloadResources(this, patcher))
            {
                string errormsg = "An error has occured while downloading the resources. The installation has been stopped.";
                _log.Error(errormsg);
                MessageBox.Show(errormsg);
                return;
            }

            try
            {
                if (patcher.BatchFileExecutionMode == PatcherPOCO.BatchFileExecutionModeEnum.Manual)
                {
                    Process.Start(patcher.InstallPath);
                    MessageBox.Show("Please manually run '" + patcher.BatName + "' in the folder that just opened.");
                }
                else
                {
                    // If you don't do this, the InstallerGrid won't be visible
                    _log.Info("Launching " + patcher.BatName + " in 3 seconds...");
                    Utils.DelayAction(3000, new Action(() =>
                    {
                        //Initiates installation process
                        try
                        {
                            Utils.runInstaller(this, patcher.BatName, patcher.InstallPath);
                        }
                        catch (Exception error)
                        {
                            string errormsg = "(DelayAction) An error has occured while executing the" + patcher.BatName + ": " + error;
                            _log.Error(errormsg);
                        }
                    }));
                }
            }
            catch (Exception error)
            {
                string errormsg = "(Confirm_Click) An error has occured while executing the" + patcher.BatName + ": " + error;
                _log.Error(errormsg);
                MessageBox.Show(errormsg);
            }
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

        private void BtnTsumihoroboshi_MouseEnter(object sender, MouseEventArgs e)
        {
            EpisodeImage.Visibility = Visibility.Visible;
            EpisodeImage.Source = new BitmapImage(new Uri("/Resources/header6.jpg", UriKind.Relative));
        }

        private void MouseEnterConsole(object sender, MouseEventArgs e)
        {
            EpisodeImage.Visibility = Visibility.Visible;
            EpisodeImage.Source = new BitmapImage(new Uri("/Resources/console.png", UriKind.Relative));
        }

        private void UminekoQuestion_MouseEnter(object sender, MouseEventArgs e)
        {
            EpisodeImage.Visibility = Visibility.Visible;
            EpisodeImage.Source = new BitmapImage(new Uri("/Resources/header_umineko_question.jpg", UriKind.Relative));
        }

        private void UminekoAnswer_MouseEnter(object sender, MouseEventArgs e)
        {
            EpisodeImage.Visibility = Visibility.Visible;
            EpisodeImage.Source = new BitmapImage(new Uri("/Resources/header_umineko_answer.jpg", UriKind.Relative));
        }

        private void MouseLeaveEpisode(object sender, MouseEventArgs e)
        {
            if (ActualWidth < 950)
            {
                EpisodeImage.Visibility = Visibility.Collapsed;
            }
            else
            {
                EpisodeImage.Source = new BitmapImage(new Uri(patcher.ImagePath, UriKind.Relative));
            }
        }

        private void BtnToggleConsole(object sender, RoutedEventArgs e)
        {
            if (consoleWindow.Visibility == Visibility.Visible)
            {
                consoleWindow.Hide();
                ShowDetailedProgressButton.Content = "Show Detailed Progress";
            }
            else
            {
                // Owner needs to be set so that the console window closes automataically
                // when the main installer window closes
                consoleWindow.Owner = this;
                consoleWindow.Show();
                ShowDetailedProgressButton.Content = "Hide Detailed Progress";
            }
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            consoleWindow = new DebugConsole { Owner = this };
            Logger.guiDebugConsoleAppender.SetDebugConsole(consoleWindow);
        }
    }

    // The following two classes are required to ensure only one expander is expanded at any time
    // See https://stackoverflow.com/questions/4449000/multiple-expander-have-to-collapse-if-one-is-expanded
    public class ExpanderToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value == parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (System.Convert.ToBoolean(value)) return parameter;
            return null;
        }
    }

    public class ExpanderListViewModel
    {
        public Object SelectedExpander { get; set; }
    }

}
