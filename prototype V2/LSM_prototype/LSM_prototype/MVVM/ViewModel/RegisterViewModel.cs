using LSM_prototype.Core;
using LSM_prototype.MVVM.Model;
using LSM_prototype.MVVM.View;
using System.Collections.ObjectModel;
using System.Security;
using System.Text.RegularExpressions;
using System.Windows;

namespace LSM_prototype.MVVM.ViewModel
{
    internal class RegisterViewModel : ViewModelBase
    {
        public string Name { get; set; } = string.Empty;
        public string Age { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string PNum { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string ID { get; set; } = string.Empty;
        public SecureString PW { get; set; } = new SecureString();

        public RelayCommand RegisterCommand => new RelayCommand(execute => Register());
        public ObservableCollection<Accounts> SharedAccounts { get; }

        public RegisterViewModel()
        {
            SharedAccounts = AccountsData.Instance.SharedAccounts;
        }

        public void Register()
        {
            string IDinput = ID.Trim();
            string PWinput = PW.ToUnsecureString();

            // Checks if the inputs are valid
            if (!ValidateInputs())
            {
                MessageBox.Show("Please correct the invalid fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Assign AccessLevel based on the number of accounts in the database you get the drill
            string accessLevel = SharedAccounts.Count == 0 ? "Admin" : "User";

            // Add the new account to the SharedAccounts and database
            var newAccount = new Accounts
            {
                Name = Name,
                Age = int.Parse(Age),
                Gender = Gender,
                PhoneNumber = PNum,
                Email = Email,
                Role = Role,
                EmpID = ID.Trim(),
                EmpPW = PW.ToUnsecureString(),
                Birthdate = "XX/XX/XXXX", // Replace with actual value if available
                HireDate = "XX/XX/XXXX", // Replace with actual value if available
                AccessLevel = accessLevel
            };

            SharedAccounts.Add(newAccount);

            AccountsData.Instance.SaveChangesToDatabase(); // Save changes to the database

            MessageBox.Show("Registration successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            LoginView loginWindow = new LoginView();
            Application.Current.MainWindow = loginWindow;
            loginWindow.Show();

            Application.Current.Windows
                .OfType<RegisterView>()
                .FirstOrDefault()?.Close();
        }

        private bool ValidateInputs()
        {
            // Validate Name
            if (string.IsNullOrWhiteSpace(Name))
                return false;

            // Validate Age (must be a valid positive number)
            if (!int.TryParse(Age, out int age) || age <= 0)
                return false;

            // Validate Gender
            if (string.IsNullOrWhiteSpace(Gender))
                return false;

            // Validate Phone Number (basic numeric check, can be expanded)
            if (string.IsNullOrWhiteSpace(PNum) || !Regex.IsMatch(PNum, @"^\d+$"))
                return false;

            // Validate Email (simple regex for email structure)
            if (string.IsNullOrWhiteSpace(Email) || !Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return false;

            // Validate Role
            if (string.IsNullOrWhiteSpace(Role))
                return false;

            // Validate EmpID
            if (string.IsNullOrWhiteSpace(ID))
                return false;

            // Validate Password (must not be empty)
            if (PW == null || PW.Length == 0)
                return false;

            return true;
        }
    }
}
