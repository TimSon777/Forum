using System.Data;
using System.Data.Common;
using Dapper;
using Forum.DAL.Abstractions.Chat;
using Forum.DAL.Abstractions.Chat.Data;
using Microsoft.Extensions.Logging;

namespace Forum.DAL.Implementations.Database.Repositories;

public class ChatRepository : IChatRepository
{
    private readonly IDbConnection _connection;
    private readonly ILogger<ChatRepository> _logger;

    public ChatRepository(IDbConnection connection, ILogger<ChatRepository> logger)
    {
        _connection = connection;
        _logger = logger;
    }

    public async Task<IEnumerable<GetMessageItemStorage>?> GetMessagesAsync(int count)
    {
        try
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

            return await _connection.QueryAsync<GetMessageItemStorage, GetUserItemStorage, GetMessageItemStorage>(query, (m, u) =>
            { 
                m.User = u;
                return m;
            }, new { count }, splitOn: @"Id");
        }
        catch (DbException ex)
        {
            _logger.LogException(ex);
            return null;
        }
    }
}