using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory_manager
{
    internal class Item
    {
        public string PartNum { get; set; }
        public string Description { get; set; }
        public int Qty { get; set; }

        public Item()
        {
            PartNum = string.Empty;
            Description = string.Empty;
            Qty = 0;
        }

        public Item(string PartNum, string Description, int Qty)
        {
            this.PartNum = PartNum;
            this.Description = Description;
            this.Qty = Qty;
        }
    }
}
