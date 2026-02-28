using System;
using System.Collections.Generic;

namespace BookSwap.Api.Models;

public partial class BookCatalog
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Isbn { get; set; }

    public virtual ICollection<PhysicalItem> PhysicalItems { get; set; } = new List<PhysicalItem>();

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();

    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
}
