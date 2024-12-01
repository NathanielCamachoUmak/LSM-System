using LSM_prototype.Core;
using LSM_prototype.MVVM.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace LSM_prototype.MVVM.ViewModel
{
    class AccountsViewModel : ViewModelBase
    {
        public ObservableCollection<Accounts> SharedAccounts { get; set; } = new ObservableCollection<Accounts>();
        public RelayCommand AddCommand => new RelayCommand(_ => AddAccount(), _ => IsValidAccount(NewAccount));
        public RelayCommand DeleteCommand => new RelayCommand(_ => DeleteAccount(), _ => SelectedAccount != null);
        public RelayCommand SaveCommand => new RelayCommand(_ => Save(), _ => SharedAccounts.All(IsValidAccount));

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
        }

        private void LoadAccountsFromDatabase()
        {
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

            SharedAccounts.Add(new Accounts
            {
                EmpID = NewAccount.EmpID,
                Name = NewAccount.Name,
                Age = NewAccount.Age,
                Gender = NewAccount.Gender,
                Birthdate = NewAccount.Birthdate,
                PhoneNumber = NewAccount.PhoneNumber,
                Email = NewAccount.Email,
                HireDate = NewAccount.HireDate,
                Role = NewAccount.Role,
                EmpPW = NewAccount.EmpPW,
                AccessLevel = NewAccount.AccessLevel
            });

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
            return !string.IsNullOrWhiteSpace(account.EmpID) &&
                   !string.IsNullOrWhiteSpace(account.Name) &&
                   account.Age > 0 &&
                   !string.IsNullOrWhiteSpace(account.Email) &&
                   account.Email.Contains("@");
        }

        private void ResetNewAccountFields()
        {
            NewAccount = new Accounts();
        }
    }
}
