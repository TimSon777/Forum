namespace LiWiMus.Core.Shared;

public class Sort
{
    public SortingBy SortingBy { get; set; }
    public Order Order { get; set; }
}

public enum SortingBy
{
    Popularity,
    Title
}