namespace LiWiMus.Core.Plans;

public static class DefaultPermissions
{
    public static IEnumerable<Permission> GetPremium()
    {
        yield return new Permission
        {
            Name = Track.WithoutAds.Name,
            Description = Track.WithoutAds.Description
        };

        yield return new Permission
        {
            Name = Track.Download.Name,
            Description = Track.Download.Description
        };

        yield return new Permission
        {
            Name = Playlist.Private.Name,
            Description = Playlist.Private.Description
        };

        yield return new Permission
        {
            Name = Playlist.Cover.Name,
            Description = Playlist.Cover.Description
        };

        yield return new Permission
        {
            Name = Avatar.Upload.Name,
            Description = Avatar.Upload.Description
        };
    }

    public static IEnumerable<Permission> GetAll()
    {
        return GetPremium();
    }

    public static class Track
    {
        public static class WithoutAds
        {
            public const string Name = $"{nameof(Track)}.{nameof(WithoutAds)}";
            public const string Description = "Listen without ads";
        }

        public static class Download
        {
            public const string Name = $"{nameof(Track)}.{nameof(Download)}";
            public const string Description = "Download songs";
        }
    }

    public static class Playlist
    {
        public static class Private
        {
            public const string Name = $"{nameof(Playlist)}.{nameof(Private)}";
            public const string Description = "Have private playlists";
        }

        public static class Cover
        {
            public const string Name = $"{nameof(Playlist)}.{nameof(Cover)}";
            public const string Description = "Upload cover for playlists";
        }
    }

    public static class Avatar
    {
        public static class Upload
        {
            public const string Name = $"{nameof(Avatar)}.{nameof(Upload)}";
            public const string Description = "Upload your own avatar";
        }
    }
}