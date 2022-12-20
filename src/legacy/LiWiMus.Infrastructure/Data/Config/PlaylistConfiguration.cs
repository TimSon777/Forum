using LiWiMus.Core.Playlists;
using LiWiMus.Core.PlaylistTracks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiWiMus.Infrastructure.Data.Config;

public class PlaylistConfiguration : IEntityTypeConfiguration<Playlist>
{
    public void Configure(EntityTypeBuilder<Playlist> builder)
    {
        builder
            .HasMany(playlist => playlist.Tracks)
            .WithMany(track => track.Playlists)
            .UsingEntity<PlaylistTrack>(
                entityBuilder => entityBuilder
                                 .HasOne(pt => pt.Track)
                                 .WithMany(track => track.PlaylistTracks)
                                 .HasForeignKey(pt => pt.TrackId),
                entityBuilder => entityBuilder
                                 .HasOne(pt => pt.Playlist)
                                 .WithMany(playlist => playlist.PlaylistTracks)
                                 .HasForeignKey(pt => pt.PlaylistId)
            );
    }
}