using IFCE.AutoGate.Core.Contracts;
using IFCE.AutoGate.Core.DomainObjects;

namespace IFCE.AutoGate.Core.Communication;

public class Result : IResult
{
    private Result()
    {
        IsSuccess = true;
    }

    private Result(Error error)
    {
        IsSuccess = false;
        Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    public static IResult Ok()
    {
        return new Result();
    }

    public static IResult Failure(Error error)
    {
        return new Result(error);
    }
}
