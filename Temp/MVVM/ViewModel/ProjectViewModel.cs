using FlexMold.Core;
using FlexMold.MVVM.Model;
using FlexMold.MVVM.View.Project;
using FlexMold.MVVM.ViewModel.Home;
using FlexMold.Utility;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using TwinCAT.Ads;
using TwinCAT.Ads.TypeSystem;
using FlexMold.MVVM.ViewModel.MachineTest;
using FlexMold.MVVM.View.MachineTest;
using System.Globalization;
using System.Text;

namespace FlexMold.MVVM.ViewModel.Project
{
    public class ProjectViewModel : ObservableObject, IProjectViewModel
    {
        private MachineTestViewModel MyMachineTestViewModel { get { return App.AppHost.Services.GetRequiredService<MachineTestViewModel>(); } }

        private string _toggleStopResumeButtonText="Stop";
        
        public static DispatcherTimer MotorsCheckTimer = new DispatcherTimer();
        public static DispatcherTimer StallErrMotorsCheckTimer = new DispatcherTimer();
        
        public static bool IsRampCompleted = true;

        private DispatcherTimer positionProcessCompleted_Timer = new DispatcherTimer();
        private bool IsPsitionDataProcessing = false;
        private float LastPositionData = 0.00F;
        private int positionCounterTimer = 0;
        private int resumeCounterTimer = 0;
        //private int progressbar_InitVal = 0;
        private int progressbar_FinalVal = 100;
        public string toggleStopResumeButtonText
        {
            get { 
                return _toggleStopResumeButtonText; 
            }
            set { 
                    _toggleStopResumeButtonText = value;
                //OnPropertyChanged(_toggleStopResumeButtonText);
                OnPropertyChanged();
            }
        }


        private char LastEventPressed = 'O';// H=Home R=Resume S=Stop W= WriteCSV O=Initial F=ForceResume
        public static bool UserStop = false;
        //public static bool EventStopToggle = false;

        private Logger log = LogManager.GetLogger("Project");
        public RelayCommand SendCSVCommand { get; set; }
        public RelayCommand BrowseProjectCommand { get; set; }
        public RelayCommand StopMotorsCommand { get; set; }
        //public RelayCommand StopResumeButtonText { get; set; }
        public RelayCommand HomeMotorsCommand { get; set; }
        public RelayCommand ForceResumeCommand { get; set; }

        public int MotorDirValue;
        public float MotorRampDelayValue;
        public float MotorTime;
        private float intProgressbar = 0.00F;
        private Model.Project _selectedMyDataModel;
        private List<float> csvdata;        
        //private List<bool> NegativeoffsetResumeDirection;//false=0 , true=1
        private bool Isintimer = false;
        string selectedPath = "";

        public IMotorStatusViewmodel MotorStatus { get { return App.AppHost.Services.GetRequiredService<IMotorStatusViewmodel>(); } }

        public Model.Project selectedMyDataModel
        {
            get { return _selectedMyDataModel; }
            set
            {
                _selectedMyDataModel = value;
                SelectedFile = null;
                OnPropertyChanged();
            }
        }

        private int _pgProcesStatusVal;

        public int pgProcesStatusVal
        {
            get { return _pgProcesStatusVal; }
            set { _pgProcesStatusVal = value; OnPropertyChanged(); }
        }

        private Porjects _myDataModels;

        public Porjects myDataModels
        {
            get { return _myDataModels; }
            set
            {
                _myDataModels = value;
                OnPropertyChanged();
            }
        }
        private bool _CSVWriteButton;

        public bool CSVWriteButton
        {
            get { return _CSVWriteButton; }
            set { _CSVWriteButton = value; OnPropertyChanged(); }
        }
        
        private bool _IsStopEnable;

        public bool IsStopEnable
        {
            get { return _IsStopEnable; }
            set { _IsStopEnable = value; OnPropertyChanged(); }
        }

        private bool _IsForceResumeValid;

        public bool IsForceResumeValid
        {
            get { return _IsForceResumeValid; }
            set { _IsForceResumeValid = value; OnPropertyChanged(); }
        }

        private bool _LaserSendButton;

        public bool LaserSendButton
        {
            get { return _LaserSendButton; }
            set { _LaserSendButton = value; OnPropertyChanged(); }
        }
        private float[] _Position;
        public float[] Position
        {
            get { return _Position; }
            set
            {
                _Position = value;
            }
        }
        private FileInfo _selectedFile;
        private readonly LaserProjectorViewModel _laserProjectorViewModel;
        private ICSVDiaglogViewModel _CSVDiaglogViewModel { get { return App.AppHost.Services.GetRequiredService<ICSVDiaglogViewModel>(); } }
        private CSVDiaglogView _CSVDiaglogView { get { return App.AppHost.Services.GetRequiredService<CSVDiaglogView>(); } }

