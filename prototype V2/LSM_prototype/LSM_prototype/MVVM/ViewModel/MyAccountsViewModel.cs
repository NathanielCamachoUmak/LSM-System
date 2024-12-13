using LSM_prototype.Core;
using LSM_prototype.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace LSM_prototype.MVVM.ViewModel
{
    class MyAccountsViewModel : ViewModelBase
    {
        public Accounts CurrentAccount { get; set; } = new Accounts();
        public ObservableCollection<Accounts> User { get; } = CurrentUser.Instance.User;
        public RelayCommand SaveCommand => new RelayCommand(_ => Save());

        public MyAccountsViewModel()
        {
            LoadAccountsFromDatabase();
        }

        private void LoadAccountsFromDatabase()
        {
            using (var context = new BenjaminDbContext())
            {
                var accountsFromDb = context.Accounts?.FirstOrDefault(a => a.AccountID == User[0].AccountID);
                CurrentAccount = accountsFromDb;
            }
        }

        private void Save()
        {
            if (!IsValidAccount(CurrentAccount)) return;

            using (var context = new BenjaminDbContext())
            {
                foreach (var account in User)
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
    }
}
