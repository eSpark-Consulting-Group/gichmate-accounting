using FlexMold.Core;
using FlexMold.MVVM.View.Home;

namespace FlexMold.MVVM.ViewModel
{
    public class AccountingViewModel : ObservableObject, IAccountingViewModel
    {
        /// <summary>
        /// Relay Commands
        /// </summary>
        public RelayCommand MachineDetailCommand { get; set; }
        public RelayCommand MotorStatusCommand { get; set; }

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public AccountingViewModel(
            MachineDetailView machineDetailView,
            MotorStatusView motorStatusView
            )
        {

            CurrentView = machineDetailView;

            MachineDetailCommand = new RelayCommand(o => { CurrentView = machineDetailView; });
            MotorStatusCommand = new RelayCommand(o => { CurrentView = motorStatusView; });
        }
    }
}