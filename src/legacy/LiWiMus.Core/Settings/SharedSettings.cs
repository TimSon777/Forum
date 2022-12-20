namespace LiWiMus.Core.Settings;

public class SharedSettings
{
    public DataSettings DataSettings { get; set; } = null!;
    public string SharedDirectory { get; set; } = null!;

    public void CreateDirectories()
    {
        Directory.CreateDirectory(Path.Combine(SharedDirectory, DataSettings.MusicDirectory));
        Directory.CreateDirectory(Path.Combine(SharedDirectory, DataSettings.PicturesDirectory));
    }
}