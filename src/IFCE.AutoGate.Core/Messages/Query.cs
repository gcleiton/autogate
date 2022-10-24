using IFCE.AutoGate.Core.Contracts;
using MediatR;

namespace IFCE.AutoGate.Core.Messages;

public abstract class Query<TEntity, TResponse> : IRequest<TResponse>
    where TEntity : IEntity
{
    public static string OrderDirectionDefault = "asc";
    private readonly string[] _orderDirectionOptions = { "asc", "desc" };

    private string _orderDirection { get; set; } = "asc";

    public string OrderBy { get; set; } = "CreatedAt";

    public string OrderDirection
    {
        get => _orderDirection;
        set => _orderDirection =
            _orderDirectionOptions.Contains(value.ToLower()) ? value.ToLower() : OrderDirectionDefault;
    }

    public abstract void ApplyOrder(ref IQueryable<TEntity> query);

    public abstract void ApplyFilter(ref IQueryable<TEntity> query);
}
