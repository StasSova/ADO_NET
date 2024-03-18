using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_DbContext.Models;

public class M_BookRelationship : DbEntity
{
    public int FirstBookId { get; set; }
    public virtual M_Book FirstBook { get; set; }
    public int SecondBookId { get; set; }
    public virtual M_Book SecondBook { get; set; }

    public int RelationshipTypeId { get; set; }
    public virtual M_RelationshipType RelationshipType { get; set; }
}
