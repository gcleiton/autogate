namespace IFCE.AutoGate.Core.Contracts;

public interface IPaginationResult<T> where T : IEntity
{
    public IList<T> Items { get; set; }
    public IPagination Pagination { get; set; }
}
