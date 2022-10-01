using IFCE.AutoGate.Domain.Contracts.Gateways;
using IFCE.AutoGate.Domain.ValueObjects;
using MediatR;

namespace IFCE.AutoGate.Application.Events.Handlers;

public class AdministratorCreatedEventHandler : INotificationHandler<AdministratorCreatedEvent>
{
    private readonly IMailSender _mailSender;

    public AdministratorCreatedEventHandler(IMailSender mailSender)
    {
        _mailSender = mailSender;
    }

    public async Task Handle(AdministratorCreatedEvent message, CancellationToken cancellationToken)
    {
        var mail = new MailMessage
        {
            To = message.Email,
            Subject = "AutoGate | Primeiro Acesso",
            Body = $"Olá {message.Name} seu código de recuperação é {message.RecoveryPasswordCode}"
        };

        await _mailSender.SendEmail(mail);
    }
}
