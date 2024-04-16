using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Models;

public class M_Genre : DbEntity
{
    public string Genre { get; set; }
    public virtual ICollection<M_Book> Books { get; set; } = new List<M_Book>();
}