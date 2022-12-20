using Ardalis.GuardClauses;
using LiWiMus.Core.FollowingUsers;
using LiWiMus.Core.Shared;
using LiWiMus.Core.Users;
using LiWiMus.Core.Users.Interfaces;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> _userRepository;

    public UserService(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Subscription> SubscribeOrUnsubscribeAsync(User follower, User following)
    {
        Guard.Against.Null(follower, nameof(follower));
        Guard.Against.Null(following, nameof(following));
        Guard.Against.Null(following.Followers, nameof(following.Followers));
        
        var followingUser = follower.Following.FirstOrDefault(x => x.Following == following);
        if (followingUser is null)
        {
            follower.Following.Add(new FollowingUser
            {
                Following = following
            });

            await _userRepository.SaveChangesAsync();
            return Subscription.Subscribed;
        }

        following.Followers.Remove(followingUser);
        await _userRepository.SaveChangesAsync();
        return Subscription.Unsubscribed;
    }
}