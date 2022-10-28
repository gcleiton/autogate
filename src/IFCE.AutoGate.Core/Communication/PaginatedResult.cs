using IFCE.AutoGate.Core.Contracts;

namespace IFCE.AutoGate.Core.Communication;

public class PaginatedResult<T> : IPaginatedResult<T> where T : class
{
    public PaginatedResult(IPaginatedList<T> paginatedList)
    {
        Items = paginatedList;
        Pagination = paginatedList.ToPagination();
    }

    public IList<T> Items { get; set; }
    public IPagination Pagination { get; set; }
}
