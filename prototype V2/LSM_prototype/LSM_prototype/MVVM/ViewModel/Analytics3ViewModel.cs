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
    internal class Analytics3ViewModel : ViewModelBase
    {
        public RelayCommand OpenOrderCommand => new RelayCommand(execute => OpenOrder());
        public ObservableCollection<string> ServicesOptions { get; set; } = new ObservableCollection<string>
        {
            "Repair",
            "Cleaning",
            "Check-up",
            "Installation",
            "Maintenance"
        };
        public ObservableCollection<KeyValuePair<string, int>> ServiceTypeCountCollection { get; } = new ObservableCollection<KeyValuePair<string, int>>();

        // Series collection for the chart
        public SeriesCollection SeriesCollection { get; set; }
        public List<string> Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public Analytics3ViewModel()
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

            ServiceTypeCountCollection.Clear();

            using (var context = new BenjaminDbContext())
            {
                // Group orders by status and count them
                var groupedList = context.ServiceOptions
                    ?.GroupBy(o => o.Name)
                    .ToDictionary(g => g.Key, g => g.Count());

                // Add grouped items to the observable collection
                foreach (var item in groupedList)
                {
                    ServiceTypeCountCollection.Add(item);
                }
            }

            // Ensure that every device option is in DeviceTypeCountCollection
            foreach (var deviceOption in ServicesOptions)
            {
                // Check if the device type is not already in the collection
                if (!ServiceTypeCountCollection.Any(d => d.Key == deviceOption))
                {
                    // If not found, add it with a value of 0
                    ServiceTypeCountCollection.Add(new KeyValuePair<string, int>(deviceOption, 0));
                }
            }

            foreach (var deviceType in ServiceTypeCountCollection)
            {
                SeriesCollection = new SeriesCollection
                {
                    // Column series for device types
                    new ColumnSeries
                    {
                        Title = deviceType.Key,
                        Values = new ChartValues<int>(
                            ServiceTypeCountCollection.Select(d => d.Value)
                        )
                    }
                };
            }

            // Generate labels based on device types
            Labels = ServiceTypeCountCollection.Select(d => d.Key).ToList();
        }
    }
}
