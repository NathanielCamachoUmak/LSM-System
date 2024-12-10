using iText.Layout.Borders;
using LSM_prototype.Core;
using LSM_prototype.MVVM.Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace LSM_prototype.MVVM.ViewModel
{
    class OrderWindowViewModel : ViewModelBase
    {
        public RelayCommand SaveCommand => new RelayCommand(execute => SaveOrderChanges());
        public RelayCommand ExportCommand => new RelayCommand(execute => ExportToPDF());
        public ObservableCollection<Accounts> SharedAccounts { get; } = new ObservableCollection<Accounts>();
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
            PopulateAccountsOptions();
            LoadItemsFromDatabase();
            OrderReciever(OrderID);
        }
        public void OrderReciever(int OrderID)
        {
            using (var context = new BenjaminDbContext())
            {
                // Find order by OrderID
                var order = context.Orders?.FirstOrDefault(o => o.OrderID == OrderID);

                if (order == null)
                {
                    MessageBox.Show("Order not found.");
                }

                OrderDetails = order;

                // Fetch selected services from the database
                var selectedServicesList = context.ServiceOptions.Where(s => s.OrderID == OrderID).ToList();

                // Iterate through all services in ServicesCheckbox and update their IsSelected property
                foreach (var service in ServicesCheckbox)
                {
                    var selectedService = selectedServicesList.FirstOrDefault(s => s.Name == service.Name);

                    if (selectedService != null)
                    {
                        // If the service is selected in the database, mark it as selected
                        service.IsSelected = true;
                    }
                    else
                    {
                        // If the service is not selected in the database, make sure it is unchecked
                        service.IsSelected = false;
                    }
                }

                // Fetch the ItemIDs of the current OrderID from the SelectableItem table
                var selectedItemIds = context.SelectableItem.Where(s => s.OrderID == OrderID).Select(s => s.ItemID).ToList();

                // Iterate through the ItemsCheckbox and check if the item is selected
                foreach (var item in ItemsCheckbox)
                {

                    if (selectedItemIds.Contains(item.ItemID))
                    {
                        item.IsSelected = true;
                    }
                    else
                    {
                        item.IsSelected = false;
                    }
                }
            }
            ETAValue();
            CalculateTotal();
        }
        private void PopulateAccountsOptions()
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
        private void LoadItemsFromDatabase()
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
        private bool IsValidOrder(Orders order)
        {
            return !string.IsNullOrWhiteSpace(order.DeviceName) &&
                   !string.IsNullOrWhiteSpace(order.CustName) &&
                   !string.IsNullOrWhiteSpace(order.CustPhoneNum) &&
                   order.CustEmail.Contains("@");
        }

        //relay command methods
        private void SaveOrderChanges()
        {
            if (!IsValidOrder(OrderDetails)) return;

            using (var context = new BenjaminDbContext())
            {
                context.Orders.Update(OrderDetails);

                // Step 1: Retrieve the selected services that match the current OrderID
                var servicesToRemove = context.ServiceOptions.Where(s => s.OrderID == OrderDetails.OrderID).ToList();

                // Step 2: Delete the selected services that match the current OrderID
                context.ServiceOptions.RemoveRange(servicesToRemove);

                // Step 3: Put the selected services into a list and add to the database
                var newSelectedServices = ServicesCheckbox.Where(s => s.IsSelected)
                                          .Select(service => new ServiceOptions
                                          {
                                              OrderID = OrderDetails.OrderID, // Link to the correct order
                                              Name = service.Name,
                                              DurationValue = service.DurationValue,
                                              DurationText = service.DurationText,
                                              Price = service.Price,
                                              IsSelected = service.IsSelected
                                          }).ToList();

                // Step 4: Check if there are any services selected
                if (!newSelectedServices.Any())
                {
                    MessageBox.Show("Please select at least one service!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Step 5: Add the list of selected services to the database
                context.ServiceOptions.AddRange(newSelectedServices);


                // Step 1: Retrieve the selected items that match the current OrderID
                var itemsToRemove = context.SelectableItem.Where(s => s.OrderID == OrderDetails.OrderID).ToList();

                // Step 2: Delete the selected items that match the current OrderID
                context.SelectableItem.RemoveRange(itemsToRemove);

                // Step 3: Put the selected items into a list and add to the database
                var newSelectedItems = ItemsCheckbox.Where(s => s.IsSelected)
                                       .Select(item => new SelectableItem
                                       {
                                           ItemID = item.Item.ItemID,
                                           OrderID = OrderDetails.OrderID,
                                           IsSelected = item.IsSelected
                                       }).ToList();

                // Step 4: Check if there are any items selected
                if (!newSelectedItems.Any())
                {
                    MessageBox.Show("Please select at least one item!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Step 5: Add the list of selected items to the database
                context.SelectableItem.AddRange(newSelectedItems);

                context.SaveChanges();

                MessageBox.Show("Changes saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            ETAValue();
            CalculateTotal();
        }
        private void ExportToPDF()
        {
            if (OrderDetails == null)
            {
                MessageBox.Show("No order selected to export!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // PDF file path is in Exports
            string filePath = $"..\\..\\..\\Exports\\Order_{OrderDetails.OrderID}.pdf";

            try
            {

                using (var writer = new iText.Kernel.Pdf.PdfWriter(filePath))
                using (var pdf = new iText.Kernel.Pdf.PdfDocument(writer))
                {
                    var document = new iText.Layout.Document(pdf);

                    // Receipt title
                    var title = new iText.Layout.Element.Paragraph($"Order Details - ID: {OrderDetails.OrderID}")
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                        .SetFontSize(20);
                    document.Add(title);

                    // Info on beloved customer
                    document.Add(new iText.Layout.Element.Paragraph("Customer Information").SetFontSize(16));
                    document.Add(new iText.Layout.Element.Paragraph($"Name: {OrderDetails.CustName}"));
                    document.Add(new iText.Layout.Element.Paragraph($"Phone Number: {OrderDetails.CustPhoneNum}"));
                    document.Add(new iText.Layout.Element.Paragraph($"Email: {OrderDetails.CustEmail}"));

                    // Main gist of info
                    document.Add(new iText.Layout.Element.Paragraph("\nOrder Details").SetFontSize(16));
                    document.Add(new iText.Layout.Element.Paragraph($"Device: {OrderDetails.DeviceName}"));
                    document.Add(new iText.Layout.Element.Paragraph($"Employee Assigned: {OrderDetails.Employee}"));
                    document.Add(new iText.Layout.Element.Paragraph($"Status: {OrderDetails.Status}"));
                    document.Add(new iText.Layout.Element.Paragraph($"Other Notes: {OrderDetails.OtherNotes}"));


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