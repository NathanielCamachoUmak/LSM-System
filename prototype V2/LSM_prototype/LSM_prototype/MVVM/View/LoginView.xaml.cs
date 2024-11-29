using LSM_prototype.MVVM.ViewModel;
using System.Security;
using System.Windows;
using System.Windows.Input;

namespace LSM_prototype.MVVM.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private readonly LoginViewModel viewModel;

        public LoginView()
        {
            InitializeComponent();
            viewModel = new LoginViewModel();
            this.DataContext = viewModel;
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve the password and assign it to the ViewModel's property
            PWbox.Password = PWTbox.Text;
            SecureString inputText = PWbox.SecurePassword;
            viewModel.empPWInput = inputText;
        }

        // When the checkbox is checked, show the password as plain text
        private void ShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            // Transfer the password to the TextBox and show it
            PWTbox.Text = PWbox.Password;
            PWTbox.Visibility = Visibility.Visible;
            PWbox.Visibility = Visibility.Collapsed;
        }

        // When the checkbox is unchecked, hide the password and show the PasswordBox again
        private void ShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            // Transfer the plain text back to the PasswordBox and hide the TextBox
            PWbox.Password = PWTbox.Text;
            PWbox.Visibility = Visibility.Visible;
            PWTbox.Visibility = Visibility.Collapsed;
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
