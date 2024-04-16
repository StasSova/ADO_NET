using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Models;

public class M_ShoppingCart : DbEntity
{
    public int UserId { get; set; }
    public virtual M_User User { get; set; }
    public virtual ICollection<M_Book> Books { get; set; }
}
