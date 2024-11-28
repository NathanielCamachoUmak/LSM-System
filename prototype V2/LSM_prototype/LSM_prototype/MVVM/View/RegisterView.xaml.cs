using LSM_prototype.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        public ObservableCollection<Accounts> SharedAccounts { get; }

        public RegisterView()
        {
            InitializeComponent();
            // Access the shared collection from the Accounts model
            SharedAccounts = AccountsData.Instance.AccountsList;

            if (SharedAccounts.Count != 0)
            {

                // Open the new main window
                LoginView loginWindow = new LoginView();
                Application.Current.MainWindow = loginWindow; // Update the application's main window
                loginWindow.Show();

                // Close the login window
                Application.Current.Windows
                    .OfType<RegisterView>()
                    .FirstOrDefault()?.Close();
                return;
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

