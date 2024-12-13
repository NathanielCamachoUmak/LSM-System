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
        public ObservableCollection<Accounts> SharedAccounts { get; } = new ObservableCollection<Accounts>();
        public ObservableCollection<Accounts> User { get; set; }

        public LoginViewModel()
        {
            User = CurrentUser.Instance.User;

            using (var context = new BenjaminDbContext())
            {
                // Clear existing items to avoid duplicates
                SharedAccounts.Clear();

                // Add filtered items to the ObservableCollection
                foreach (var account in context.Accounts.Where(account => account.AccessLevel == "Admin"))
                {
                    SharedAccounts.Add(account);
                }
            }

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

            using (var context = new BenjaminDbContext())
            {
                // Find the account in the database where the EmpID matches the input
                var account = context.Accounts.FirstOrDefault(a => a.EmpID == IDinput);

                if (account != null)
                {
                    // Check if the password matches
                    if (account.EmpPW == PWinput)
                    {
                        User.Add(account); // Add the account to the User list or perform any other logic
                        return true; // Authentication success
                    }
                    else
                    {
                        return false; // Password doesn't match
                    }
                }
            }

            return false; // EmpID doesn't exist or no match found
        }

    }
}
