using IFCE.AutoGate.Domain.Entities;

namespace IFCE.AutoGate.Domain.Contracts.Repositories;

public interface IDriverRepository : IRepository<Driver>
{
    void Add(Driver driver);
    void Update(Driver driver);

    Task<bool> CheckByVehiclePlates(IEnumerable<string> plates);

    Task<bool> CheckByVehiclePlates(IEnumerable<string> plates, Guid exceptDriverId);
}
