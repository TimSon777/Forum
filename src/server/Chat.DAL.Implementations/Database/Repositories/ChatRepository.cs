using System.Data;
using System.Data.Common;
using Chat.DAL.Abstractions.Chat;
using Chat.DAL.Abstractions.Chat.Data;
using Dapper;
using Microsoft.Extensions.Logging;

namespace Chat.DAL.Implementations.Database.Repositories;

public class ChatRepository : IChatRepository
{
    private readonly IDbConnection _connection;
    private readonly ILogger<ChatRepository> _logger;

    public ChatRepository(IDbConnection connection, ILogger<ChatRepository> logger)
    {
        _connection = connection;
        _logger = logger;
    }

    public async Task<bool> AddMessageAsync(AddMessageStorageItem messageStorageItem)
    {
        try
        {
            const string userAddQuery = 
                @"
                    INSERT INTO ""Users"" (""Name"")
                    VALUES (@userName) ON CONFLICT DO NOTHING;
                ";
        
            const string messageAddQuery =
                @"
                    INSERT INTO ""Messages""(""UserId"", ""Text"")
                    SELECT ""Id"", (@text) 
                    FROM ""Users"" 
                    WHERE ""Name"" = (@userName)
                ";

            const string query = userAddQuery + messageAddQuery;

            await _connection.ExecuteAsync(query, new { userName = messageStorageItem.User.Name, text = messageStorageItem.Text });
            return true;
        }
        catch (DbException ex)
        {
            _logger.LogException(ex);
            return false;
        }
    }
    
    public async Task<IEnumerable<GetMessageItemStorage>?> GetMessagesAsync(int count)
    {
        try
        {
            const string query = 
                @"
                    SELECT *
                    FROM (
                        SELECT m.""Text"" MessageText, m.""Id"" ""Id"", u.""Name"" UserName
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