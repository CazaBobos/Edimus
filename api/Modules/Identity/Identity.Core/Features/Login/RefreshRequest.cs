using MediatR;

namespace Identity.Core.Features.Login;
public class RefreshRequest : IRequest<LoginResponse>
{
    public string ExpiredToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public string Ip { get; set; } = string.Empty;
}