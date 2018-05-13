using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Higurashi_Installer_WPF.InstallerScripts
{
    /// Returns true if install succeeded, false otherwise
    public class UminekoQuestionInstaller : InstallerBase, InstallerScriptInterface
    {
        //TODO: is this correct?
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public UminekoQuestionInstaller() : base() { }

        /// <summary>
        /// Checks for arc.nsa and if so deems the folder a valid install directory.
        /// </summary>
        /// <returns>true if the install directory is valid, false otherwise</returns>
        public bool CheckInstallDirectoryIsValid() => !RelPathExists("arc.nsa");

        /// <summary>
        /// Perform the install
        /// </summary>
        /// <returns>true if install successful, false otherwise</returns>
        public bool DoInstall(PatcherPOCO settings)
        {
            //I would prefer to have this done in the constructor, but it ends up easier this way
            this.settings = settings;

            //try and create the download folder
            TryCreateDir(settings.DownloadFolderPath);

            //check correct installation folder
            //TODO: list some files from the game folder? or open EXPLORER window?
            /*if (!RelPathExists("arc.nsa"))
            {
                _log.Warn($"It looks like you selected the wrong install folder (you chose '{settings.GamePath}')");
                if (!UserAnsweredYes("Do you want to install anyway? [y / n]"))
                {
                    _log.Error("Install Cancelled - Wrong Directory");
                    return false;
                }
            }*/
            
            //rename Umineko1to4 and 0.utf to keep a backup before doing any operations
            try
            {
                RelFileMove("Umineko1to4.exe", "Umineko1to4_old.exe");
                RelFileMove("0.utf", "0_old.utf");
            }
            catch (Exception e)
            {
                _log.Warn("Error backing up umineko1to4.exe or 0.utf - continuing install anyway");
                _log.Warn(e.ToString());
            }

            //TODO: make this repeat if download fails
            _log.Info("Downloading and verifying all files. You can close and reopen this at any time, your progress will be saved.");
            try
            {
                bool downloadWasSuccessful = false;
                for (int i = 0; i < 20 || downloadWasSuccessful; i++)
                {
                    downloadWasSuccessful = DownloadToDownloadFolder(@"https://github.com/07th-mod/resources/raw/master/umineko-question/umi_full.meta4");
                }

                if(!downloadWasSuccessful)
                {
                    _log.Warn("Couldn't download files... - installer stopped");
                    return false;
                }
            }
            catch (Exception e)
            {
                _log.Warn("Couldn't download files... - installer stopped");
                _log.Warn(e.ToString());
                return false;
            }

            //Copy the executables and script from the download directory to the game directory
            try
            {
                File.Copy(DownloadFolderRelPath("Umineko1to4.exe"), RelPath("Umineko1to4.exe"));
                File.Copy(DownloadFolderRelPath("0.utf"), RelPath("0.u"));
            }
            catch
            {
                _log.Error("Failed to move exe Umineko1to4 or script file 0.utf from downloads to game directory - patch has NOT been installed!");
                return false;
            }

            //Rename 'saves' to 'mysav' for new executable
            try
            {
                RelFileMove("saves", "mysav");
            }
            catch
            {
                _log.Warn("Couldn't move 'saves' to 'mysav' - saves might not be preserved.");
            }

            //extract files (technically this could be done automatically by scanning the metafile for archive files)
            _log.Info("Extracting Files");
            List<string> filesToExtract = new List<string> { @"Umineko-Graphics.zip.001", @"Umineko-Update-04_2018.zip", @"Umineko-Voices.7z" };
            foreach (string fileToExtract in filesToExtract)
            {
                if (!ExtractDownloadedFileToGameRoot(fileToExtract))
                {
                    _log.Error($"Failed to extract {fileToExtract} - please try running the installer again.");
                    return false;
                }
            }

            //TODO: Maybe just move the files to be deleted to a separate folder, for safety.
            RelFolderCopy(@"en\bmp\background\r_click", @"en\bmp\r_click");
            _log.Info("Character Bio hotfix finished");

            //TODO: List files that can be deleted, and let the user delete them manually

            _log.Info("Install Finished Successfully");

            return true;
        }
    }
}
