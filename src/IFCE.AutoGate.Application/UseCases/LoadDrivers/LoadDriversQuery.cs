using IFCE.AutoGate.Application.Dtos;
using IFCE.AutoGate.Core.Contracts;
using IFCE.AutoGate.Core.DomainObjects;
using IFCE.AutoGate.Core.Extensions;
using IFCE.AutoGate.Core.Messages;
using IFCE.AutoGate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IFCE.AutoGate.Application.UseCases.LoadDrivers;

public class LoadDriversQuery : PaginatedQuery<Driver, DriverDto>
{
    public string? Name { get; set; }
    public string? Plate { get; set; }
    public string? Model { get; set; }

    public override void ApplyFilter(ref IQueryable<Driver> query)
    {
        if (!string.IsNullOrEmpty(Name)) query = query.Where(d => d.Name.Contains(Name));
        if (!string.IsNullOrEmpty(Plate))
            query = query.Where(d =>
                d.Vehicles.Any(v => EF.Functions.ILike(v.Plate, $"%{Plate}%")));
        if (!string.IsNullOrEmpty(Model))
            query = query.Where(d =>
                d.Vehicles.Any(v => EF.Functions.ILike(v.Model, $"%{Model}%")));
    }

    public override void ApplyOrder(ref IQueryable<Driver> query)
    {
        if (OrderDirection.EqualsIgnoreCase("asc"))
            query = OrderBy switch
            {
                "Name" => query.OrderBy(a => a.Name),
                _ => query.OrderBy(a => a.CreatedAt)
            };
        else
            query = OrderBy switch
            {
                "Name" => query.OrderByDescending(a => a.Name),
                _ => query.OrderByDescending(a => a.CreatedAt)
            };
    }

    public override IPaginatedList<DriverDto> ToPaginatedList(ref IQueryable<Driver> query)
    {
        var queryable = query.Select(d => new DriverDto
        {
            Id = d.Id,
            Name = d.Name,
            Email = d.Email,
            BirthDate = d.BornAt.ToString(),
            License = d.License,
            Phone = d.Phone,
            CardNumber = d.Tag,
            Vehicles = d.Vehicles.Select(v => new VehicleDto
            {
                Id = v.Id,
                Model = v.Model,
                Plate = v.Plate,
                Category = new CategoryDto
                {
                    Id = v.Category.Id,
                    Name = v.Category.Name
                }
            })
        });

        return new PaginatedList<DriverDto>(queryable, Page, PageSize);
    }
}
