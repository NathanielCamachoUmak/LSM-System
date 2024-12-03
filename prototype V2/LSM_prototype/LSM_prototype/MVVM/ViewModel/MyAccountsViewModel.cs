using LSM_prototype.Core;
using LSM_prototype.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LSM_prototype.MVVM.ViewModel
{
    class MyAccountsViewModel : ViewModelBase
    {
        public ObservableCollection<Accounts> SharedAccounts { get; set; } = new ObservableCollection<Accounts>();
        public ObservableCollection<Accounts> User { get; }
        public RelayCommand SaveCommand => new RelayCommand(_ => Save(), _ => SharedAccounts.All(IsValidAccount));

        public MyAccountsViewModel()
        {
            User = CurrentUser.Instance.User;
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

        private void Save()
        {
            MessageBox.Show("adwadw");
            if (!SharedAccounts.All(IsValidAccount)) return;

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
            return !string.IsNullOrWhiteSpace(account.EmpID) &&
                   !string.IsNullOrWhiteSpace(account.Name) &&
                   account.Age > 0 &&
                   !string.IsNullOrWhiteSpace(account.Email) &&
                   account.Email.Contains("@");
        }
    }
}
