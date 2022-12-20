using LiWiMus.Core.FollowingUsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiWiMus.Infrastructure.Data.Config;

public class FollowingUserConfiguration : IEntityTypeConfiguration<FollowingUser>
{
    public void Configure(EntityTypeBuilder<FollowingUser> builder)
    {
        builder.HasOne(lu => lu.Follower)
               .WithMany(u => u.Following);

        builder.HasOne(lu => lu.Following)
               .WithMany(u => u.Followers);
    }
}