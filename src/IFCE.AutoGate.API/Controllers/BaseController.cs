using IFCE.AutoGate.API.Responses;
using IFCE.AutoGate.Domain.Errors;
using Microsoft.AspNetCore.Mvc;
using IResult = IFCE.AutoGate.Core.Contracts.IResult;

namespace IFCE.AutoGate.API.Controllers;

[ApiController]
public abstract class BaseController : Controller
{
    protected IActionResult CreatedResponse(IResult result, string uri, string message)
    {
        return ResponseResult(result, Created(uri, new CreatedResponse(message)));
    }

    protected IActionResult UnprocessableEntityResponse(string message, IEnumerable<string> errors)
    {
        return UnprocessableEntity(new UnprocessableEntityResponse(message, errors));
    }

    protected IActionResult ConflictResponse(string message)
    {
        return Conflict(new ConflictResponse(message));
    }

    private IActionResult ResponseResult(IResult result, IActionResult actionResult)
    {
        if (result.IsFailure)
        {
            var error = result.Error;
            switch (error)
            {
                case AlreadyExistsError:
                    return ConflictResponse(error.Message);
                case ValidationError validationError:
                    return UnprocessableEntityResponse(validationError.Message, validationError.Errors);
            }
        }

        return actionResult;
    }
}
