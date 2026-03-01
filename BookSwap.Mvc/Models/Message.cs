using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookSwap.Mvc.Models;

public partial class Message
{
    public int Id { get; set; }

    [Display(Name = "Транзакція")]
    public int? TransactionId { get; set; }

    [Display(Name = "Відправник")]
    public int? SenderId { get; set; }

    [Display(Name = "Текст повідомлення")]
    public string MessageText { get; set; } = null!;

    [Display(Name = "Відправлено")]
    public DateTime? SentAt { get; set; }

    [Display(Name = "Відправник")]
    public virtual User? Sender { get; set; }

    [Display(Name = "Транзакція")]
    public virtual Transaction? Transaction { get; set; }
}
