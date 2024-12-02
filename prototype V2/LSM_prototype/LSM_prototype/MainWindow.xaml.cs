using LSM_prototype.MVVM.View;
using LSM_prototype.MVVM.ViewModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
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
        public MainWindow()
        {
            InitializeComponent();
            MainViewModel mainvm = new MainViewModel();
            this.DataContext = mainvm;
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
            foreach (var button in new[] { Button_win1, Button_win2, Button_win3, Logout })
            {
                if (button != sender && button.IsChecked == true)
                {
                    button.IsChecked = false;
                }
            }
        }
    }
}