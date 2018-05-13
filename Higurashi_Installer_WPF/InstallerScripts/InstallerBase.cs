using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Higurashi_Installer_WPF.InstallerScripts
{
    /// <summary>
    /// Install settings common to all installers.
    /// </summary>

    public interface InstallerScriptInterface
    {
        /// <summary>
        /// All derivative installer classes must contain the 'DoInstall' function which performs the installation.
        /// </summary>
        /// <returns>true if install successful, false otherwise</returns>
        bool DoInstall(PatcherPOCO settings);

        /// <summary>
        /// This function should return true if the install directory seems correct. Just always return true if there is no way to check for install directory correctness.
        /// </summary>
        /// <returns>true if the install directory is valid, false otherwise</returns>
        bool CheckInstallDirectoryIsValid();
    }

    /// <summary>
    /// Implementation shared between all installers (mainly convenience functions)
    /// </summary>
    public abstract class InstallerBase
    {
        //TODO: is this correct?
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected PatcherPOCO settings;

        /// <summary>
        /// Constructor for base installer class.
        /// </summary>
        /// <param name="settings"></param>
        protected InstallerBase()
        {

        }

        /// <summary>
        /// Tries to create the given path (folder), if it doesn't already exist. 
        /// </summary>
        /// <param name="path">The path to be created. All folders in the path will be created if they don't exist.</param>
        /// <returns>true if creation successful or the folder already exists, false otherwise.</returns>
        protected bool TryCreateDir(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    _log.Info($"Created Directory {path}");
                }
                else
                {
                    _log.Info($"Directory {path} already exists");
                }
                return true;
            }
            catch (Exception e)
            {
                _log.Warn($"Couldn't create directory {path}");
                _log.Warn(e.ToString());
                return false;
            }
        }

        /// <summary>
        /// creates an absolute path string, given a relative path string from the download folder
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        protected string DownloadFolderRelPath(string path) => Path.Combine(settings.DownloadFolderPath, path);

        /// <summary>
        /// creates an absolute path string, given a relative path string from the game folder 
        /// For example, RelPath(@'temp\test.exe') => @'{gameFolderPath}\temp\test.exe'
        /// </summary>
        /// <param name="path">Path to append to the game path to get the final, absolute path</param>
        /// <returns> The absolute path to the input path ({gameFolder}\{input path})</returns>
        protected string RelPath(string path) => Path.Combine(settings.GamePath, path);

        /// <summary>
        /// Checks if a path exists, where 'path' is a path relative to the game folder
        /// </summary>
        /// <param name="path">Path relative to the game folder to be checked for existance</param>
        /// <returns>true if path exists, false otherwise</returns>
        protected bool RelPathExists(string path) => File.Exists(RelPath(path));

        /// <summary>
        /// Moves a file, where both the source and destination paths are relative to the game folder
        /// TODO: make this work on folders as well?
        /// </summary>
        /// <param name="sourcePath">The path, relative to the game directory, of the file to be moved.</param>
        /// <param name="destPath">The path, relative to the game directory, where the file should be placed.</param>
        protected void RelFileMove(string sourcePath, string destPath) => File.Move(RelPath(sourcePath), RelPath(destPath));

        /// <summary>
        /// Copy from one folder to another, overwriting any existing files (by default)
        /// </summary>
        /// <remarks>Note: this function requires the Microsoft.VisualBasic Assembly reference to be included</remarks>
        /// <param name="sourcePath"></param>
        /// <param name="destPath"></param>
        /// <param name="overwrite"></param>
        protected void RelFolderCopy(string sourcePath, string destPath, bool overwrite = true) => Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(RelPath(sourcePath), RelPath(destPath), overwrite);

        //TODO: Maybe just move the files to be deleted to a separate folder, for safety.

        /// <summary>
        /// Prompts the user with 'promptString' to answer Y/N. 
        /// </summary>
        /// <param name="promptString"></param>
        /// <returns>Returns true if Y was pressed, false if N was pressed</returns>
        protected bool UserAnsweredYes(string promptString)
        {
            _log.Warn(promptString);
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Y)
                {
                    return true;
                }
                else if (keyInfo.Key == ConsoleKey.N)
                {
                    return false;
                }

                _log.Warn("\nPlease type 'Y' or 'N'");
            }
        }

        /// <summary>
        /// Extracts a downloaded file to the root of the game folder path (assumes it is in the download folder)
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        protected bool ExtractDownloadedFileToGameRoot(string filename) =>
            ExternalProgramRunner.ExtractFile(sevenZipPath: settings.SevenZipPath,
                                              archivePath: Path.Combine(settings.DownloadFolderPath, filename),
                                              extractionPath: settings.GamePath);

        /// <summary>
        /// Begins Download of a metalink file. All downloaded files are placed in the download folder (set in the 'settings' variable).
        /// </summary>
        /// <param name="metaLinkURL"></param>
        /// <returns></returns>
        protected bool DownloadToDownloadFolder(string metaLinkURL) =>
            ExternalProgramRunner.DownloadMetaLink(aria2cPath: settings.Aria2cPath,
                                                   metaLinkURL: metaLinkURL,
                                                   downloadFolder: settings.DownloadFolderPath);
    }
}
