namespace IFCE.AutoGate.Application.Dtos;

public class AuthorizedTransitDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string License { get; set; }
    public string PhotoUrl { get; set; }
    public string VehiclePlate { get; set; }
    public string VehicleModel { get; set; }
    public string VehicleCategory { get; set; }
    public int TransitType { get; set; }
    public int TransitTotal { get; set; }
    public IEnumerable<LastTransitDto> LastTransits { get; set; }
}

public class LastTransitDto
{
    public DateTime TransitAt { get; set; }
    public string VehicleCategory { get; set; }
    public string VehiclePlate { get; set; }
    public string VehicleModel { get; set; }
    public int TransitType { get; set; }
}
