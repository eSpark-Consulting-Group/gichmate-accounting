using MVVMApp20.Core;

namespace MVVMApp20.MVVM.ViewModel
{
    public class ReportViewModel : ObservableObject
    {
        private object _currentView;

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public ReportPageViewModel ReportPageVM { get; set; }
        public HomeViewModel HomeVM { get; set; }

        public RelayCommand ShowReportPageCommand { get; }
        public RelayCommand ShowHomePageCommand { get; }

        public ReportViewModel()
        {
            ReportPageVM = new ReportPageViewModel();
            HomeVM = new HomeViewModel();

            CurrentView = ReportPageVM;

            ShowReportPageCommand = new RelayCommand(o => { CurrentView = ReportPageVM; });
            ShowHomePageCommand = new RelayCommand(o => { CurrentView = HomeVM; });
        }
    }
}
