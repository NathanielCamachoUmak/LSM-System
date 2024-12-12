using LSM_prototype.Core;
using LSM_prototype.MVVM.Model;
using LSM_prototype.MVVM.View;
using System.Collections.ObjectModel;

namespace LSM_prototype.MVVM.ViewModel
{
    internal class CancelledOrdersViewModel : ViewModelBase
    {
        public RelayCommand OpenOrderCommand => new RelayCommand(execute => OpenOrder());

        public ObservableCollection<Orders> CancelledOrders { get;  } = new ObservableCollection<Orders>();

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
            using (var context = new BenjaminDbContext())
            {
                var ordersFromDb = context.Orders?.Where(s => s.Status == "Cancelled").ToList() ?? new List<Orders>();
                foreach (var order in ordersFromDb)
                {
                    CancelledOrders.Add(order);
                }
            }
        }
    }
}

