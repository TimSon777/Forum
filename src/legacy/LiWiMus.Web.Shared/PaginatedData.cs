namespace LiWiMus.Web.Shared;

public class PaginatedData<T> where T : class
{
    public int ActualPage { get; }

    public int ItemsPerPage { get; }

    public long TotalItems { get; }

    public int TotalPages { get; }

    public IEnumerable<T> Data { get; }

    public PaginatedData(int pageIndex, int pageSize, long count, IEnumerable<T> data)
    {
        ActualPage = pageIndex;
        ItemsPerPage = pageSize;
        TotalItems = count;
        TotalPages = (int) Math.Ceiling((decimal) count / pageSize);
        Data = data;
    }
}