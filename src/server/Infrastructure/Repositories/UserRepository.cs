using Application.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class UserRepository : RepositoryBase<User>, IUserRepository
{
    private readonly ForumDbContext _context;

    public UserRepository(ForumDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<User?> FindUserAsync(string userName, bool isAdmin)
    {
        return await _context.Users
            .Include(u => u.Connections)
            .Include(u => u.Mate)
            .FirstOrDefaultAsync(u => u.Name == userName && u.IsAdmin == isAdmin);
    }

    public async Task<User?> FindActiveUserWithoutMateAsync(bool isAdmin)
    {
        return await _context.Users
            .Include(u => u.Connections)
            .Include(u => u.Mate)
            .FirstOrDefaultAsync(u => u.IsAdmin == isAdmin && u.Mate == null && u.Connections.Any());
    }
    
    public async Task<User> GetUserAsync(string userName, bool isAdmin)
    {
        return await FindUserAsync(userName, isAdmin)
               ?? throw new InvalidOperationException($"User by user name {userName} and isAdmin = {isAdmin} not found");
    }

    public async Task<User> GetUserAsync(string userName)
    {
        return await _context.Users
            .Include(u => u.Connections)
            .Include(u => u.Mate)
            .FirstAsync(u => u.Name == userName);
    }
}