using System.ComponentModel.DataAnnotations;

namespace LiWiMus.Core.Transactions;

public class Transaction : BaseEntity
{
    public User User { get; set; } = null!;
    public int UserId { get; set; }
    public decimal Amount { get; set; }

    [StringLength(100)]
    public string Description { get; set; } = null!;
}