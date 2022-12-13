using SharedKernel.Data;

namespace Domain.Events;

public sealed class MessageEvent : BaseEvent
{
    public string UserName { get; set; } = default!;
    public string Text { get; set; } = default!;
    public string? FileId { get; set; } = default!;
}