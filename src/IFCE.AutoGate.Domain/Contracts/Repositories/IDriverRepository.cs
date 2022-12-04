using IFCE.AutoGate.Domain.Entities;

namespace IFCE.AutoGate.Domain.Contracts.Repositories;

public interface IDriverRepository : IRepository<Driver>
{
    Task<Driver> LoadById(Guid id);
    Task<Vehicle> LoadVehicleByTag(string tag);
    void Add(Driver driver);
    void Update(Driver driver);

    void AddTransit(Transit transit);
    Task<Transit> LoadTransitById(Guid id);
    Task<int> CountTransitQuantityByDriverId(Guid id);
    Task<IEnumerable<Transit>> LoadTransitsByDriverId(Guid id, int quantity);
    Task<bool> CheckByVehiclePlates(IEnumerable<string> plates);
    Task<bool> CheckByVehicleTags(IEnumerable<string> tags);

    Task<bool> CheckByVehiclePlates(IEnumerable<string> plates, Guid exceptDriverId);
}
