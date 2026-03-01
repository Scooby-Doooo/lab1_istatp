using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookSwap.Mvc.Models;

public partial class BookCatalog
{
    public int Id { get; set; }

    [Display(Name = "Назва")]
    public string Title { get; set; } = null!;

    [Display(Name = "ISBN")]
    public string? Isbn { get; set; }

    [Display(Name = "Екземпляри")]
    public virtual ICollection<PhysicalItem> PhysicalItems { get; set; } = new List<PhysicalItem>();

    [Display(Name = "Автори")]
    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();

    [Display(Name = "Жанри")]
    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
}
