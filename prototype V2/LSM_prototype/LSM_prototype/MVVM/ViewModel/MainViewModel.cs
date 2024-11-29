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
        public RelayCommand OngoingOrdersViewCommand { get; set; }
        public RelayCommand ManageOrdersViewCommand { get; set; }
        public RelayCommand InventoryViewCommand { get; set; }
        public RelayCommand AccountsViewCommand { get; set; }
        public RelayCommand AnalyticsCommand { get; set; }
        public RelayCommand LogoutCommand => new RelayCommand(execute => Logout());

        public HomeViewModel HomeVM { get; set; }
        public OngoingOrdersViewModel OngoingOrdersVM { get; set; }
        public ManageOrdersViewModel ManageOrdersVM { get; set; }
        public InventoryViewModel InventoryVM { get; set; }
        public AccountsViewModel AccountsVM { get; set; }
        public ObservableCollection<Accounts> SharedAccounts { get; }

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
            SharedAccounts = AccountsData.Instance.AccountsList;

            HomeVM = new HomeViewModel();
            OngoingOrdersVM = new OngoingOrdersViewModel();
            ManageOrdersVM = new ManageOrdersViewModel();
            InventoryVM = new InventoryViewModel();
            AccountsVM = new AccountsViewModel();

            CurrentView = HomeVM;

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

        private void Logout()
        {

            //ISRAEL HELPPPPPPPPPPPPPPPPPP
            //when I delete all accounts and save to database it doesnt update the observable collection,
            //and it still accepts the deleted account login details

            if (SharedAccounts.Count == 0)
            {
                MessageBox.Show("register window");
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
                MessageBox.Show("login window");
                LoginView loginWindow = new LoginView();
                Application.Current.MainWindow = loginWindow;
                loginWindow.Show();

                Application.Current.Windows
                    .OfType<MainWindow>()
                    .FirstOrDefault()?.Close();
            }
        }
    }
}
