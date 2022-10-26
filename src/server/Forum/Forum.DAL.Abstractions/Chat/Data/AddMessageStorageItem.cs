namespace Forum.DAL.Abstractions.Chat.Data;

public class AddMessageStorageItem
{
    public AddUserStorageItem User { get; init; } = null!;
    public string Text { get; init; } = "";
}