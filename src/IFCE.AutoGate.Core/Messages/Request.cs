using MediatR;

namespace IFCE.AutoGate.Core.Messages;

public abstract class Request<T> : Message, IRequest<T>
{
}
