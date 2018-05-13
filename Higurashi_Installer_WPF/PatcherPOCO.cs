using System;
using Higurashi_Installer_WPF.InstallerScripts;
using System.IO;

namespace Higurashi_Installer_WPF
{
    public class PatcherPOCO
    {
        //Checkboxes for custom
        public Boolean IsPS3 { get; set; }
        public Boolean IsPatch { get; set; }
        public Boolean IsVoices { get; set; }
        public Boolean IsSteamSprites { get; set; }
        public Boolean IsUI { get; set; }
        public Boolean IsBackup { get; set; }

        //Option for DropDownMenu
        public Boolean IsCustom { get; set; }
        public Boolean IsFull { get; set; }
        public Boolean IsVoiceOnly { get; set; }

        //Chapter information for install
        public String InstallPath { get; set; }
        public String ChapterName { get; set; }
        public String ExeName { get; set; }
        public String Version { get; set; }
        public String InstallUpdate { get; set; }
        public String InstallType { get; set; }
        public Boolean isDRM { get; set; }
        public String DataFolder { get; set; }
        public String ImagePath { get; set; }

        //Install path of each chapter
        public String PathWatanagashi { get; set; }
        public String PathOnikakushi { get; set; }
        public String PathTatarigoroshi { get; set; }
        public String PathHimatsubushi { get; set; }
        public String PathMeakashi { get; set; }

        //Is chapters ready for install or update
        public Boolean IsInstallWatanagashi { get; set; }
        public Boolean IsInstallOnikakushi { get; set; }
        public Boolean IsInstallTatarigoroshi { get; set; }
        public Boolean IsInstallHimatsubushi { get; set; }
        public Boolean IsInstallMeakashi { get; set; }

        // Variables used for c# installer
        public String GamePath { get; set; } // This is the GameRoot (the root of the game directory)

        //helper functions. Not sure if should make them auto getters, as it may be more confusing
        public string DownloadFolderPath { get => Path.Combine(GamePath, DataFolder, "Downloads"); }

        //These two functions will vary depending on linux/windows/mac - how to deal with this?
        public string SevenZipPath { get => Path.Combine(InstallPath, "7za.exe"); }
        public string Aria2cPath { get => Path.Combine(InstallPath, "aria2c.exe"); }

        //This is the main installer object which performs the installation
        public InstallerScriptInterface InstallerScript;
    }
}
