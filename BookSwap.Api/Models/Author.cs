using System;
using System.Collections.Generic;

namespace BookSwap.Api.Models;

public partial class Author
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public virtual ICollection<BookCatalog> Books { get; set; } = new List<BookCatalog>();
}
