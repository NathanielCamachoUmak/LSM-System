using LSM_prototype.Core;
using LSM_prototype.MVVM.Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace LSM_prototype.MVVM.ViewModel
{
    class InventoryViewModel : ViewModelBase
    {
        public static ObservableCollection<Item> Items { get; set; }

        public RelayCommand AddCommand => new RelayCommand(execute => AddItem());
        public RelayCommand DeleteCommand => new RelayCommand(execute => DeleteItem(), canExecute => SelectedItem != null);
        public RelayCommand SaveCommand => new RelayCommand(execute => Save(), canExecute => CanSave());
        public InventoryViewModel()
        {
            Items = new ObservableCollection<Item>();

            //temporary, can delete
            Items.Add(new Item { Name = "item 1", Price = 100.00m, Stock = 10 });
            Items.Add(new Item { Name = "item 2", Price = 200.00m, Stock = 9 });
            Items.Add(new Item { Name = "item 3", Price = 300.00m, Stock = 8 });
            Items.Add(new Item { Name = "item 4", Price = 400.00m, Stock = 7 });
            Items.Add(new Item { Name = "item 5", Price = 500.00m, Stock = 6 });
            
        }

        private Item _selectedItem;

        public Item SelectedItem
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
            Items.Add(new Item
            {
                Name = "NEW ITEM",
                Price = 0.00m,
                Stock = 0
            });
        }

        private void DeleteItem()
        {
            var result = MessageBox.Show($"Are you sure you want to delete {SelectedItem.Name}?",
                                         $"ITEM DELETION CONFIRMATION",
                                         MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                Items.Remove(SelectedItem);
            }
        }

        //save to database using this
        private void Save()
        {
        }

        //add a check to see if database is up and items can e saved
        private bool CanSave()
        {
            //if ok, return true
            return true;
        }
    }
}

