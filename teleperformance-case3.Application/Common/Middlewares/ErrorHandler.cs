using System.Net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using teleperformance_case3.Application.Common.Exceptions;

namespace teleperformance_case3.Application.Common.Middlewares;

public class ErrorHandler
{
    private readonly RequestDelegate _next;

    public ErrorHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ApiException ex)
        {
            var code = ex switch
            {
                { } => HttpStatusCode.InternalServerError
            };

            await HandleExceptionAsync(context, ex.Error, code);
        }
        catch (ValidationException ex)
        {
            var code = ex switch
            {
                { } => HttpStatusCode.BadRequest
            };

            await HandleExceptionAsync(context, ex.Error, code);
        }
        catch (UnauthorizedException ex)
        {
            var code = ex switch
            {
                { } => HttpStatusCode.Unauthorized
            };

            await HandleExceptionAsync(context, ex.Error, code);
        }
        catch (Exception ex)
        {
            var error = new Error
            {
                Message = $"Undefined error occured. Message: {ex.Message}",
                StackTrace = ex.StackTrace
            };
            var code = ex switch
            {
                ApiException => HttpStatusCode.InternalServerError,
                ValidationException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };
            await HandleExceptionAsync(context, error, code);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Error error, HttpStatusCode code)
    {
        var resp = ApiReturn.ErrorResponse(error, (int) code);
        var result = JsonConvert.SerializeObject(resp);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int) code;
        return context.Response.WriteAsync(result);
    }
}