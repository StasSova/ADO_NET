using System;
using System.Collections.Generic;

namespace DB_Context;

public partial class Author
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
