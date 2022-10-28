using MediatR;

namespace IFCE.AutoGate.Core.Messages;

public abstract class Query<TResponse> : IRequest<TResponse>
{
}
