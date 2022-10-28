namespace IFCE.AutoGate.Core.Extensions;

public static class StringExtension
{
    public static bool EqualsIgnoreCase(this string? value, string toCompare)
    {
        return value is not null && value.Equals(toCompare, StringComparison.InvariantCultureIgnoreCase);
    }
}
