using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSM_prototype.MVVM.Model
{
    class Orders
    {
        public string OrderID { get; set; }
        public string Item { get; set; }
        public string ETA { get; set; }
        public string Status { get; set; }
        public string Technician { get; set; }
        public string Problem { get; set; }
        public string OtherNotes { get; set; }
        public string CustName { get; set; }
        public string CustEmail { get; set; }
        public string CustPhoneNum { get; set; }
    }
}
