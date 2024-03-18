using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_DbContext.Models;

public class M_PublishingHouse : DbEntity
{
    public M_PublishingHouse()
    {
        Name = "Publishing House";
    }

    public string Name { get; set; }
    public virtual ICollection<M_Book> Books { get; set; } = new List<M_Book>();
}
