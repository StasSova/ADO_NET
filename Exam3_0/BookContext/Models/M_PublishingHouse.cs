using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Models;

public class M_PublishingHouse : DbEntity
{
    public string Name { get; set; }
    public virtual ICollection<M_Book> Books { get; set; }
}
