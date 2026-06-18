using Mediator;

namespace Users.Core.Features.RemoveUser;
public class RemoveUserRequest : IRequest<RemoveUserResponse>
{
    public int UserId { get; set; }
}
