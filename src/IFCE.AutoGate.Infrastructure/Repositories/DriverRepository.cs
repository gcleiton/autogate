using IFCE.AutoGate.Domain.Contracts.Repositories;
using IFCE.AutoGate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IFCE.AutoGate.Infrastructure.Repositories;

public class DriverRepository : Repository<Driver>, IDriverRepository
{
    public DriverRepository(ApplicationDbContext context) : base(context)
    {
    }

    public Task<Driver> LoadById(Guid id)
    {
        return _context.Drivers
            .AsNoTracking()
            .Include(d => d.Vehicles).ThenInclude(v => v.Category)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public void Add(Driver driver)
    {
        _context.Drivers.Add(driver);
        _context.Vehicles.AttachRange(driver.Vehicles);
    }

    public void Update(Driver driver)
    {
        var existingDriver = _context.Drivers
            .AsNoTracking()
            .Where(d => d.Id == driver.Id).Include(d => d.Vehicles)
            .First();

        _context.Entry(existingDriver).CurrentValues.SetValues(driver);

        foreach (var vehicle in driver.Vehicles)
            _context.Entry(vehicle).State = existingDriver.Vehicles.Any(v => v.Id == vehicle.Id)
                ? EntityState.Modified
                : EntityState.Added;

        _context.Drivers.Update(driver);
    }

    public void AddTransit(Transit transit)
    {
        _context.Transits.Add(transit);
    }

    public Task<Transit> LoadTransitById(Guid id)
    {
        return _context.Transits
            .AsNoTracking()
            .Include(t => t.Driver)
            .Include(t => t.Vehicle).ThenInclude(v => v.Category)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public Task<int> CountTransitQuantityByDriverId(Guid id)
    {
        return _context.Transits.CountAsync(t => t.DriverId == id);
    }

    public async Task<IEnumerable<Transit>> LoadTransitsByDriverId(Guid id, int quantity)
    {
        return _context.Transits.AsNoTracking().Include(t => t.Driver).Include(t => t.Vehicle)
            .ThenInclude(v => v.Category).OrderByDescending(t => t.TransitDate).Skip(0).Take(quantity).ToList();
    }

    public Task<bool> CheckByVehiclePlates(IEnumerable<string> plates)
    {
        return _context.Vehicles.AnyAsync(v => plates.Contains(v.Plate));
    }

    public Task<bool> CheckByVehicleTags(IEnumerable<string> tags)
    {
        return _context.Vehicles.AnyAsync(v => tags.Contains(v.Tag));
    }

    public Task<bool> CheckByVehiclePlates(IEnumerable<string> plates, Guid exceptDriverId)
    {
        return _context.Vehicles.AnyAsync(v => v.DriverId != exceptDriverId && plates.Contains(v.Plate));
    }

    public Task<Vehicle> LoadVehicleByTag(string tag)
    {
        return _context.Vehicles.FirstOrDefaultAsync(v => v.Tag == tag);
    }
}
