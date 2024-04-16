using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Models;

public class M_Book : DbEntity
{
    public M_Book()
    {
        DateOfPress = DateTime.Now;
    }
    public string Title { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
    public int NumberOfPage { get; set; }
    public DateTime DateOfPress { get; set; }
    public int? PublishingHouseId { get; set; }
    public virtual M_PublishingHouse? PublishingHouse { get; set; }
    public int? AuthorId { get; set; }
    public virtual M_Author Author { get; set; }
    public virtual ICollection<M_Genre> Genres { get; set; } = new List<M_Genre>();
    public decimal CostPrice { get; set; }
    public decimal SellingPrice { get; set; }
    public decimal Discount { get; set; }
    public virtual ICollection<M_ShoppingCart> ShoppingCarts { get; set; }
}