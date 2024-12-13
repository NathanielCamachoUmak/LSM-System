using LSM_prototype.Core;
using LSM_prototype.MVVM.Model;
using LSM_prototype.MVVM.View;
using System.Collections.ObjectModel;
using System.Windows;

namespace LSM_prototype.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObjects
    {
        public RelayCommand Analytics1ViewCommand { get; set; }
        public RelayCommand Analytics2ViewCommand { get; set; }
        public RelayCommand Analytics3ViewCommand { get; set; }
        public RelayCommand Analytics4ViewCommand { get; set; }
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand AddOrdersViewCommand { get; set; }
        public RelayCommand ManageOrdersViewCommand { get; set; }
        public RelayCommand OngoingOrdersViewCommand { get; set; }
        public RelayCommand CompletedOrdersViewCommand { get; set; }
        public RelayCommand CancelledOrdersViewCommand { get; set; }
        public RelayCommand InventoryViewCommand { get; set; }
        public RelayCommand AccountsViewCommand { get; set; }
        public RelayCommand MyAccountsViewCommand { get; set; }
        public RelayCommand LogoutCommand => new RelayCommand(execute => Logout());

        public Analytics1ViewModel Analytics1VM { get; set; }
        public Analytics2ViewModel Analytics2VM { get; set; }
        public Analytics3ViewModel Analytics3VM { get; set; }
        public Analytics4ViewModel Analytics4VM { get; set; }
        public HomeView HomeVM { get; set; }
        public AddOrdersViewModel AddOrdersVM { get; set; }
        public ManageOrdersViewModel ManageOrdersVM { get; set; }
        public OngoingOrdersViewModel OngoingOrdersVM { get; set; }
        public CompletedOrdersViewModel CompletedOrdersVM { get; set; }
        public CancelledOrdersViewModel CancelledOrdersVM { get; set; }
        public InventoryViewModel InventoryVM { get; set; }
        public AccountsViewModel AccountsVM { get; set; }
        public MyAccountsViewModel MyAccountsVM { get; set; }
        public ObservableCollection<Accounts> SharedAccounts { get; }
        public ObservableCollection<Accounts> User { get; }

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
            SharedAccounts = AccountsData.Instance.SharedAccounts;
            User = CurrentUser.Instance.User;

            Analytics1VM = new Analytics1ViewModel();
            Analytics2VM = new Analytics2ViewModel();
            Analytics3VM = new Analytics3ViewModel();
            Analytics4VM = new Analytics4ViewModel();
            HomeVM = new HomeView();
            AddOrdersVM = new AddOrdersViewModel();
            ManageOrdersVM = new ManageOrdersViewModel();
            OngoingOrdersVM = new OngoingOrdersViewModel();
            CompletedOrdersVM = new CompletedOrdersViewModel();
            CancelledOrdersVM = new CancelledOrdersViewModel();
            InventoryVM = new InventoryViewModel();
            AccountsVM = new AccountsViewModel();
            MyAccountsVM = new MyAccountsViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o =>
            {
                if (!(CurrentView == AddOrdersVM ||
                      CurrentView == ManageOrdersVM ||
                      CurrentView == OngoingOrdersVM ||
                      CurrentView == CompletedOrdersVM ||
                      CurrentView == CancelledOrdersVM ||
                      CurrentView == Analytics1VM ||
                      CurrentView == Analytics2VM ||
                      CurrentView == Analytics3VM ||
                      CurrentView == Analytics4VM))
                {
                    // If the current view is the same, close it (set to HomeVM)
                    CurrentView = HomeVM;
                }
            });

            Analytics1ViewCommand = new RelayCommand(o =>
            {
                if (CurrentView == Analytics1VM)
                {
                    // If the current view is the same, close it (set to HomeVM)
                    CurrentView = HomeVM;
                }
                else
                {
                    Analytics1VM.LoadOrdersFromDatabase();
                    CurrentView = Analytics1VM;
                }
            });

            Analytics2ViewCommand = new RelayCommand(o =>
            {
                if (CurrentView == Analytics2VM)
                {
                    // If the current view is the same, close it (set to HomeVM)
                    CurrentView = HomeVM;
                }
                else
                {
                    Analytics2VM.LoadOrdersFromDatabase();
                    CurrentView = Analytics2VM;
                }
            });

            Analytics3ViewCommand = new RelayCommand(o =>
            {
                if (CurrentView == Analytics3VM)
                {
                    // If the current view is the same, close it (set to HomeVM)
                    CurrentView = HomeVM;
                }
                else
                {
                    Analytics3VM.LoadOrdersFromDatabase();
                    CurrentView = Analytics3VM;
                }
            });

            Analytics4ViewCommand = new RelayCommand(o =>
            {
                if (CurrentView == Analytics4VM)
                {
                    // If the current view is the same, close it (set to HomeVM)
                    CurrentView = HomeVM;
                }
                else
                {
                    Analytics4VM.LoadOrdersFromDatabase();
                    CurrentView = Analytics4VM;
                }
            });

            AddOrdersViewCommand = new RelayCommand(o =>
            {
                if (CurrentView == AddOrdersVM)
                {
                    // If the current view is the same, close it (set to HomeVM)
                    CurrentView = HomeVM;
                }
                else
                {
                    SharedAccounts.Clear();
                    LoadAccountsFromDatabase();
                    AddOrdersVM.LoadItemsFromDatabase();
                    AddOrdersVM.PopulateAccountsOptions();
                    AddOrdersVM.LoadOrdersFromDatabase();

                    CurrentView = AddOrdersVM;
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
                    ManageOrdersVM.LoadOrdersFromDatabase();
                    CurrentView = ManageOrdersVM;
                }
            });

            OngoingOrdersViewCommand = new RelayCommand(o =>
            {
                if (CurrentView == OngoingOrdersVM)
                {
                    // If the current view is the same, close it (set to HomeVM)
                    CurrentView = HomeVM;
                }
                else
                {
                    OngoingOrdersVM.LoadOrdersFromDatabase();
                    CurrentView = OngoingOrdersVM;
                }
            });

            CompletedOrdersViewCommand = new RelayCommand(o =>
            {
                if (CurrentView == CompletedOrdersVM)
                {
                    // If the current view is the same, close it (set to HomeVM)
                    CurrentView = HomeVM;
                }
                else
                {
                    CompletedOrdersVM.LoadOrdersFromDatabase();
                    CurrentView = CompletedOrdersVM;
                }
            });

            CancelledOrdersViewCommand = new RelayCommand(o =>
            {
                if (CurrentView == CancelledOrdersVM)
                {
                    // If the current view is the same, close it (set to HomeVM)
                    CurrentView = HomeVM;
                }
                else
                {
                    CancelledOrdersVM.LoadOrdersFromDatabase();
                    CurrentView = CancelledOrdersVM;
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
                    InventoryVM.LoadItemsFromDatabase();
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
                    AccountsVM.LoadAccountsFromDatabase();
                    CurrentView = AccountsVM;
                }
            });

            MyAccountsViewCommand = new RelayCommand(o =>
            {
                if (CurrentView == MyAccountsVM)
                {
                    // If the current view is the same, close it (set to HomeVM)
                    CurrentView = HomeVM;
                }
                else
                {
                    CurrentView = MyAccountsVM;
                }
            });
        }

        private void Logout()
        {
            SharedAccounts.Clear();
            User.Clear();

            LoadAccountsFromDatabase();

            if (SharedAccounts.Count == 0)
            {
                // Open the new main window
                RegisterView regWindow = new RegisterView();
                Application.Current.MainWindow = regWindow;
                regWindow.Show();

                // Close the login window
                Application.Current.Windows
                    .OfType<MainWindow>()
                    .FirstOrDefault()?.Close();
            }
            else
            {
                LoginView loginWindow = new LoginView();
                Application.Current.MainWindow = loginWindow;
                loginWindow.Show();

                Application.Current.Windows
                    .OfType<MainWindow>()
                    .FirstOrDefault()?.Close();
            }
        }

        private void LoadAccountsFromDatabase()
        {
            using (var context = new BenjaminDbContext())
            {
                // Clear existing items to avoid duplicates
                SharedAccounts.Clear();

                // Add filtered items to the ObservableCollection
                foreach (var account in context.Accounts.Where(account => account.AccessLevel == "Admin"))
                {
                    SharedAccounts.Add(account);
                }
            }
        }
    }
}
