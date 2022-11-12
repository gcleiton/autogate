using IFCE.AutoGate.Application.Dtos;
using IFCE.AutoGate.Core.Messages;

namespace IFCE.AutoGate.Application.UseCases.LoadDriverById;

public class LoadDriverByIdQuery : Query<DriverDto>
{
    public LoadDriverByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}
