using IFCE.AutoGate.API.Responses;
using IFCE.AutoGate.Core.Contracts;
using IFCE.AutoGate.Domain.Errors;
using Microsoft.AspNetCore.Mvc;
using IResult = IFCE.AutoGate.Core.Contracts.IResult;

namespace IFCE.AutoGate.API.Controllers;

[ApiController]
public abstract class BaseController : Controller
{
    private readonly INotification _notification;

    protected BaseController()
    {
    }

    protected BaseController(INotification notification)
    {
        _notification = notification;
    }

    protected IActionResult OkResponse(string message)
    {
        return Ok(new OkResponse(message));
    }

    protected IActionResult OkResponse(object obj)
    {
        return HandleResponse(Ok(obj));
    }

    protected IActionResult HandleResponse(IActionResult actionResult)
    {
        if (_notification.HasError())
        {
            var error = _notification.Error;
            switch (error)
            {
                case AuthenticationError:
                    return UnauthorizedResponse(error.Message);
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

    protected IActionResult CreatedResponse(IResult result, string uri, string message)
    {
        return ResponseResult(result, Created(uri, new CreatedResponse(message)));
    }

    protected IActionResult NoContentResponse(IResult result)
    {
        return ResponseResult(result, NoContent());
    }

    protected IActionResult UnauthorizedResponse(string message)
    {
        return Unauthorized(new UnauthorizedResponse(message));
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
