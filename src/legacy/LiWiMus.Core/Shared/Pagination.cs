namespace LiWiMus.Core.Shared;

// ReSharper disable once ClassNeverInstantiated.Global
public class Pagination
{
    public string Title { get; set; }
    public int ItemsPerPage { get; set; }
    public int Page { get; set; }
    public Sort Sort { get; set; }
}
