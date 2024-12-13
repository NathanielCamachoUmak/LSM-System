using iText.Commons.Bouncycastle.Asn1.X509;
using iText.Layout.Borders;
using LSM_prototype.Core;
using LSM_prototype.MVVM.Model;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
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
        public bool CanModifyOrder => OrderDetails?.Status != "Ongoing";


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

            if (OrderDetails.Discounted == true)
            {
                discount = 0.20m;
            }

            SubTotal = CompTotal + ServTotal;

            Tax = SubTotal * 0.12m;
            Discount = (SubTotal + Tax) * discount;
            Total = (SubTotal + Tax) - Discount;
        }
        public bool IsValidOrder(Orders order)
        {
            // Validate device type
            if (string.IsNullOrWhiteSpace(order.DeviceType))
            {
                MessageBox.Show($"Invalid device type!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Validate device name
            if (string.IsNullOrWhiteSpace(order.DeviceName))
            {
                MessageBox.Show($"Invalid device name!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Validate employee assigned
            if (string.IsNullOrWhiteSpace(order.Employee))
            {
                MessageBox.Show($"Invalid employee!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Validate employee assigned
            if (string.IsNullOrWhiteSpace(order.CustName))
            {
                MessageBox.Show($"Invalid customer name!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Validate Phone Number
            if (string.IsNullOrWhiteSpace(order.CustPhoneNum) || !Regex.IsMatch(order.CustPhoneNum, @"^\d+$"))
            {
                MessageBox.Show($"Invalid phone number!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Validate Email 
            if (string.IsNullOrWhiteSpace(order.CustEmail) || !Regex.IsMatch(order.CustEmail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show($"Invalid email!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        //relay command methods
        private void SaveOrderChanges()
        {
            if (!IsValidOrder(OrderDetails)) return;

            using (var context = new BenjaminDbContext())
            {
                // Step 1: Retrieve and delete existing services for the current OrderID
                var servicesToRemove = context.ServiceOptions.Where(s => s.OrderID == OrderDetails.OrderID).ToList();
                context.ServiceOptions.RemoveRange(servicesToRemove);

                // Step 2: Add the selected services to the database
                var newSelectedServices = ServicesCheckbox.Where(s => s.IsSelected)
                                              .Select(service => new ServiceOptions
                                              {
                                                  OrderID = OrderDetails.OrderID,
                                                  Name = service.Name,
                                                  DurationValue = service.DurationValue,
                                                  DurationText = service.DurationText,
                                                  Price = service.Price,
                                                  IsSelected = service.IsSelected
                                              }).ToList();

                // Check if there are any services selected
                if (!newSelectedServices.Any())
                {
                    MessageBox.Show("Please select at least one service!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Add the selected services to the database
                context.ServiceOptions.AddRange(newSelectedServices);

                // Step 3: Retrieve and delete selected items for the current OrderID
                var itemsToRemove = context.SelectableItem.Where(s => s.OrderID == OrderDetails.OrderID).ToList();
                context.SelectableItem.RemoveRange(itemsToRemove);

                // Step 4: Add the selected items to the database
                var newSelectedItems = ItemsCheckbox.Where(s => s.IsSelected)
                                               .Select(item => new SelectableItem
                                               {
                                                   ItemID = item.Item.ItemID,
                                                   OrderID = OrderDetails.OrderID,
                                                   IsSelected = item.IsSelected
                                               }).ToList();

                // Check if there are any items selected
                if (!newSelectedItems.Any())
                {
                    MessageBox.Show("Please select at least one item!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Add the selected items to the database
                context.SelectableItem.AddRange(newSelectedItems);
                context.SaveChanges();

                var oldStatus = context.Orders.FirstOrDefault(o => o.OrderID == OrderDetails.OrderID);
                // If the order is completed, decrement used items from the inventory
                if (OrderDetails.Status == "Completed" && oldStatus.Status != "Completed")
                {
                    // Retrieve all items associated with the completed order and marked as selected
                    var usedItems = context.SelectableItem
                                           .Where(s => s.OrderID == OrderDetails.OrderID && s.IsSelected)
                                           .ToList();

                    foreach (var usedItem in usedItems)
                    {
                        // Find the corresponding item in the Inventory
                        var inventoryItem = context.Item.FirstOrDefault(i => i.ItemID == usedItem.ItemID);

                        if (inventoryItem != null)
                        {
                            if (inventoryItem.Stock >= 1)
                            {
                                inventoryItem.Stock -= 1;
                            }
                            else
                            {
                                context.SelectableItem.Remove(usedItem);
                                MessageBox.Show($"Insufficient stock for item: {inventoryItem.Name}",
                                                "Stock Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                        else
                        {
                            context.SelectableItem.Remove(usedItem);
                            MessageBox.Show($"Item not found in inventory: ID {usedItem.ItemID}",
                                            "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }

                context.SaveChanges();

                MessageBox.Show("Changes saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }


            using (var context = new BenjaminDbContext())
            {
                var employeeAccount = context.Accounts.FirstOrDefault(a => a.Name == OrderDetails.Employee);

                OrderDetails.AccountID = employeeAccount.AccountID;

                context.Orders.Update(OrderDetails);
                context.SaveChanges();
            }

            ETAValue();
            CalculateTotal();
            OrderReciever(OrderDetails.OrderID);
        }


        private void ExportToPDF()
        {
            if (OrderDetails == null)
            {
                MessageBox.Show("No order selected to export!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // PDF file path wher we can find the exported documents
            string filePath = $"..\\..\\..\\Exports\\Receipt_Order_{OrderDetails.OrderID}.pdf";

            try
            {
                using (var writer = new iText.Kernel.Pdf.PdfWriter(filePath))
                using (var pdf = new iText.Kernel.Pdf.PdfDocument(writer))
                {
                    var document = new iText.Layout.Document(pdf);

                    // THIS IS THE TITLE OR HEADER OR WHATEVER
                    document.Add(new iText.Layout.Element.Paragraph("Benjafix").SetFontSize(24)
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                    document.Add(new iText.Layout.Element.Paragraph($"Receipt - Order ID: {OrderDetails.OrderID}")
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                        .SetFontSize(18).SetUnderline());

                    // THESE ARE THE DETAILS OF THE ORDER AND WHATEVER
                    document.Add(new iText.Layout.Element.Paragraph("\nCustomer Information")
                        .SetFontSize(16));
                    document.Add(new iText.Layout.Element.Paragraph($"Name: {OrderDetails.CustName}"));
                    document.Add(new iText.Layout.Element.Paragraph($"Phone Number: {OrderDetails.CustPhoneNum}"));
                    document.Add(new iText.Layout.Element.Paragraph($"Email: {OrderDetails.CustEmail}"));
              
                    document.Add(new iText.Layout.Element.Paragraph("\nOrder Details").SetFontSize(16));
                    document.Add(new iText.Layout.Element.Paragraph($"Device: {OrderDetails.DeviceName}"));
                    document.Add(new iText.Layout.Element.Paragraph($"Model: {OrderDetails.DeviceType}"));
                    document.Add(new iText.Layout.Element.Paragraph($"Employee Assigned: {OrderDetails.Employee}"));
                    document.Add(new iText.Layout.Element.Paragraph($"Other Notes: {OrderDetails.OtherNotes}"));

                    // TABLE WHERE THE STUFF WE USED AND DID ARE AT
                    var compServiceTable = new iText.Layout.Element.Table(3).UseAllAvailableWidth();
                    compServiceTable.AddHeaderCell("Service/Component");
                    compServiceTable.AddHeaderCell("Price");
                    compServiceTable.AddHeaderCell("Type");

                    foreach (var service in ServicesCheckbox.Where(s => s.IsSelected))
                    {
                        compServiceTable.AddCell(service.Name);
                        compServiceTable.AddCell($"₱{service.Price:F2}");
                        compServiceTable.AddCell("Service");
                    }

                    foreach (var item in ItemsCheckbox.Where(i => i.IsSelected))
                    {
                        compServiceTable.AddCell(item.Item.Name);
                        compServiceTable.AddCell($"₱{item.Item.Price:F2}");
                        compServiceTable.AddCell("Component");
                    }

                    document.Add(compServiceTable);

                    // THE PRICING AND WHATEVER THE FOOK
                    var pricingTable = new iText.Layout.Element.Table(2).UseAllAvailableWidth();
                    pricingTable.AddHeaderCell("Description");
                    pricingTable.AddHeaderCell("Amount");

                    pricingTable.AddCell("Total Price of Components");
                    pricingTable.AddCell($"₱{CompTotal:F2}");

                    pricingTable.AddCell("Total Price of Services");
                    pricingTable.AddCell($"₱{ServTotal:F2}");

                    pricingTable.AddCell("Subtotal");
                    pricingTable.AddCell($"₱{SubTotal:F2}");

                    pricingTable.AddCell("12% VAT");
                    pricingTable.AddCell($"₱{Tax:F2}");

                    if (DiscountCheckbox)
                    {
                        pricingTable.AddCell("20% Discount");
                        pricingTable.AddCell($"₱{Discount:F2}");
                    }
                    else
                    {
                        pricingTable.AddCell("20% Discount");
                        pricingTable.AddCell("₱0.00");
                    }

                    pricingTable.AddCell("Total Cost");
                    pricingTable.AddCell($"₱{Total:F2}");

                    document.Add(new iText.Layout.Element.Paragraph("\nPricing Details").SetFontSize(16));
                    document.Add(pricingTable);

                    // THANK YOU FOR USING OUR SERVICE LMAO
                    document.Add(new iText.Layout.Element.Paragraph("\nThank you for choosing Benjafix!")
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                        .SetFontSize(14));

                    document.Close();
                }

                MessageBox.Show($"Receipt exported successfully to {filePath}!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to export receipt: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}