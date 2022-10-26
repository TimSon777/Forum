using Forum.DAL.Abstractions.Chat.Data;

namespace Forum.DAL.Abstractions.Chat;

public interface IChatRepository
{
    Task<IEnumerable<GetMessageItemStorage>?> GetMessagesAsync(int count);
}