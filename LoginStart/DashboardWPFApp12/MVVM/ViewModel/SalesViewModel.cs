using MVVMApp20.Core;
using System;

namespace MVVMApp20.MVVM.ViewModel
{
    public class SalesViewModel : ObservableObject
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

        public SalesPageViewModel SalesPageVM { get; set; }
        public HomeViewModel HomeVM { get; set; }

        public RelayCommand ShowSalesPageCommand { get; }
        public RelayCommand ShowHomePageCommand { get; }

        public SalesViewModel()
        {
            SalesPageVM = new SalesPageViewModel();
            HomeVM = new HomeViewModel();

            CurrentView = SalesPageVM;

            ShowSalesPageCommand = new RelayCommand(o => { CurrentView = SalesPageVM; });
            ShowHomePageCommand = new RelayCommand(o => { CurrentView = HomeVM; });
        }
    }
}