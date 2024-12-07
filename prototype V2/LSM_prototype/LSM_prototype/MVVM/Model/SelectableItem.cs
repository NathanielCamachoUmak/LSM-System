using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LSM_prototype.MVVM.Model
{
    public class SelectableItem
    {

        [Key]
        public int SelectableItemID { get; set; }

        [ForeignKey("Item")]
        public int ItemID { get; set; }
        public Item Item { get; set; }

        [ForeignKey("Orders")]
        public int OrderID { get; set; }
        public Orders Orders { get; set; }

        //public Item Item { get; set; }  // Original Item
        //public bool IsSelected { get; set; }


        //public string Name => Item.Name; // Name from the Item
        //public decimal Price => Item.Price; // Price from the Item
        //public int Stock => Item.Stock; // Stock from the Item

        public SelectableItem(Item item)
        {
            //Item = item;
            //IsSelected = false; // Default to unselected
        }
    }
}
