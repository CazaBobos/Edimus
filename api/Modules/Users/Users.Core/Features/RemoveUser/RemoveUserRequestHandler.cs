using AutoMapper;
using MediatR;
using Users.Core.Abstractions;
using Users.Core.Model;

namespace Users.Core.Features.RemoveUser;
public class RemoveUserRequestHandler : IRequestHandler<RemoveUserRequest, RemoveUserResponse>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;

    public RemoveUserRequestHandler(IUsersRepository usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
    }
    public async Task<RemoveUserResponse> Handle(RemoveUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetById(request.UserId, cancellationToken);

        if (user is not null)
        {
            user.Remove();
            await _usersRepository.Update(user, cancellationToken);
        }

        return new RemoveUserResponse
        {
            User = _mapper.Map<UserModel>(user)
        };
    }
}
