using System.Collections.Immutable;

namespace Shared.Contracts;

public class PagedResult<T>
{
    public IReadOnlyCollection<T> Items { get; init; } = new List<T>();
    public int TotalCount { get; init; }
    public int PageSize { get; init; }
    public int CurrentPage { get; init; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
}
