using System.Collections.ObjectModel;

namespace LSM_prototype.MVVM.Model
{
    public class AccountsData
    {
        private static AccountsData? _instance;
        public static AccountsData Instance => _instance ??= new AccountsData();

        public ObservableCollection<Accounts> SharedAccounts { get; } = new ObservableCollection<Accounts>();

        private AccountsData()
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

        public void SaveChangesToDatabase()
        {
            using (var context = new BenjaminDbContext())
            {
                context.Accounts.RemoveRange(context.Accounts); // Clear current database entries
                context.Accounts.AddRange(SharedAccounts);     // Add updated accounts
                context.SaveChanges();                         // Commit changes
            }
        }
    }
}