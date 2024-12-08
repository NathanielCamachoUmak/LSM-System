using LSM_prototype.MVVM.Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace LSM_prototype.MVVM.ViewModel
{
    class OrderWindowViewModel : ViewModelBase
    {
        public ObservableCollection<Accounts> SharedAccounts { get; } = new ObservableCollection<Accounts>();
        public ObservableCollection<string> AccountsOptions { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> StatusOptions { get; set; } = new ObservableCollection<string>
        {
            "Ongoing",
            "Cancelled",
            "Completed"
        };
        public ObservableCollection<ServiceOptions> ServicesCheckbox { get; set; } = new ObservableCollection<ServiceOptions>
        {
            new ServiceOptions { Name = "Repair", DurationValue = 3, DurationText = "3 days" },
            new ServiceOptions { Name = "Cleaning", DurationValue = 1, DurationText = "1 days" },
            new ServiceOptions { Name = "Check-up", DurationValue = 2, DurationText = "2 days" },
            new ServiceOptions { Name = "Installation", DurationValue = 2, DurationText = "2 days" },
            new ServiceOptions { Name = "Maintenance", DurationValue = 3, DurationText = "3 days" }
        };
        public ObservableCollection<SelectableItem> ItemsCheckbox { get; set; } = new ObservableCollection<SelectableItem>();

        private Orders _orderDetails;
        public Orders OrderDetails
        {
            get => _orderDetails;
            set
            {
                _orderDetails = value;
                OnPropertyChanged(nameof(OrderDetails));
            }
        }

        public OrderWindowViewModel(int OrderID)
        {
            OrderReciever(OrderID);
            PopulateAccountsOptions();
            LoadItemsFromDatabase();
        }

        public void OrderReciever(int OrderID)
        {
            using (var context = new BenjaminDbContext())
            {
                // Find order by OrderID
                var order = context.Orders?.FirstOrDefault(o => o.OrderID == OrderID);

                if (order != null)
                {
                    // Assign to OrderDetails if found
                    OrderDetails = order;
                }
                else
                {
                    MessageBox.Show("Order not found.");
                }
            }
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
        public void LoadItemsFromDatabase()
        {
            ItemsCheckbox.Clear();
            using (var context = new BenjaminDbContext())
            {
                var itemsFromDb = context.Item?.ToList() ?? new List<Item>();
                foreach (var item in itemsFromDb)
                {
                    ItemsCheckbox.Add(new SelectableItem
                    {
                        Item = item,
                        ItemID = item.ItemID,
                        IsSelected = false // Default state
                    });
                }
            }
        }
    }
}



//var orderServices = context.ServiceOptions
//                          ?.Where(service => service.OrderID == orderIdToFind)
//                          .ToList();
