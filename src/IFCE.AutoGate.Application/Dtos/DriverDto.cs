using IFCE.AutoGate.Domain.Entities;

namespace IFCE.AutoGate.Application.Dtos;

public class DriverDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string BirthDate { get; set; }
    public string Phone { get; set; }
    public string License { get; set; }
    public IEnumerable<VehicleDto> Vehicles { get; set; }

    public static DriverDto FromEntity(Driver driver)
    {
        return new DriverDto
        {
            Id = driver.Id,
            Name = driver.Name,
            Email = driver.Email,
            BirthDate = driver.BornAt.ToString(),
            License = driver.License,
            Phone = driver.Phone,
            Vehicles = driver.Vehicles.Select(v => new VehicleDto
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
        };
    }
}

public class VehicleDto
{
    public Guid Id { get; set; }
    public string Plate { get; set; }
    public string Model { get; set; }
    public string CardNumber { get; set; }
    public CategoryDto Category { get; set; }
}

public class CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
