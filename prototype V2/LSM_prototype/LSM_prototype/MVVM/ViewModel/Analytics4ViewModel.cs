using LiveCharts.Wpf;
using LiveCharts;
using LSM_prototype.Core;
using LSM_prototype.MVVM.Model;
using LSM_prototype.MVVM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSM_prototype.MVVM.ViewModel
{
    internal class Analytics4ViewModel : ViewModelBase
    {
        public RelayCommand OpenOrderCommand => new RelayCommand(execute => OpenOrder());
        public ObservableCollection<string> EmployeeList { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<KeyValuePair<string, int>> EmployeeOrderCountCollection { get; } = new ObservableCollection<KeyValuePair<string, int>>();

        // Series collection for the chart
        public SeriesCollection SeriesCollection { get; set; }
        public List<string> Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public Analytics4ViewModel()
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
            Formatter = value => value.ToString("N0");

            EmployeeOrderCountCollection.Clear();
            EmployeeList.Clear();

            using (var context = new BenjaminDbContext())
            {
                var accountsFromDb = context.Accounts?.Where(a => a.AccessLevel != "Admin").ToList() ?? new List<Accounts>();
                foreach (var account in accountsFromDb)
                {
                    EmployeeList.Add(account.Name);
                }

                // Group orders by status and count them
                var groupedList = context.Orders
                    ?.GroupBy(o => o.Employee)
                    .ToDictionary(g => g.Key, g => g.Count());

                // Add grouped items to the observable collection
                foreach (var item in groupedList)
                {
                    EmployeeOrderCountCollection.Add(item);
                }
            }

            // Ensure that every device option is in DeviceTypeCountCollection
            foreach (var deviceOption in EmployeeList)
            {
                // Check if the device type is not already in the collection
                if (!EmployeeOrderCountCollection.Any(d => d.Key == deviceOption))
                {
                    // If not found, add it with a value of 0
                    EmployeeOrderCountCollection.Add(new KeyValuePair<string, int>(deviceOption, 0));
                }
            }

            foreach (var deviceType in EmployeeOrderCountCollection)
            {
                SeriesCollection = new SeriesCollection
                {
                    // Column series for device types
                    new ColumnSeries
                    {
                        Title = deviceType.Key,
                        Values = new ChartValues<int>(
                            EmployeeOrderCountCollection.Select(d => d.Value)
                        )
                    }
                };
            }

            // Generate labels based on device types
            Labels = EmployeeOrderCountCollection.Select(d => d.Key).ToList();
        }
    }
}
