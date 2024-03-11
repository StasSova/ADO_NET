using System;
using System.Collections.Generic;

namespace DbCntx;

public partial class CompanyArchive
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? DeletedDate { get; set; }
}
