namespace IFCE.AutoGate.Core.Contracts;

public interface IPaginatedResult<T> where T : class
{
    IList<T> Items { get; set; }
    IPagination Pagination { get; set; }
}
