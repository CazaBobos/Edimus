using MediatR;

namespace Users.Core.Features.RecoverPassword;
public class RecoverPasswordRequest : IRequest<RecoverPasswordResponse>
{
    public string Email { get; set; } = string.Empty;
}
