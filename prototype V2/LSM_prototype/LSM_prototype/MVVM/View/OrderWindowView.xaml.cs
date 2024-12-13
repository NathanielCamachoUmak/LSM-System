using LSM_prototype.MVVM.Model;
using LSM_prototype.MVVM.ViewModel;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LSM_prototype.MVVM.View
{
    public partial class OrderWindowView : Window
    {
        private readonly OrderWindowViewModel orderVM;

        public ObservableCollection<Accounts> User { get; } = CurrentUser.Instance.User;
        public ObservableCollection<ServiceOptions> ServicesCheckbox { get; }

        public OrderWindowView(int orderID)
        {
            InitializeComponent();

            // Create the ViewModel instance and pass the OrderID
            orderVM = new OrderWindowViewModel(orderID);

            // Set the DataContext of the window to the ViewModel
            this.DataContext = orderVM;

            if (User[0].AccessLevel == "Admin")
            {
                employeeBox.IsHitTestVisible = true;
            }
            else
            {
                employeeBox.IsHitTestVisible = false;
            }

            UpdateCheckBoxHitTestVisibility();
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
            }
        }

        private void CheckBox_Checked2(object sender, RoutedEventArgs e)
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

        private void CheckBox_Unchecked2(object sender, RoutedEventArgs e)
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

        private void UpdateCheckBoxVisibilityButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateCheckBoxHitTestVisibility();
        }

        private void UpdateCheckBoxHitTestVisibility()
        {
            // enable/diable service checkbox depending on order status
            foreach (var item in orderVM.ServicesCheckbox)
            {
                if (orderVM.OrderDetails.Status != "Ongoing")
                {
                item.IsSelectable = false;
                }
                else
                {
                    item.IsSelectable = true;
                }
            }

            // enable/diable items checkbox depending on order status
            foreach (var item in orderVM.ItemsCheckbox)
            {
                if (orderVM.OrderDetails.Status != "Ongoing")
                {
                    item.IsSelectable = false;
                }
                else
                {
                    item.IsSelectable = true;
                }
            }

            if (IsValidOrder(orderVM.OrderDetails))
            {
                if (orderVM.OrderDetails.Status != "Ongoing")
                {
                    deviceBox.IsHitTestVisible = false;
                    modelBox.IsHitTestVisible = false;
                    employeeBox.IsHitTestVisible = false;
                    statusBox.IsHitTestVisible = false;
                    otherDetailsBox.IsHitTestVisible = false;
                    custNameBox.IsHitTestVisible = false;
                    custPNumBox.IsHitTestVisible = false;
                    custEmailBox.IsHitTestVisible = false;
                    discountBox.IsHitTestVisible = false;
                    save_btn.IsEnabled = false;
                }
                else
                {
                    deviceBox.IsHitTestVisible = true;
                    modelBox.IsHitTestVisible = true;
                    employeeBox.IsHitTestVisible = true;
                    statusBox.IsHitTestVisible = true;
                    otherDetailsBox.IsHitTestVisible = true;
                    custNameBox.IsHitTestVisible = true;
                    custPNumBox.IsHitTestVisible = true;
                    custEmailBox.IsHitTestVisible = true;
                    discountBox.IsHitTestVisible = true;
                    save_btn.IsEnabled = true;
                }
            }
        }
        public bool IsValidOrder(Orders order)
        {
            // Validate device type
            if (string.IsNullOrWhiteSpace(order.DeviceType))
                return false;

            // Validate device name
            if (string.IsNullOrWhiteSpace(order.DeviceName))
                return false;

            // Validate employee assigned
            if (string.IsNullOrWhiteSpace(order.Employee))
                return false;

            // Validate employee assigned
            if (string.IsNullOrWhiteSpace(order.CustName))
                return false;

            // Validate Phone Number
            if (string.IsNullOrWhiteSpace(order.CustPhoneNum) || !Regex.IsMatch(order.CustPhoneNum, @"^\d+$"))
                return false;

            // Validate Email 
            if (string.IsNullOrWhiteSpace(order.CustEmail) || !Regex.IsMatch(order.CustEmail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return false;

            return true;
        }
    }
}
