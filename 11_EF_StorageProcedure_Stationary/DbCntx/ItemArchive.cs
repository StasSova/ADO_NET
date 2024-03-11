using System;
using System.Collections.Generic;

namespace DbCntx;

public partial class ItemArchive
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int TypeId { get; set; }

    public int Count { get; set; }

    public decimal CostPrice { get; set; }

    public decimal Price { get; set; }

    public DateTime? DeletedDate { get; set; }
}
