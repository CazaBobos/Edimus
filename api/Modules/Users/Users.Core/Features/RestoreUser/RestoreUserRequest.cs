using Mediator;

namespace Users.Core.Features.RestoreUser;
public class RestoreUserRequest : IRequest<RestoreUserResponse>
{
    public int UserId { get; set; }
}
