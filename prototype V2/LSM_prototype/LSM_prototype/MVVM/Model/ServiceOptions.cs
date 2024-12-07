using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LSM_prototype.MVVM.Model
{
    public class ServiceOptions : INotifyPropertyChanged
    {
        [Key]
        public int ServiceOptionsID {  get; set; }

        public string Name { get; set; }
        public int DurationValue { get; set; }
        public string DurationText { get; set; }

        [ForeignKey("Orders")]
        public int OrderID { get; set; }
        public Orders Orders { get; set; }

        private bool _isSelected;

        [NotMapped]
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
