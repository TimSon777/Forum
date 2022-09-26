namespace Chat.DAL.Abstractions.Chat;

public interface IChatRepository
{
    Task<bool> AddMessageAsync(AddMessageItem messageItem);
    Task<IEnumerable<GetMessageItem>?> GetMessagesAsync(int count);
}