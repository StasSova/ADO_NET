using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _14_Dapper.DbEntity
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public int ProductSectionId { get; set; }
        public int Quantity { get; set; }
    }
}
