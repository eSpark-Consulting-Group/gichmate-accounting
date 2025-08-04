using FlexMold.Core;
using FlexMold.Utility;
using NLog;
using System;
using System.Threading;
using System.Windows;
using TwinCAT.Ads;
using Thread = System.Threading.Thread;

namespace FlexMold.MVVM.ViewModel
{
    internal class ConnectionLostViewModel : ObservableObject
    {
        private static Logger log = LogManager.GetLogger("TwinCat Connection");
        private string _EventMessage;

        public string EventMessage
        {
            get { return _EventMessage; }
            set { _EventMessage = value; OnPropertyChanged(); }
        }

        private bool _Restore;
        private int retry;

        private bool setState()
        {
            log.Log(LogLevel.Info, $"{retry} Attempting to Start PLC ....");
            return true;
        }
    }
}