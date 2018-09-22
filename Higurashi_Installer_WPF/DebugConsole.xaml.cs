using log4net.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Higurashi_Installer_WPF
{
    /// <summary>
    /// Interaction logic for DebugConsole.xaml
    /// 
    /// The debug console consists of the "current line" and "previous lines"
    /// This is done just to make processing the Aria2C output easier.
    /// The current line is copied to the previous lines buffer, unless the 
    /// current line is:
    ///  - a temporary progress update from Aria2C (only the last temp update will be printed)
    ///  - a all-spaces blank line from Aria2C
    ///  Note also that any color codes (using the ASCII "ESC" character 
    ///  followed by the color code) are not used or interpreted, so you'll
    ///  see some garbage in the output whenever color codes are used.
    /// </summary>
    public partial class DebugConsole : Window
    {
        //Match "[#abc3b6" or similar (hex number always differs)
        //these sorts of lines are considered "temporary" as they are only used by Aria2C to report progress
        Regex Aria2CTempOutputRegex = new Regex(@"^\[#[\w]+ ",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

        bool currentLineIsTemporary;

        public DebugConsole()
        {
            InitializeComponent();
        }

        //Call this function to display a logging event on the DebugConsole.
        //semi-duplicate 'update' lines from Aria2C will be handled automatically.
        public void DisplayLoggingEvent(LoggingEvent loggingEvent)
        {
            // Discard all whitespace only lines, unless the line is "\r\n"
            // This is purely because aria2C sometimes outputs 80 spaces followed by a new line, which we want to ignore.
            if(loggingEvent.RenderedMessage != "\r\n" && loggingEvent.RenderedMessage.Trim() == "")
            {
                return;
            }

            //append current line to previous lines under the following two cases: 
            // - the current line is not a temporary aria2c line
            // - OR the next line is not a temporary aria2c line (this ensures 
            //   at least one temporary update is added to previous lines buffer)
            if (!currentLineIsTemporary || !Aria2CTempOutputRegex.IsMatch(loggingEvent.RenderedMessage))
            {
                DebugConsolePreviousLines.AppendText(DebugConsoleCurrentLine.Text);
                DebugConsolePreviousLines.AppendText(Environment.NewLine);
            }
            
            //set the 'current line' logging textbox, and mark the current line's temporary status
            DebugConsoleCurrentLine.Text = $"{loggingEvent.TimeStamp} [{loggingEvent.Level.Name}]: {loggingEvent.RenderedMessage}";
            currentLineIsTemporary = Aria2CTempOutputRegex.IsMatch(loggingEvent.RenderedMessage);
        }

        //If the user clicks the 'X' to close the window, just hide the window instead of closing it.
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void OpenLogFolder_Click(object sender, RoutedEventArgs e)
        {
            Logger.TryShowLogFolder();
        }
    }
}
