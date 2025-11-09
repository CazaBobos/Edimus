using System.Net;

namespace Shared.Core.Exceptions;
public class HttpForbiddenException : HttpRequestException
{
    public HttpStatusCode HttpStatusCode { get; }
    public List<string> ErrorMessages { get; }

    public HttpForbiddenException() : this("You don't have permission to access this resource.") { }

    public HttpForbiddenException(string message , List<string> errorMessages = default)
        : base(message)
    {
        HttpStatusCode = HttpStatusCode.Forbidden;
        ErrorMessages = errorMessages;
    }

}

