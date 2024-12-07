using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSM_prototype.MVVM.Model
{
    public class SelectableItem
    {
        public Item Item { get; set; }  // Original Item

        public bool IsSelected { get; set; }

        public string Name => Item.Name; // Name from the Item
        public decimal Price => Item.Price; // Price from the Item
        public int Stock => Item.Stock; // Stock from the Item

        public SelectableItem(Item item)
        {
            Item = item;
            IsSelected = false; // Default to unselected
        }
    }
}
