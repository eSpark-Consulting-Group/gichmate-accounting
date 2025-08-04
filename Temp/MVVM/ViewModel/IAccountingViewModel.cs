using FlexMold.Core;

namespace FlexMold.MVVM.ViewModel
{
    public interface IAccountingViewModel
    {
        object CurrentView { get; set; }
        RelayCommand MachineDetailCommand { get; set; }
        RelayCommand MotorStatusCommand { get; set; }
    }
}