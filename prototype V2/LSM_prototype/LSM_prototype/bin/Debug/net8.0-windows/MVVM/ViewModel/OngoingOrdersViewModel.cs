using LSM_prototype.Core;
using LSM_prototype.MVVM.Model;
using LSM_prototype.MVVM.View;
using System.Collections.ObjectModel;
using System.Windows;

namespace LSM_prototype.MVVM.ViewModel
{
    class OngoingOrdersViewModel : ViewModelBase
    {
        public RelayCommand OpenOrderCommand => new RelayCommand(execute => OpenOrder());
        public ObservableCollection<Accounts> User { get; } = CurrentUser.Instance.User;

        public ObservableCollection<Orders> _ongoingOrders = new ObservableCollection<Orders>();
        public ObservableCollection<Orders> OngoingOrders
        {
            get => _ongoingOrders;
            set
            {
                if (_ongoingOrders != value)
                {
                    _ongoingOrders = value;
                    OnPropertyChanged(nameof(OngoingOrders));
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

        public OngoingOrdersViewModel()
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
            OngoingOrders.Clear();
            if (User[0].AccessLevel == "Admin")
            {
                using (var context = new BenjaminDbContext())
                {
                    var ordersFromDb = context.Orders?.Where(s => s.Status == "Ongoing").ToList() ?? new List<Orders>();
                    foreach (var order in ordersFromDb)
                    {
                        OngoingOrders.Add(order);
                    }
                }
            }
            else
            {
                using (var context = new BenjaminDbContext())
                {
                    var ordersFromDb = context.Orders?.Where(s => s.AccountID == User[0].AccountID && s.Status == "Ongoing").ToList() ?? new List<Orders>();
                    foreach (var order in ordersFromDb)
                    {
                        OngoingOrders.Add(order);
                    }
                }
            }
        }
    }
}
