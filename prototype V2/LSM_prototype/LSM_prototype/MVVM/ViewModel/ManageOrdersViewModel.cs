﻿using LSM_prototype.Core;
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
        public RelayCommand ExportCommand => new RelayCommand(execute => ExportToPDF());
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

        //for the checkbox
        public ObservableCollection<ServiceOptions> ServicesCheckbox { get; set; } = new ObservableCollection<ServiceOptions>
        {
            new ServiceOptions { Name = "Repair", DurationValue = 3, DurationText = "3 days" },
            new ServiceOptions { Name = "Cleaning", DurationValue = 1, DurationText = "1 days" },
            new ServiceOptions { Name = "Check-up", DurationValue = 2, DurationText = "2 days" },
            new ServiceOptions { Name = "Installation", DurationValue = 2, DurationText = "2 days" },
            new ServiceOptions { Name = "Maintenance", DurationValue = 3, DurationText = "3 days" }
        };
        public ObservableCollection<SelectableItem> ItemsCheckbox { get; set; } = new ObservableCollection<SelectableItem>();


        public string Status { get; set; } = "Ongoing";

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

            //just shows the list of services selected
            foreach (var service in selectedServices)
            {
                MessageBox.Show($"{service.Name}, {service.DurationText}");
            }

            //just shows the list of items selected
            foreach (var item in selectedItems)
            {
                if (item.Item != null)
                {
                    MessageBox.Show($"{item.Item.Name}, {item.Item.Stock}, {item.Item.Price}");
                }
                else
                {
                    MessageBox.Show("Item data is missing.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }


            // Add the new order to the database
            using (var context = new BenjaminDbContext())
            {
                var newOrder = new Orders
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
                        DurationText = service.DurationText

                    };

                    context.ServiceOptions.Add(newselectedServices);
                }

                // Directly add the selected items to the new order
                foreach (var item in selectedItems)
                {
                    var newSelectableItem = new SelectableItem
                    {
                        ItemID = item.Item.ItemID, // Link to the actual item
                        OrderID = newOrder.OrderID, // Link to the newly created order
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

        private bool IsValidInput()
        {
            NewOrder.Problem = "estte";

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

        public void ExportToPDF()
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("No order selected to export!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // PDF file path is in Exports
            string filePath = $"..\\..\\..\\Exports\\Order_{SelectedItem.CustName}.pdf";

            try
            {
                
                using (var writer = new iText.Kernel.Pdf.PdfWriter(filePath))
                using (var pdf = new iText.Kernel.Pdf.PdfDocument(writer))
                {
                    var document = new iText.Layout.Document(pdf);

                    // Receipt title
                    var title = new iText.Layout.Element.Paragraph($"Order Details - ID: {SelectedItem.OrderID}")
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                        .SetFontSize(20);
                    document.Add(title);

                    // Info on beloved customer
                    document.Add(new iText.Layout.Element.Paragraph("Customer Information").SetFontSize(16));
                    document.Add(new iText.Layout.Element.Paragraph($"Name: {SelectedItem.CustName}"));
                    document.Add(new iText.Layout.Element.Paragraph($"Phone Number: {SelectedItem.CustPhoneNum}"));
                    document.Add(new iText.Layout.Element.Paragraph($"Email: {SelectedItem.CustEmail}"));

                    // Main gist of info
                    document.Add(new iText.Layout.Element.Paragraph("\nOrder Details").SetFontSize(16));
                    document.Add(new iText.Layout.Element.Paragraph($"Device: {SelectedItem.DeviceName}"));
                    document.Add(new iText.Layout.Element.Paragraph($"Employee Assigned: {SelectedItem.Employee}"));
                    document.Add(new iText.Layout.Element.Paragraph($"Status: {SelectedItem.Status}"));
                    document.Add(new iText.Layout.Element.Paragraph($"Problem: {SelectedItem.Problem}"));
                    document.Add(new iText.Layout.Element.Paragraph($"Other Notes: {SelectedItem.OtherNotes}"));

                    
                    document.Close();
                }

                MessageBox.Show($"Order exported successfully to {filePath}!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to export order: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

