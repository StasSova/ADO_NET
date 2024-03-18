using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_DbContext.Models;

public class M_ShoppingCart : DbEntity
{
    public int UserId { get; set; }
    public virtual M_User User { get; set; }
    public virtual ICollection<M_BookForSale> Books { get; set; } = new List<M_BookForSale>();
}
