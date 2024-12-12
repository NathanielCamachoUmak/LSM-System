﻿using LiveCharts;
using LiveCharts.Wpf;
using LSM_prototype.Core;
using LSM_prototype.MVVM.Model;
using LSM_prototype.MVVM.View;
using System.Collections.ObjectModel;

namespace LSM_prototype.MVVM.ViewModel
{
    internal class Analytics2ViewModel : ViewModelBase
    {
        public RelayCommand OpenOrderCommand => new RelayCommand(execute => OpenOrder());
        public ObservableCollection<string> DeviceOptions { get; set; } = new ObservableCollection<string>
        {
            "Laptop",
            "Desktop Computer",
            "Smartphone",
            "Tablet",
            "Game Console",
            "Others"
        };
        public ObservableCollection<KeyValuePair<string, int>> DeviceTypeCountCollection { get; } = new ObservableCollection<KeyValuePair<string, int>>();

        // Series collection for the chart
        public SeriesCollection SeriesCollection { get; set; }
        public List<string> Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public Analytics2ViewModel()
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

            DeviceTypeCountCollection.Clear();

            using (var context = new BenjaminDbContext())
            {
                // Group orders by status and count them
                var groupedList = context.Orders
                    ?.GroupBy(o => o.DeviceType)
                    .ToDictionary(g => g.Key, g => g.Count());

                // Add grouped items to the observable collection
                foreach (var item in groupedList)
                {
                    DeviceTypeCountCollection.Add(item);
                }
            }

            // Ensure that every device option is in DeviceTypeCountCollection
            foreach (var deviceOption in DeviceOptions)
            {
                // Check if the device type is not already in the collection
                if (!DeviceTypeCountCollection.Any(d => d.Key == deviceOption))
                {
                    // If not found, add it with a value of 0
                    DeviceTypeCountCollection.Add(new KeyValuePair<string, int>(deviceOption, 0));
                }
            }

            foreach (var deviceType in DeviceTypeCountCollection)
            {
                SeriesCollection = new SeriesCollection
                {
                    // Column series for device types
                    new ColumnSeries
                    {
                        Title = deviceType.Key,
                        Values = new ChartValues<int>(
                            DeviceTypeCountCollection.Select(d => d.Value)
                        )
                    }
                };
            }

            // Generate labels based on device types
            Labels = DeviceTypeCountCollection.Select(d => d.Key).ToList();
        }
    }
}