using FlexMold.Core;
using FlexMold.MVVM.Model;
using FlexMold.Utility;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Media;
using TwinCAT;
using TwinCAT.Ads;
using TwinCAT.Ads.TypeSystem;

namespace FlexMold.MVVM.ViewModel.SplashScreen
{
    public class FoeViewModel : ObservableObject, ITabViewModel, IFoeViewModel
    {
        private int retry = 0;
        private string foe_sPathName;
        private bool Init_Status = false;
        private bool Foe_Status = false;
        private bool Op_Status = false;
        private string foe_dwPass;



        private bool ValidateMyString(string filpath)
        {
            // Define your regex pattern
            string pattern = @"\.(bin|hex)$";

            if (string.IsNullOrEmpty(filpath))
            {
                // Handle the case when filePath is null or empty
                return false;
            }

            // Perform the regex match
            bool isMatch = Regex.IsMatch(filpath, pattern);

            // Handle the validation result
            if (isMatch)
            {
                return true;
            }
            else
            {
                log.Log(LogLevel.Info, "Please Select File Path");
                return false;
            }
        }

        private bool? _isCheckedAll;

        public bool? IsCheckedAll
        {
            get => _isCheckedAll;
            set
            {
                if (value == _isCheckedAll) return;
                _isCheckedAll = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<FoeModel> MyDevicesAndBoxes { get; set; }
        //public   
        //{
        //    get { return _MyDevicesAndBoxes; }
        //    set
        //    {
        //        _MyDevicesAndBoxes = value;
        //        OnPropertyChanged();
        //    }
        //}


        private FoeModel _SelectedDeviceAndBox;
        public FoeModel SelectedDeviceAndBox
        {
            get { return _SelectedDeviceAndBox; }
            set
            {
                _SelectedDeviceAndBox = value;
                OnPropertyChanged();
            }
        }
        private uint _OverAllProgress;

        public uint OverAllProgress
        {
            get { return _OverAllProgress; }
            set
            {
                _OverAllProgress = value;
                OnPropertyChanged();
            }
        }
        int runningIndex = 0;
        private Logger log;
        public RelayCommand UpdateFirmwareCommand { get; set; }
        public RelayCommand CheckAllCommand { get; set; }
        public RelayCommand BrowseFirmwareCommand { get; set; }
        public RelayCommand RefreshDataGridCommand { get; set; }
        private bool _BT_FirmwareEnable;

        public bool BT_FirmwareEnable
        {
            get { return _BT_FirmwareEnable; }
            set { _BT_FirmwareEnable = value; OnPropertyChanged(); }
        }
        private void BrowseFirmwareFile()
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "bin files (*.bin)|*.bin|hex files (*.hex)|*.hex";
                log.Log(LogLevel.Info, $"Select Project Folder");
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Foe_sPathName = new FileInfo(dialog.FileName).FullName;
                }
            }
        }
        private string _DeviceStatusIndex;
        public string DeviceStatusIndex
        {
            get { return _DeviceStatusIndex; }
            set { _DeviceStatusIndex = value; OnPropertyChanged(); }
        }

        public string Foe_sPathName { get => foe_sPathName; set { foe_sPathName = value; OnPropertyChanged(); } }
        public string Foe_dwPass { get => foe_dwPass; 
            set 
            {
                foe_dwPass = value; 
                OnPropertyChanged();
            } 
        }

        public string Foe_sNetId { get; private set; }
        public string Foe_Remarks { get;  set; }

        private List<int> NotificatioHandles;
        public event EventHandler<bool> TUpdate_Status;

        private void RegisterNotification()
        {

           
        }

        List<FoeModel> checkedSlaves;
        int cur;
        string curStr;
       
        private void ResetSetContols()
        {
            MyDevicesAndBoxes[runningIndex].Foe_nPercent = 0;
            MyDevicesAndBoxes[runningIndex].Foe_bError = false;
            MyDevicesAndBoxes[runningIndex].Foe_bBusy = false;
            MyDevicesAndBoxes[runningIndex].Foe_nErrID = 0;
            MyDevicesAndBoxes[runningIndex].Ecs_bError = false;
            MyDevicesAndBoxes[runningIndex].Ecs_bBusy = false;
            MyDevicesAndBoxes[runningIndex].Ecs_nErrId = 0;
            MyDevicesAndBoxes[runningIndex].SlaveState = "";
            MyDevicesAndBoxes[runningIndex].DateTimeLU = DateTime.Now.ToString("t");
        }       
      
        private void Client_AdsNotificationFoe(object sender, AdsNotificationEventArgs e)
        {

        }
        private void ResetTwinCatVariables()
        {
        }
        private void ResetFoeCollection()
        {
        }
        private void ResetDeviceCollection()
        {
            for (int i = 0; i < MyDevicesAndBoxes.Count; i++)
            {
                MyDevicesAndBoxes[i].DateTimeLU = "";
                MyDevicesAndBoxes[i].Foe_nPercent = 0;
                MyDevicesAndBoxes[i].Foe_nErrID = 0;
                MyDevicesAndBoxes[i].Foe_bError = false;
                MyDevicesAndBoxes[i].Foe_nBytesWritten = 0;
                MyDevicesAndBoxes[i].Foe_bBusy = false;
                MyDevicesAndBoxes[i].Foe_Status = false;
                MyDevicesAndBoxes[i].SlaveState = "";
                MyDevicesAndBoxes[i].SlaveStateBackground = Brushes.Transparent;
            }
        }
        public FoeViewModel()
        {
            MyDevicesAndBoxes = new();
            IsCheckedAll = true;
            ResetFoeCollection();

            BT_FirmwareEnable = true;
            //Foe_dwPass = "287454020";
            Foe_dwPass = "11223344";
            log = LogManager.GetLogger("Motor Firmaware Update");
            UpdateFirmwareCommand = new RelayCommand(o =>
            {
                
            });
            BrowseFirmwareCommand = new RelayCommand(o => { BrowseFirmwareFile(); });
            RefreshDataGridCommand = new RelayCommand(o =>
            {

            });
            CheckAllCommand = new RelayCommand(o =>
            {
                for (int i = 0; i < MyDevicesAndBoxes.Count; i++)
                    MyDevicesAndBoxes[i].IsChecked = (bool)IsCheckedAll;
            });

            
        }


    }
}