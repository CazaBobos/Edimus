using AutoMapper;
using Identity.Core.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Abstractions;
using Shared.Core.Services;
using Shared.Core.Settings;

namespace Identity.Core.Features.Login;
public class LoginRequestHandler : IRequestHandler<LoginRequest, LoginResponse>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;
    private readonly IJwtSettings _jwtSettings;
    private readonly IJwtService _jwtService;
    public LoginRequestHandler(
        IUsersRepository usersRepository,
        IMapper mapper,
        IJwtSettings jwtSettings,
        IJwtService jwtService)
    {
        _usersRepository = usersRepository;
        _jwtSettings = jwtSettings;
        _jwtService = jwtService;
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
            Role = user.Role,
            ExpiresIn = _jwtSettings.ExpirationInMinutes,
            Token = _jwtService.GenerateToken(userRecord),
            RefreshToken = "",
        };
    }
}