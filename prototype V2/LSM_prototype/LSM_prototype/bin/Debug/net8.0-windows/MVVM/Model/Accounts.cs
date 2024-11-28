using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace LSM_prototype.MVVM.Model
{
    public class Accounts
    {
        [Key]
        public int AccountID { get; set; }

        public string Name { get; set; } = string.Empty; // Default to an empty string
        public string Gender { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Birthdate { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string HireDate { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string EmpID { get; set; } = string.Empty;
        public string EmpPW { get; set; } = string.Empty;
        public string AccessLevel { get; set; } = string.Empty;
    }
}
