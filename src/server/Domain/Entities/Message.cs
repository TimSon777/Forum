using System.ComponentModel.DataAnnotations.Schema;
using SharedKernel.Data;

namespace Domain.Entities;

[Table("Messages")]
public sealed class Message : BaseEntity<int>
{
    public string Text { get; set; } = default!;
    public string? FileKey { get; set; }

    public User UserFrom { get; set; } = default!;
    public User? UserTo { get; set; }
}