using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Abstractions;
using Identity.Core.Abstractions;
using Shared.Core.Services;

namespace Identity.Core.Features.Login;
public class LoginRequestHandler : IRequestHandler<LoginRequest, LoginResponse>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IJwtService _jwtService;
    private readonly IMapper _mapper;
    public LoginRequestHandler(IUsersRepository usersRepository, IMapper mapper,IJwtService jwtService)
    {
        _usersRepository = usersRepository;
        _jwtService= jwtService;
        _mapper = mapper;
    }
    public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var hashedPass = HashService.CreateHash(request.Password);

        var user = await _usersRepository.AsQueryable()
            .Where(u => u.Password == hashedPass)
            .Where(u => u.Username == request.UserOrEmail || u.Email == request.UserOrEmail)
            .SingleAsync(cancellationToken);

        var userRecord = _mapper.Map<IUserRecord>(user);

        return new LoginResponse
        {
            Username = user.Username,
            Email = user.Email,
            CompanyIds = user.CompanyIds,
            Token = _jwtService.GenerateToken(userRecord),
        };
    }
}