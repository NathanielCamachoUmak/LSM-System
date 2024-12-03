using LSM_prototype.MVVM.Model;
using LSM_prototype.MVVM.View;
using LSM_prototype.MVVM.ViewModel;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LSM_prototype
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Accounts> User { get; }
        public MainWindow()
        {
            InitializeComponent();
            MainViewModel mainvm = new MainViewModel();
            this.DataContext = mainvm;


            User = CurrentUser.Instance.User;

            if (User[0].AccessLevel != "Admin")
            {
                Acc_btn.Visibility = Visibility.Collapsed;
                MyAcc_btn.Visibility = Visibility.Visible;
            }
            else if (User[0].AccessLevel == "Admin")
            {
                Acc_btn.Visibility = Visibility.Visible;
                MyAcc_btn.Visibility = Visibility.Collapsed;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

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
            foreach (var button in new[] { Ord_btn, Inv_btn, Acc_btn, MyAcc_btn, Logout })
            {
                if (button != sender && button.IsChecked == true)
                {
                    button.IsChecked = false;
                }
            }
        }
    }
}