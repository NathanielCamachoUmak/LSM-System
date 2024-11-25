using System.ComponentModel.DataAnnotations;

namespace LSM_prototype.MVVM.Model
{
    public class Item
    {
        [Key]
        public int ItemID { get; set; }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}