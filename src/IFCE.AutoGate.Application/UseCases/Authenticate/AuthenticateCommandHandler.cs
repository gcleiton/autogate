using FluentValidation;
using IFCE.AutoGate.Core.Messages;
using IFCE.AutoGate.Domain.Contracts.Gateways;
using IFCE.AutoGate.Domain.Contracts.Repositories;
using IFCE.AutoGate.Domain.Errors;
using MediatR;
using INotification = IFCE.AutoGate.Core.Contracts.INotification;

namespace IFCE.AutoGate.Application.UseCases.Authenticate;

public class AuthenticateCommandHandler : CommandHandler<AuthenticateCommand>,
    IRequestHandler<AuthenticateCommand, AuthenticateCommandResult>
{
    private readonly IAdministratorRepository _administratorRepository;
    private readonly IHasher _hasher;
    private readonly ITokenHandler _tokenHandler;

    public AuthenticateCommandHandler(
        IValidator<AuthenticateCommand> validator,
        INotification notification,
        IAdministratorRepository administratorRepository,
        IHasher hasher,
        ITokenHandler tokenHandler
    ) : base(validator, notification)
    {
        _administratorRepository = administratorRepository;
        _hasher = hasher;
        _tokenHandler = tokenHandler;
    }

    public async Task<AuthenticateCommandResult> Handle(AuthenticateCommand request,
        CancellationToken cancellationToken)
    {
        var errors = Validate(request);
        if (errors.Any())
        {
            Failure(new ValidationError(errors));
            return null;
        }

        var administrator = await _administratorRepository.LoadByEmail(request.Email);

        if (administrator is not null)
        {
            var isPasswordMatch = _hasher.Validate(request.Password, administrator.Password);

            if (isPasswordMatch)
            {
                var token = await _tokenHandler.Generate(administrator);
                return new AuthenticateCommandResult
                {
                    AccessToken = token
                };
            }
        }

        Failure(new AuthenticationError());
        return null;
    }
}
