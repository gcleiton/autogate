using FluentValidation;

namespace IFCE.AutoGate.Core.Messages;

public abstract class CommandHandler<T> where T : Command
{
    protected readonly IValidator<T> _validator;

    protected CommandHandler(IValidator<T> validator)
    {
        _validator = validator;
    }

    public IEnumerable<string> Validate(T command)
    {
        var validationResult = _validator.Validate(command);
        return validationResult.Errors.Select(e => e.ErrorMessage);
    }
}
