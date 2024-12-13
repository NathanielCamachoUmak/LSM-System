using LSM_prototype.Core;
using LSM_prototype.MVVM.Model;
using LSM_prototype.MVVM.View;
using System.Collections.ObjectModel;

namespace LSM_prototype.MVVM.ViewModel
{
    internal class CompletedOrdersViewModel : ViewModelBase
    {
        public RelayCommand OpenOrderCommand => new RelayCommand(execute => OpenOrder());
        public ObservableCollection<Accounts> User { get; } = CurrentUser.Instance.User;

        public ObservableCollection<Orders> _completedOrders = new ObservableCollection<Orders>();
        public ObservableCollection<Orders> CompletedOrders
        {
            get => _completedOrders;
            set
            {
                if (_completedOrders != value)
                {
                    _completedOrders = value;
                    OnPropertyChanged(nameof(CompletedOrders));
                }
            }
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

        public CompletedOrdersViewModel()
        {
            LoadOrdersFromDatabase();
        }

        private void OpenOrder()
        {
            OrderWindowView orderWindow = new OrderWindowView(SelectedItem.OrderID);
            orderWindow.Show();
        }

        public void LoadOrdersFromDatabase()
        {
            CompletedOrders.Clear();
            if (User[0].AccessLevel == "Admin")
            {
                using (var context = new BenjaminDbContext())
                {
                    var ordersFromDb = context.Orders?.Where(s => s.Status == "Completed").ToList() ?? new List<Orders>();
                    foreach (var order in ordersFromDb)
                    {
                        CompletedOrders.Add(order);
                    }
                }
            }
            else
            {
                using (var context = new BenjaminDbContext())
                {
                    var ordersFromDb = context.Orders?.Where(s => s.AccountID == User[0].AccountID && s.Status == "Completed").ToList() ?? new List<Orders>();
                    foreach (var order in ordersFromDb)
                    {
                        CompletedOrders.Add(order);
                    }
                }
            }
        }
    }
}