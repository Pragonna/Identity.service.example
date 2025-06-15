using Shared.DataAccess.Common;

namespace Shared.DataAccess;

public class Paginate<TEntity> where TEntity : Entity
{
    public List<TEntity> Items { get; set; } = [];
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPrevious => PageIndex > 1;
    public bool HasNext => PageIndex < TotalPages;

    public Paginate(List<TEntity> items, int pageIndex, int pageSize)
    {
        Items = items;
        PageIndex = pageIndex;
        PageSize = pageSize;
    }
}