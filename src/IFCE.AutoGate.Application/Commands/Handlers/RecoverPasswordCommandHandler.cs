using FluentValidation;
using IFCE.AutoGate.Application.Events;
using IFCE.AutoGate.Core.Communication;
using IFCE.AutoGate.Core.Contracts;
using IFCE.AutoGate.Core.Messages;
using IFCE.AutoGate.Domain.Contracts.Repositories;
using IFCE.AutoGate.Domain.Errors;
using MediatR;

namespace IFCE.AutoGate.Application.Commands.Handlers;

public class RecoverPasswordCommandHandler : CommandHandler<RecoverPasswordCommand>,
    IRequestHandler<RecoverPasswordCommand, IResult>
{
    private readonly IAdministratorRepository _administratorRepository;

    public RecoverPasswordCommandHandler(IValidator<RecoverPasswordCommand> validator,
        IAdministratorRepository administratorRepository) : base(validator)
    {
        _administratorRepository = administratorRepository;
    }

    public async Task<IResult> Handle(RecoverPasswordCommand command, CancellationToken cancellationToken)
    {
        var errors = Validate(command);
        if (errors.Any()) return Result.Failure(new ValidationError(errors));

        var administrator = await _administratorRepository.LoadByEmail(command.Email);
        if (administrator is null) return Result.Failure(new NotFoundError(""));

        administrator.ForgetPassword();

        var forgotPasswordEvent = new PasswordForgotEvent(administrator.Name, administrator.Email,
            administrator.RecoveryPasswordCode ?? Guid.Empty);
        administrator.AddNotification(forgotPasswordEvent);

        _administratorRepository.Update(administrator);
        _administratorRepository.UnitOfWork.Commit();

        return Result.Ok();
    }
}
