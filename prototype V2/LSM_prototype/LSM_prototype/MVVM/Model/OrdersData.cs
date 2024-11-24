using System.Collections.ObjectModel;

namespace LSM_prototype.MVVM.Model
{
    public class OrdersData
    {
        // Singleton instance
        private static OrdersData _instance;
        public static OrdersData Instance => _instance ??= new OrdersData();

        // Shared collection of accounts
        public ObservableCollection<Orders> OrdersList { get; }

        // Private constructor to enforce singleton pattern
        private OrdersData()
        {
            OrdersList = new ObservableCollection<Orders>();

            //temporary, can delete
            OrdersList.Add(new Orders { OrderID = "a123456789", Item = "Laptop", ETA = "5 business days", Status = "Ongoing", Technician = "God Hand",
                                       Problem = "screen replacement", OtherNotes = null});

            OrdersList.Add(new Orders { OrderID = "b123456789", Item = "Laptop", ETA = "5 business days", Status = "Ongoing", Technician = "God Hand",
                                       Problem = "screen replacement", OtherNotes = null});

            OrdersList.Add(new Orders { OrderID = "c123456789", Item = "Laptop", ETA = "5 business days", Status = "Ongoing", Technician = "God Hand",
                                       Problem = "screen replacement", OtherNotes = null});

            OrdersList.Add(new Orders { OrderID = "d123456789", Item = "Laptop", ETA = "5 business days", Status = "Ongoing", Technician = "God Hand",
                                       Problem = "screen replacement", OtherNotes = null});

            OrdersList.Add(new Orders { OrderID = "e123456789", Item = "Laptop", ETA = "5 business days", Status = "Ongoing", Technician = "God Hand",
                                       Problem = "screen replacement", OtherNotes = null});
        }
    }
}