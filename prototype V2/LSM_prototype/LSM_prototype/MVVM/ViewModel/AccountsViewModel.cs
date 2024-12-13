using LSM_prototype.Core;
using LSM_prototype.MVVM.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace LSM_prototype.MVVM.ViewModel
{
    class AccountsViewModel : ViewModelBase
    {
        public RelayCommand AddCommand => new RelayCommand(_ => AddAccount());
        public RelayCommand DeleteCommand => new RelayCommand(_ => DeleteAccount(), _ => SelectedAccount != null);
        public RelayCommand SaveCommand => new RelayCommand(_ => Save());
        public ObservableCollection<Accounts> SharedAccounts { get; set; } = new ObservableCollection<Accounts>();
        public List<string> RoleOptions { get; set; } = new List<string>
        {
            "Admin",
            "Employee"
        };

        private Accounts _newAccount = new Accounts();
        public Accounts NewAccount
        {
            get => _newAccount;
            set { _newAccount = value; OnPropertyChanged(); }
        }

        private Accounts? _selectedAccount;

        public Accounts? SelectedAccount
        {
            get => _selectedAccount;
            set
            {
                _selectedAccount = value;
                OnPropertyChanged();
            }
        }

        public AccountsViewModel()
        {
            LoadAccountsFromDatabase();
            NewAccount.Birthdate = DateTime.Now;
            NewAccount.HireDate = DateTime.Now;
        }

        public void LoadAccountsFromDatabase()
        {
            SharedAccounts.Clear();
            using (var context = new BenjaminDbContext())
            {
                var accountsFromDb = context.Accounts?.ToList() ?? new List<Accounts>();
                foreach (var account in accountsFromDb)
                {
                    SharedAccounts.Add(account);
                }
            }
        }

        private void AddAccount()
        {
            if (!IsValidAccount(NewAccount)) return;

            string ID;
            do
            {
                ID = GenerateEmpID();
            }
            while (!IsEmpIDUnique(ID));

            using (var context = new BenjaminDbContext())
            {
                var newAcc = new Accounts
                {
                    Name = NewAccount.Name,
                    Gender = NewAccount.Gender,
                    Birthdate = NewAccount.Birthdate,
                    PhoneNumber = NewAccount.PhoneNumber,
                    Email = NewAccount.Email,
                    HireDate = NewAccount.HireDate,
                    EmpID = ID,
                    EmpPW = NewAccount.EmpPW,
                    AccessLevel = NewAccount.AccessLevel
                };
                context.Accounts.Add(newAcc);
                context.SaveChanges();

                LoadAccountsFromDatabase();
            }

            ResetNewAccountFields();
            MessageBox.Show("Account added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DeleteAccount()
        {
            if (SelectedAccount == null) return;

            var result = MessageBox.Show($"Are you sure you want to delete {SelectedAccount.Name}?",
                                         "Account Deletion Confirmation",
                                         MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                using (var context = new BenjaminDbContext())
                {
                    if (SelectedAccount.AccountID != 0)
                    {
                        var accountToRemove = context.Accounts.Find(SelectedAccount.AccountID);
                        if (accountToRemove != null)
                        {
                            context.Accounts.Remove(accountToRemove);
                            context.SaveChanges();
                        }
                    }
                }

                SharedAccounts.Remove(SelectedAccount);
                MessageBox.Show("Account deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Save()
        {
            if (!SharedAccounts.All(IsValidAccount)) return;

            using (var context = new BenjaminDbContext())
            {
                foreach (var account in SharedAccounts)
                {
                    if (account.AccountID == 0)
                        context.Accounts.Add(account);
                    else
                        context.Accounts.Update(account);
                }
                context.SaveChanges();
            }

            MessageBox.Show("Changes saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool IsValidAccount(Accounts account)
        {
            // Validate Name
            if (string.IsNullOrWhiteSpace(account.Name))
            {
                MessageBox.Show($"Invalid name!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Validate Gender
            if (string.IsNullOrWhiteSpace(account.Gender))
            {
                MessageBox.Show($"Invalid gender!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Validate Phone Number (basic numeric check, can be expanded)
            if (string.IsNullOrWhiteSpace(account.PhoneNumber) || !Regex.IsMatch(account.PhoneNumber, @"^\d+$"))
            {
                MessageBox.Show($"Invalid phone number!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Validate Email (simple regex for email structure)
            if (string.IsNullOrWhiteSpace(account.Email) || !Regex.IsMatch(account.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show($"Invalid email!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Validate Password (must not be empty)
            if (string.IsNullOrWhiteSpace(account.EmpPW))
            {
                MessageBox.Show($"Invalid password!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
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

        private void ResetNewAccountFields()
        {
            NewAccount = new Accounts();
            NewAccount.Birthdate = DateTime.Now;
            NewAccount.HireDate = DateTime.Now;
        }
    }
}
