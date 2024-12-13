using iText.Commons.Bouncycastle.Asn1.X509;
using LSM_prototype.Core;
using LSM_prototype.MVVM.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Principal;
using System.Windows;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace LSM_prototype.MVVM.ViewModel
{
    class InventoryViewModel : ViewModelBase
    {
        public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();
        public RelayCommand AddCommand => new RelayCommand(_ => AddItem());
        public RelayCommand DeleteCommand => new RelayCommand(_ => DeleteItem());
        public RelayCommand SaveCommand => new RelayCommand(_ => Save());

        private Item? _selectedItem;

        public Item? SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged();
                }
            }
        }

        private Item _newItem = new Item();
        public Item newItem
        {
            get => _newItem;
            set
            {
                if (_newItem != value)
                {
                    _newItem = value;
                    OnPropertyChanged();
                }
            }
        }

        public InventoryViewModel()
        {
            LoadItemsFromDatabase();
        }

        public void LoadItemsFromDatabase()
        {
            Items.Clear();
            using (var context = new BenjaminDbContext())
            {
                var itemsFromDb = context.Item?.ToList() ?? new List<Item>();
                foreach (var item in itemsFromDb)
                {
                    Items.Add(item);
                }
            }
        }

        private void AddItem()
        {
            if (!IsValidItem(newItem)) return;

            using (var context = new BenjaminDbContext())
            {

                var newItems = new Item
                {
                    Name = newItem.Name,
                    Price = newItem.Price,
                    Stock = newItem.Stock
                };
                context.Item.Add(newItems);
                context.SaveChanges();

                LoadItemsFromDatabase();
            }

            ResetNewItemFields();
            MessageBox.Show("Item added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DeleteItem()
        {
            if (SelectedItem == null) return;

            var result = MessageBox.Show($"Are you sure you want to delete {SelectedItem.Name}?",
                                         "Item Deletion Confirmation",
                                         MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                using (var context = new BenjaminDbContext())
                {
                    if (SelectedItem.ItemID != 0)
                    {
                        var itemToRemove = context.Item.Find(SelectedItem.ItemID);
                        if (itemToRemove != null)
                        {
                            context.Item.Remove(itemToRemove);
                            context.SaveChanges();
                        }
                    }
                }

                Items.Remove(SelectedItem);
                MessageBox.Show("Item deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Save()
        {
            if (!Items.All(IsValidItem)) return;

            using (var context = new BenjaminDbContext())
            {
                foreach (var item in Items)
                {
                    if (item.ItemID == 0)
                        context.Item.Add(item);
                    else
                        context.Item.Update(item);
                }
                context.SaveChanges();
            }

            MessageBox.Show("Changes saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool IsValidItem(Item item)
        {
            // Check if Name is empty or whitespace
            if (string.IsNullOrWhiteSpace(item.Name))
            {
                MessageBox.Show("Name cannot be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Parse PriceInput to decimal and check if it is greater than 0
            if (!decimal.TryParse(item.PriceInput, out decimal price))
            {
                MessageBox.Show("Invalid price input. Please enter a valid number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (price <= 0)
            {
                MessageBox.Show("Price must be greater than 0.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Check if Stock is negative
            if (item.Stock < 0)
            {
                MessageBox.Show("Stock cannot be negative.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // If all checks pass
            return true;
        }

        private void ResetNewItemFields()
        {
            newItem = new Item();
        }
    }
}


