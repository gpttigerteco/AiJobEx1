namespace AiJobEx1.Shared;

public class PagedResult<T>
{
    public IReadOnlyCollection<T> Items { get; }
    public int PageNumber { get; }
    public int PageSize { get; }
    public long TotalCount { get; }

    public PagedResult(IEnumerable<T> items, int pageNumber, int pageSize, long totalCount)
    {
        Items = items.ToArray();
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;
    }
}
