using LSM_prototype.Core;
using LSM_prototype.MVVM.Model;
using LSM_prototype.MVVM.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;



//MANAGE ORDERS AND ONGOING ORDERS SHOULD USE THE SAME TABLE AND SHOULD REFLECT ON CHANGES MADE ON EACH OTHER -Andre, nov 18 2024



namespace LSM_prototype.MVVM.ViewModel
{
    class OngoingOrdersViewModel : ViewModelBase
    {
        public RelayCommand AddCommand => new RelayCommand(execute => AddItem());
        public RelayCommand DeleteCommand => new RelayCommand(execute => DeleteItem(), canExecute => SelectedItem != null);
        public RelayCommand SaveCommand => new RelayCommand(execute => Save(), canExecute => CanSave());
        public ObservableCollection<Orders> SharedOrders { get; }

        public OngoingOrdersViewModel()
        {
            // Access the shared collection
            SharedOrders = OrdersData.Instance.OrdersList;
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

        //can we check if item is already in database? using name as primary key
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
    }
}
