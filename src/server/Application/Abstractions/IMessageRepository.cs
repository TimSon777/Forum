using Domain.Entities;

namespace Application.Abstractions;

public interface IMessageRepository
{
    Task<IEnumerable<Message>> GetMessagesAsync(int count);
}