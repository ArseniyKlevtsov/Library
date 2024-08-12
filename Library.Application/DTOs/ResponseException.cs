using System.Net;

namespace Library.Application.DTOs;

public class ResponseException(HttpStatusCode httpStatusCode, string message)
{
    public HttpStatusCode StatusCode { get; set; } = httpStatusCode;
    public string Message { get; set; } = message;
}
