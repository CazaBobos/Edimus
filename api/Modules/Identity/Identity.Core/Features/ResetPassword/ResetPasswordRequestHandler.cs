using Identity.Core.Abstractions;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace Identity.Core.Features.ResetPassword;
public class ResetPasswordRequestHandler : IRequestHandler<ResetPasswordRequest, ResetPasswordResponse>
{
    private readonly IUsersRepository _usersRepository;

    public ResetPasswordRequestHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async ValueTask<ResetPasswordResponse> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.AsQueryable()
            .Where(u => u.Id == request.UserId)
            .Where(u => u.Enabled)
            .SingleAsync(cancellationToken);

        user.ResetPassword(request.Token, request.NewPassword);

        await _usersRepository.Update(user, cancellationToken);

        return new ResetPasswordResponse();
    }
}
