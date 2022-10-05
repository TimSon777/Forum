using Chat.DAL.Abstractions.Chat.Data;

namespace Chat.DAL.Abstractions.Chat;

public interface IChatRepository
{
    Task<bool> AddMessageAsync(AddMessageStorageItem messageStorageItem);
    Task<IEnumerable<GetMessageItemStorage>?> GetMessagesAsync(int count);
}