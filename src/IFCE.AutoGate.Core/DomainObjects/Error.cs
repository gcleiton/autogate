namespace IFCE.AutoGate.Core.DomainObjects;

public abstract class Error
{
    protected Error(string name)
    {
        Name = name;
    }

    protected Error(string name, string message)
    {
        Name = name;
        Message = message;
    }

    public string Name { get; set; }
    public string Message { get; set; }
}
