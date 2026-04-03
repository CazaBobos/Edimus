using Identity.Core.Abstractions;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Core.Services;
using Shared.Core.Settings;

namespace Users.Core.Features.RecoverPassword;
public class RecoverPasswordRequestHandler : IRequestHandler<RecoverPasswordRequest, RecoverPasswordResponse>
{
    private readonly IUsersRepository _usersRepository;
    private readonly MailSettings _mailSettings;
    private readonly IMailService _mailService;
    private readonly string _frontendUrl;

    public RecoverPasswordRequestHandler(IUsersRepository usersRepository, MailSettings mailSettings, IMailService mailService, IConfiguration configuration)
    {
        _usersRepository = usersRepository;
        _mailSettings = mailSettings;
        _mailService = mailService;
        _frontendUrl = configuration["Email:FrontendUrl"] ?? "http://localhost:3000";
    }

    public async ValueTask<RecoverPasswordResponse> Handle(RecoverPasswordRequest request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.AsQueryable()
            .Where(u => u.Email == request.Email)
            .Where(u => u.Enabled)
            .SingleAsync(cancellationToken);

        var token = user.GenerateResetToken();
        var resetLink = $"{_frontendUrl}/reset-password?token={token}&userId={user.Id}";

        await _usersRepository.Update(user, cancellationToken);

        await _mailService.SendAsync(new MailRequest
        {
            From = _mailSettings.From,
            To = user.Email,
            Subject = "Password Recovery",
            Body = $"Click the following link to reset your password. It expires in 15 minutes.<br/><br/><a href=\"{resetLink}\">Reset password</a>",
        });

        return new RecoverPasswordResponse();
    }
}
