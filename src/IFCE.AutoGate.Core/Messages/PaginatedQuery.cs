using IFCE.AutoGate.Core.Contracts;

namespace IFCE.AutoGate.Core.Messages;

public abstract class PaginatedQuery<TEntity, TResponse> : Query<TEntity, TResponse>
    where TEntity : IEntity
    where TResponse : IPaginationResult<TEntity>
{
    public static int PageSizeMaximum = 100;

    private int _pageSize { get; set; } = 10;

    public int Page { get; set; } = 1;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > PageSizeMaximum ? PageSizeMaximum : value;
    }
}
