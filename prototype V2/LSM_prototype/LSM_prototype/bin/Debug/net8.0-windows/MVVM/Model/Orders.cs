using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LSM_prototype.MVVM.Model
{
    public class Orders : INotifyPropertyChanged
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }

        [ForeignKey("Accounts")]
        public int AccountID { get; set; }
        public Accounts Accounts { get; set; }

        public string Status { get; set; } = string.Empty;

        private string _employee;
        public string Employee
        {
            get { return _employee; }
            set
            {
                if (_employee != value)
                {
                    _employee = value;
                    OnPropertyChanged(nameof(Employee)); // Notify UI of change
                }
            }
        }

        private string _deviceType;
        public string DeviceType
        {
            get { return _deviceType; }
            set
            {
                if (_deviceType != value)
                {
                    _deviceType = value;
                    OnPropertyChanged(nameof(DeviceType)); // Notify UI of change
                }
            }
        }
        public string DeviceName { get; set; } = string.Empty;
        public string OtherNotes { get; set; } = string.Empty;
        public string CustName { get; set; } = string.Empty;
        public string CustEmail { get; set; } = string.Empty;
        public string CustPhoneNum { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }

        private bool _discounted;
        public bool Discounted
        {
            get => _discounted;
            set
            {
                _discounted = value;
                OnPropertyChanged(nameof(Discounted));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
