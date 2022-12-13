namespace Domain.Data;

public class SaveMessageItem
{
     
    public required string UserName { get; set; } = default!;
    public required string Text { get; set; } = default!;
    public required string? FileId { get; set; }
}