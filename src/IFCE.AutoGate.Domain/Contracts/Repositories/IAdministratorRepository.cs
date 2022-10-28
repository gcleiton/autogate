using IFCE.AutoGate.Domain.Entities;

namespace IFCE.AutoGate.Domain.Contracts.Repositories;

public interface IAdministratorRepository : IRepository<Administrator>
{
    public Task<bool> CheckByEmail(string email);
    public Task<Administrator> LoadByEmail(string email);
    public Task<Administrator> LoadByRecoveryPasswordCode(Guid code);
    public void Add(Administrator administrator);
    public void Update(Administrator administrator);
}
