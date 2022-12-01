namespace File.Domain.Data;

public class FileItem
{
    public string Name { get; set; } = "";
    public Stream Body { get; set; } = null!;
    public string ContentType { get; set; } = default!;
}