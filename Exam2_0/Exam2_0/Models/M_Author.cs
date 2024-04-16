using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Models;

public class M_Author : DbEntity
{
    public M_Author()
    {
        DateOfBirth = DateTime.Now;
    }
    public string FullName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public virtual ICollection<M_Book> Books { get; set; }
}
