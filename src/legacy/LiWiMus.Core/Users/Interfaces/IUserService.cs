using LiWiMus.Core.Shared;

namespace LiWiMus.Core.Users.Interfaces;

public interface IUserService
{
    Task<Subscription> SubscribeOrUnsubscribeAsync(User follower, User following);
}