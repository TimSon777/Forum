using SharedKernel.Data;

namespace Queue.Listener.Forum.Database.Entities;

public sealed class User : BaseEntity
{
    public string Name { get; set; } = default!;
}