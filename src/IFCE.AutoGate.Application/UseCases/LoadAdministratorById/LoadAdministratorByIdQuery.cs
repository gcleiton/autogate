using IFCE.AutoGate.Application.Results;
using IFCE.AutoGate.Core.Messages;

namespace IFCE.AutoGate.Application.UseCases.LoadAdministratorById;

public class LoadAdministratorByIdQuery : Query<AdministratorResult>
{
    public LoadAdministratorByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}
