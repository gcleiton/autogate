using IFCE.AutoGate.Core.Communication;
using IFCE.AutoGate.Core.Contracts;

namespace IFCE.AutoGate.Core.DomainObjects;

public class PaginatedList<T> : List<T>, IPaginatedList<T> where T : IEntity
{
    public PaginatedList(IQueryable<T> query, int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
        Total = query.Count();
        TotalPages = (int)Math.Ceiling(Total / (double)PageSize);

        AddRange(query.Skip((Page - 1) * PageSize).Take(PageSize));
    }

    public bool OnFirstPage => Page == 1;

    public bool HasPreviousPage => Page > 1;

    public bool HasNextPage => Page < TotalPages;

    public bool HasPages => TotalPages > 1;

    public int TotalInPage => Count;

    public int Page { get; }
    public int PageSize { get; }
    public int Total { get; }
    public int TotalPages { get; }

    public IPagination ToPagination()
    {
        return new Pagination
        {
            Total = Total,
            CurrentPage = Page,
            HasPages = HasPages,
            LastPage = TotalPages,
            PerPage = PageSize,
            HasMorePages = HasNextPage,
            OnFirstPage = OnFirstPage,
            TotalInPage = TotalInPage
        };
    }
}
