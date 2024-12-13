using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LSM_prototype.MVVM.Model
{
    public class SelectableItem : INotifyPropertyChanged
    {

        [Key]
        public int SelectableItemID { get; set; }

        [ForeignKey("Item")]
        public int ItemID { get; set; }
        public Item Item { get; set; }

        [ForeignKey("Orders")]
        public int OrderID { get; set; }
        public Orders Orders { get; set; }

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


        private bool _isSelectable = true;
        [NotMapped]
        public bool IsSelectable
        {
            get => _isSelectable;
            set
            {
                _isSelectable = value;
                OnPropertyChanged(nameof(IsSelectable));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
