using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Core.Abstractions;
using Users.Core.Model;

namespace Users.Core.Features.GetManyUsers;
public class GetManyUsersRequestHandler : IRequestHandler<GetManyUsersRequest, GetManyUsersResponse>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;

    public GetManyUsersRequestHandler(IUsersRepository usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
    }
    public async Task<GetManyUsersResponse> Handle(GetManyUsersRequest request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.AsQueryable()
            .Where(u => request.Username == null || u.Username.Contains(request.Username))
            .Where(u => request.Email == null || u.Email.Contains(request.Email))
            .Where(u => request.RoleId == null || u.Role == request.RoleId)
            .ToListAsync(cancellationToken);

        return new GetManyUsersResponse
        {
            Users = _mapper.Map<List<UserModel>>(user)
        };
    }
}
