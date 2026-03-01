using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookSwap.Mvc.Models;

public partial class PhysicalItem
{
    public int Id { get; set; }

    [Display(Name = "Книга")]
    public int? BookId { get; set; }

    [Display(Name = "Власник")]
    public int? OwnerId { get; set; }

    [Display(Name = "Статус")]
    public string? Status { get; set; }

    [Display(Name = "Опис")]
    public string? Description { get; set; }

    [Display(Name = "Створено")]
    public DateTime? CreatedAt { get; set; }

    [Display(Name = "Оновлено")]
    public DateTime? UpdatedAt { get; set; }

    [Display(Name = "Книга")]
    public virtual BookCatalog? Book { get; set; }

    [Display(Name = "Власник")]
    public virtual User? Owner { get; set; }

    [Display(Name = "Транзакції")]
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
