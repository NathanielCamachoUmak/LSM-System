using LSM_prototype.Core;
using LSM_prototype.MVVM.Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace LSM_prototype.MVVM.ViewModel
{
    class ManageOrdersViewModel: ViewModelBase
    {
        public RelayCommand AddCommand => new RelayCommand(execute => AddItem());
        public RelayCommand DeleteCommand => new RelayCommand(execute => DeleteItem(), canExecute => SelectedItem != null);
        public RelayCommand SaveCommand => new RelayCommand(execute => Save(), canExecute => CanSave());
        public ObservableCollection<Orders> SharedOrders { get; }
        public ObservableCollection<Accounts> SharedAccounts { get; }

        public ObservableCollection<Item> inventory { get; }
        public ManageOrdersViewModel()
        {
            inventory = new ObservableCollection<Item>();
            SharedOrders = OrdersData.Instance.OrdersList;
            SharedAccounts = AccountsData.Instance.SharedAccounts;
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

        private void AddItem()
        {
            SharedOrders.Add(new Orders
            {
                OrderID = "123412142412",
                Item = "Laptop",
                ETA = "5 business days",
                Status = "Ongoing",
                Technician = "God Hand",
                Problem = "screen replacement",
                OtherNotes = null
            });
        }

        private void DeleteItem()
        {
            var result = MessageBox.Show($"Are you sure you want to delete {SelectedItem.OrderID}?",
                                         $"ITEM DELETION CONFIRMATION",
                                         MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                SharedOrders.Remove(SelectedItem);
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

        public void LoadItemsFromDatabase()
        {
            inventory.Clear();

            using (var context = new BenjaminDbContext())
            {
                var itemsFromDb = context.Item?.ToList() ?? new List<Item>();
                foreach (var item in itemsFromDb)
                {
                    inventory.Add(item);
                }
            }
        }
    }
}

