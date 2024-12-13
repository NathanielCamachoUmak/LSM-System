using LSM_prototype.Core;
using LSM_prototype.MVVM.Model;
using LSM_prototype.MVVM.View;
using System.Collections.ObjectModel;
using System.Windows;

namespace LSM_prototype.MVVM.ViewModel
{
    internal class CancelledOrdersViewModel : ViewModelBase
    {
        public RelayCommand OpenOrderCommand => new RelayCommand(execute => OpenOrder());
        public ObservableCollection<Accounts> User { get; } = CurrentUser.Instance.User;

        public ObservableCollection<Orders> _cancelledOrders = new ObservableCollection<Orders>();
        public ObservableCollection<Orders> CancelledOrders
        {
            get => _cancelledOrders;
            set
            {
                if (_cancelledOrders != value)
                {
                    _cancelledOrders = value;
                    OnPropertyChanged(nameof(CancelledOrders));
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

        public CancelledOrdersViewModel()
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
            CancelledOrders.Clear();
            if (User[0].AccessLevel == "Admin")
            {
                using (var context = new BenjaminDbContext())
                {
                    var ordersFromDb = context.Orders?.Where(s => s.Status == "Cancelled").ToList() ?? new List<Orders>();
                    foreach (var order in ordersFromDb)
                    {
                        CancelledOrders.Add(order);
                    }
                }
            }
            else
            {
                using (var context = new BenjaminDbContext())
                {
                    var ordersFromDb = context.Orders?.Where(s => s.AccountID == User[0].AccountID && s.Status == "Cancelled").ToList() ?? new List<Orders>();
                    foreach (var order in ordersFromDb)
                    {
                        CancelledOrders.Add(order);
                    }
                }
            }
        }
    }
}
