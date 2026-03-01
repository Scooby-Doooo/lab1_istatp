using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookSwap.Mvc.Models;

public partial class User
{
    public int Id { get; set; }

    [Display(Name = "Ім'я користувача")]
    public string Username { get; set; } = null!;

    [Display(Name = "Електронна пошта")]
    public string Email { get; set; } = null!;

    [Display(Name = "Пароль")]
    public string PasswordHash { get; set; } = null!;

    [Display(Name = "Місто")]
    public int? CityId { get; set; }

    [Display(Name = "Створено")]
    public DateTime? CreatedAt { get; set; }

    public virtual City? City { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<PhysicalItem> PhysicalItems { get; set; } = new List<PhysicalItem>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
