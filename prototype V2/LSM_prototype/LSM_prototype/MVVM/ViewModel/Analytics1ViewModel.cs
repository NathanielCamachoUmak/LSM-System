using LiveCharts;
using LiveCharts.Wpf;
using LSM_prototype.Core;
using LSM_prototype.MVVM.Model;
using LSM_prototype.MVVM.View;
using System.Collections.ObjectModel;
using System.Windows;

namespace LSM_prototype.MVVM.ViewModel
{
    internal class Analytics1ViewModel : ViewModelBase
    {
        public ObservableCollection<Orders> Orders { get; } = new ObservableCollection<Orders>();
        public ObservableCollection<ServiceOptions> SelectedServices { get; } = new ObservableCollection<ServiceOptions>();

        private ObservableCollection<KeyValuePair<string, int>> StatusCountCollection { get; } = new ObservableCollection<KeyValuePair<string, int>>();
        public decimal RevenueCollection { get; set; }

        // Series collection for the chart
        public SeriesCollection PieChart1 { get; set; }
        public SeriesCollection PieChart2 { get; set; }

        public Analytics1ViewModel()
        {
            LoadOrdersFromDatabase();
        }
        
        public void LoadOrdersFromDatabase()
        {
            Orders.Clear();
            StatusCountCollection.Clear();
            decimal serviceRevenue = 0;
            decimal itemRevenue = 0;

            using (var context = new BenjaminDbContext())
            {
                var itemsFromDb = context.Item?.ToList() ?? new List<Item>();

                var ordersFromDb = context.Orders?.ToList() ?? new List<Orders>();
                foreach (var order in ordersFromDb)
                {
                    Orders.Add(order);
                }

                var completedOrders = Orders.Where(o => o.Status == "Completed").ToList();
                foreach (var order in completedOrders)
                {
                    var selectedServices = context.ServiceOptions.Where(s => s.OrderID == order.OrderID).ToList();
                    foreach(var service in selectedServices)
                    {
                        serviceRevenue += service.Price;
                    }

                    var selectedItems = context.SelectableItem.Where(s => s.OrderID == order.OrderID).ToList();
                    foreach (var item in selectedItems)
                    {
                        itemRevenue += item.Item.Price;
                    }
                }

                RevenueCollection = serviceRevenue + itemRevenue;

                // Group orders by status and count them
                var groupedList = context.Orders
                    ?.GroupBy(o => o.Status)
                    .ToDictionary(g => g.Key, g => g.Count());

                // Add grouped items to the observable collection
                foreach (var item in groupedList)
                {
                    StatusCountCollection.Add(item);
                }
            }


            // Create the series collection for the pie chart
            PieChart1 = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Completed",
                    Values = new ChartValues<int>
                    {
                        StatusCountCollection.FirstOrDefault(s => s.Key == "Completed").Value
                    },
                    DataLabels = true,
                    FontSize = 25,
                    FontWeight = FontWeights.Black
                },
                new PieSeries
                {
                    Title = "Ongoing",
                    Values = new ChartValues<int>
                    {
                        StatusCountCollection.FirstOrDefault(s => s.Key == "Ongoing").Value
                    },
                    DataLabels = true,
                    FontSize = 25,
                    FontWeight = FontWeights.Black
                },
                new PieSeries
                {
                    Title = "Cancelled",
                    Values = new ChartValues<int>
                    {
                        StatusCountCollection.FirstOrDefault(s => s.Key == "Cancelled").Value
                    },
                    DataLabels = true,
                    FontSize = 25,
                    FontWeight = FontWeights.Black
                }
            };

            PieChart2 = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Service Revenue",
                    Values = new ChartValues<decimal>{serviceRevenue},
                    DataLabels = true,
                    FontSize = 25,
                    FontWeight = FontWeights.Black
                },
                new PieSeries
                {
                    Title = "Item Revenue",
                    Values = new ChartValues<decimal>{itemRevenue},
                    DataLabels = true,
                    FontSize = 25,
                    FontWeight = FontWeights.Black
                }
            };
        }


    }
}