using IFCE.AutoGate.API.Responses;
using IFCE.AutoGate.Domain.Errors;
using Microsoft.AspNetCore.Mvc;
using IResult = IFCE.AutoGate.Core.Contracts.IResult;

namespace IFCE.AutoGate.API.Controllers;

[ApiController]
public abstract class BaseController : Controller
{
    protected IActionResult OkResponse(string message)
    {
        return Ok(new OkResponse(message));
    }

    protected IActionResult CreatedResponse(IResult result, string uri, string message)
    {
        return ResponseResult(result, Created(uri, new CreatedResponse(message)));
    }

    protected IActionResult NoContentResponse(IResult result)
    {
        return ResponseResult(result, NoContent());
    }

    protected IActionResult NotFoundResponse(string message)
    {
        return NotFound(new NotFoundResponse(message));
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
                case NotFoundError:
                    return NotFoundResponse(error.Message);
                case AlreadyExistsError:
                    return ConflictResponse(error.Message);
                case ValidationError validationError:
                    return UnprocessableEntityResponse(validationError.Message, validationError.Errors);
            }
        }

        return actionResult;
    }
}
