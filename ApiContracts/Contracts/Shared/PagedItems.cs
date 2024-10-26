namespace Shared.Contracts.Shared;

public class PagedItems<T>
{
    public IList<T> Items { get; init; } = new List<T>(); 
    public int TotalCount { get; init; }
    public int PageSize { get; init; }
    public int CurrentPage { get; init; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
 
}
