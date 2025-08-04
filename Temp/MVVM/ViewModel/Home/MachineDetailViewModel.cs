using FlexMold.MVVM.Model;
using FlexMold.Utility;
using NLog;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Media;

namespace FlexMold.MVVM.ViewModel.Home
{
    public class MachineDetailViewModel : StatusBaseViewModel, IMachineDetailViewModel
    {
        public ST_EcSlaveState[] MySlaves
        { get => _MySlaves; set { _MySlaves = value; OnPropertyChanged(); } }

        private ST_EcSlaveState[] _MySlaves;
        private ESlaveLink[] _Slave_Link_Status;
        public ESlaveLink[] Slave_Link_Status
        {
            get => _Slave_Link_Status;
            set
            {
                try
                {
                    SetLinkStatus(value);
                    _Slave_Link_Status = value;
                }
                catch (Exception ex)
                {
                    var log = LogManager.GetLogger("Machine Link Status");
                    log.Log(LogLevel.Error, ex.Message);
                }
            }
        }
        private void SetLinkStatus(ESlaveLink[] _value)
        {
            var log = LogManager.GetLogger("Machine Link Status");
            var v = GetMismatchedIndexes(Slave_Link_Status, _value);

            if (Slave_Link_Status != null)
            {
                for (int i = 0; i < v.Length; i++)
                {
                    DispLinkStatus(_value, v[i], log);
                }
            }
            else
            {
                for (int i = 0; i < v.Length; i++)
                {
                    DispLinkStatus(_value, i, log);
                }
            }
        }

        void DispLinkStatus(ESlaveLink[] _value, int index, Logger log)
        {
            MySlaves[index].Slave_Link_State = _value[index];
            switch (MySlaves[index].Slave_Link_State)
            {
                case ESlaveLink.EC_LINK_STATE_OK:
                    var _tmg = SetUI(index, MySlaves[index].Slave_Link_State, Brushes.Green);
                    log.Log(LogLevel.Info, _tmg);
                    TNF.ShowSuccess(_tmg);
                    break;
                case ESlaveLink.EC_LINK_STATE_Other_Fail:
                case ESlaveLink.EC_LINK_STATE_ADDITIONAL_LINK:
                case ESlaveLink.EC_LINK_STATE_NOT_PRESENT:
                case ESlaveLink.EC_LINK_STATE_LINK_WITHOUT_COMM:
                case ESlaveLink.ERR_PREOP_INIT_ERR:
                case ESlaveLink.EC_LINK_STATE_MISSING_LINK:
                    _tmg = SetUI(index, MySlaves[index].Slave_Link_State, Brushes.Red);
                    log.Log(LogLevel.Warn, _tmg);
                    TNF.ShowWarning(_tmg);
                    break;
                case ESlaveLink.UnAvailable:
                    _tmg = SetUI(index, MySlaves[index].Slave_Link_State, Brushes.White);
                    log.Log(LogLevel.Trace, _tmg);
                    //TNF.ShowWarning(_tmg);
                    break;
                default:
                    _tmg = SetUI(index, MySlaves[index].Slave_Link_State, Brushes.Orange);
                    log.Log(LogLevel.Error, "! Undocumented State ! " + _tmg);
                    TNF.ShowError(_tmg);
                    break;
            }
        }

        private string SetUI(int i, ESlaveLink sw, SolidColorBrush br)
        {
            var ds = Slave_StateMachine_Status == null ? "" : $"{Slave_StateMachine_Status[i]}";
            MySlaves[i].ToolTip =
                $"Box_{i} \n" +
                $" Link State : {sw}\n" +
                $" State Machine : {ds}\n";
            MySlaves[i].StrokeColor = br;
            var _tmg = $"{MySlaves[i].SlaveName} --> Link State {MySlaves[i].Slave_Link_State}";
            return _tmg;
        }
        private string SetUI(int i, ESlaveStateMachine sw, SolidColorBrush br)
        {
            var ds = Slave_Link_Status == null ? "" : $"{Slave_Link_Status[i]}";
            MySlaves[i].ToolTip =
                $"Box_{i} \n" +
                $" Link State : {ds}\n" +
                $" State Machine : {sw}\n";
            MySlaves[i].FillColor = br;
            var _tmg = $"{MySlaves[i].SlaveName} --> State Machine {MySlaves[i].Slave_State_Machine}";
            return _tmg;
        }

        private ESlaveStateMachine[] _Slave_StateMachine_Status;        

        public ESlaveStateMachine[] Slave_StateMachine_Status
        {
            get => _Slave_StateMachine_Status;
            set
            {
                try { 
                _Slave_StateMachine_Status = value;
                }
                catch (Exception ex)
                {
                    var log = LogManager.GetLogger("Machine Link Status");
                    log.Log(LogLevel.Error, ex.Message);
                }

            }
        }
     
        private void Slave_Link_Status_Changed(object? sender, EventArgs e)
        {
        }

        private void Slave_StateMachine_Status_Changed(object? sender, EventArgs e)
        {
        }
        public MachineDetailViewModel()
        {
            try { 

            }
            catch (Exception ex)
            {
                var log = LogManager.GetLogger("Machine Link Status");
                log.Log(LogLevel.Error, ex.Message);
            }

        }
    }
}