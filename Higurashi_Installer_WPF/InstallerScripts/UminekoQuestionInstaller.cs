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
            try
            {
                //I would prefer to have this done in the constructor, but it ends up easier this way
                this.settings = settings;

                //try and create the download folder
                TryCreateDir(settings.DownloadFolderPath);
            
                // Keep a backup of the files to be replaced, before doing any operations
                try
                {
                    RelFileCopy("Umineko1to4.exe", $"Umineko1to4_{GetDateString()}.exe");
                    RelFileCopy("0.utf", $"0_{GetDateString()}.utf");
                }
                catch (Exception e)
                {
                    _log.Warn("Error backing up umineko1to4.exe or 0.utf - continuing install anyway");
                }

                _log.Info("Downloading and verifying all files. You can close and reopen this at any time, your progress will be saved.");
                DownloadToDownloadFolderWithRetry(@"https://github.com/07th-mod/resources/raw/master/umineko-question/umi_full.meta4");

                //Copy the executables and script from the download directory to the game directory
                File.Copy(DownloadFolderRelPath("Umineko1to4.exe"), RelPath("Umineko1to4.exe"), overwrite:true);
                File.Copy(DownloadFolderRelPath("0.utf"), RelPath("0.u"), overwrite:true);

                //extract files (technically this could be done automatically by scanning the metafile for archive files)
                _log.Info("Extracting Files");
                List<string> filesToExtract = new List<string> { @"Umineko-Graphics.zip.001", @"Umineko-Update-04_2018.zip", @"Umineko-Voices.7z" };
                foreach (string fileToExtract in filesToExtract)
                {
                    ExtractDownloadedFileToGameRoot(fileToExtract);
                }

                //TODO: Maybe just move the files to be deleted to a separate folder, for safety.
                RelFolderCopy(@"en\bmp\background\r_click", @"en\bmp\r_click");
                _log.Info("Character Bio hotfix finished");

                //TODO: List files that can be deleted, and let the user delete them manually

                _log.Info("Install Finished Successfully");

                return true;
            }
            catch(FileMoveCopyCreateException e)
            {
                _log.Error("Couldn't Move, Copy, or Create files (see details below) - installer stopped");
                _log.Error(e.ToString());
            }
            catch (FileExtractionException e)
            {
                _log.Error("Couldn't extract files... - installer stopped");
                _log.Error(e.ToString());
            }
            catch(DownloadException e)
            {
                _log.Error("Couldn't download files... - installer stopped");
                _log.Error(e.ToString());
            }
            catch(Exception e)
            {
                _log.Error("An unknown exception has occured - installer stopped");
                _log.Error(e.ToString());
            }

            return false;
        }
    }
}
