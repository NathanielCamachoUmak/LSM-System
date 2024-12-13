using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LSM_prototype.MVVM.Model
{
    public class Item
    {
        [Key]
        public int ItemID { get; set; }

        public string Name { get; set; } = string.Empty;

        // The actual decimal value stored in the database
        public decimal Price { get; set; } = 0.0m;

        [NotMapped]
        public string PriceInput
        {
            get => Price.ToString("F2");
            set
            {
                if (decimal.TryParse(value, out var parsedValue))
                    Price = parsedValue;
            }
        }

        public int Stock { get; set; } = 0;
    }

}