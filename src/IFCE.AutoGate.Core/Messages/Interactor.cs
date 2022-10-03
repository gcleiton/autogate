using FluentValidation;
using IFCE.AutoGate.Core.Contracts;
using IFCE.AutoGate.Core.DomainObjects;

namespace IFCE.AutoGate.Core.Messages;

public abstract class Interactor<TRequest, TResponse> where TRequest : class where TResponse : class
{
    private readonly INotification _notification;
    private readonly IValidator<TRequest> _validator;

    protected Interactor(IValidator<TRequest> validator, INotification notification)
    {
        _validator = validator;
        _notification = notification;
    }

    public IEnumerable<string> Validate(TRequest request)
    {
        var validationResult = _validator.Validate(request);
        return validationResult.Errors.Select(e => e.ErrorMessage);
    }

    public TResponse Failure(Error error)
    {
        _notification.AddError(error);
        return null;
    }
}
