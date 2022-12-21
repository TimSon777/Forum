using SharedKernel.Data;

namespace Domain.Entities;

public sealed class Connection : BaseEntity<int>
{
    public User User { get; set; } = default!;
    public string ConnectionId { get; set; } = default!;
}