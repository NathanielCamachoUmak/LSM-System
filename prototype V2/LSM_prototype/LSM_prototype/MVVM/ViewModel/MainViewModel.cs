using LSM_prototype.Core;

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
            InventoryVM = new InventoryViewModel();
            AccountsVM = new AccountsViewModel();
            CurrentView = InventoryVM;

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
