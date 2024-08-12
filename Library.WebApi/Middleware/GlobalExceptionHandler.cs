using System.Net;
using System.Security.Authentication;
using Library.Application.DTOs;

namespace OnlineShop.WebApi.Middleware;

public class GlobalExceptionHandler(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        ResponseException response = exception switch
        {
            ArgumentException ex => new ResponseException(HttpStatusCode.BadRequest, ex.Message),
            NotImplementedException ex => new ResponseException(HttpStatusCode.NotImplemented, ex.Message),
            AuthenticationException ex => new ResponseException(HttpStatusCode.Unauthorized, ex.Message),
            _ => new ResponseException(HttpStatusCode.InternalServerError, "Internal server error. Please retry later. exception.Message:" + exception.Message)
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)response.StatusCode;
        await context.Response.WriteAsJsonAsync(response);
    }
}