using Identity.Core.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Services;
using Shared.Core.Settings;

namespace Users.Core.Features.RecoverPassword;
public class RecoverPasswordRequestHandler : IRequestHandler<RecoverPasswordRequest, RecoverPasswordResponse>
{
    private readonly IUsersRepository _usersRepository;
    private readonly MailSettings _mailSettings;
    private readonly IMailService _mailService;

    public RecoverPasswordRequestHandler(IUsersRepository usersRepository, MailSettings mailSettings, IMailService mailService)
    {
        _usersRepository = usersRepository;
        _mailSettings = mailSettings;
        _mailService = mailService;
    }
    public async Task<RecoverPasswordResponse> Handle(RecoverPasswordRequest request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.AsQueryable()
            .Where(u => u.Email == request.Email)
            .Where(u => u.Enabled)
            .SingleAsync(cancellationToken);

        user.SetRandomPassword();

        await _usersRepository.Update(user, cancellationToken);

        await _mailService.SendAsync(new MailRequest
        {
            From = _mailSettings.From,
            To = user.Email,
            Subject = "Password Recovery",
            Body = user.Password,
        });

        return new RecoverPasswordResponse();
    }
}
