using System.Net;

namespace Shared.Core.Exceptions;
public class HttpBadRequestException : HttpRequestException
{
    public HttpStatusCode HttpStatusCode { get; }
    public List<string> ErrorMessages { get; }

    public HttpBadRequestException() : this("There's been a problem with the request.") { }

    public HttpBadRequestException(string message, List<string> errorMessages = default)
        : base(message)
    {
        HttpStatusCode = HttpStatusCode.BadRequest;
        ErrorMessages = errorMessages;
    }

}

