using FluentValidation;
using IFCE.AutoGate.Core.Contracts;
using IFCE.AutoGate.Core.DomainObjects;

namespace IFCE.AutoGate.Core.Messages;

public abstract class CommandHandler<TRequest>
{
    private readonly INotification _notification;
    private readonly IValidator<TRequest> _validator;

    protected CommandHandler(INotification notification)
    {
        _notification = notification;
    }

    protected CommandHandler(IValidator<TRequest> validator, INotification notification)
    {
        _validator = validator;
        _notification = notification;
    }

    protected IEnumerable<string> Validate(TRequest request)
    {
        var validationResult = _validator.Validate(request);
        return validationResult.Errors.Select(e => e.ErrorMessage);
    }

    protected void AddError(Error error)
    {
        _notification.AddError(error);
    }
}
