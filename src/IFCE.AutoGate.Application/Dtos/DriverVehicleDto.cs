namespace IFCE.AutoGate.Application.Dtos;

public class DriverVehicleDto
{
    public Guid Id { get; set; }
    public string Plate { get; set; }
    public string Model { get; set; }
    public string CardNumber { get; set; }
    public Guid CategoryId { get; set; }
}
