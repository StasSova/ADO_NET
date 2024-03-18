using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_DbContext.Models;

public class M_User : DbEntity
{
    public M_User()
    {
        Username = "User Name";
        Password = "User Password";
    }

    public string Username { get; set; }
    public string Password { get; set; }

    public virtual ICollection<M_ShoppingCart> ShoppingCarts { get; set; } = new List<M_ShoppingCart>();
}
