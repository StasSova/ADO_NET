using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_DbContext.Models;

public class M_Ganre : DbEntity
{
    public M_Ganre()
    {
        Genre = "Genre";
    }

    public string Genre { get; set; }
    public virtual ICollection<M_Book> Books { get; set; } = new List<M_Book>();
}

