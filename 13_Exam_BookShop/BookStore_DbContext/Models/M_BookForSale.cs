using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_DbContext.Models;

public class M_BookForSale : M_Book
{
    public M_BookForSale() : base()
    {
        ShoppingCarts = new List<M_ShoppingCart>();
    }
    public decimal CostPrice { get; set; }
    public decimal SellingPrice { get; set; }
    public decimal Discount {  get; set; }
    public bool IsOnSale { get; set; }

    // Навигационное свойство для связи с M_ShoppingCart
    public virtual ICollection<M_ShoppingCart> ShoppingCarts { get; set; }
    public virtual M_WarehouseItem WarehouseItem { get; set; }
}
