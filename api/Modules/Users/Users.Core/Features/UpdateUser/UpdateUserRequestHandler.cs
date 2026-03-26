using Mapster;
using Mediator;
using Shared.Core.Exceptions;
using Users.Core.Abstractions;
using Users.Core.Model;

namespace Users.Core.Features.UpdateUser;
public class UpdateUserRequestHandler : IRequestHandler<UpdateUserRequest, UpdateUserResponse>
{
    private readonly IUsersRepository _usersRepository;

    public UpdateUserRequestHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }
    public async ValueTask<UpdateUserResponse> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetById(request.Id, cancellationToken);

        if (user is null) throw new HttpNotFoundException();

        if (request.User.Role > request.Role && request.User.Role > user.Role)
            throw new HttpForbiddenException();

        user.Update(
            username: request.Username,
            email: request.Email,
            currentPassword: request.CurrentPassword,
            newPassword: request.NewPassword,
            role: request.Role,
            companyIds: request.CompanyIds
        );

        await _usersRepository.Update(user, cancellationToken);

        return new UpdateUserResponse
        {
            User = user.Adapt<UserModel>()
        };
    }
}
