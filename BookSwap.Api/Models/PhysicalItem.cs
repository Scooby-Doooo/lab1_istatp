using System;
using System.Collections.Generic;

namespace BookSwap.Api.Models;

public partial class PhysicalItem
{
    public int Id { get; set; }

    public int? BookId { get; set; }

    public int? OwnerId { get; set; }

    public string? Status { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual BookCatalog? Book { get; set; }

    public virtual User? Owner { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
