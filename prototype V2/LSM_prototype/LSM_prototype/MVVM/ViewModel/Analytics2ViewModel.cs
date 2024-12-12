using LiveCharts;
using LiveCharts.Wpf;
using LSM_prototype.Core;
using LSM_prototype.MVVM.Model;
using LSM_prototype.MVVM.View;
using System.Collections.ObjectModel;
using System.Windows;

namespace LSM_prototype.MVVM.ViewModel
{
    internal class Analytics2ViewModel : ViewModelBase
    {
        public RelayCommand OpenOrderCommand => new RelayCommand(execute => OpenOrder());
        public RelayCommand ExportCommand => new RelayCommand(execute => ExportToPDF());

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
        private void ExportToPDF()
        {
            // PDF file path where the document will be saved
            string filePath = $"..\\..\\..\\Exports\\Analytics_Device_Type.pdf";

            try
            {
                using (var writer = new iText.Kernel.Pdf.PdfWriter(filePath))
                using (var pdf = new iText.Kernel.Pdf.PdfDocument(writer))
                {
                    var document = new iText.Layout.Document(pdf);

                    document.Add(new iText.Layout.Element.Paragraph("Analytics Report")
                        .SetFontSize(24)
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                    document.Add(new iText.Layout.Element.Paragraph("Device Type Usage Analytics")
                        .SetFontSize(18)
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                        .SetUnderline());

                    document.Add(new iText.Layout.Element.Paragraph("\nDevice Type Usage")
                        .SetFontSize(16));

                    var deviceTable = new iText.Layout.Element.Table(2).UseAllAvailableWidth();
                    deviceTable.AddHeaderCell("Device Type");
                    deviceTable.AddHeaderCell("Count");

                    int totalDevices = 0;

                    foreach (var deviceType in DeviceTypeCountCollection)
                    {
                        deviceTable.AddCell(deviceType.Key);
                        deviceTable.AddCell(deviceType.Value.ToString());
                        totalDevices += deviceType.Value;
                    }

                    deviceTable.AddCell("Total Devices");
                    deviceTable.AddCell(totalDevices.ToString());

                    document.Add(deviceTable);

                    document.Close();
                }

                MessageBox.Show($"Analytics exported successfully to {filePath}!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to export analytics: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
