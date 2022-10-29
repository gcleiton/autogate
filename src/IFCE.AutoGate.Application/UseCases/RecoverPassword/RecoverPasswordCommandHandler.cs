using FluentValidation;
using IFCE.AutoGate.Application.Events.PasswordForgot;
using IFCE.AutoGate.Core.Messages;
using IFCE.AutoGate.Domain.Contracts.Repositories;
using IFCE.AutoGate.Domain.Errors;
using MediatR;
using INotification = IFCE.AutoGate.Core.Contracts.INotification;

namespace IFCE.AutoGate.Application.UseCases.RecoverPassword;

public class RecoverPasswordCommandHandler : CommandHandler<RecoverPasswordCommand>,
    IRequestHandler<RecoverPasswordCommand, bool>
{
    private readonly IAdministratorRepository _administratorRepository;

    public RecoverPasswordCommandHandler(IValidator<RecoverPasswordCommand> validator, INotification notification,
        IAdministratorRepository administratorRepository) : base(validator, notification)
    {
        _administratorRepository = administratorRepository;
    }

    public async Task<bool> Handle(RecoverPasswordCommand command, CancellationToken cancellationToken)
    {
        var errors = Validate(command);
        if (errors.Any()) return Failure(new ValidationError(errors));

        var administrator = await _administratorRepository.LoadByEmail(command.Email);
        if (administrator is null) return Failure(new NotFoundError("Administrador não encontrado."));

        administrator.ForgetPassword();

        var forgotPasswordEvent = new PasswordForgotEvent(administrator.Name, administrator.Email,
            administrator.RecoveryPasswordCode ?? Guid.Empty);
        administrator.AddEvent(forgotPasswordEvent);

        _administratorRepository.Update(administrator);
        var isSuccess = await _administratorRepository.UnitOfWork.Commit();

        return isSuccess || Failure(new UnexpectedError());
    }
}
