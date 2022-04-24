using System.Net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

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
        catch (Exception ex)
        {
            var error = new Error
            {
                Message = ex.Message,
                StackTrace = ex.StackTrace
            };
            var code = ex switch
            {
                ApiException => HttpStatusCode.InternalServerError,
                KeyNotFoundException => HttpStatusCode.InternalServerError,
                _ => HttpStatusCode.InternalServerError
            };

            var resp = ApiReturn.ErrorResponse(new ApiException(error).Error, (int) code);
            var result = JsonConvert.SerializeObject(resp);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) code;
            await context.Response.WriteAsync(result);
        }
    }
}