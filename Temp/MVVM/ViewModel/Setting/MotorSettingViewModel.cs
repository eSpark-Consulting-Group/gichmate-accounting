using FlexMold.Core;
using FlexMold.Utility;
using MaterialDesignThemes.Wpf;
using NLog;
using System;

namespace FlexMold.MVVM.ViewModel.Setting
{
    public class MotorSettingViewModel : ObservableObject, IMotorSettingViewModel
    {
        private Logger log;
        public RelayCommand FirmwareCommand { get; set; }
        private uint _SelectedMotorIndex;
        private bool _IsDialogOpen;

        public bool IsDialogOpen
        {
            get { return _IsDialogOpen; }
            set
            {
                _IsDialogOpen = value;
                OnPropertyChanged();
            }
        }

        public uint SelectedMotorIndex
        {
            get { return _SelectedMotorIndex; }
            set
            {
                _SelectedMotorIndex = value;
                OnPropertyChanged();
            }
        }

        private uint[] _DeviceIds;

        public uint[] DeviceIds
        {
            get { return _DeviceIds; }
            set
            {
                _DeviceIds = value;
                OnPropertyChanged();
            }
        }
        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine();
        }
        public MotorSettingViewModel()
        {
            log = LogManager.GetLogger("Motor Setting");
        }
    }
}