using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Entities;
using Shared.Core.Exceptions;
using Users.Core.Abstractions;
using Users.Core.Model;

namespace Users.Core.Features.CreateUser;
public class CreateUserRequestHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;

    public CreateUserRequestHandler(IUsersRepository usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
    }
    public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var user = new User(
            request.Username,
            request.Email,
            request.Password,
            request.Role);

        var existingUser = await _usersRepository.AsQueryable()
            .Where(u => u.Username == request.Username)
            .Where(u => u.Email == request.Email)
            .AnyAsync(cancellationToken);

        if (existingUser) throw new HttpConflictException("There's already a user with the given parameters");

        await _usersRepository.Add(user, cancellationToken);

        return new CreateUserResponse
        {
            User = _mapper.Map<UserModel>(user)
        };
    }
}
