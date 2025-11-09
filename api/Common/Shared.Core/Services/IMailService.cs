namespace Shared.Core.Services;

public interface IMailService
{
    Task SendAsync(MailRequest request);
}
