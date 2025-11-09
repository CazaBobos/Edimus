using AutoMapper;
using MediatR;
using Users.Core.Abstractions;
using Users.Core.Model;

namespace Users.Core.Features.RestoreUser;
public class RestoreUserRequestHandler : IRequestHandler<RestoreUserRequest, RestoreUserResponse>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;

    public RestoreUserRequestHandler(IUsersRepository usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
    }
    public async Task<RestoreUserResponse> Handle(RestoreUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetById(request.UserId, cancellationToken);

        if (user is not null)
        {
            user.Restore();
            await _usersRepository.Update(user, cancellationToken);
        }

        return new RestoreUserResponse
        {
            User = _mapper.Map<UserModel>(user)
        };
    }
}