        public FileInfo SelectedFile
        {
            get { return _selectedFile; }
            set
            {
                _selectedFile = value;
                CSVWriteButton = false;
                IsForceResumeValid = false;
                LaserSendButton = false;
                if (value != null)
                {
                    var se = value.Name.Substring(value.Name.Length - 4);
                    switch (se)
                    {
                        case ".csv":
                            {
                                CSVWriteButton = true;

                                if (File.Exists(CSVHelper.GetCurrentDirectoryPath() + "\\AppData\\" + value.Name + ".Resume"))
                                {
                                    toggleStopResumeButtonText = "Resume";
                                    OnPropertyChanged();
                                    log.Log(LogLevel.Info, "Resume file found>>>"+ value.Name + ".Resume");
                                }

                                //IsForceResumeValid = true;
                                //else
                                IsForceResumeValid = false;
                                MotorStatus.IsMotorsIdle = true;
                                IsStopEnable = true;
                            }
                            break;
                        case ".las":
                            LaserSendButton = true;
                            IsForceResumeValid = false;
                            MotorStatus.IsMotorsIdle = false;
                            IsStopEnable = false;
                            break;
                        default:
                            break;
                    }
                }
                OnPropertyChanged();
            }
        }

        //public string Base64Encode(string plainText)
        //{
        //    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        //    return System.Convert.ToBase64String(plainTextBytes);
        //}
        //public string Base64Decode(string base64EncodedData)
        //{
        //    var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        //    return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        //}
       
