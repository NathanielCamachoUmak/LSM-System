using System.Collections.ObjectModel;

namespace LSM_prototype.MVVM.Model
{
    public class OrdersData
    {
        // Singleton instance
        private static OrdersData _instance;
        public static OrdersData Instance => _instance ??= new OrdersData();

        // Shared collection of accounts
        public ObservableCollection<Orders> SharedOrders { get; } = new ObservableCollection<Orders>();

        // Private constructor to enforce singleton pattern
        private OrdersData()
        {

            using (var context = new BenjaminDbContext())
            {
                var ordersFromDb = context.Orders?.ToList() ?? new List<Orders>();
                foreach (var orders in ordersFromDb)
                {
                    SharedOrders.Add(orders);
                }
            }

        }

        public void SaveChangesToDatabase()
        {
            using (var context = new BenjaminDbContext())
            {
                context.Orders.RemoveRange(context.Orders); // Clear current database entries
                context.Orders.AddRange(SharedOrders);     // Add updated accounts
                context.SaveChanges();                         // Commit changes
            }
        }
    }
}