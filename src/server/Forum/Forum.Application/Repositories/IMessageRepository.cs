using Forum.Domain.Entities;

namespace Forum.Application.Repositories;

public interface IMessageRepository
{
    Task<IEnumerable<Message>> GetMessagesAsync(int count);
}