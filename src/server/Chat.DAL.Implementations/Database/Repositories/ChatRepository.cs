using System.Data;
using System.Data.Common;
using Chat.DAL.Abstractions.Chat;
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

    public async Task<bool> AddMessageAsync(AddMessageItem messageItem)
    {
        try
        {
            const string userAddQuery = $"INSERT INTO {Naming.User.TableName}({Naming.User.Name})" +
                                        "VALUES (@userName) ON CONFLICT DO NOTHING;";
        
            const string messageAddQuery =
                $"INSERT INTO {Naming.Message.TableName}({Naming.Message.ForeignKeyUser}, {Naming.Message.Text})" +
                $"SELECT {Naming.User.PrimaryKey}, (@text) FROM {Naming.User.TableName} WHERE {Naming.User.Name} = (@userName)";

            const string query = userAddQuery + messageAddQuery;

            await _connection.ExecuteAsync(query, new { userName = messageItem.User.Name, text = messageItem.Text });
            return true;
        }
        catch (DbException ex)
        {
            _logger.LogException(ex);
            return false;
        }
    }
    
    public async Task<IEnumerable<GetMessageItem>?> GetMessagesAsync(int count)
    {
        try
        {
            var query = $"SELECT * FROM {Naming.Message.TableName} m JOIN {Naming.User.TableName} u" 
                        + $" ON u.{Naming.User.PrimaryKey} = m.{Naming.Message.ForeignKeyUser}" 
                        + $" ORDER BY m.{Naming.Message.PrimaryKey} DESC LIMIT {count}";

            return await _connection.QueryAsync<GetMessageItem, GetUserItem, GetMessageItem>(query, (m, u) =>
            { 
                m.User = u;
                return m;
            });
        }
        catch (DbException ex)
        {
            _logger.LogException(ex);
            return null;
        }
    }
}