using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LSM_prototype.MVVM.Model
{
    public class Orders
    {
        [Key]
        public int OrderID { get; set; }

        [ForeignKey("Accounts")]
        public int AccountID { get; set; }
        public Accounts Accounts { get; set; }

        public string Status { get; set; } = string.Empty;
        public string Employee { get; set; } = string.Empty;
        public string DeviceType { get; set; } = string.Empty;
        public string DeviceName { get; set; } = string.Empty;
        public string OtherNotes { get; set; } = string.Empty;
        public string CustName { get; set; } = string.Empty;
        public string CustEmail { get; set; } = string.Empty;
        public string CustPhoneNum { get; set; } = string.Empty;
    }
}
