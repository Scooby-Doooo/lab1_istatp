using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookSwap.Mvc.Models;

public partial class Transaction
{
    public int Id { get; set; }

    [Display(Name = "Ініціатор")]
    public int? RequesterId { get; set; }

    [Display(Name = "Цільовий предмет")]
    public int? TargetItemId { get; set; }

    [Display(Name = "Статус")]
    public string? Status { get; set; }

    [Display(Name = "Створено")]
    public DateTime? CreatedAt { get; set; }

    [Display(Name = "Повідомлення")]
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    [Display(Name = "Ініціатор")]
    public virtual User? Requester { get; set; }

    [Display(Name = "Цільовий предмет")]
    public virtual PhysicalItem? TargetItem { get; set; }
}
