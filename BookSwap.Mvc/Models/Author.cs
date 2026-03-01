using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookSwap.Mvc.Models;

public partial class Author
{
    public int Id { get; set; }

    [Display(Name = "Повне ім'я")]
    public string FullName { get; set; } = null!;

    [Display(Name = "Книги")]
    public virtual ICollection<BookCatalog> Books { get; set; } = new List<BookCatalog>();
}
