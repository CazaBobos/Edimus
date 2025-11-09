using MediatR;
using Shared.Core.Abstractions;

namespace Users.Core.Features.RestoreUser;
public class RestoreUserRequest : IRequest<RestoreUserResponse>
{
    public int UserId { get; set; }
    public IUserRecord User { get; set; } = null!;
}
