using LSM_prototype.Core;
using LSM_prototype.MVVM.Model;
using LSM_prototype.MVVM.View;
using System.Collections.ObjectModel;
using System.Security;
using System.Windows;

namespace LSM_prototype.MVVM.ViewModel
{
    class LoginViewModel : ViewModelBase
    {
        public RelayCommand LoginCommand => new RelayCommand(execute => Login());
        public ObservableCollection<Accounts> SharedAccounts { get; }
        public ObservableCollection<Accounts> User { get; set; }

        public LoginViewModel()
        {
            // Access the shared collection from the Accounts model
            SharedAccounts = AccountsData.Instance.SharedAccounts;
            User = CurrentUser.Instance.User;

            //checks if database accounts is 0
            if (SharedAccounts.Count == 0)
            {

                // Open the new main window
                RegisterView regWindow = new RegisterView();
                Application.Current.MainWindow = regWindow; // Update the application's main window
                regWindow.Show();

                // Close the login window
                Application.Current.Windows
                    .OfType<LoginView>()
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

        private SecureString _empPWInput = new SecureString();

        public SecureString empPWInput
        {
            get { return _empPWInput; }
            set { _empPWInput = value; }
        }


        private void Login()
        {
            if (Verify() == true)
            {

                MessageBox.Show($"{User[0].Name}, {User[0].EmpID}, {User[0].EmpPW}, {User[0].AccessLevel}");

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
            string IDinput = empIDInput.Trim();
            string PWinput = empPWInput.ToUnsecureString();

            for (int i = 0; i < SharedAccounts.Count; i++)
            {
                if (SharedAccounts[i].EmpID == IDinput)
                {
                    if (SharedAccounts[i].EmpPW == PWinput)
                    {
                        User.Add(SharedAccounts[i]);
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
