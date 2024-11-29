using LSM_prototype.Core;
using LSM_prototype.MVVM.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace LSM_prototype.MVVM.ViewModel
{
    class InventoryViewModel : ViewModelBase
    {
        public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();
        public RelayCommand AddCommand => new RelayCommand(_ => AddItem(), _ => IsValidInput());
        public RelayCommand DeleteCommand => new RelayCommand(_ => DeleteItem(), _ => SelectedItem != null);
        public RelayCommand SaveCommand => new RelayCommand(_ => Save(), _ => Items.All(IsValidItem));

        private string? _newItemName = string.Empty;
        private decimal _newItemPrice;
        private int _newItemStock;

        public string? NewItemName
        {
            get => _newItemName;
            set { _newItemName = value; OnPropertyChanged(); }
        }

        public decimal NewItemPrice
        {
            get => _newItemPrice;
            set { _newItemPrice = value; OnPropertyChanged(); }
        }

        public int NewItemStock
        {
            get => _newItemStock;
            set { _newItemStock = value; OnPropertyChanged(); }
        }

        private Item? _selectedItem;

        public Item? SelectedItem
        {
            get => _selectedItem;
            set { _selectedItem = value; OnPropertyChanged(); }
        }

        public InventoryViewModel()
        {
            LoadItemsFromDatabase();
        }

        private void LoadItemsFromDatabase()
        {
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
            if (!IsValidInput()) return;

            Items.Add(new Item
            {
                Name = NewItemName,
                Price = NewItemPrice,
                Stock = NewItemStock
            });

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

        private bool IsValidInput()
        {
            return !string.IsNullOrWhiteSpace(NewItemName) && NewItemPrice >= 0 && NewItemStock >= 0;
        }

        private bool IsValidItem(Item item)
        {
            return !string.IsNullOrWhiteSpace(item.Name) && item.Price >= 0 && item.Stock >= 0;
        }

        private void ResetNewItemFields()
        {
            NewItemName = string.Empty;
            NewItemPrice = 0.00m;
            NewItemStock = 0;
        }
    }
}


