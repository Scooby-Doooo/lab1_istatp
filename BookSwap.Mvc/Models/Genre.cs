using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookSwap.Mvc.Models;

public partial class Genre
{
    public int Id { get; set; }

    [Display(Name = "Назва жанру")]
    public string Name { get; set; } = null!;

    [Display(Name = "Книги")]
    public virtual ICollection<BookCatalog> Books { get; set; } = new List<BookCatalog>();
}
