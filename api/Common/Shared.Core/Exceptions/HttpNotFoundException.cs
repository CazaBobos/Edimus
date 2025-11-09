using System.Net;

namespace Shared.Core.Exceptions;
public class HttpNotFoundException : HttpRequestException, ICustomException
{
    public HttpStatusCode HttpStatusCode { get; }
    public List<string> ErrorMessages { get; }

    public HttpNotFoundException() : this("The requested resource was not found.") { }

    public HttpNotFoundException(string message, List<string> errorMessages = default) 
        : base(message)
    {
        HttpStatusCode = HttpStatusCode.NotFound;
        ErrorMessages = errorMessages;
    }

}

