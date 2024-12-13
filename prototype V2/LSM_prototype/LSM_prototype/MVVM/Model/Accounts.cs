using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;

namespace LSM_prototype.MVVM.Model
{
    public class Accounts : INotifyPropertyChanged
    {
        [Key]
        public int AccountID { get; set; }

        public string Name { get; set; } = string.Empty; // Default to an empty string
        public string Gender { get; set; } = string.Empty;

        [NotMapped]
        public int Age => CalculateAge(Birthdate);

        private int CalculateAge(DateTime birthdate)
        {
            var today = DateTime.Today; // Get the current date (without time)
            int age = today.Year - birthdate.Year;
            if (today < birthdate.AddYears(age)) age--; // Adjust if the birthday hasn't occurred yet this year
            return age;
        }

        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string EmpID { get; set; } = string.Empty;
        public string EmpPW { get; set; } = string.Empty;
        public string AccessLevel { get; set; } = string.Empty;

        private DateTime _birthdate;

        public DateTime Birthdate
        {
            get => _birthdate;
            set
            {
                if (_birthdate != value)
                {
                    _birthdate = value;
                    OnPropertyChanged(nameof(Birthdate));
                }
            }
        }
        public DateTime HireDate { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
