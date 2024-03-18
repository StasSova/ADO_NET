using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_DbContext.Models;

public class M_WarehouseItem : DbEntity
{
    public int BookForSaleId { get; set; }
    public virtual M_BookForSale BookForSale { get; set; }
    public int Quantity { get; set; }
}
