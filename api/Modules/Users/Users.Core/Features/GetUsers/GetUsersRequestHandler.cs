using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Extensions;
using Users.Core.Abstractions;
using Users.Core.Model;

namespace Users.Core.Features.GetUsers;
public class GetUsersRequestHandler : IRequestHandler<GetUsersRequest, GetUsersResponse>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;

    public GetUsersRequestHandler(IUsersRepository usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
    }
    public async Task<GetUsersResponse> Handle(GetUsersRequest request, CancellationToken cancellationToken)
    {
        var query = _usersRepository.AsQueryable()
            .Where(u => request.Username == null || u.Username.Contains(request.Username))
            .Where(u => request.Email == null || u.Email.Contains(request.Email));

        var users = await query
            .Paginate(request.Limit, request.Page)
            .ToListAsync(cancellationToken);

        return new GetUsersResponse
        {
            Count = query.Count(),
            Limit = request.Limit,
            Page = request.Page,
            Users = _mapper.Map<List<UserModel>>(users),
        };
    }
}
