using Mapster;
using Mediator;
using Users.Core.Abstractions;
using Users.Core.Model;

namespace Users.Core.Features.RestoreUser;
public class RestoreUserRequestHandler : IRequestHandler<RestoreUserRequest, RestoreUserResponse>
{
    private readonly IUsersRepository _usersRepository;

    public RestoreUserRequestHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }
    public async ValueTask<RestoreUserResponse> Handle(RestoreUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetById(request.UserId, cancellationToken);

        if (user is not null)
        {
            user.Restore();
            await _usersRepository.Update(user, cancellationToken);
        }

        return new RestoreUserResponse
        {
            User = user.Adapt<UserModel>()
        };
    }
}
