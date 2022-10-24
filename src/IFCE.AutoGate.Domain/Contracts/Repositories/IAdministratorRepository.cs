using IFCE.AutoGate.Core.Contracts;
using IFCE.AutoGate.Core.DomainObjects;
using IFCE.AutoGate.Core.Messages;
using IFCE.AutoGate.Domain.Entities;

namespace IFCE.AutoGate.Domain.Contracts.Repositories;

public interface IAdministratorRepository : IRepository<Administrator>
{
    public Task<PaginatedList<Administrator>> LoadAll(
        PaginatedQuery<Administrator, IPaginationResult<Administrator>> query);

    public Task<bool> CheckByEmail(string email);
    public Task<Administrator> LoadByEmail(string email);
    public Task<Administrator> LoadByRecoveryPasswordCode(Guid code);
    public void Add(Administrator administrator);
    public void Update(Administrator administrator);
}
