using MediatR;

namespace Identity.Core.Features.Login;
public class ExchangeRequest : IRequest<LoginResponse>
{
    public string AlternateToken { get; set; } = string.Empty;
    public string Ip { get; set; } = string.Empty;
}