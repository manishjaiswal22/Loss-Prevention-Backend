namespace LossPrevention.Api.Models.DTOs.Common;

/// <summary>
/// Standard pagination wrapper for data grids
/// </summary>
/// <typeparam name="T">Collection item type</typeparam>
public class PagedResponse<T>
{
    public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalRecords / (PageSize > 0 ? PageSize : 10));
    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;

    public PagedResponse() { }

    public PagedResponse(IEnumerable<T> items, int count, int pageIndex, int pageSize)
    {
        Items = items;
        TotalRecords = count;
        PageIndex = pageIndex;
        PageSize = pageSize;
    }
}
