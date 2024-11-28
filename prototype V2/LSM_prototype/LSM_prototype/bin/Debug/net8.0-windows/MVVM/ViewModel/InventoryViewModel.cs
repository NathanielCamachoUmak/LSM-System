using LSM_prototype.Core;
using LSM_prototype.MVVM.Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace LSM_prototype.MVVM.ViewModel
{
    class InventoryViewModel : ViewModelBase
    {
        public static ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();

        public RelayCommand AddCommand => new RelayCommand(execute => AddItem());
        public RelayCommand DeleteCommand => new RelayCommand(execute => DeleteItem(), canExecute => SelectedItem != null);
        public RelayCommand SaveCommand => new RelayCommand(execute => Save(), canExecute => CanSave());

        // Holds the value of inputted items
        private string? _newItemName = string.Empty;
        public string? NewItemName
        {
            get => _newItemName;
            set
            {
                _newItemName = value;
                OnPropertyChanged();
            }
        }

        private decimal _newItemPrice;
        public decimal NewItemPrice
        {
            get => _newItemPrice;
            set
            {
                _newItemPrice = value;
                OnPropertyChanged();
            }
        }

        private int _newItemStock;
        public int NewItemStock
        {
            get => _newItemStock;
            set
            {
                _newItemStock = value;
                OnPropertyChanged();
            }
        }

        public InventoryViewModel()
        {
            Items = new ObservableCollection<Item>();

            // Fetch data from the database
            using (var context = new BenjaminDbContext())
            {
                var itemsFromDb = context.Item?.ToList() ?? new List<Item>();
                foreach (var item in itemsFromDb)
                {
                    Items.Add(item);
                }
            }

            //temporary, can delete
            //Items.Add(new Item { Name = "item 1", Price = 100.00m, Stock = 10 });
            //Items.Add(new Item { Name = "item 2", Price = 200.00m, Stock = 9 });
            //Items.Add(new Item { Name = "item 3", Price = 300.00m, Stock = 8 });
            //Items.Add(new Item { Name = "item 4", Price = 400.00m, Stock = 7 });
            //Items.Add(new Item { Name = "item 5", Price = 500.00m, Stock = 6 });

        }

        private Item? _selectedItem;

        public Item? SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        //can we check if item is already in database? using name as primary key
        private void AddItem()
        {
            // Validate input fields before adding the item
            if (string.IsNullOrWhiteSpace(NewItemName) || NewItemPrice < 0 || NewItemStock < 0)
            {
                MessageBox.Show("Please enter valid values for the new item.");
                return;
            }

            var newItem = new Item
            {
                Name = NewItemName,
                Price = NewItemPrice,
                Stock = NewItemStock
            };

            Items.Add(newItem);

            // Reset the input fields for the next item
            NewItemName = string.Empty;
            NewItemPrice = 0.00m;
            NewItemStock = 0;
        }

        private void DeleteItem()
        {
            var result = MessageBox.Show($"Are you sure you want to delete {SelectedItem.Name}?",
                                 "ITEM DELETION CONFIRMATION",
                                 MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                if (SelectedItem.ItemID == 0)
                {
                    // Item is not saved to the database yet, just remove it from the collection
                    Items.Remove(SelectedItem);
                }
                else
                {
                    // Item exists in the database, delete it
                    using (var context = new BenjaminDbContext())
                    {
                        context.Item.Remove(SelectedItem);
                        context.SaveChanges();
                    }

                    Items.Remove(SelectedItem);
                }
            }
        }

        //save to database using this
        private void Save()
        {
            using (var context = new BenjaminDbContext())
            {
                foreach (var item in Items)
                {
                    if (item.ItemID == 0)
                    {
                        // New item
                        context.Item.Add(item);
                    }
                    else
                    {
                        // Existing item
                        context.Item.Update(item);
                    }
                }

                context.SaveChanges();
            }

            MessageBox.Show("Changes saved successfully!");
        }

        //add a check to see if database is up and items can e saved
        private bool CanSave()
        {
            // Ensure that all items have valid data
            return Items.All(item =>
                !string.IsNullOrWhiteSpace(item.Name) && // Name is not null or empty
                item.Price >= 0 && // Price is not negative
                item.Stock >= 0    // Stock is not negative
            );  
        }
    }
}

