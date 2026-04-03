using Dawn;
using Identity.Core.Abstractions;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Abstractions;
using Shared.Core.Services;
using Shared.Core.Settings;

namespace Identity.Core.Features.Login;
public class LoginRequestHandler : IRequestHandler<LoginRequest, LoginResponse>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IJwtSettings _jwtSettings;
    private readonly IJwtService _jwtService;
    public LoginRequestHandler(
        IUsersRepository usersRepository,
        IJwtSettings jwtSettings,
        IJwtService jwtService)
    {
        _usersRepository = usersRepository;
        _jwtSettings = jwtSettings;
        _jwtService = jwtService;
    }
    public async ValueTask<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.AsQueryable()
            .Where(u => u.Username == request.UserOrEmail || u.Email == request.UserOrEmail)
            .SingleAsync(cancellationToken);

        Guard.Operation(HashService.Verify(request.Password, user.Password), "Invalid credentials.");

        var userRecord = (IUserRecord)user;

        return new LoginResponse
        {
            Username = user.Username,
            Email = user.Email,
            CompanyIds = user.CompanyIds,
            Role = user.Role,
            TokenExpiresAt = DateTimeOffset.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes).ToUnixTimeMilliseconds(),
            RefreshTokenExpiresAt = DateTimeOffset.UtcNow.AddDays(_jwtSettings.RefreshExpirationInDays).ToUnixTimeMilliseconds(),
            Token = _jwtService.GenerateToken(userRecord),
            RefreshToken = _jwtService.GenerateRefreshToken(userRecord),
        };
    }
}