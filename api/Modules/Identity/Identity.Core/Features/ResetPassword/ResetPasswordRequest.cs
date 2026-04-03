using Mediator;

namespace Identity.Core.Features.ResetPassword;
public class ResetPasswordRequest : IRequest<ResetPasswordResponse>
{
    public int UserId { get; set; }
    public string Token { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}
