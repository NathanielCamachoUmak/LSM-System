using LSM_prototype.Core;
using LSM_prototype.MVVM.Model;
using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;
using System.Security.Principal;
using System.Windows;
using System.Windows.Controls;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace LSM_prototype.MVVM.ViewModel
{
    class ManageOrdersViewModel : ViewModelBase
    {
        public RelayCommand AddCommand => new RelayCommand(execute => AddItem());
        public RelayCommand SaveCommand => new RelayCommand(execute => Save(), canExecute => CanSave());
        public RelayCommand ExportCommand => new RelayCommand(execute => ExportToPDF());
        public ObservableCollection<Orders> SharedOrders { get; } = new ObservableCollection<Orders>();
        public ObservableCollection<Accounts> SharedAccounts { get; } = new ObservableCollection<Accounts>();
        public ObservableCollection<Item> Items { get; } = new ObservableCollection<Item>();

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


        public ObservableCollection<Dictionary<string, string>> ServiceOptions { get; set; } = new ObservableCollection<Dictionary<string, string>>
        {
            new Dictionary<string, string> { { "Repair", "1-2 days" } },
            new Dictionary<string, string> { { "Cleaning", "1-2 days" } },
            new Dictionary<string, string> { { "Check-up", "1 hour" } },
            new Dictionary<string, string> { { "Installation", "2-3 hours" } },
            new Dictionary<string, string> { { "Maintenance", "2 hours" } }
        };

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

        public void LoadItemsFromDatabase()
        {
            //Items.Clear();
            using (var context = new BenjaminDbContext())
            {
                var itemsFromDb = context.Item?.ToList() ?? new List<Item>();
                foreach (var item in itemsFromDb)
                {
                    Items.Add(item);
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

