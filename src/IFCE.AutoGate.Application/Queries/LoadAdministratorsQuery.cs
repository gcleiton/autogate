using IFCE.AutoGate.Core.Contracts;
using IFCE.AutoGate.Core.Extensions;
using IFCE.AutoGate.Core.Messages;
using IFCE.AutoGate.Domain.Entities;

namespace IFCE.AutoGate.Application.Queries;

public class LoadAdministratorsQuery : PaginatedQuery<Administrator, IPaginationResult<Administrator>>
{
    public string? Name { get; set; }
    public string? Email { get; set; }

    public override void ApplyFilter(ref IQueryable<Administrator> query)
    {
        if (!string.IsNullOrEmpty(Name)) query = query.Where(a => a.Name.Contains(Name));
        if (!string.IsNullOrEmpty(Email)) query = query.Where(a => a.Email.Contains(Email));
    }

    public override void ApplyOrder(ref IQueryable<Administrator> query)
    {
        if (OrderDirection.EqualsIgnoreCase("asc"))
            query = OrderBy switch
            {
                "Name" => query.OrderBy(a => a.Name),
                "Email" => query.OrderBy(a => a.Email),
                _ => query.OrderBy(a => a.CreatedAt)
            };
        else
            query = OrderBy switch
            {
                "Name" => query.OrderByDescending(a => a.Name),
                "Email" => query.OrderByDescending(a => a.Email),
                _ => query.OrderByDescending(a => a.CreatedAt)
            };
    }
}
