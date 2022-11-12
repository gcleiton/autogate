namespace IFCE.AutoGate.Application.Dtos;

public class DriverDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string BirthDate { get; set; }
    public string Phone { get; set; }
    public string License { get; set; }
    public string CardNumber { get; set; }
    public IEnumerable<VehicleDto> Vehicles { get; set; }
}

public class VehicleDto
{
    public Guid Id { get; set; }
    public string Plate { get; set; }
    public string Model { get; set; }
    public CategoryDto Category { get; set; }
}

public class CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
