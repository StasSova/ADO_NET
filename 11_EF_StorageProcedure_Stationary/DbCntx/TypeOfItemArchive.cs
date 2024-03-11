using System;
using System.Collections.Generic;

namespace DbCntx;

public partial class TypeOfItemArchive
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public DateTime? DeletedDate { get; set; }
}
