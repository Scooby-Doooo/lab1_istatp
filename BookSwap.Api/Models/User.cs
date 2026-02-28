using System;
using System.Collections.Generic;

namespace BookSwap.Api.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public int? CityId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual City? City { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<PhysicalItem> PhysicalItems { get; set; } = new List<PhysicalItem>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
