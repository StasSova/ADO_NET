using System;
using System.Collections.Generic;

namespace DbCntx;

public partial class TypeOfItem
{
    public TypeOfItem()
    {
        Type = "Type";
        Count = 0;
    }
    public TypeOfItem(TypeOfItem typeOfItem)
    {
        if (typeOfItem != null)
        {
            Id = typeOfItem.Id;
            Type = typeOfItem.Type;
            Count = typeOfItem.Count;
            Items = typeOfItem.Items;
        }
    }
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public int? Count { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
