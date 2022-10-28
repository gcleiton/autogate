using MediatR;

namespace IFCE.AutoGate.Core.Messages;

public abstract class Command<T> : Message, IRequest<T>
{
}
