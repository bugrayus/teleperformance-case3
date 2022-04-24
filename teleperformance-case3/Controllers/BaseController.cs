using MediatR;
using Microsoft.AspNetCore.Mvc;
using teleperformance_case3.Application.Common;

namespace teleperformance_case3.Controllers;

public class BaseController : ControllerBase
{
    private ISender _mediator = null!;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    [NonAction]
    protected IActionResult Success<T>(string message, T data)
    {
        return StatusCode(200, new ApiResponse<T>
        {
            Message = message,
            Data = data
        });
    }

    [NonAction]
    protected IActionResult Created<T>(string message, T data)
    {
        return StatusCode(201, new ApiResponse<T>
        {
            Message = message,
            Data = data
        });
    }

    [NonAction]
    protected IActionResult NoContent<T>(string message, T data)
    {
        return StatusCode(204, new ApiResponse<T>
        {
            Message = message,
            Data = data
        });
    }

    protected IActionResult BadRequest<T>(string message, T data)
    {
        return StatusCode(400, new ApiResponse<T>
        {
            Message = message,
            Data = data
        });
    }

    [NonAction]
    protected IActionResult Unauthorized<T>(string message, T data)
    {
        return StatusCode(401, new ApiResponse<T>
        {
            Message = message,
            Data = data
        });
    }

    [NonAction]
    protected IActionResult Forbidden<T>(string message, T data)
    {
        return StatusCode(403, new ApiResponse<T>
        {
            Message = message,
            Data = data
        });
    }

    [NonAction]
    protected IActionResult NotFound<T>(string message, T data)
    {
        return StatusCode(404, new ApiResponse<T>
        {
            Message = message,
            Data = data
        });
    }

    [NonAction]
    protected IActionResult UnprocessableEntity<T>(string message, T data)
    {
        return StatusCode(422, new ApiResponse<T>
        {
            Message = message,
            Data = data
        });
    }

    [NonAction]
    protected IActionResult Error<T>(string message, T data)
    {
        return StatusCode(500, new ApiResponse<T>
        {
            Message = message,
            Data = data
        });
    }
}