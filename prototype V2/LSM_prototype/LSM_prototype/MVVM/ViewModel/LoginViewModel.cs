using LSM_prototype.Core;
using LSM_prototype.MVVM.Model;
using LSM_prototype.MVVM.View;
using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;
using System.Security;
using System.Windows;

namespace LSM_prototype.MVVM.ViewModel
{
    class LoginViewModel : ViewModelBase
    {
        public RelayCommand LoginCommand => new RelayCommand(execute => Login());
        public ObservableCollection<Accounts> SharedAccounts { get; }

        public LoginViewModel()
        {
            // Access the shared collection from the Accounts model
            SharedAccounts = AccountsData.Instance.AccountsList;

            if (SharedAccounts.Count == 0)
            {

                // Open the new main window
                RegisterView regWindow = new RegisterView();
                Application.Current.MainWindow = regWindow; // Update the application's main window
                regWindow.Show();

                // Close the login window
                Application.Current.Windows
                    .OfType<MainWindow>()
                    .FirstOrDefault()?.Close();
                return;
            }
        }

        private string _empIDInput = string.Empty;

        public string empIDInput
        {
            get { return _empIDInput; }
            set { _empIDInput = value; }
        }

        private SecureString _empPWInput;

        public SecureString empPWInput
        {
            get { return _empPWInput; }
            set { _empPWInput = value; }
        }


        private void Login()
        {
            if (Verify() == true)
            {
                MessageBox.Show("Login Successful");

                // Open the new main window
                MainWindow mainWindow = new MainWindow();
                Application.Current.MainWindow = mainWindow; // Update the application's main window
                mainWindow.Show();

                // Close the login window
                Application.Current.Windows
                    .OfType<LoginView>()
                    .FirstOrDefault()?.Close();
            }
            else
            {
                MessageBox.Show("Login Unsuccessful");
            }
        }

        private bool Verify()
        {
            string IDinput = empIDInput.ToUpper().Trim();
            string PWinput = empPWInput.ToUnsecureString();

            for (int i = 0; i < SharedAccounts.Count; i++)
            {
                if (SharedAccounts[i].EmpID == IDinput)
                {
                    if (SharedAccounts[i].EmpPW == PWinput)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return false;
        }
    }
}
