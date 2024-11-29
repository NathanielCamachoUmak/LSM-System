using LSM_prototype.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LSM_prototype.MVVM.View
{
    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : Window
    {

        public RegisterView()
        {
            InitializeComponent();
        }

        // When the checkbox is checked, show the password as plain text
        private void ShowPassword_Checked1(object sender, RoutedEventArgs e)
        {
            // Transfer the password to the TextBox and show it
            PWTbox1.Text = PWbox1.Password;
            PWTbox1.Visibility = Visibility.Visible;
            PWbox1.Visibility = Visibility.Collapsed;
        }

        // When the checkbox is unchecked, hide the password and show the PasswordBox again
        private void ShowPassword_Unchecked1(object sender, RoutedEventArgs e)
        {
            // Transfer the plain text back to the PasswordBox and hide the TextBox
            PWbox1.Password = PWTbox1.Text;
            PWbox1.Visibility = Visibility.Visible;
            PWTbox1.Visibility = Visibility.Collapsed;
        }

        // When the checkbox is checked, show the password as plain text
        private void ShowPassword_Checked2(object sender, RoutedEventArgs e)
        {
            // Transfer the password to the TextBox and show it
            PWTbox2.Text = PWbox2.Password;
            PWTbox2.Visibility = Visibility.Visible;
            PWbox2.Visibility = Visibility.Collapsed;
        }

        // When the checkbox is unchecked, hide the password and show the PasswordBox again
        private void ShowPassword_Unchecked2(object sender, RoutedEventArgs e)
        {
            // Transfer the plain text back to the PasswordBox and hide the TextBox
            PWbox2.Password = PWTbox2.Text;
            PWbox2.Visibility = Visibility.Visible;
            PWTbox2.Visibility = Visibility.Collapsed;
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
    }
}

