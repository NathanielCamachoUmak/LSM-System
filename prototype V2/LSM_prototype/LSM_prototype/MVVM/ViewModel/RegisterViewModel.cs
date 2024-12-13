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
        public DateTime Birthdate { get; set; }
        public DateTime Hiredate { get; set; }
        public SecureString PW { get; set; } = new SecureString();

        public RelayCommand RegisterCommand => new RelayCommand(execute => Register());
        public ObservableCollection<Accounts> SharedAccounts { get; }

        public RegisterViewModel()
        {
            SharedAccounts = AccountsData.Instance.SharedAccounts;
            Birthdate = DateTime.Now;
            Hiredate = DateTime.Now;
        }

        public void Register()
        {
            string IDinput = ID.Trim();
            string PWinput = PW.ToUnsecureString();

            // Checks if the inputs are valid
            if (!ValidateInputs())
                return;
            
            do
            {
                ID = GenerateEmpID();
            }
            while (!IsEmpIDUnique(ID));

            // Add the new account to the SharedAccounts and database
            var newAccount = new Accounts
            {
                Name = Name,
                Gender = Gender,
                PhoneNumber = PNum,
                Email = Email,
                EmpID = ID,
                EmpPW = PW.ToUnsecureString(),
                Birthdate = Birthdate,
                HireDate = Hiredate,
                AccessLevel = "Admin"
            };

            SharedAccounts.Add(newAccount);

            AccountsData.Instance.SaveChangesToDatabase(); // Save changes to the database

            MessageBox.Show($"Registration successful!\nEmployee ID: {newAccount.EmpID}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            LoginView loginWindow = new LoginView();
            Application.Current.MainWindow = loginWindow;
            loginWindow.Show();

            Application.Current.Windows
                .OfType<RegisterView>()
                .FirstOrDefault()?.Close();
        }
        public string GenerateEmpID()
        {
            Random random = new Random();

            // Generate a random letter (A-Z)
            char letter = (char)random.Next('A', 'Z' + 1);

            // Generate an 8-digit random number
            int number = random.Next(10000000, 100000000); // Ensures the number has 8 digits

            // Combine the letter and number to create the EmpID
            return $"{letter}{number}";
        }

        public bool IsEmpIDUnique(string empID)
        {
            using (var context = new BenjaminDbContext())
            {
                // Check if the EmpID already exists in the database
                return !context.Accounts.Any(a => a.EmpID == empID);
            }
        }

        private bool ValidateInputs()
        {
            // Validate Name
            if (string.IsNullOrWhiteSpace(Name))
            {
                MessageBox.Show($"Invalid name!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Validate Gender
            if (string.IsNullOrWhiteSpace(Gender))
            {
                MessageBox.Show($"Invalid gender!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Validate Phone Number (basic numeric check, can be expanded)
            if (string.IsNullOrWhiteSpace(PNum) || !Regex.IsMatch(PNum, @"^\d+$"))
            {
                MessageBox.Show($"Invalid phone number!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Validate Email (simple regex for email structure)
            if (string.IsNullOrWhiteSpace(Email) || !Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show($"Invalid password!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Validate Password (must not be empty)
            if (PW == null || PW.Length == 0)
            {
                MessageBox.Show($"Invalid password!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
    }
}
