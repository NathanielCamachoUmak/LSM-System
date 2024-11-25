using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace LSM_prototype.MVVM.Model
{
    public class Accounts
    {
        [Key]
        public int AccountID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Birthdate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string HireDate { get; set; }
        public string Role { get; set; }
        public string EmpID { get; set; }
        public string EmpPW { get; set; }
        public string AccessLevel { get; set; }
    }
}
