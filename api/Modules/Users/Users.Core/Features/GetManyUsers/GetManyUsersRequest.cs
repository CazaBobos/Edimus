using MediatR;
using Shared.Core.Entities;

namespace Users.Core.Features.GetManyUsers;
public class GetManyUsersRequest : IRequest<GetManyUsersResponse>
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public UserRole? RoleId { get; set; }
}
