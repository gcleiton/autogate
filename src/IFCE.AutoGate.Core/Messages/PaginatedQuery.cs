using IFCE.AutoGate.Core.Contracts;

namespace IFCE.AutoGate.Core.Messages;

public abstract class PaginatedQuery<TEntity, TResult> : Query<TEntity, IPaginatedResult<TResult>>
    where TEntity : IEntity
    where TResult : class
{
    public static int PageSizeMaximum = 100;

    private int _pageSize { get; set; } = 10;

    public int Page { get; set; } = 1;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > PageSizeMaximum ? PageSizeMaximum : value;
    }

    public abstract IPaginatedList<TResult> ToPaginatedList(ref IQueryable<TEntity> query);
}
