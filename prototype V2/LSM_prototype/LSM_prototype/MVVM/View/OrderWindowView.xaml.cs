using LSM_prototype.MVVM.Model;
using LSM_prototype.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LSM_prototype.MVVM.View
{
    public partial class OrderWindowView : Window
    {
        private readonly OrderWindowViewModel orderVM;

        public OrderWindowView(int orderID)
        {
            InitializeComponent();

            // Create the ViewModel instance and pass the OrderID
            orderVM = new OrderWindowViewModel(orderID);

            // Set the DataContext of the window to the ViewModel
            this.DataContext = orderVM;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Button_Minimize(object sender, RoutedEventArgs e)
        {
            var orderWindow = Application.Current.Windows.OfType<OrderWindowView>().FirstOrDefault();

            orderWindow.WindowState = WindowState.Minimized;
        }

        private void Button_Maximize(object sender, RoutedEventArgs e)
        {
            var orderWindow = Application.Current.Windows.OfType<OrderWindowView>().FirstOrDefault();

            if (orderWindow.WindowState != WindowState.Maximized)
            {
                orderWindow.WindowState = WindowState.Maximized;
                windowBorder.BorderThickness = new Thickness(0);
                windowBorder.CornerRadius = new CornerRadius(0);
            }
            else
            {
                orderWindow.WindowState = WindowState.Normal;
                windowBorder.BorderThickness = new Thickness(10, 0, 10, 10);
                windowBorder.CornerRadius = new CornerRadius(10);
            }
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            var orderWindow = Application.Current.Windows.OfType<OrderWindowView>().FirstOrDefault();

            orderWindow.Close();
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            // Cast sender to CheckBox
            var checkBox = sender as CheckBox;

            if (checkBox != null)
            {
                orderVM.ETAValue();
                orderVM.CalculateTotal();
                orderVM.DiscountCheckbox = true;
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            // Cast sender to CheckBox
            var checkBox = sender as CheckBox;

            if (checkBox != null)
            {
                orderVM.ETAValue();
                orderVM.CalculateTotal();
                orderVM.DiscountCheckbox = false;
            }
        }
    }
}
