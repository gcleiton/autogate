using IFCE.AutoGate.Core.Contracts;

namespace IFCE.AutoGate.Core.Communication;

public class Pagination : IPagination
{
    public int Total { get; set; }
    public int TotalInPage { get; set; }
    public int CurrentPage { get; set; }
    public int PerPage { get; set; }
    public int LastPage { get; set; }
    public bool HasPages { get; set; }
    public bool HasMorePages { get; set; }
    public bool OnFirstPage { get; set; }
}
