using SharedKernel.Data;

namespace Domain.Entities;

public sealed class Message : BaseEntity<int>
{
    public string Text { get; set; } = default!;
    public string? FileKey { get; set; }

    public User UserFrom { get; set; } = default!;
    public User? UserTo { get; set; }
}