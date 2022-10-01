namespace IFCE.AutoGate.Core.Contracts;

public interface ITracking
{
    public DateTime CreatedAt { get; }
    public int? CreatedBy { get; }
    public DateTime ModifiedAt { get; }
    public int? ModifiedBy { get; }

    public void AddCreator(int? id);
    public void ChangeModifier(int? id);
}
