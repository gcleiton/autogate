using System.Linq.Expressions;
using IFCE.AutoGate.Domain.Entities;

namespace IFCE.AutoGate.Domain.Contracts.Repositories;

public interface IAdministratorRepository : IRepository<Administrator>
{
    public Task<bool> Any(Expression<Func<Administrator, bool>> predicate);
    public Task<bool> CheckByEmail(string email);
    public Task<Administrator> LoadById(Guid id);
    public Task<Administrator> LoadByEmail(string email);
    public Task<Administrator> LoadByRecoveryPasswordCode(Guid code);
    public void Add(Administrator administrator);
    public void Update(Administrator administrator);
    public void Remove(Administrator administrator);
}
