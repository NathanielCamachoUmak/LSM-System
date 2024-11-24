using LSM_prototype.Core;
using LSM_prototype.MVVM.Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace LSM_prototype.MVVM.ViewModel
{
    class AccountsViewModel : ViewModelBase
    {
        public RelayCommand AddCommand => new RelayCommand(execute => AddItem());
        public RelayCommand DeleteCommand => new RelayCommand(execute => DeleteItem(), canExecute => SelectedItem != null);
        public RelayCommand SaveCommand => new RelayCommand(execute => Save(), canExecute => CanSave());
        public ObservableCollection<Accounts> SharedAccounts { get; }

        public AccountsViewModel()
        {
            // Access the shared collection from the Accounts model
            SharedAccounts = AccountsData.Instance.AccountsList;
        }

        private Accounts _selectedItem;

        public Accounts SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        //can we check if item is already in database? using name as primary key
        private void AddItem()
        {
            SharedAccounts.Add(new Accounts
            {
                Name = "NEW ACCOUNT",
                Gender = "Male/Female",
                Age = 0,
                Birthdate = "2004 / 03 / 31",
                PhoneNumber = "09XX-XXX-XXX",
                Email = "email@email.com",
                HireDate = "2004 / 03 / 30",
                Role = "ROLE"
            });
        }

        private void DeleteItem()
        {
            var result = MessageBox.Show($"Are you sure you want to delete {SelectedItem.Name}?",
                                         $"ITEM DELETION CONFIRMATION",
                                         MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                SharedAccounts.Remove(SelectedItem);
            }
        }

        //save to database using this
        private void Save()
        {

        }

        //add a check to see if database is up and items can e saved
        private bool CanSave()
        {
            //if ok, return true
            return true;
        }
    }
}
