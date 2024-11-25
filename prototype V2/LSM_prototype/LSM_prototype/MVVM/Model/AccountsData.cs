using System.Collections.ObjectModel;

namespace LSM_prototype.MVVM.Model
{
    public class AccountsData
    {
        // Singleton instance
        private static AccountsData _instance;
        public static AccountsData Instance => _instance ??= new AccountsData();

        // Shared collection of accounts
        public ObservableCollection<Accounts> AccountsList { get; }

        // Private constructor to enforce singleton pattern
        private AccountsData()
        {
            // Load data from the database into the ObservableCollection
            using (var context = new BenjaminDbContext())
            {
                var accountsFromDb = context.Accounts.ToList();
                AccountsList = new ObservableCollection<Accounts>(accountsFromDb);
            }
            //AccountsList = new ObservableCollection<Accounts>();

            //AccountsList.Add(new Accounts{Name = "Employee 1", Gender = "Male", Age = 30, Birthdate = "2004 / 03 / 31",
            //                              PhoneNumber = "09XX-XXX-XXX", Email = "email@email.com", HireDate = "2004 / 03 / 30", Role = "Janitor",
            //                              EmpID = "A12346169", EmpPW = "12345", AccessLevel = "Admin"});

            //AccountsList.Add(new Accounts{Name = "Employee 1", Gender = "Male", Age = 30, Birthdate = "2004 / 03 / 31",
            //                              PhoneNumber = "09XX-XXX-XXX", Email = "email@email.com", HireDate = "2004 / 03 / 30", Role = "Janitor",
            //                              EmpID = "Q12346169", EmpPW = "qwert", AccessLevel = "User"});

            //AccountsList.Add(new Accounts{Name = "Employee 1", Gender = "Male", Age = 30, Birthdate = "2004 / 03 / 31",
            //                              PhoneNumber = "09XX-XXX-XXX", Email = "email@email.com", HireDate = "2004 / 03 / 30", Role = "Janitor",
            //                              EmpID = "W12346169", EmpPW = "asdfg", AccessLevel = "User"});
        }
    }
}