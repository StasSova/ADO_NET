using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_DbContext.Models;

public class M_RelationshipType : DbEntity
{
    public M_RelationshipType()
    {
        Relationship = "continue";
    }

    public string Relationship { get; set; }
    public virtual ICollection<M_BookRelationship> BookRelationships { get; set; } = new List<M_BookRelationship>();
}
