using System.Net;

namespace Library.WebApi.DTOs;

public class ResponseException(HttpStatusCode httpStatusCode, string message)
{
    public HttpStatusCode StatusCode { get; set; } = httpStatusCode;
    public string Message { get; set; } = message;
}
