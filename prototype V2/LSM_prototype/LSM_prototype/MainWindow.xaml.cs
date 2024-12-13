using LSM_prototype.MVVM.Model;
using LSM_prototype.MVVM.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace LSM_prototype
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Accounts> User { get; } = CurrentUser.Instance.User;
        public MainWindow()
        {
            InitializeComponent();
            MainViewModel mainvm = new MainViewModel();
            this.DataContext = mainvm;



            if (User[0].AccessLevel != "Admin")
            {
                Acc_btn.Visibility = Visibility.Collapsed;
                MyAcc_btn.Visibility = Visibility.Visible;
                Analytics_Expander.Visibility = Visibility.Collapsed;
                black_bar.Visibility = Visibility.Collapsed;
            }
            else if (User[0].AccessLevel == "Admin")
            {
                Acc_btn.Visibility = Visibility.Visible;
                MyAcc_btn.Visibility = Visibility.Collapsed;
                Analytics_Expander.Visibility = Visibility.Visible;
                black_bar.Visibility = Visibility.Visible;
            }
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
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void Button_Maximize(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
            {
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
                windowBorder.BorderThickness = new Thickness(0);
                windowBorder.CornerRadius = new CornerRadius(0);
            }
            else
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
                windowBorder.BorderThickness = new Thickness(10,0,10,10);
                windowBorder.CornerRadius = new CornerRadius(10);
            }
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var button in new[] { Analytics_btn, Analytics1_btn,
                                           Analytics2_btn, Analytics3_btn, 
                                           Analytics4_btn, Orders_Expander_btn, 
                                           Add_Orders_btn, All_Orders_btn, 
                                           Ongoing_Orders_btn, Completed_Orders_btn, 
                                           Cancelled_Orders_btn, Inv_btn, Acc_btn,
                                           MyAcc_btn, Logout })
            {
                if (button != sender && button.IsChecked == true)
                {
                    button.IsChecked = false;
                }
            }
        }

        private void Orders_Expander_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var button in new[] { Orders_Expander_btn, Inv_btn, Acc_btn, MyAcc_btn, Logout })
            {
                if (button != sender && button.IsChecked == true)
                {
                    button.IsChecked = false;
                }
            }
            Orders_Expander.Margin = new Thickness(-2, 0, -2, -258);
            Orders_Expander.BorderThickness = new Thickness(2, 0, 2, 2);
        }

        private void Orders_Expander_Unchecked(object sender, RoutedEventArgs e)
        {
            Orders_Expander.Margin = new Thickness(-2, 0, -2, 0);
            Orders_Expander.BorderThickness = new Thickness(2, 0, 2, 0);
        }

        private void Analytics_Expander_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var button in new[] { Analytics_btn, Inv_btn, Acc_btn, MyAcc_btn, Logout })
            {
                if (button != sender && button.IsChecked == true)
                {
                    button.IsChecked = false;
                }
            }
            Analytics_Expander.Margin = new Thickness(-2, 0, -2, -206);
            Analytics_Expander.BorderThickness = new Thickness(2, 0, 2, 2);
        }

        private void Analytics_Expander_Unchecked(object sender, RoutedEventArgs e)
        {
            Analytics_Expander.Margin = new Thickness(-2, 0, -2, 0);
            Analytics_Expander.BorderThickness = new Thickness(2, 0, 2, 0);
        }
    }
}