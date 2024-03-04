using System;
using System.Collections.Generic;

namespace DB_Context;

public partial class Book
{
    public int Id { get; set; }

    public int? AuthorId { get; set; }

    public string Name { get; set; } = null!;

    public virtual Author? Author { get; set; }
}
