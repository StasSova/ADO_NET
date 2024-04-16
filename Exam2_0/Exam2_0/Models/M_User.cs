using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Models;

public class M_User : DbEntity
{
    public string Username { get; set; }
    public string Password { get; set; }
    public virtual ICollection<M_ShoppingCart> ShoppingCarts { get; set; }
}
