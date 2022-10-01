using IFCE.AutoGate.Domain.ValueObjects;

namespace IFCE.AutoGate.Domain.Contracts.Gateways;

public interface IMailSender
{
    Task<bool> SendEmail(MailMessage message);
}
