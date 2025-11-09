//namespace Identity.Core.Features.Login;
//
//using System;
//using System.Linq;
//using System.Security.Cryptography;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Localization;
//using Microsoft.Extensions.Options;
//using Users.Core.Abstractions;
//
//public class AuthHandler :
//    IRequestHandler<LoginRequest, LoginResponse>,
//    IRequestHandler<RefreshRequest, LoginResponse>,
//    IRequestHandler<ExchangeRequest, LoginResponse>
//{
//    private readonly IUsersRepository _userRepository;
//    private readonly ILoginAuditRepository _loginAuditRepository;
//    private readonly IStringLocalizer<User> _localizer;
//    private readonly ITokenService _tokenService;
//    private readonly JwtSettings _config;
//
//    public AuthHandler(
//        IUserRepository userRepository,
//        ILoginAuditRepository loginAuditRepository,
//        ITokenService tokenService,
//        IOptions<JwtSettings> config,
//        IStringLocalizer<User> localizer)
//    {
//        _userRepository = userRepository;
//        _loginAuditRepository = loginAuditRepository;
//        _localizer = localizer;
//        _tokenService = tokenService;
//        _config = config.Value;
//    }
//
//    public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
//    {
//        var user = await _userRepository
//            .AsQueryable()
//            .Include(x => x.PersonalInfo)
//            .Include(x => x.Access)
//            .Where(x => x.UserName == request.UserName)
//            .Where(x => x.Password == GetMd5(request.Password))
//            .FirstOrDefaultAsync(cancellationToken);
//
//        if (user == null) throw new IdentityException(_localizer["Invalid credentials"]);
//
//        var token = _tokenService.GenerateJwtToken(user, request.Ip);
//
//        await CreateLoginAudit(user, token, cancellationToken);
//
//        return new LoginCommandResponse
//        {
//            UserName = token.UserName,
//            Email = token.Email,
//            FirstName = token.FirstName,
//            LastName = token.LastName,
//            Expires = token.Expires,
//            Token = token.Token,
//            RefreshToken = token.RefreshToken
//        };
//    }
//
//    public async Task<LoginCommandResponse> Handle(RefreshCommand request, CancellationToken cancellationToken)
//    {
//        var principal = _tokenService.GetPrincipalFromExpiredToken(request.ExpiredToken);
//
//        var activeRefreshTokenMinDate = DateTime.UtcNow.AddDays(-_config.RefreshTokenExpirationInDays);
//
//        var loginAudit = await _loginAuditRepository
//            .AsQueryable()
//            .Include(x => x.User)
//            .Include(x => x.User.PersonalInfo)
//            .Include(x => x.User.Access)
//            .Where(x => x.User.UserName == principal.GetUserName())
//            .Where(x => x.SessionId == request.RefreshToken)
//            .Where(x => x.LoggedInAt > activeRefreshTokenMinDate)
//            .Where(x => x.LoggedOutAt == null)
//            .Where(x => x.Ip == request.Ip)
//            .FirstOrDefaultAsync(cancellationToken);
//
//        var user = loginAudit.User;
//        if (user == null) throw new IdentityException(_localizer["Invalid credentials"]);
//
//        var token = _tokenService.GenerateJwtToken(user, request.Ip);
//
//        await UpdateLoginAudit(loginAudit, token, cancellationToken);
//
//        return new LoginCommandResponse
//        {
//            UserName = token.UserName,
//            Email = token.Email,
//            FirstName = token.FirstName,
//            LastName = token.LastName,
//            Expires = token.Expires,
//            Token = token.Token,
//            RefreshToken = token.RefreshToken
//        };
//    }
//
//    public async Task<LoginCommandResponse> Handle(ExchangeTokenCommand request, CancellationToken cancellationToken)
//    {
//        if (request.AlternateToken == null)
//            throw new IdentityException(_localizer["Invalid credentials"]);
//
//        var principal = _tokenService.GetPrincipalFromExpiredToken(request.AlternateToken);
//
//        var userIdClaim = principal.Claims.FirstOrDefault(x => x.Type == "userId");
//        if (userIdClaim == null) throw new IdentityException(_localizer["Invalid credentials"]);
//
//        var userId = Convert.ToInt32(userIdClaim.Value);
//
//        var user = await _userRepository
//            .AsQueryable()
//            .Include(x => x.PersonalInfo)
//            .Include(x => x.Access)
//            .Where(x => x.Id == userId)
//            .FirstOrDefaultAsync(cancellationToken);
//
//        if (user == null) throw new IdentityException(_localizer["Invalid credentials"]);
//
//        var token = _tokenService.GenerateJwtToken(user, request.Ip);
//
//        await CreateLoginAudit(user, token, cancellationToken);
//
//        return new LoginCommandResponse
//        {
//            UserName = token.UserName,
//            Email = token.Email,
//            FirstName = token.FirstName,
//            LastName = token.LastName,
//            Expires = token.Expires,
//            Token = token.Token,
//            RefreshToken = token.RefreshToken
//        };
//    }
//
//    private async Task CreateLoginAudit(User user, AuthToken token, CancellationToken cancellationToken)
//    {
//        var loginAudit = new LoginAudit(user.Id, token.Ip, token.RefreshToken);
//        await _loginAuditRepository.Add(loginAudit, cancellationToken);
//        await _loginAuditRepository.SaveChanges(cancellationToken);
//    }
//
//    private async Task UpdateLoginAudit(LoginAudit loginAudit, AuthToken token, CancellationToken cancellationToken)
//    {
//        loginAudit.UpdateRefreshToken(token.RefreshToken);
//        await _loginAuditRepository.Update(loginAudit);
//        await _loginAuditRepository.SaveChanges(cancellationToken);
//    }
//
//    [System.Diagnostics.CodeAnalysis.SuppressMessage("Security", "SCS0006:Weak hashing function", Justification = "Legacy encryption")]
//    private string GetMd5(string source)
//    {
//        var md5Provider = new MD5CryptoServiceProvider();
//        var sourceBytes = Encoding.ASCII.GetBytes(source);
//        var hash = md5Provider.ComputeHash(sourceBytes);
//
//        return ToHexString(hash);
//    }
//
//    private string ToHexString(byte[] hash)
//    {
//        return hash.Aggregate(string.Empty, (s, b) => $"{s}{b:X2}");
//    }
//}