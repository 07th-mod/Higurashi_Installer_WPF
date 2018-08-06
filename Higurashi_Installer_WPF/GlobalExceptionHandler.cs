using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Higurashi_Installer_WPF
{
    class GlobalExceptionHandler
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void Setup()
        {
            AppDomain.CurrentDomain.UnhandledException += GlobalUnhandledExceptionHandler;
        }

        private static void GlobalUnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            Exception e = default(Exception);
            e = (Exception)unhandledExceptionEventArgs.ExceptionObject;
            string windowCaption = "07th Mod Installer Crash Reporter";
            string errorHeader = $"Sorry, it looks like the installer crashed. A log file has been saved to [{Logger.GetFullLogFilePath()}]. " +
                                  "We would appreciate if you report this issue and provide your log - after you press OK you can choose to report a Git Issue or visit our Discord.";
            string outerErrorDescription = $"Outer Exception: {e.Message}\n{e.StackTrace}";
            string innerErrorDescription = $"Inner Exception: {(e.InnerException == null ? "No Inner Exception" : e.InnerException.ToString())}";

            //Try and log the error description to file (If there is an exception but the logger is not yet initialized, just continue to displaying the messageboxes)
            try
            {
                _log.Error(outerErrorDescription);
                _log.Error(innerErrorDescription);
            }
            catch (Exception _) { }

            try
            {
                //Inform user the program has crashed, open the discord server/github page if user wishes
                MessageBox.Show(errorHeader, windowCaption);
                if (MessageBox.Show("Do you want to open the 07th Mod Discord (#higu_support)? (https://discord.gg/acSbBtD)", windowCaption, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Process.Start("https://discord.gg/acSbBtD");
                }
                if (MessageBox.Show("Do you want to open the Github Issues Page? (https://github.com/07th-mod/Higurashi_Installer_WPF/issues)", windowCaption, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Process.Start("https://github.com/07th-mod/Higurashi_Installer_WPF/issues");
                }

                //Now show the raw error information
                MessageBox.Show("The raw error information will now be displayed. You can also view this information in the log file.", windowCaption);
                if (e.InnerException != null)
                {
                    MessageBox.Show(innerErrorDescription, windowCaption);
                }
                MessageBox.Show(outerErrorDescription, windowCaption);
            }
            catch (Exception _) { }
        }

    }
}
