using LSM_prototype.Core;
using LSM_prototype.MVVM.Model;
using LSM_prototype.MVVM.View;
using System.Collections.ObjectModel;
using System.Windows;

namespace LSM_prototype.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObjects
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand ManageOrdersViewCommand { get; set; }
        public RelayCommand InventoryViewCommand { get; set; }
        public RelayCommand AccountsViewCommand { get; set; }
        public RelayCommand MyAccountsViewCommand { get; set; }
        public RelayCommand LogoutCommand => new RelayCommand(execute => Logout());

        public HomeViewModel HomeVM { get; set; }
        public ManageOrdersViewModel ManageOrdersVM { get; set; }
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

            HomeVM = new HomeViewModel();
            ManageOrdersVM = new ManageOrdersViewModel();
            InventoryVM = new InventoryViewModel();
            AccountsVM = new AccountsViewModel();
            MyAccountsVM = new MyAccountsViewModel();

            CurrentView = HomeVM;

            ManageOrdersViewCommand = new RelayCommand(o =>
            {
                if (CurrentView == ManageOrdersVM)
                {
                    // If the current view is the same, close it (set to HomeVM)
                    CurrentView = HomeVM;
                }
                else
                {
                    ManageOrdersVM.LoadItemsFromDatabase();

                    SharedAccounts.Clear();
                    LoadAccountsFromDatabase();
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
                // Assuming you want to load all accounts from the database again
                var accountsFromDb = context.Accounts?.ToList() ?? new List<Accounts>();

                // Add the accounts to the SharedAccounts collection
                foreach (var account in accountsFromDb)
                {
                    SharedAccounts.Add(account);
                }
            }
        }
    }
}
