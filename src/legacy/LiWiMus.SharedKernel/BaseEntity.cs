using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.SharedKernel;

public abstract class BaseEntity : IAggregateRoot
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}