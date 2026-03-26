using Mapster;
using Mediator;
using Users.Core.Abstractions;
using Users.Core.Model;

namespace Users.Core.Features.RemoveUser;
public class RemoveUserRequestHandler : IRequestHandler<RemoveUserRequest, RemoveUserResponse>
{
    private readonly IUsersRepository _usersRepository;

    public RemoveUserRequestHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }
    public async ValueTask<RemoveUserResponse> Handle(RemoveUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetById(request.UserId, cancellationToken);

        if (user is not null)
        {
            user.Remove();
            await _usersRepository.Update(user, cancellationToken);
        }

        return new RemoveUserResponse
        {
            User = user.Adapt<UserModel>()
        };
    }
}
