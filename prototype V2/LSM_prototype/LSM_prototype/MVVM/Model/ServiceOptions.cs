using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LSM_prototype.MVVM.Model
{
    public class ServiceOptions : INotifyPropertyChanged
    {
        [Key]
        public int ServiceOptionsID {  get; set; }

        public string Name { get; set; }
        public int DurationValue { get; set; }
        public string DurationText { get; set; }

        private bool _isSelected;
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
