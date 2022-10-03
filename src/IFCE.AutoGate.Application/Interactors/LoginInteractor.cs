using FluentValidation;
using IFCE.AutoGate.Application.Dtos;
using IFCE.AutoGate.Application.Requests;
using IFCE.AutoGate.Core.Messages;
using IFCE.AutoGate.Domain.Contracts.Gateways;
using IFCE.AutoGate.Domain.Contracts.Repositories;
using IFCE.AutoGate.Domain.Errors;
using MediatR;
using INotification = IFCE.AutoGate.Core.Contracts.INotification;

namespace IFCE.AutoGate.Application.UseCases;

public class LoginInteractor : Interactor<LoginRequest, AccessTokenDto>,
    IRequestHandler<LoginRequest, AccessTokenDto>
{
    private readonly IAdministratorRepository _administratorRepository;
    private readonly IHasher _hasher;
    private readonly ITokenHandler _tokenHandler;

    public LoginInteractor(
        IValidator<LoginRequest> validator,
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

    public async Task<AccessTokenDto> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var errors = Validate(request);
        if (errors.Any()) return Failure(new ValidationError(errors));

        var administrator = await _administratorRepository.LoadByEmail(request.Email);

        if (administrator is not null)
        {
            var isPasswordMatch = _hasher.Validate(request.Password, administrator.Password);

            if (isPasswordMatch)
            {
                var token = await _tokenHandler.Generate(administrator);
                return new AccessTokenDto
                {
                    AccessToken = token
                };
            }
        }

        return Failure(new AuthenticationError());
    }
}
