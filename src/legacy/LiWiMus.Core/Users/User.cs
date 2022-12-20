using System.ComponentModel.DataAnnotations;
using LiWiMus.Core.Artists;
using LiWiMus.Core.Chats;
using LiWiMus.Core.Constants;
using LiWiMus.Core.FollowingUsers;
using LiWiMus.Core.LikedAlbums;
using LiWiMus.Core.LikedArtists;
using LiWiMus.Core.LikedPlaylists;
using LiWiMus.Core.LikedSongs;
using LiWiMus.Core.Plans;
using LiWiMus.Core.Playlists;
using LiWiMus.Core.Roles;
using LiWiMus.Core.Users.Enums;

namespace LiWiMus.Core.Users;

public class User : BaseUserEntity
{
    [StringLength(50)]
    [RegularExpression(RegularExpressions.DisableTags)]
    public string? FirstName { get; set; }

    [MaxLength(50)]
    [RegularExpression(RegularExpressions.DisableTags)]
    public string? SecondName { get; set; }
    
    [StringLength(50)]
    [RegularExpression(RegularExpressions.DisableTags)]
    public string? Patronymic { get; set; }
    
    public DateOnly? BirthDate { get; set; }
    public Gender? Gender { get; set; }

    public decimal Balance { get; set; }

    public string? AvatarLocation { get; set; }

    public List<UserArtist> UserArtists { get; set; } = new();
    public List<Artist> Artists { get; set; } = new();
    
    public List<LikedAlbum> LikedAlbums { get; set; } = new();
    public List<LikedArtist> LikedArtists { get; set; } = new();
    public List<LikedPlaylist> LikedPlaylists { get; set; } = new();
    public List<LikedSong> LikedSongs { get; set; } = new();
    public List<FollowingUser> Followers { get; set; } = new();
    public List<FollowingUser> Following { get; set; } = new();
    public List<Chat> UserChats { get; set; } = new();
    public List<Playlist> Playlists { get; set; } = new();
    public List<Role> Roles { get; set; } = new();
    public List<UserPlan> Plans { get; set; } = new();
}