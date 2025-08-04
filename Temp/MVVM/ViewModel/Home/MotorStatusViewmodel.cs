using FlexMold.Core;
using FlexMold.MVVM.Model;
using FlexMold.Utility;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Windows;
using System.Windows.Media;

namespace FlexMold.MVVM.ViewModel.Home
{
    public class MotorStatusViewmodel : StatusBaseViewModel, IMotorStatusViewmodel
    {
        public ObservableCollection<MotorStatusControl> MyMotors { get; set; }
        public RelayCommand CalibrateCommand { get; set; }
        public RelayCommand DebugDetailClickCommand { get; set; }
        public RelayCommand OnLoadedCommand { get; set; }
        public RelayCommand OnUnLoadedCommand { get; set; }

        private void DisplayError(MotorStatusControl o)
        {
            MessageBox.Show(o.ToolTip);
            Console.WriteLine();
        }
        private bool _IsMotorsIdle;

        public bool IsMotorsIdle
        {
            get { return _IsMotorsIdle; }
            set { _IsMotorsIdle = value; OnPropertyChanged(); }
        }

        public void Calibrate()
        {
            var log = LogManager.GetLogger("Calibration");
            CalibrateButtonEnable = false;
            try
            {
                log.Info("Please Select valid Calibration File ....");
                using System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
                openFileDialog.InitialDirectory = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
                openFileDialog.Filter = "csv files (*.txt)|*.csv|All files (*.*)|*.*";
                openFileDialog.RestoreDirectory = true;

            }
            catch (Exception ex)
            {
                CalibrateButtonEnable = true;
                log.Error(ex.Message);
                TNF.ShowError(ex.Message);
                return;
            }
        }
        private float[] _mPosition;
        public float[] mPosition
        {
            get { return _mPosition; }
            set
            {
                SetMotorPos(value);
                _mPosition = value;
            }
        }
        private uint[] _DeviceStatus;
        public uint[] DeviceStatus
        {
            get { return _DeviceStatus; }
            set
            {
                SetDeviceStatus(value);
                _DeviceStatus = value;
            }
        }
        void DispDeviceStatus(uint[] _value, int i)
        {
            var swe = (EDeviceStatus)Enum.ToObject(typeof(EDeviceStatus), _value[i]);
            var log = LogManager.GetLogger("Calibration");
            switch (_value[i])
            {
                case (int)EDeviceStatus.None:
                    var _tmgs = SetUI(i, swe, Brushes.Transparent);
                    TNF.ShowInformation(_tmgs);
                    log.Log(LogLevel.Info, _tmgs);
                    CalibrateButtonEnable = true;
                    break;
                case (int)EDeviceStatus.CalibrationSquence01Started:
                    _tmgs = SetUI(i, swe, Brushes.Yellow);
                    TNF.ShowInformation(_tmgs);
                    log.Log(LogLevel.Info, _tmgs);
                    CalibrateButtonEnable = false;
                    break;
                case (int)EDeviceStatus.CalibrationSquence01Failed:
                    _tmgs = SetUI(i, swe, Brushes.Red);
                    TNF.ShowError(_tmgs);
                    log.Log(LogLevel.Error, _tmgs);
                    CalibrateButtonEnable = true;
                    break;
                case (int)EDeviceStatus.CalibrationSquence01Success:
                    _tmgs = SetUI(i, swe, Brushes.LightBlue);
                    TNF.ShowSuccess(_tmgs);
                    log.Log(LogLevel.Info, _tmgs);
                    CalibrateButtonEnable = false;
                    break;
                case (int)EDeviceStatus.CalibrationSquence02Started:
                    _tmgs = SetUI(i, swe, Brushes.Yellow);
                    TNF.ShowInformation(_tmgs);
                    log.Log(LogLevel.Info, _tmgs);
                    CalibrateButtonEnable = false;
                    break;
                case (int)EDeviceStatus.CalibrationSquence02Failed:
                    _tmgs = SetUI(i, swe, Brushes.Red);
                    TNF.ShowError(_tmgs);
                    log.Log(LogLevel.Error, _tmgs);
                    CalibrateButtonEnable = false;
                    break;
                case (int)EDeviceStatus.CalibrationSquence02Success:
                    _tmgs = SetUI(i, swe, Brushes.Blue);
                    TNF.ShowSuccess(_tmgs);
                    log.Log(LogLevel.Info, _tmgs);
                    CalibrateButtonEnable = false;
                    break;
                case (int)EDeviceStatus.RampSequenceCompleted:
                    FlexMold.MVVM.ViewModel.Project.ProjectViewModel.IsRampCompleted = true;
                    _tmgs = SetUI(i, swe, Brushes.Green);
                    TNF.ShowSuccess(_tmgs);
                    log.Log(LogLevel.Info, _tmgs);
                    CalibrateButtonEnable = false;
                    break;
                case (int)EDeviceStatus.HomePosition:
                    _tmgs = SetUI(i, swe, Brushes.Gray);
                    TNF.ShowSuccess(_tmgs);
                    log.Log(LogLevel.Warn, _tmgs);
                    CalibrateButtonEnable = false;
                    break;
                default:
                    _tmgs = SetUI(i, swe, Brushes.Red);
                    TNF.ShowError(_tmgs);
                    log.Log(LogLevel.Error, "! Undocumented Status !" + _tmgs);
                    CalibrateButtonEnable = false;
                    break;
            }
        }
        private string SetUI(int i, EmotorDiagnostic sw, SolidColorBrush br)
        {
            var ds = DeviceStatus == null ? "" : $"\n\t DeviceStatus : {(EDeviceStatus)DeviceStatus[i]}";
            string _tmgs = $"Motor[{i}]" +
                $"\n\t Motor Diagnostic : {sw}" +
                $"\n\t Motor Position : {MyMotors[i].MotorPosition:0.000}mm" + ds;
            MyMotors[i].FillColor = br;
            MyMotors[i].ToolTip = _tmgs;
            return $"Motor{i} --> Diagnostic : {sw}";
        }
        private string SetUI(int i, EDeviceStatus aa, SolidColorBrush br)
        {
            var md = MotorDiagnistics == null ? "" : $"\n\t Diagnostic : {(EmotorDiagnostic)MotorDiagnistics[i]}";
            var _tmgs = $"Motor[{i}]" + md +
                $"\n\t Position : {MyMotors[i].MotorPosition:0.000}mm" +
                $"\n\t DeviceStatus : {aa}";
            MyMotors[i].StrokeColor = br;
            MyMotors[i].ToolTip = _tmgs;
            return $"Motor{i} --> DeviceStatus : {aa}";
        }
        private void Position_SetUI(int i)
        {
            var ds = DeviceStatus == null ? "" : $"\n\t DeviceStatus : {(EDeviceStatus)DeviceStatus[i]}";
            var md = MotorDiagnistics == null ? "" : $"\n\t Diagnostic : {(EmotorDiagnostic)MotorDiagnistics[i]}";
            var _tmgs = $"Motor[{i}]" + md +
                $"\n\t Position : {MyMotors[i].MotorPosition:0.000}mm" + ds;
            MyMotors[i].ToolTip = _tmgs;
        }
        private void SetDeviceStatus(uint[] _value)
        {
            try
            {
                var v = GetMismatchedIndexes(DeviceStatus, _value);
                if (DeviceStatus != null)
                {
                    for (int i = 0; i < v.Length; i++)
                    {
                        DispDeviceStatus(_value, v[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < v.Length; i++)
                    {
                        DispDeviceStatus(_value, i);
                    }
                }
            }
            catch(Exception ex)
            {
                var log = LogManager.GetLogger("Device Status");
                TNF.ShowError(ex.Message);
                log.Log(LogLevel.Error, "! SetDevice Status Error =" + ex.Message);
            }
        }
        private void SetMotorPos(float[] _value)
        {
            try
            {
                var v = GetMismatchedIndexes(mPosition, _value);
                if (mPosition != null)
                {
                    for (int i = 0; i < v.Length; i++)
                    {
                        MyMotors[v[i]].MotorPosition = _value[v[i]];
                        Position_SetUI(v[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < v.Length; i++)
                    {
                        MyMotors[i].MotorPosition = _value[i];
                        Position_SetUI(i);
                    }
                }
            }
            catch(Exception ex)
            {
                var log = LogManager.GetLogger("Device Status");
                TNF.ShowError(ex.Message);
                log.Log(LogLevel.Error, "! SetMotorPos Error =" + ex.Message);
            }
        }
        private uint[] _MotorDiagnistics;
        public UInt32[] MotorDiagnistics
        {
            get { return _MotorDiagnistics; }
            set
            {
                SetMotorDiagnisticStatus(value);
                _MotorDiagnistics = value;
            }
        }
        void DisplayMotorDiagStatus(uint[] _value, int i, Logger log)
        {
            MyMotors[i].MotorPosition = mPosition[i];
            var sw = (EmotorDiagnostic)Enum.ToObject(typeof(EmotorDiagnostic), _value[i]);
            switch (sw)
            {
                case EmotorDiagnostic.Healthy:
                    var _tmgs = SetUI(i, sw, Brushes.Green);
                    TNF.ShowSuccess(_tmgs);
                    log.Log(LogLevel.Info, _tmgs);
                    break;
                case EmotorDiagnostic.Stepper_Driver_Failed:
                    _tmgs = SetUI(i, sw, Brushes.Red);
                    TNF.ShowError(_tmgs);
                    log.Log(LogLevel.Error, _tmgs);
                    break;
                case EmotorDiagnostic.Stall:
                    FlexMold.MVVM.ViewModel.Project.ProjectViewModel.StallErrMotorsCheckTimer.Start();
                    _tmgs = SetUI(i, sw, Brushes.Orange);
                    TNF.ShowWarning(_tmgs);
                    log.Log(LogLevel.Warn, _tmgs);
                    break;
                default:
                    _tmgs = SetUI(i, sw, Brushes.Red);
                    TNF.ShowError(_tmgs);
                    log.Log(LogLevel.Error, "! Undocumented Status !" + _tmgs);
                    break;
            }
        }

        private void SetMotorDiagnisticStatus(uint[] _value)
        {
            try
            {
                var log = LogManager.GetLogger("Motor Diagnistic Status");
                var v = GetMismatchedIndexes(MotorDiagnistics, _value);
                if (MotorDiagnistics != null)
                {
                    for (int i = 0; i < v.Length; i++)
                    {
                        DisplayMotorDiagStatus(_value, v[i], log);
                    }
                    SetDeviceMode(_value, log);
                }
                else
                {
                    for (int i = 0; i < v.Length; i++)
                    {
                        DisplayMotorDiagStatus(_value, i, log);
                    }
                }
                if (v.Length != 0)
                    SetDeviceMode(_value, log);
            }catch(Exception ex)
            {
                var log = LogManager.GetLogger("Device Status");
                TNF.ShowError(ex.Message);
                log.Log(LogLevel.Error, "! SetMotorDiagnisticStatus Error =" + ex.Message);
            }
        }
        private void SetDeviceMode(uint[] _value, Logger log)
        {
        }

        public IList<float> CalibrationData { get; set; }
        private bool ValidateCSVData()
        {
            var log = LogManager.GetLogger("CSV Validator");
            return false;
        }
        private void WriteDistpacementCSVData(IList<float> data)
        {
            var log = LogManager.GetLogger("Calibration");
            log.Log(LogLevel.Info, $"{JsonSerializer.Serialize(data)}");
        }
        private void Motor_PositionChanged(object sender, EventArgs e)
        {
        }
        private void Motor_DeviceStatusChanged(object sender, EventArgs e)
        {
            IsMotorsIdle = false;
            var log = LogManager.GetLogger("Device Status");
        }
        private void Motor_MotorDiagnosticChanged(object sender, EventArgs e)
        {
        }
        private bool _CalibrateButton;

        public bool CalibrateButtonEnable
        {
            get { return _CalibrateButton; }
            set { _CalibrateButton = value; OnPropertyChanged(); }
        }

        public MotorStatusViewmodel()
        {
            try { 
            CalibrateButtonEnable = true;
            CalibrationData = new List<float>();
            CalibrateCommand = new RelayCommand(async => { Calibrate(); });
            DebugDetailClickCommand = new RelayCommand(o => { DisplayError(o as MotorStatusControl); });
            //Array.Fill<uint>(validSeq01Val, (int)EDeviceStatus.CalibrationSquence01Success);
            //Array.Fill<uint>(validSeq02Val, (int)EDeviceStatus.CalibrationSquence02Success);
            //Array.Fill<uint>(validSeq02Val, (int)EDeviceStatus.RampSequenceCompleted);
            //Array.Fill<uint>(healthyMotorDiag, (int)EmotorDiagnostic.Healthy);
            //Array.Fill<uint>(validForHome, (int)EDeviceStatus.None);
            }
            catch (Exception ex)
            {
                var log = LogManager.GetLogger("Device Status");
                TNF.ShowError(ex.Message);
                log.Log(LogLevel.Error, "! MotorStatusViewmodel Error =" + ex.Message);
            }

        }
    }
}
