using Library.Application.Exceptions;
using Library.WebApi.DTOs;
using System.Net;
using System.Security.Authentication;

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
            ReadTokenException ex => new ResponseException(HttpStatusCode.BadRequest, ex.Message),
            NotImplementedException ex => new ResponseException(HttpStatusCode.NotImplemented, ex.Message),
            AuthenticationException ex => new ResponseException(HttpStatusCode.Unauthorized, ex.Message),
            AlreadyExistsException ex => new ResponseException(HttpStatusCode.Conflict, ex.Message),
            KeyNotFoundException ex => new ResponseException(HttpStatusCode.NotFound, ex.Message),
            _ => new ResponseException(HttpStatusCode.InternalServerError, "Internal server error. Please retry later. exception.Message:" + exception.Message)
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)response.StatusCode;
        await context.Response.WriteAsJsonAsync(response);
    }
}