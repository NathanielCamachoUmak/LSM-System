using System.ComponentModel.DataAnnotations;

namespace LSM_prototype.MVVM.Model
{
    public class Item
    {
        [Key]
        public int ItemID { get; set; }

        public string Name { get; set; } = string.Empty; // Initialize with a default value
        public decimal Price { get; set; } = 0.0m;
        public int Stock { get; set; } = 0;
    }
}