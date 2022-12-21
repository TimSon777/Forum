using System.ComponentModel.DataAnnotations.Schema;
using SharedKernel.Data;

namespace Domain.Entities;

[Table("Connections")]
public sealed class Connection : BaseEntity
{
    public User User { get; set; } = default!;
    public string ConnectionId { get; set; } = default!;
}