using FluentValidation;
using IFCE.AutoGate.Core.Communication;
using IFCE.AutoGate.Core.Contracts;
using IFCE.AutoGate.Core.Messages;
using IFCE.AutoGate.Domain.Contracts.Gateways;
using IFCE.AutoGate.Domain.Contracts.Repositories;
using IFCE.AutoGate.Domain.Errors;
using MediatR;

namespace IFCE.AutoGate.Application.Commands.Handlers;

public class ChangePasswordCommandHandler : CommandHandler<ChangePasswordCommand>,
    IRequestHandler<ChangePasswordCommand, IResult>
{
    private readonly IAdministratorRepository _administratorRepository;
    private readonly IHasher _hasher;

    public ChangePasswordCommandHandler(IValidator<ChangePasswordCommand> validator,
        IAdministratorRepository administratorRepository, IHasher hasher) : base(validator)
    {
        _administratorRepository = administratorRepository;
        _hasher = hasher;
    }

    public async Task<IResult> Handle(ChangePasswordCommand message, CancellationToken cancellationToken)
    {
        var errors = Validate(message);
        if (errors.Any()) return Result.Failure(new ValidationError(errors));

        var administrator = await _administratorRepository.LoadByRecoveryPasswordCode(message.RecoveryPasswordCode);
        if (administrator is null || !administrator.IsRecoveryPasswordValid())
            return Result.Failure(
                new NotFoundError("O código de recuperação da senha está inválido ou expirado"));

        var hashedPassword = _hasher.Hash(message.Password);
        administrator.ChangePassword(hashedPassword);

        _administratorRepository.Update(administrator);
        await _administratorRepository.UnitOfWork.Commit();

        return Result.Ok();
    }
}
