using IFCE.AutoGate.Core.Contracts;
using MediatR;

namespace IFCE.AutoGate.Core.Messages;

public abstract class Command : Message, IRequest<IResult>
{
}
