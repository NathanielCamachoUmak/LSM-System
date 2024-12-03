using LSM_prototype.Core;
using LSM_prototype.MVVM.Model;
using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;
using System.Security.Principal;
using System.Windows;
using System.Windows.Controls;

namespace LSM_prototype.MVVM.ViewModel
{
    class ManageOrdersViewModel : ViewModelBase
    {
        public RelayCommand AddCommand => new RelayCommand(execute => AddItem());
        public RelayCommand SaveCommand => new RelayCommand(execute => Save(), canExecute => CanSave());
        public ObservableCollection<Orders> SharedOrders { get; } = new ObservableCollection<Orders>();
        public ObservableCollection<Accounts> SharedAccounts { get; } = new ObservableCollection<Accounts>();

        private Orders _newOrder = new Orders();
        public Orders NewOrder
        {
            get => _newOrder;
            set
            { 
                _newOrder = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> AccountsOptions { get; set; } = new ObservableCollection<string>();

        public ObservableCollection<string> StatusOptions { get; set; } = new ObservableCollection<string>
        {
            "Ongoing",
            "Cancelled",
            "Completed"
        };
        public string Status { get; set; } = "Ongoing";

        public ManageOrdersViewModel()
        {
            LoadAccountsFromDatabase();
            LoadItemsFromDatabase();
            PopulateAccountsOptions();
        }

        public void PopulateAccountsOptions()
        {
            AccountsOptions.Clear();
            LoadAccountsFromDatabase();

            foreach (var account in SharedAccounts)
            {
                if (account.AccessLevel != "Admin")
                {
                    AccountsOptions.Add(account.Name);
                }
            }
        }

        private Orders _selectedItem;

        public Orders SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        private Accounts _selectedAccount;
        public Accounts SelectedAccount
        {
            get => _selectedAccount;
            set
            {
                _selectedAccount = value;
                OnPropertyChanged();
            }
        }

        private void AddItem()
        {
            MessageBox.Show("test");
            if (!IsValidInput())
            {
                MessageBox.Show("adsasddas");
                return;
            }
            MessageBox.Show(NewOrder.Employee);

            SharedOrders.Add(new Orders
            {
                DeviceName = NewOrder.DeviceName,
                Status = "Ongoing",
                Problem = NewOrder.Problem,
                OtherNotes = NewOrder.OtherNotes,
                Employee = NewOrder.Employee,
                CustName = NewOrder.CustName,
                CustPhoneNum = NewOrder.CustPhoneNum,
                CustEmail = NewOrder.CustEmail,
                AccountID = SelectedAccount?.AccountID ?? 0
            });

            ResetNewOrderFields();
            MessageBox.Show("Order added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        //save to database using this
        private void Save()
        {
            if (!SharedOrders.All(IsValidOrder)) return;

            using (var context = new BenjaminDbContext())
            {
                foreach (var order in SharedOrders)
                {
                    if (order.OrderID == 0)
                        context.Orders.Add(order);
                    else
                        context.Orders.Update(order);
                }
                context.SaveChanges();
            }

            MessageBox.Show("Changes saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool CanSave()
        {
            //if ok, return true
            return true;
        }

        public void LoadItemsFromDatabase()
        {
            SharedOrders.Clear();
            using (var context = new BenjaminDbContext())
            {
                var ordersFromDb = context.Orders?.ToList() ?? new List<Orders>();
                foreach (var order in ordersFromDb)
                {
                    SharedOrders.Add(order);
                }
            }
        }

        private void LoadAccountsFromDatabase()
        {
            SharedAccounts.Clear();
            using (var context = new BenjaminDbContext())
            {
                var accountsFromDb = context.Accounts?.ToList() ?? new List<Accounts>();
                foreach (var account in accountsFromDb)
                {
                    SharedAccounts.Add(account);
                }
            }
        }

        private bool IsValidInput()
        {
            return !string.IsNullOrWhiteSpace(NewOrder.DeviceName) &&
                   !string.IsNullOrWhiteSpace(NewOrder.Problem) &&
                   !string.IsNullOrWhiteSpace(NewOrder.CustName) &&
                   !string.IsNullOrWhiteSpace(NewOrder.CustPhoneNum) &&
                   //NewOrder.Status == "Ongoing" &&
                   NewOrder.CustEmail.Contains("@");
        }
        private bool IsValidOrder(Orders order)
        {
            return !string.IsNullOrWhiteSpace(order.DeviceName) &&
                   !string.IsNullOrWhiteSpace(order.Problem) &&
                   !string.IsNullOrWhiteSpace(order.CustName) &&
                   !string.IsNullOrWhiteSpace(order.CustPhoneNum) &&
                   order.CustEmail.Contains("@");
        }

        private void ResetNewOrderFields()
        {
            NewOrder = new Orders();
        }
    }
}

