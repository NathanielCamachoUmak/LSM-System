using System.Collections.ObjectModel;

namespace LSM_prototype.MVVM.Model
{
    class CurrentUser
    {
        private static CurrentUser? _instance;
        public static CurrentUser Instance => _instance ??= new CurrentUser();

        public ObservableCollection<Accounts> User { get; } = new ObservableCollection<Accounts>();

        private CurrentUser()
        {
        }
    }
}