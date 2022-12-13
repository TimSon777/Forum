namespace SharedKernel.Data;

public class BaseEntity<TKey> : BaseEntity
{
    public TKey Id { get; set; } = default!;
}

public class BaseEntity
{ }