using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Higurashi_Installer_WPF
{
    class StandaloneUtils
    {
        public class BatchFileModifier
        {
            readonly static Regex aria2cRegex = new Regex(@"aria2c([^\s]*)\s+", RegexOptions.IgnoreCase);
            readonly static Regex httpRegex = new Regex(@"http", RegexOptions.IgnoreCase);

            /// <summary>
            /// Modifies aria2c commands in a batch file to disable IPV6
            /// Uses the heuristic that every aria2c command has the string "http" on the same line, to prevent false positives
            /// </summary>
            /// <param name="path"></param>
            public static void DisableIPV6(string path)
            {
                var modifiedLines = File.ReadAllLines(path).Select(line =>
                    (aria2cRegex.IsMatch(line) && httpRegex.IsMatch(line)) ? aria2cRegex.Replace(line, "aria2c.exe --disable-ipv6=true ") : line
                );

                File.WriteAllLines(path, modifiedLines);
            }

            public static bool ForceWindowsLineEndings(string path)
            {
                //Force the batch file to CRLF format before executing it
                //https://stackoverflow.com/questions/841396/what-is-a-quick-way-to-force-crlf-in-c-sharp-net
                string entireBatchFile = File.ReadAllText(path);
                string fixedBatchFile = entireBatchFile.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "\r\n");

                if (entireBatchFile != fixedBatchFile)
                {
                    File.WriteAllText(path, fixedBatchFile);
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

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
