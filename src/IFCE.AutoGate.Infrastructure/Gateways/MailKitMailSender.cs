using IFCE.AutoGate.Core.Settings;
using IFCE.AutoGate.Domain.Contracts.Gateways;
using IFCE.AutoGate.Domain.ValueObjects;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace IFCE.AutoGate.Infrastructure.Gateways;

public class MailKitMailSender : IMailSender
{
    private readonly MailSettings _settings;

    public MailKitMailSender(IOptions<MailSettings> setting)
    {
        _settings = setting.Value;
    }

    public async Task<bool> SendEmail(MailMessage message)
    {
        try
        {
            var mail = prepareMimeMessage(message);
            using var smtpClient = new SmtpClient();

            var secureSocket = _settings.UseSSL ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.None;
            await smtpClient.ConnectAsync(_settings.Host, _settings.Port, secureSocket);
            await smtpClient.AuthenticateAsync(_settings.Username, _settings.Password);
            await smtpClient.SendAsync(mail);
            await smtpClient.DisconnectAsync(true);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private MimeMessage prepareMimeMessage(MailMessage message)
    {
        var mail = new MimeMessage();
        mail.From.Add(new MailboxAddress(_settings.DisplayName, message.From ?? _settings.From));
        mail.Sender = new MailboxAddress(message.DisplayName ?? _settings.DisplayName,
            message.From ?? _settings.From);

        mail.To.Add(MailboxAddress.Parse(message.To));

        mail.Subject = message.Subject;

        var body = new BodyBuilder
        {
            HtmlBody = message.Body
        };
        mail.Body = body.ToMessageBody();

        return mail;
    }
}
