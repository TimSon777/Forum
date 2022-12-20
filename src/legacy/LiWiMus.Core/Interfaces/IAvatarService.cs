namespace LiWiMus.Core.Interfaces;

public interface IAvatarService
{
    Task SetRandomAvatarAsync(User user);
}