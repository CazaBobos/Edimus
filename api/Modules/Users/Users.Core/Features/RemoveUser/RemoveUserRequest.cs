using MediatR;
using Shared.Core.Abstractions;

namespace Users.Core.Features.RemoveUser;
public class RemoveUserRequest : IRequest<RemoveUserResponse>
{
    public int UserId { get; set; }
    public IUserRecord User { get; set; } = null!;
}
