﻿using LSM_prototype.MVVM.ViewModel;
using System.Security;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace LSM_prototype.MVVM.View
{
    public partial class RegisterView : Window
    {
        private readonly RegisterViewModel viewModel;

        public RegisterView()
        {
            InitializeComponent();
            viewModel = new RegisterViewModel();
            this.DataContext = viewModel;
        }

        bool PWvisible;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve the password and assign it to the ViewModel's property
            if (PWvisible)
            {
                PWbox.Password = PWTbox.Text;
            }
            SecureString inputText = PWbox.SecurePassword;
            viewModel.PW = inputText;
        }

        // When the checkbox is checked, show the password as plain text
        private void ShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            PWvisible = true;
            // Transfer the password to the TextBox and show it
            PWTbox.Text = PWbox.Password;
            PWTbox.Visibility = Visibility.Visible;
            PWbox.Visibility = Visibility.Collapsed;
        }

        // When the checkbox is unchecked, hide the password and show the PasswordBox again
        private void ShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            PWvisible = false;
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

        private void AgeTxtBox_preview(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void Pnum_preview(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^-0-9]+").IsMatch(e.Text);
        }
    }
}
