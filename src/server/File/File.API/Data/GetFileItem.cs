namespace File.API.Data;

public class GetFileItem
{
    public string Name { get; set; } = "";
    public Stream Body { get; set; } = null!;
}