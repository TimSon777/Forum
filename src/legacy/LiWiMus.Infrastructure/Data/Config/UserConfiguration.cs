using LiWiMus.Core.Artists;
using LiWiMus.Core.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiWiMus.Infrastructure.Data.Config;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Gender)
               .HasConversion<string>();

        builder.HasMany(u => u.Artists)
               .WithMany(a => a.Owners)
               .UsingEntity<UserArtist>(
                   j => j
                        .HasOne(ua => ua.Artist)
                        .WithMany(a => a.UserArtists)
                        .HasForeignKey(ua => ua.ArtistId),
                   j => j
                        .HasOne(ua => ua.User)
                        .WithMany(u => u.UserArtists)
                        .HasForeignKey(ua => ua.UserId));
    }
}