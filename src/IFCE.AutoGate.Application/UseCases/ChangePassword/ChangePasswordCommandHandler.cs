using FluentValidation;
using IFCE.AutoGate.Core.Messages;
using IFCE.AutoGate.Domain.Contracts.Gateways;
using IFCE.AutoGate.Domain.Contracts.Repositories;
using IFCE.AutoGate.Domain.Errors;
using MediatR;
using INotification = IFCE.AutoGate.Core.Contracts.INotification;

namespace IFCE.AutoGate.Application.UseCases.ChangePassword;

public class ChangePasswordCommandHandler : CommandHandler<ChangePasswordCommand>,
    IRequestHandler<ChangePasswordCommand, bool>
{
    private readonly IAdministratorRepository _administratorRepository;
    private readonly IHasher _hasher;

    public ChangePasswordCommandHandler(IValidator<ChangePasswordCommand> validator, INotification notification,
        IAdministratorRepository administratorRepository, IHasher hasher) : base(validator, notification)
    {
        _administratorRepository = administratorRepository;
        _hasher = hasher;
    }

    public async Task<bool> Handle(ChangePasswordCommand message, CancellationToken cancellationToken)
    {
        var errors = Validate(message);
        if (errors.Any()) return Failure(new ValidationError(errors));

        var administrator = await _administratorRepository.LoadByRecoveryPasswordCode(message.RecoveryPasswordCode);
        if (administrator is null || !administrator.IsRecoveryPasswordValid())
            return Failure(new NotFoundError("O código de recuperação da senha está inválido ou expirado"));

        var hashedPassword = _hasher.Hash(message.Password);
        administrator.ChangePassword(hashedPassword);

        _administratorRepository.Update(administrator);
        await _administratorRepository.UnitOfWork.Commit();

        return true;
    }
}
