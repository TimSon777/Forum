using Domain.Data;

namespace Application.Abstractions;

public interface IForumRepository
{
    Task SaveMessageAsync(SaveMessageItem message);
}