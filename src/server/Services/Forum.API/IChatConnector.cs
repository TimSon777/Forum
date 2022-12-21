using Forum.Handler.Data;

namespace Forum.API;

public interface IChatConnector
{
    Task ConnectAsync(string userName, string connectionId, bool isAdmin);
    Task DisconnectAsync(string userName, string connectionId, bool isAdmin);
    Task SendMessageAsync(string userName, SendMessageHubItem message);
}