        public static void DoEvents()
        {
            var frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background,
                new DispatcherOperationCallback(
                    delegate (object f)
                    {
                        ((DispatcherFrame)f).Continue = false;
                        return null;
                    }), frame);
            Dispatcher.PushFrame(frame);
        }
        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine();
        }
        private void BrowseProject()
        {
            Logger log = LogManager.GetLogger("Browse Project");
            try
            {

                using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
                {
                    log.Log(LogLevel.Info, $"Select Project Folder");
                    System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        myDataModels = new Porjects();
                        selectedPath = dialog.SelectedPath;
                        Regex reg = new Regex(@".las$|.csv$");
                        DirectoryInfo di = new DirectoryInfo(selectedPath);
                        var laserFilesPath = di.GetFiles("*", SearchOption.AllDirectories)
                                 .Where(path => reg.IsMatch(path.FullName))
                                 .ToList();
                        var panels = laserFilesPath.GroupBy(p => p.Directory.Name);
                        var projectsTemp = laserFilesPath.GroupBy(p => p.Directory.Parent.Name);
                        log.Log(LogLevel.Info, $"{projectsTemp.Count()} Projects Found");
                        log.Log(LogLevel.Info, $"{panels.Count()} Panels Found");
                        log.Log(LogLevel.Info, $"{laserFilesPath.Count()} Files Found");
                        foreach (var item in panels)
                        {
                            myDataModels.Add(new Model.Project()
                            {
                                Panel = item.Key,
                                ProjectName = item.FirstOrDefault().Directory.Parent.Name,
                                LaserFile = item.ToList(),
                            });
                        }
                    }
                    else
                        log.Log(LogLevel.Warn, $"User Cancelled Project Browser");
                }
            }
            catch (Exception ex)
            {
                log.Log(LogLevel.Error, ex.Message);
            }
        }

        private void Client_AdsNotification(object sender, AdsNotificationEventArgs e)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                log.Log(LogLevel.Error, ex.Message);
            }
        }
        private PanelData[] CalcSoftStarter(IList<float> _csvdata)
        {
            // Inputs
            double rampDelay = 5;
            double missionTime = 1000;

            // Fixed Params
            double displacementFactor = 2;
            double maxRPM = 200;
            double minRPM = 1;
            uint rampSteps = 10;
            var pd = new PanelData[_csvdata.Count];

            // Calculations
            for (int i = 0; i < _csvdata.Count; i++)
            {
                double requiredRotations = _csvdata[i] / displacementFactor;
                double requiredRPM = (requiredRotations / missionTime) * 60;
                double rampRPM = requiredRPM / rampSteps;
                double timePerStep = rampDelay / rampSteps;
                pd[i].RampRpmTime = timePerStep;
                double displacementPerStep = timePerStep * (rampRPM / 60) * displacementFactor;
                double totalDisplacementPerRamp = 0;
                pd[i].RampRpm = new double[rampSteps];
                for (int j = 1; j <= rampSteps; j++)
                {
                    totalDisplacementPerRamp += displacementPerStep * j;
                    pd[i].RampRpm[j - 1] = rampRPM * j;
                }
                pd[i].RampDisplacement = totalDisplacementPerRamp;
                double requiredDisplacementWithoutRamps = _csvdata[i] - totalDisplacementPerRamp * 2;
                pd[i].Displacement = requiredDisplacementWithoutRamps;
                double timeWithoutRamps = missionTime - (rampDelay * 2);
                pd[i].Time = timeWithoutRamps;
                double requiredRotationsWithoutRamps = requiredDisplacementWithoutRamps / displacementFactor;
                double requiredRPMAfterWithoutRamps = (requiredRotationsWithoutRamps / timeWithoutRamps) * 60;
                pd[i].RPM = requiredRPMAfterWithoutRamps;
            }
            return pd;
        }
        struct PanelData
        {
            public double[] RampRpm;
            public double RampRpmTime;
            public double RampDisplacement;
            public double Displacement;
            public double Time;
            public double RPM;
        }
        private async Task CSVSend()
        {
            csvdata = CSVHelper.ReadCSV_DoubleData(SelectedFile);
            if (csvdata.Count == 0)
            {
                log.Log(LogLevel.Error, $"Please select correct CSV\n" +
                $" File doesn't conatain desired data");
                TNF.ShowError($"CSV Failed");
                return;
            }
           
        }



        private void PositionProcess_Elapsed(object sender, EventArgs e)
        {
            try
            {
                if (IsRampCompleted)
                {
                    pgProcesStatusVal = 100;
                    //positionProcessCompleted_Timer.Stop();
                }                
                else { pgProcesStatusVal = (Math.Abs(progressbar_FinalVal - ((int)(MotorStatus.mPosition[0])))); }

                log.Log(LogLevel.Info, $"This is four444555" + pgProcesStatusVal);
                OnPropertyChanged();
            }
            catch { }

        }


        private void StallStopProcess_Timer_Elapsed(object sender, EventArgs e)
        {
            IsRampCompleted = false;
            progressbar_FinalVal = 50;
        }


        private void Timer_Elapsed(object sender, EventArgs e)
        {
           
        }

        public ProjectViewModel()
        {
            try { 

            MotorsCheckTimer.Tick += new EventHandler(Timer_Elapsed);
            MotorsCheckTimer.Interval = new TimeSpan(0, 0, 3);

                //StallStopProcess_WriteCSVData
                StallErrMotorsCheckTimer.Tick += new EventHandler(StallStopProcess_Timer_Elapsed);
            StallErrMotorsCheckTimer.Interval = new TimeSpan(0, 0, 3);
                
                

            positionProcessCompleted_Timer.Tick += new EventHandler(PositionProcess_Elapsed);
            positionProcessCompleted_Timer.Interval = new TimeSpan(0, 0, 3);
            positionProcessCompleted_Timer.Start();
            SendCSVCommand = new RelayCommand(async o =>
            {
                await CSVSend();
                IsStopEnable = true;
            });
            BrowseProjectCommand = new RelayCommand(o => {
                try
                {
                    toggleStopResumeButtonText = "Stop";
                    OnPropertyChanged();
                    UserStop = false;
                    //EventStopToggle = false;
                }
                catch (Exception ee)
                { }
                 
                BrowseProject();
                //File.Delete(SelectedFile + ".Resume");

            });
            StopMotorsCommand = new RelayCommand(o =>
            {
                if (toggleStopResumeButtonText == "Stop")
                {
                   
                    toggleStopResumeButtonText = "Resume";
                    OnPropertyChanged();

                    log.Log(LogLevel.Warn, $"Stopping Motors");
                    var data = (int)EMode.Abort;
                    log.Log(LogLevel.Warn, $"{EMode.Abort}");

                }
                else
                {
                    log.Log(LogLevel.Warn, $"Resuming Motors");

                    toggleStopResumeButtonText = "Stop";
                    OnPropertyChanged();

                    //IsPsitionDataProcessing = true;
                    //LastPositionData = MotorStatus.mPosition[0];
                    //if (!positionProcessCompleted_Timer.IsEnabled)
                    //{
                    //    positionProcessCompleted_Timer.Start();
                    //    intProgressbar = 0;
                    //    pgProcesStatusVal = int.Parse(Math.Abs(intProgressbar).ToString());
                    //    OnPropertyChanged();
                    //}

                    LastEventPressed = 'R';
                }
                //EventStopToggle = false;
                UserStop = false;
            });
            //StopResumeButtonText = new RelayCommand(o => { });
            HomeMotorsCommand = new RelayCommand(o =>
            {
                if (LastEventPressed == 'S')
                    { 
                //create current offsetCovreUP file 
                }

                IsStopEnable = false;
            });
            
            ForceResumeCommand = new RelayCommand(o =>
            {
                LastEventPressed = 'F';

                //if (!MotorsCheckTimer.IsEnabled)
                //{ MotorsCheckTimer.Start();
                //    log.Log(LogLevel.Info, "Force Resume Started!");
                //}

            });

                //NegativeoffsetResume = new List<float>(DeviceCount);
                //NegativeoffsetResumeDirection = new List<bool>(DeviceCount);

                //for (int i = 0; i < DeviceCount; i++)
                //{
                //    NegativeoffsetResume.Add(0.00f);
                //    //NegativeoffsetResumeDirection.Add(false);
                //}
            }
            catch (Exception ex)
            {
                var log = LogManager.GetLogger("Project");
                log.Log(LogLevel.Error, ex.Message);
            }

        }


    }
}
