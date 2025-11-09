using MediatR;

namespace Identity.Core.Features.Login;
public class LoginRequest : IRequest<LoginResponse>
{
    public string UserOrEmail { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Ip { get; set; } = string.Empty;
}
