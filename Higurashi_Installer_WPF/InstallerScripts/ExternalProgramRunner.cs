using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Higurashi_Installer_WPF.InstallerScripts
{
    public class ExternalProgramRunner
    {
        //TODO: is this correct?
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary> Same as 'Run' but returns the true/false instead of an integer value</summary>
        static public bool Run(string exePath, string arguments, string printOnSuccess, string printOnFail) => IRun(exePath, arguments, printOnSuccess, printOnFail) == 0;

        /// <summary>
        /// <returns>call this if you want the integer return value from a process
        /// </summary>
        /// <remarks>
        /// see https://stackoverflow.com/questions/240171/launching-an-application-exe-from-c
        /// </remarks>
        /// <param name="exePath"></param>
        /// <param name="arguments"></param>
        /// <param name="logger"></param>
        /// <param name="printOnSuccess"></param>
        /// <param name="printOnFail"></param>
        /// <returns>Returns true if program execution successful, talse otherwise</returns>
        static public int IRun(string exePath, string arguments, string printOnSuccess = null, string printOnFail = null)
        {
            Process proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = exePath,
                    Arguments = arguments,
                    //UseShellExecute = false,  //color won't be displayed if this is enabled
                    //RedirectStandardOutput = true,
                    //CreateNoWindow = true
                }
            };

            //printout data 
            /*proc.OutputDataReceived += new DataReceivedEventHandler((sender, e) => {
                if (!String.IsNullOrEmpty(e.Data))
                {
                    //con.Write(e.Data);
                    _log.LogColor(e.Data, ConsoleColor.White, ConsoleColor.Black, true, textDescription:"");
                }
            });*/

            _log.Info($"Running: '{exePath}' Args: '{arguments}'");

            // Run the external process & wait for it to finish
            proc.Start();
            //proc.BeginOutputReadLine(); //begin asynchronous read operations, see https://msdn.microsoft.com/en-us/library/system.diagnostics.process.beginoutputreadline(v=vs.110).aspx
            proc.WaitForExit();

            if (proc.ExitCode == 0)
            {
                if (printOnSuccess != null)
                    _log.Info(printOnSuccess);
            }
            else
            {
                if (printOnFail != null)
                    _log.Error(printOnFail);
            }

            return proc.ExitCode;
        }

        /// <summary>
        /// Extract a file to the specified extractionPath
        /// </summary>
        /// <param name="sevenZipPath">7zip .exe path to be executed</param>
        /// <param name="archivePath">Absolute Path to the archive to be extracted (.zip, .7z etc)</param>
        /// <param name="extractionPath">Absolute Path where files should be extract to</param>
        /// <param name="logger">Logger class where 7zip console output should be placed</param>
        /// <returns>true if extraction successful, false otherwise</returns>
        static public bool ExtractFile(string sevenZipPath, string archivePath, string extractionPath)
        {
            _log.Info($"Begin 7zip Extracting '{archivePath}' to '{extractionPath}'");
            return Run(exePath: sevenZipPath,
                       arguments: $"x {archivePath} -aoa -o{extractionPath}",
                       printOnSuccess: $"Extraction of {archivePath} OK",
                       printOnFail: $"Error during extraction of {archivePath}");
        }


        /// <summary>
        /// Download a metalink from the specified URL and place all files in the 'downloadFolder
        /// </summary>
        /// <remarks>
        /// Metalink files can be created using the tool here: https://github.com/drojf/MetalinkGenerator
        /// </remarks>
        /// <param name="aria2cPath">Aria2c .exe path to be executed</param>
        /// <param name="metaLinkURL">URL of the metalink to be downloaded</param>
        /// <param name="downloadFolder">Absolue Path to folder where download should be placed</param>
        /// <param name="logger">Logger class where console output goes</param>
        /// <param name="numConnections">Maximum number of concurrent connections per file (Default: 8)</param>
        /// <param name="maxconcurrent">Maximum number of files to be downloaded simultaneously (Default: 1)</param>
        /// <returns>true if download successful, false otherwise</returns>
        static public bool DownloadMetaLink(string aria2cPath, string metaLinkURL, string downloadFolder, int numConnections = 8, int maxconcurrent = 1)
        {
            _log.Info($"Begin Downloading Metalink '{metaLinkURL}' to '{downloadFolder}'");
            return Run(exePath: aria2cPath,
                       arguments: $"--file-allocation=none --continue=true --check-integrity=true --max-concurrent-downloads={maxconcurrent} " +
                                          $"--retry-wait 5 -x {numConnections} --follow-metalink=mem --dir={downloadFolder} {metaLinkURL}",
                       printOnSuccess: $"Download metalink {metaLinkURL} OK",
                       printOnFail: $"Error during download of {metaLinkURL}");
        }


    }


}
