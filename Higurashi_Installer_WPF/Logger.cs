using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Higurashi_Installer_WPF
{
    public class GUIDebugConsoleAppender : AppenderSkeleton
    {
        DebugConsole debugConsole;

        public void SetDebugConsole(DebugConsole debugConsole) //TODO: remove this and use an event system to fire off callbacks
        {
            this.debugConsole = debugConsole;
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            if (debugConsole == null)
                return;

            Application.Current.Dispatcher.Invoke(() =>
            {
                debugConsole.DisplayLoggingEvent(loggingEvent);
            });
        }
    }

    public class Logger
    {
        public static GUIDebugConsoleAppender guiDebugConsoleAppender;
        public static readonly string logFilePath = @"Logs\PatcherLog.txt";
        public static RollingFileAppender roller;

        public static void Setup()
        {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();

            PatternLayout patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = "%date [%thread] %-5level - %message%newline";
            patternLayout.ActivateOptions();

            roller = new RollingFileAppender();
            roller.AppendToFile = false;
            roller.File = Path.Combine(Directory.GetCurrentDirectory(), logFilePath);
            roller.Layout = patternLayout;
            roller.MaxSizeRollBackups = 5;
            roller.MaximumFileSize = "1GB";
            roller.RollingStyle = RollingFileAppender.RollingMode.Size;
            roller.StaticLogFileName = true;
            roller.ActivateOptions();
            hierarchy.Root.AddAppender(roller);

            ConsoleAppender console = new ConsoleAppender();
            console.Layout = patternLayout;
            console.ActivateOptions();
            hierarchy.Root.AddAppender(console);

            MemoryAppender memory = new MemoryAppender();
            memory.ActivateOptions();
            hierarchy.Root.AddAppender(memory);

            guiDebugConsoleAppender = new GUIDebugConsoleAppender();
            guiDebugConsoleAppender.ActivateOptions();
            hierarchy.Root.AddAppender(guiDebugConsoleAppender);

            hierarchy.Root.Level = Level.Info;
            hierarchy.Configured = true;
        }

        /// <summary>
        /// This function MUST be called after logger's Setup() function has been called.
        /// </summary>
        /// <returns></returns>
        public static string GetFullLogFilePath()
        {
            return roller.File;
        }

        /// <summary>
        /// Will attempt to show the installer log folder to the user. On any failure
        /// will show a message box indicating that the log folder couldn't be shown.
        /// </summary>
        public static void TryShowLogFolder()
        {
            try
            {
                StandaloneUtils.OpenFolderInExplorer(Path.GetDirectoryName(GetFullLogFilePath()));
            }
            catch(Exception e)
            {
                MessageBox.Show($"Couldn't open log folder at [{GetFullLogFilePath()}]:\n\n{e.ToString()}");
            }
        }
    }
}
