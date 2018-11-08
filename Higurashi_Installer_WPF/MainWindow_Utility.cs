using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Higurashi_Installer_WPF
{
    /// <summary>
    /// Holds utility functions which aid in setting the MainWindow GUI
    /// </summary>
    public partial class MainWindow : Window
    {
        public void TextWarningPath_SetTextError(string text)
        {
            TextWarningPath_SetText(text, Colors.Red);
        }

        public void TextWarningPath_SetTextSuccess(string text)
        {
            TextWarningPath_SetText(text, Colors.Lime);
        }

        public void TextWarningPath_SetTextInformation(string text)
        {
            TextWarningPath_SetText(text, Colors.White);
        }

        /// <summary>
        /// Sets the text to the specified color, and makes the text visible
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        private void TextWarningPath_SetText(string text, Color color)
        {
            TextWarningPath.Text = text;
            TextWarningPath.Foreground = new SolidColorBrush(color);
            TextWarningPath.Visibility = Visibility.Visible;
        }
    }
}
