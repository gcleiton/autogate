using IFCE.AutoGate.Core.Contracts;

namespace IFCE.AutoGate.Core.Communication;

public class PaginationResult<T> : IPaginationResult<T> where T : IEntity
{
    public PaginationResult()
    {
        Items = new List<T>();
        Pagination = new Pagination();
    }

    public IList<T> Items { get; set; }
    public IPagination Pagination { get; set; }
}
