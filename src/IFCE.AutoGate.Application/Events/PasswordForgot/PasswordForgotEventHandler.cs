using IFCE.AutoGate.Domain.Contracts.Gateways;
using IFCE.AutoGate.Domain.ValueObjects;
using MediatR;

namespace IFCE.AutoGate.Application.Events.PasswordForgot;

public class PasswordForgotEventHandler : INotificationHandler<PasswordForgotEvent>
{
    private readonly IMailSender _mailSender;

    public PasswordForgotEventHandler(IMailSender mailSender)
    {
        _mailSender = mailSender;
    }

    public async Task Handle(PasswordForgotEvent message, CancellationToken cancellationToken)
    {
        var mail = new MailMessage
        {
            To = message.Email,
            Subject = "AutoGate | Recuperação de Senha",
            Body = $"Olá {message.Name} seu código de recuperação é {message.RecoveryPasswordCode}"
        };

        await _mailSender.SendEmail(mail);
    }
}
