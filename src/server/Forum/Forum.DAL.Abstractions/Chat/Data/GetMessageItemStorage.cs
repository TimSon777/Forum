namespace Forum.DAL.Abstractions.Chat.Data;

// ReSharper disable once ClassNeverInstantiated.Global
public class GetMessageItemStorage
{
    public GetUserItemStorage User { get; set; } = null!;
    public string Text { get; set; } = "";
    public string? FileKey { get; set; }
}