using System.Collections.Generic;
using System.ComponentModel;

namespace Higurashi_Installer_WPF
{
    //View Model for the ComboBox which shows the methods to run the install.bat
    //See https://www.codeproject.com/Articles/301678/Step-by-Step-WPF-Data-Binding-with-Comboboxes
    public class ExecutionModeComboViewModel : INotifyPropertyChanged
    {
        // this class represents a single combo box item
        public class ComboBoxBatchFileExecutionMode
        {
            public PatcherPOCO.BatchFileExecutionModeEnum ExecutionMode { get; set; }
            public string Description { get; set; }
        }

        //Setup possible batch file execution modes available in the combo box. Can be moved to the 
        //Constructor of this class if you need to initialize or restrict these at runtime.
        //Used by the XAML to populate the combobox
        public List<ComboBoxBatchFileExecutionMode> CombobBoxExecutionModes { get; set; } = new List<ComboBoxBatchFileExecutionMode>() {
            new ComboBoxBatchFileExecutionMode()
            {
                ExecutionMode = PatcherPOCO.BatchFileExecutionModeEnum.NormalWithLogging,
                Description = "Default" //For windows 10
            },

            new ComboBoxBatchFileExecutionMode()
            {
                ExecutionMode = PatcherPOCO.BatchFileExecutionModeEnum.ShellExecuteWithLogging,
                Description = "Safe Mode (for Win 7)"
            },

            new ComboBoxBatchFileExecutionMode()
            {
                ExecutionMode = PatcherPOCO.BatchFileExecutionModeEnum.Manual,
                Description = "Manual Mode"
            },
        };

        // Need a void constructor in order to use as an object element in the XAML.
        public ExecutionModeComboViewModel() {}

        // Backing variable for BatchFileExecutionMode function
        private PatcherPOCO.BatchFileExecutionModeEnum _batchFileExecutionMode = PatcherPOCO.BatchFileExecutionModeEnum.NormalWithLogging;

        // Gets/sets the current execution mode shown by the combo box
        public PatcherPOCO.BatchFileExecutionModeEnum BatchFileExecutionMode
        {
            get { return _batchFileExecutionMode; }
            set
            {
                if (_batchFileExecutionMode != value)
                {
                    _batchFileExecutionMode = value;
                    NotifyPropertyChanged("BatchFileExecutionMode");
                }
            }
        }

        // These are required so that the combobox is updated automatically when the
        // "BatchFileExecutionMode" changes, and vice versa
        #region INotifyPropertyChanged Members

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
