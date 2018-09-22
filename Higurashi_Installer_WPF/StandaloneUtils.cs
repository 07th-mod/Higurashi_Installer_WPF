using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Higurashi_Installer_WPF
{
    class StandaloneUtils
    {
        public static void OpenFolderInExplorer(string path)
        {
            Process.Start(new System.Diagnostics.ProcessStartInfo()
            {
                FileName = path,
                UseShellExecute = true,
                Verb = "open"
            });
        }

        public static bool SelectFileInExplorer(string filePath)
        {
            if(!File.Exists(filePath))
            {
                return false;
            }

            // combine the arguments together
            // it doesn't matter if there is a space after ','
            string argument = "/select, \"" + filePath + "\"";

            System.Diagnostics.Process.Start("explorer.exe", argument);

            return true;
        }
    }
}
