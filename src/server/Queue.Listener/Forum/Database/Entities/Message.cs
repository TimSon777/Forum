using SharedKernel.Data;

namespace Queue.Listener.Forum.Database.Entities;

public sealed class Message : BaseEntity
{
    public string Text { get; set; } = default!;
    public User User { get; set; } = default!;
}