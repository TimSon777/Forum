using LiWiMus.SharedKernel;

namespace LiWiMus.Web.MVC.Areas.Search.ViewModels;

public class SortViewModel
{
    public SortingBy SortingBy { get; set; }
    public Order Order { get; set; }
}

public enum SortingBy
{
    Popularity,
    Title
}