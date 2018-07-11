using System;
using System.Collections.Generic;

namespace Higurashi_Installer_WPF
{
    public class PatcherPOCO
    {
        public enum BatchFileExecutionModeEnum
        {
            NormalWithLogging,              //for windows 10. batch file is run with non-shell execute, output is redirected to this installer program
            ShellExecuteWithLogging,        //for windows 7. batch file is run with shell execute, output is redirected to file. File is monitored for changes. 
            Manual,                         //only downloads and extracts the resources - the user has to run the install.bat themselves
        }

        public PatcherPOCO(ExecutionModeComboViewModel executionModeComboToBind)
        {
            //Keeping a reference allows the combo box value to sync with the BatchFileExecutionMode property on this PatcherPOCO
            ExecutionModeComboViewModel = executionModeComboToBind;
        }

        private ExecutionModeComboViewModel ExecutionModeComboViewModel;

        //wrapper/binding function for ExecutionModeComboViewModel's BatchFileExecutionMode() function. Defines how the the install.bat should be run
        public BatchFileExecutionModeEnum BatchFileExecutionMode
        {
            get { return ExecutionModeComboViewModel.BatchFileExecutionMode; }
            set { ExecutionModeComboViewModel.BatchFileExecutionMode = value; }
        }

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
        private List<String> ExeNames;
        public List<String> GetExeNames() => ExeNames;
        public void SetExeNames(params String[] exeNames) => ExeNames = new List<string>(exeNames);
        public String Version { get; set; }
        public String InstallUpdate { get; set; }
        public String InstallType { get; set; }
        public Boolean isDRM { get; set; }
        public String DataFolder { get; set; }
        public String ImagePath { get; set; }
        public String BatName { get; set; }

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
