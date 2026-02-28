using System;
using System.Collections.Generic;

namespace BookSwap.Api.Models;

public partial class Message
{
    public int Id { get; set; }

    public int? TransactionId { get; set; }

    public int? SenderId { get; set; }

    public string MessageText { get; set; } = null!;

    public DateTime? SentAt { get; set; }

    public virtual User? Sender { get; set; }

    public virtual Transaction? Transaction { get; set; }
}
