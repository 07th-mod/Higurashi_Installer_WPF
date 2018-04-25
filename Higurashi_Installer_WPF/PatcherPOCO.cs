using System;

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
    }
}
