using System.Net;

namespace Shared.Core.Exceptions;

public interface ICustomException
{
    HttpStatusCode HttpStatusCode { get; }
    List<string> ErrorMessages { get; }
}
