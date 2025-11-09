using MediatR;
using Shared.Core.Abstractions;
using Shared.Core.Entities;

namespace Users.Core.Features.UpdateUser;

public class UpdateUserRequest : IRequest<UpdateUserResponse>
{
    public int Id { get; set; }
    public string? Username{ get; set; }
    public string? CurrentPassword { get; set; }
    public string? NewPassword { get; set; }
    public string? Email { get; set; }
    public UserRole? Role { get; set; }
    public List<int>? CompanyIds { get; set; }
    public IUserRecord User { get; set; } = null!;
}
