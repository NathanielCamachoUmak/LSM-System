using LSM_prototype.Core;
using LSM_prototype.MVVM.Model;
using LSM_prototype.MVVM.View;
using System.Collections.ObjectModel;
using System.Windows;

namespace LSM_prototype.MVVM.ViewModel
{
    class ManageOrdersViewModel : ViewModelBase
    {
        public RelayCommand OpenOrderCommand => new RelayCommand(execute => OpenOrder());

        public ObservableCollection<Orders> _sharedOrders = new ObservableCollection<Orders>();
        public ObservableCollection<Orders> SharedOrders
        {
            get => _sharedOrders;
            set
            {
                if (_sharedOrders != value)
                {
                    _sharedOrders = value;
                    OnPropertyChanged(nameof(SharedOrders));
                }
            }
        }
        public ObservableCollection<Accounts> User { get; } = CurrentUser.Instance.User;

        public ManageOrdersViewModel()
        {
            LoadOrdersFromDatabase();
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

        private void OpenOrder()
        {
            OrderWindowView orderWindow = new OrderWindowView(SelectedItem.OrderID);
            orderWindow.Show();
        }

        public void LoadOrdersFromDatabase()
        {
            SharedOrders.Clear();
            if (User[0].AccessLevel == "Admin")
            {
                using (var context = new BenjaminDbContext())
                {
                    var ordersFromDb = context.Orders?.ToList() ?? new List<Orders>();
                    foreach (var order in ordersFromDb)
                    {
                        SharedOrders.Add(order);
                    }
                }
            }
            else
            {
                using (var context = new BenjaminDbContext())
                {
                    var ordersFromDb = context.Orders?.Where(s => s.AccountID == User[0].AccountID).ToList() ?? new List<Orders>();
                    foreach (var order in ordersFromDb)
                    {
                        SharedOrders.Add(order);
                    }
                }
            }
        }
    }
}

