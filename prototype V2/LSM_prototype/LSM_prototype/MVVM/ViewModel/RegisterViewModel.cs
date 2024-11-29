using LSM_prototype.Core;
using LSM_prototype.MVVM.Model;
using LSM_prototype.MVVM.View;
using System.Collections.ObjectModel;
using System.Security;
using System.Windows;

namespace LSM_prototype.MVVM.ViewModel
{
    internal class RegisterViewModel : ViewModelBase
    {
        public string Name { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string PNum { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string ID { get; set; }
        public SecureString PW { get; set; }

        public RelayCommand RegisterCommand => new RelayCommand(execute => Register());
        public ObservableCollection<Accounts> SharedAccounts { get; }

        public RegisterViewModel()
        {
            SharedAccounts = AccountsData.Instance.AccountsList;
        }

        public void Register()
        {
            int age = Int32.Parse(Age);
            string IDinput = ID.Trim();
            string PWinput = PW.ToUnsecureString();
            string AccessLevel;

            //change the condition to check if database has no accounts
            if (SharedAccounts.Count == 0)
            {
                AccessLevel = "Admin";
            }
            else
            {
                AccessLevel = "User";
            }


            //save this to the database pls
            //iba pala pagkagawa mo ng lists sa AccountsViewModel
            //the new account is saved in SharedAccounts naman na
            SharedAccounts.Add(new Accounts { Name = Name, Age = age, Gender = Gender, PhoneNumber = PNum,
                                              Email = Email, Role = Role, EmpID = IDinput, EmpPW = PWinput,
                                              Birthdate = "XX/XX/XXXX", HireDate = "XX/XX/XXXX", AccessLevel = AccessLevel
            });


            LoginView loginWindow = new LoginView();
            Application.Current.MainWindow = loginWindow;
            loginWindow.Show();

            Application.Current.Windows
                .OfType<RegisterView>()
                .FirstOrDefault()?.Close();
        }
    }
}
