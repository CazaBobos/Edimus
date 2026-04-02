namespace Shared.Core.Settings;
public interface IJwtSettings
{
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public string Secret { get; set; }
    public int ExpirationInMinutes { get; set; }
    public int RefreshExpirationInDays { get; set; }
}
