using LSM_prototype.Core;
using System.Windows;

namespace LSM_prototype.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObjects
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand OngoingOrdersViewCommand { get; set; }
        public RelayCommand ManageOrdersViewCommand { get; set; }
        public RelayCommand InventoryViewCommand { get; set; }
        public RelayCommand AccountsViewCommand { get; set; }
        public RelayCommand AnalyticsCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public OngoingOrdersViewModel OngoingOrdersVM { get; set; }
        public ManageOrdersViewModel ManageOrdersVM { get; set; }
        public InventoryViewModel InventoryVM { get; set; }
        public AccountsViewModel AccountsVM { get; set; }

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

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            OngoingOrdersVM = new OngoingOrdersViewModel();
            ManageOrdersVM = new ManageOrdersViewModel();
            InventoryVM = new InventoryViewModel();
            AccountsVM = new AccountsViewModel();

            CurrentView = ManageOrdersVM;

            OngoingOrdersViewCommand = new RelayCommand(o =>
            {
                if (CurrentView == OngoingOrdersVM)
                {
                    // If the current view is the same, close it (set to HomeVM)
                    CurrentView = HomeVM;
                }
                else
                {
                    CurrentView = OngoingOrdersVM;
                }
            });

            ManageOrdersViewCommand = new RelayCommand(o =>
            {
                if (CurrentView == ManageOrdersVM)
                {
                    // If the current view is the same, close it (set to HomeVM)
                    CurrentView = HomeVM;
                }
                else
                {
                    CurrentView = ManageOrdersVM;
                }
            });

            InventoryViewCommand = new RelayCommand(o =>
            {
                if (CurrentView == InventoryVM)
                {
                    // If the current view is the same, close it (set to HomeVM)
                    CurrentView = HomeVM;
                }
                else
                {
                    CurrentView = InventoryVM;
                }
            });

            AccountsViewCommand = new RelayCommand(o =>
            {
                if (CurrentView == AccountsVM)
                {
                    // If the current view is the same, close it (set to HomeVM)
                    CurrentView = HomeVM;
                }
                else
                {
                    CurrentView = AccountsVM;
                }
            });
        }
    }
}
