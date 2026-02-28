using System;
using System.Collections.Generic;

namespace BookSwap.Api.Models;

public partial class Transaction
{
    public int Id { get; set; }

    public int? RequesterId { get; set; }

    public int? TargetItemId { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual User? Requester { get; set; }

    public virtual PhysicalItem? TargetItem { get; set; }
}
