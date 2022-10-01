using IFCE.AutoGate.Domain.Entities;

namespace IFCE.AutoGate.Domain.Contracts.Repositories;

public interface IAdministratorRepository : IRepository<Administrator>
{
    public Task<bool> CheckByEmail(string email);
    public void Add(Administrator administrator);
}
