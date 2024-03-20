/*
 * Name: James A. Chase
 * File: Item.cs
 * Date: 18 March 2024
*/

namespace inventory_manager
{
    internal class Item
    {
        // Values to hold Item number, description and quantity
        public string PartNum { get; set; }
        public string Description { get; set; }
        public int Qty { get; set; }

        // generic constructor
        public Item()
        {
            PartNum = string.Empty;
            Description = string.Empty;
            Qty = 0;
        }

        // Constructor if provided with values for all three
        public Item(string PartNum, string Description, int Qty)
        {
            this.PartNum = PartNum;
            this.Description = Description;
            this.Qty = Qty;
        }
    }
}
