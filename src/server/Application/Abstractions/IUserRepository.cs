using Domain.Entities;

namespace Application.Abstractions;

public interface IUserRepository : IRepositoryBase<User>
{
    Task<User?> FindUserAsync(string userName, bool isAdmin);
    Task<User> GetUserAsync(string userName, bool isAdmin);
    Task<User?> FindActiveUserWithoutMateAsync(bool isAdmin);
    Task<User> GetUserAsync(string userName);
}