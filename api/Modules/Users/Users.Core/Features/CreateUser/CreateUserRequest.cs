using MediatR;
using Shared.Core.Abstractions;
using Shared.Core.Entities;

namespace Users.Core.Features.CreateUser;
public class CreateUserRequest : IRequest<CreateUserResponse>
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public List<int> Companies { get; set; } = new();
    public IUserRecord? User { get; set; }
}