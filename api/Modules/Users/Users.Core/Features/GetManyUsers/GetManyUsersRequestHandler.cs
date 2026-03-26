using Mapster;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Users.Core.Abstractions;
using Users.Core.Model;

namespace Users.Core.Features.GetManyUsers;
public class GetManyUsersRequestHandler : IRequestHandler<GetManyUsersRequest, GetManyUsersResponse>
{
    private readonly IUsersRepository _usersRepository;

    public GetManyUsersRequestHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }
    public async ValueTask<GetManyUsersResponse> Handle(GetManyUsersRequest request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.AsQueryable()
            .Where(u => request.Username == null || u.Username.Contains(request.Username))
            .Where(u => request.Email == null || u.Email.Contains(request.Email))
            .Where(u => request.RoleId == null || u.Role == request.RoleId)
            .ToListAsync(cancellationToken);

        return new GetManyUsersResponse
        {
            Users = user.Adapt<List<UserModel>>()
        };
    }
}
