using LSM_prototype.MVVM.View;
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
            }
            else
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var button in new[] { Button_win1, Button_win2, Button_win3, Button_win4, Button_win5 })
            {
                if (button != sender && button.IsChecked == true)
                {
                    button.IsChecked = false;
                }
            }
        }

        private void Button_logout(object sender, RoutedEventArgs e)
        {
            LoginView loginWindow = new LoginView();
            Application.Current.MainWindow = loginWindow;
            loginWindow.Show();

            Application.Current.Windows
                .OfType<MainWindow>()
                .FirstOrDefault()?.Close();
        }
    }
}