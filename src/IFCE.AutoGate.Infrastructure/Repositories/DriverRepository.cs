using IFCE.AutoGate.Domain.Contracts.Repositories;
using IFCE.AutoGate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IFCE.AutoGate.Infrastructure.Repositories;

public class DriverRepository : Repository<Driver>, IDriverRepository
{
    public DriverRepository(ApplicationDbContext context) : base(context)
    {
    }

    public void Add(Driver driver)
    {
        _context.Drivers.Add(driver);
    }

    public Task<bool> CheckByVehiclePlates(IEnumerable<string> plates)
    {
        return _context.Vehicles.AnyAsync(v => plates.Contains(v.Plate));
    }
}
