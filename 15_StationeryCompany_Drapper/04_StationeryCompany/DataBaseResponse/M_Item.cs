using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_StationeryCompany.DataBaseResponse
{
    public class M_Item
    {
        public int Id;
        public string Name;
        public int Type_ID;
        public int Count;
        public decimal CostPrice;
        public decimal Price;
        public M_Item() 
        {
            Name = "Item Name";
            Count = 10;
            CostPrice = new(10.25);
            Price = new(25.4);
        }
    }
}
