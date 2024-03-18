using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_DbContext.Models;

public class M_Author : DbEntity
{
    public M_Author()
    {
        FirstName = "First Name";
        LastName = "Last Name";
        DateOfBirth = DateTime.Now;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }

    public virtual ICollection<M_Book> Books { get; set; } = new List<M_Book>();
}
