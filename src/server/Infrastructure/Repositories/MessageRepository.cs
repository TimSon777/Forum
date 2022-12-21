using System.Data;
using Application.Abstractions;
using Dapper;
using Domain.Entities;

namespace Infrastructure.Repositories;

public sealed class MessageRepository : IMessageRepository
{
    private readonly IDbConnection _connection;

    public MessageRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<IEnumerable<Message>> GetMessagesAsync(int count)
    {
        const string query = 
            @"
                SELECT *
                FROM (
                    SELECT m.""Text"" MessageText, m.""FileKey"" FileKey, m.""Id"" ""Id"", u.""Name"" UserName
                    FROM ""Messages"" m JOIN ""Users"" u 
                        ON u.""Id"" = m.""UserId""
                    ORDER BY m.""Id"" DESC 
                    LIMIT @count
                ) sub
                ORDER BY ""Id""
            ";

        return await _connection.QueryAsync<Message, User, Message>(query, (m, u) =>
        { 
            m.User = u;
            return m;
        }, new { count }, splitOn: @"Id");
    }
}