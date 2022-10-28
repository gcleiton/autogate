namespace IFCE.AutoGate.Core.Contracts;

public interface IPaginatedList<T> : IList<T> where T : class
{
    int Page { get; }
    int PageSize { get; }
    int Total { get; }
    int TotalPages { get; }
    int TotalInPage { get; }

    bool HasPreviousPage { get; }
    bool HasNextPage { get; }
    bool HasPages { get; }

    IPagination ToPagination();
}
