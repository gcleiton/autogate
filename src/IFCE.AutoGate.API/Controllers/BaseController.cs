using IFCE.AutoGate.API.Responses;
using IFCE.AutoGate.Core.Contracts;
using IFCE.AutoGate.Domain.Errors;
using Microsoft.AspNetCore.Mvc;

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

    protected IActionResult OkResponse(object obj)
    {
        return HandleResponse(Ok(obj));
    }

    protected IActionResult CreatedResponse(string uri, string message)
    {
        var response = Created(uri, new CreatedResponse(message));
        return HandleResponse(response);
    }

    protected IActionResult NoContentResponse()
    {
        return HandleResponse(NoContent());
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

    private IActionResult HandleResponse(IActionResult actionResult)
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
}
