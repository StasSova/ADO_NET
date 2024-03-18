using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_DbContext.Models;

public class M_Book : DbEntity
{
    public M_Book()
    {
        Title = "Title";
        Description = "Description";
        DateOfPress = DateTime.Now;
    }

    public string Title { get; set; }
    public string Description { get; set; }
    public int NumberOfPage { get; set; }
    public DateTime DateOfPress { get; set; }

    public int? PublishingHouseId { get; set; }
    public virtual M_PublishingHouse PublishingHouse { get; set; }

    public int? AuthorId { get; set; }
    public virtual M_Author Author { get; set; }

    public virtual M_BookForSale BookForSale { get; set; }
    public virtual ICollection<M_Ganre> Ganres { get; set; } = new List<M_Ganre>();
    public virtual ICollection<M_BookRelationship> RelatedBooks { get; set; } = new List<M_BookRelationship>();
}