using Dawn;
using Identity.Core.Abstractions;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Abstractions;
using Shared.Core.Services;
using Shared.Core.Settings;

namespace Identity.Core.Features.Login;
public class RefreshRequestHandler : IRequestHandler<RefreshRequest, LoginResponse>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IJwtSettings _jwtSettings;
    private readonly IJwtService _jwtService;

    public RefreshRequestHandler(
        IUsersRepository usersRepository,
        IJwtSettings jwtSettings,
        IJwtService jwtService)
    {
        _usersRepository = usersRepository;
        _jwtSettings = jwtSettings;
        _jwtService = jwtService;
    }

    public async ValueTask<LoginResponse> Handle(RefreshRequest request, CancellationToken cancellationToken)
    {
        var userId = _jwtService.ValidateRefreshToken(request.RefreshToken);
        Guard.Operation(userId.HasValue, "Invalid or expired refresh token.");

        var user = await _usersRepository.AsQueryable()
            .Where(u => u.Id == userId!.Value)
            .SingleAsync(cancellationToken);

        var userRecord = (IUserRecord)user;

        return new LoginResponse
        {
            Username = user.Username,
            Email = user.Email,
            CompanyIds = user.CompanyIds,
            Role = user.Role,
            ExpiresIn = _jwtSettings.ExpirationInMinutes,
            Token = _jwtService.GenerateToken(userRecord),
            RefreshToken = _jwtService.GenerateRefreshToken(userRecord),
        };
    }
}
