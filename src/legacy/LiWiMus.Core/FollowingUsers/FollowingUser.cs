namespace LiWiMus.Core.FollowingUsers;

public class FollowingUser : BaseEntity
{
    public User Follower { get; set; } = null!;
    public User Following { get; set; } = null!;

    public int FollowerId { get; set; }
    public int FollowingId { get; set; }
}