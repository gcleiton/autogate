using IFCE.AutoGate.Application.Dtos;
using IFCE.AutoGate.Core.Messages;
using Microsoft.AspNetCore.Http;

namespace IFCE.AutoGate.Application.UseCases.UpdateDriver;

public class UpdateDriverCommand : Command<bool>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IFormFile? Photo { get; set; }
    public string Email { get; set; }
    public DateOnly BirthDate { get; set; }
    public string Phone { get; set; }
    public string License { get; set; }

    public IEnumerable<DriverVehicleDto> Vehicles { get; set; }
}
