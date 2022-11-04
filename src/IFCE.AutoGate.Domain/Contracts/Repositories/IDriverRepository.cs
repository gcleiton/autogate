using IFCE.AutoGate.Domain.Entities;

namespace IFCE.AutoGate.Domain.Contracts.Repositories;

public interface IDriverRepository : IRepository<Driver>
{
    public void Add(Driver driver);

    public Task<bool> CheckByVehiclePlates(IEnumerable<string> plates);
}
