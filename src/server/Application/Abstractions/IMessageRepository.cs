using Domain.Data;
using Domain.Entities;

namespace Application.Abstractions;

public interface IMessageRepository
{
    Task<IEnumerable<Message>> GetMessagesAsync(string userName, int count);
    Task SaveMessageAsync(SaveMessageItem saveMessageItem);
}