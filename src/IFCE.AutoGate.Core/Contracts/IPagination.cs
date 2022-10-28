namespace IFCE.AutoGate.Core.Contracts;

public interface IPagination
{
    int Total { get; set; }
    int TotalInPage { get; set; }
    int CurrentPage { get; set; }
    int PerPage { get; set; }
    int LastPage { get; set; }
    bool HasPages { get; set; }
    bool HasMorePages { get; set; }
    bool OnFirstPage { get; set; }
}
