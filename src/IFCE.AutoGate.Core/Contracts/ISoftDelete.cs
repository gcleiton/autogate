namespace IFCE.AutoGate.Core.Contracts;

public interface ISoftDelete
{
    public DateTime? DisabledAt { get; }

    public void Disable();
}
