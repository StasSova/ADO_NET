using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_DbContext.Models;

public class M_BookForSale : DbEntity
{
    public M_BookForSale()
    {
        ShoppingCarts = new List<M_ShoppingCart>();
    }

    public int BookId { get; set; }
    public virtual M_Book Book { get; set; }
    public decimal CostPrice { get; set; }
    public decimal SellingPrice { get; set; }
    public bool IsOnSale { get; set; }

    // Навигационное свойство для связи с M_ShoppingCart
    public virtual ICollection<M_ShoppingCart> ShoppingCarts { get; set; }
    public virtual M_WarehouseItem WarehouseItem { get; set; }
}
