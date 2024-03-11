using System;
using System.Collections.Generic;

namespace DbCntx;

public partial class ManagerArchive
{
    public int Id { get; set; }

    public string Fname { get; set; } = null!;

    public string Lname { get; set; } = null!;

    public DateTime? DeletedDate { get; set; }
}
