using LSM_prototype.Core;
using LSM_prototype.MVVM.Model;
using LSM_prototype.MVVM.View;
using System.Collections.ObjectModel;
using System.Windows;

namespace LSM_prototype.MVVM.ViewModel
{
    class ManageOrdersViewModel : ViewModelBase
    {
        public RelayCommand AddCommand => new RelayCommand(execute => AddItem());
        public RelayCommand SaveCommand => new RelayCommand(execute => Save(), canExecute => CanSave());
        public RelayCommand OpenOrderCommand => new RelayCommand(execute => OpenOrder());
        public ObservableCollection<Orders> SharedOrders { get; } = new ObservableCollection<Orders>();
        public ObservableCollection<Accounts> SharedAccounts { get; } = new ObservableCollection<Accounts>();
        public ObservableCollection<Item> Items { get; } = new ObservableCollection<Item>();
        public ObservableCollection<string> AccountsOptions { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> StatusOptions { get; set; } = new ObservableCollection<string>
        {
            "Ongoing",
            "Cancelled",
            "Completed"
        };
        public ObservableCollection<string> DeviceOptions { get; set; } = new ObservableCollection<string>
        {
            "Laptop",
            "Desktop Computer",
            "Smartphone",
            "Tablet",
            "Game Console",
            "Others"
        };

        //for the checkbox
        public ObservableCollection<ServiceOptions> ServicesCheckbox { get; set; } = new ObservableCollection<ServiceOptions>
        {
            new ServiceOptions { Name = "Repair", DurationValue = 3, DurationText = "3 days", Price = 100.00m },
            new ServiceOptions { Name = "Cleaning", DurationValue = 1, DurationText = "1 days", Price = 200.00m },
            new ServiceOptions { Name = "Check-up", DurationValue = 2, DurationText = "2 days", Price = 300.00m },
            new ServiceOptions { Name = "Installation", DurationValue = 2, DurationText = "2 days", Price = 400.00m },
            new ServiceOptions { Name = "Maintenance", DurationValue = 3, DurationText = "3 days", Price = 500.00m }
        };
        public ObservableCollection<SelectableItem> ItemsCheckbox { get; set; } = new ObservableCollection<SelectableItem>();

        private int _totalDuration;
        public int TotalDuration
        {
            get => _totalDuration;
            set
            {
                if (_totalDuration != value)
                {
                    _totalDuration = value;
                    OnPropertyChanged(nameof(TotalDuration));
                }
            }
        }

        private decimal _compTotal;
        public decimal CompTotal
        {
            get => _compTotal;
            set
            {
                if (_compTotal != value)
                {
                    _compTotal = value;
                    OnPropertyChanged(nameof(CompTotal));
                }
            }
        }

        private decimal _servTotal;
        public decimal ServTotal
        {
            get => _servTotal;
            set
            {
                if (_servTotal != value)
                {
                    _servTotal = value;
                    OnPropertyChanged(nameof(ServTotal));
                }
            }
        }

        private decimal _tax;
        public decimal Tax
        {
            get => _tax;
            set
            {
                if (_tax != value)
                {
                    _tax = value;
                    OnPropertyChanged(nameof(Tax));
                }
            }
        }

        private decimal _subTotal;
        public decimal SubTotal
        {
            get => _subTotal;
            set
            {
                if (_subTotal != value)
                {
                    _subTotal = value;
                    OnPropertyChanged(nameof(SubTotal));
                }
            }
        }

        private decimal _discount;
        public decimal Discount
        {
            get => _discount;
            set
            {
                if (_discount != value)
                {
                    _discount = value;
                    OnPropertyChanged(nameof(Discount));
                }
            }
        }

        private decimal _total;
        public decimal Total
        {
            get => _total;
            set
            {
                if (_total != value)
                {
                    _total = value;
                    OnPropertyChanged(nameof(Total));
                }
            }
        }

        private bool _discountCheckbox;
        public bool DiscountCheckbox
        {
            get => _discountCheckbox;
            set
            {
                if (_discountCheckbox != value)
                {
                    _discountCheckbox = value;
                    OnPropertyChanged(nameof(DiscountCheckbox));
                }
            }
        }

        public ManageOrdersViewModel()
        {
            LoadAccountsFromDatabase();
            LoadOrdersFromDatabase();
            PopulateAccountsOptions();
            LoadItemsFromDatabase();
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

        private void AddItem()
        {
            if (!IsValidInput())
            {
                MessageBox.Show("there is an invalid input");
                return;
            }

            //lists for the items and services selected
            var selectedServices = GetSelectedServices();
            var selectedItems = GetSelectedItems();

            if (!selectedServices.Any())
            {
                MessageBox.Show("Please select at least one service!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!selectedItems.Any())
            {
                MessageBox.Show("Please select at least one component!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Add the new order to the database
            using (var context = new BenjaminDbContext())
            {
                var newOrder = new Orders
                {
                    DeviceType = NewOrder.DeviceType,
                    DeviceName = NewOrder.DeviceName,
                    Status = "Ongoing",
                    Employee = NewOrder.Employee,
                    OtherNotes = NewOrder.OtherNotes,
                    CustName = NewOrder.CustName,
                    CustPhoneNum = NewOrder.CustPhoneNum,
                    CustEmail = NewOrder.CustEmail,
                    AccountID = SelectedAccount?.AccountID ?? 0
                };

                context.Orders.Add(newOrder);
                context.SaveChanges(); // Save to the database

                // Directly add the selected services to the new order
                foreach (var service in selectedServices)
                {
                    var newselectedServices = new ServiceOptions
                    {
                        OrderID = newOrder.OrderID, // Link to the newly created order
                        Name = service.Name,
                        DurationValue = service.DurationValue,
                        DurationText = service.DurationText,
                        Price = service.Price,
                        IsSelected = service.IsSelected

                    };

                    context.ServiceOptions.Add(newselectedServices);
                }

                // Directly add the selected items to the new order
                foreach (var item in selectedItems)
                {
                    var newSelectableItem = new SelectableItem
                    {
                        ItemID = item.Item.ItemID, // Link to the actual item
                        OrderID = newOrder.OrderID,
                        IsSelected = item.IsSelected
                    };

                    context.SelectableItem.Add(newSelectableItem);
                }

                context.SaveChanges(); // Save to the database
            }
            LoadOrdersFromDatabase();

            ResetNewOrderFields();
            MessageBox.Show("Order added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void OpenOrder()
        {
            OrderWindowView orderWindow = new OrderWindowView(SelectedItem.OrderID);
            orderWindow.Show();
        }

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
            return true;
        }

        public void LoadOrdersFromDatabase()
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

        //loads items to use show in checkbox
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

        //loads the list of selected services
        public List<ServiceOptions> GetSelectedServices()
        {
            return ServicesCheckbox.Where(service => service.IsSelected).ToList();
        }
        //loads the list of selected items
        public List<SelectableItem> GetSelectedItems()
        {
            return ItemsCheckbox.Where(item => item.IsSelected).ToList();
        }

        public void ETAValue()
        {
            TotalDuration = 0;

            // Iterate through all services in ServicesCheckbox and update their IsSelected property
            foreach (var service in ServicesCheckbox)
            {
                if (service.IsSelected)
                {
                    // If the service is selected, add its DurationValue to the TotalDuration
                    TotalDuration += service.DurationValue;
                }
            }
        }
        public void CalculateTotal()
        {
            CompTotal = 0;
            ServTotal = 0;
            SubTotal = 0;
            Tax = 0;
            Total = 0;
            Discount = 0;
            decimal discount = 0;

            foreach (var item in ItemsCheckbox)
            {
                if (item.IsSelected)
                {
                    CompTotal += item.Item.Price;
                }
            }

            foreach (var service in ServicesCheckbox)
            {
                if (service.IsSelected)
                {
                    ServTotal += service.Price;
                }
            }

            if (DiscountCheckbox == true)
            {
                discount = 0.20m;
            }

            SubTotal = CompTotal + ServTotal;

            Tax = SubTotal * 0.12m;
            Discount = (SubTotal + Tax) * discount;
            Total = (SubTotal + Tax) - Discount;
        }

        private bool IsValidInput()
        {
            return !string.IsNullOrWhiteSpace(NewOrder.DeviceName) &&
                   !string.IsNullOrWhiteSpace(NewOrder.CustName) &&
                   !string.IsNullOrWhiteSpace(NewOrder.CustPhoneNum) &&
                   //NewOrder.Status == "Ongoing" &&
                   NewOrder.CustEmail.Contains("@");
        }
        private bool IsValidOrder(Orders order)
        {
            return !string.IsNullOrWhiteSpace(order.DeviceName) &&
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

