using LiWiMus.SharedKernel;

namespace LiWiMus.Web.MVC.Areas.Search.ViewModels;

public class SearchViewModel
{
    private string _title;
    public string Title
    {
        get => _title;
        // ReSharper disable once ConstantNullCoalescingCondition
        set => _title = value ?? "";
    }

    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public int Page { get; set; }
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
    public int ItemsPerPage { get; set; }

    // ReSharper disable once MemberCanBePrivate.Global
    public SearchViewModel()
    {
        SortViewModel = new SortViewModel
        {
            Order = Order.Desc,
            SortingBy = SortingBy.Popularity
        };
        _title = "";
        Page = 1;
        ItemsPerPage = 3;
    }

    public static readonly SearchViewModel Default = new();
    public SortViewModel SortViewModel { get; set; }
}