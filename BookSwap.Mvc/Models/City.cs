using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookSwap.Mvc.Models;

public partial class City
{
    public int Id { get; set; }

    [Display(Name = "Назва міста")]
    public string Name { get; set; } = null!;

    [Display(Name = "Користувачі")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
