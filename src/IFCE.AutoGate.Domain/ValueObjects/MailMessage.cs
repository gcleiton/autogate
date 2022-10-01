namespace IFCE.AutoGate.Domain.ValueObjects;

public class MailMessage
{
    public string To { get; set; }

    public string? From { get; set; }
    public string? DisplayName { get; set; }

    public string Subject { get; set; }
    public string? Body { get; set; }
}
