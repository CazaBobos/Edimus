using System.Net;

namespace Shared.Core.Exceptions;
public class HttpConflictException : HttpRequestException, ICustomException
{
    public HttpStatusCode HttpStatusCode { get; }
    public List<string> ErrorMessages { get; }

    public HttpConflictException() : this("There's been a conflict with the server.") { }

    public HttpConflictException(string message, List<string> errorMessages = default)
        : base(message)
    {
        HttpStatusCode = HttpStatusCode.Conflict;
        ErrorMessages = errorMessages;
    }

}

