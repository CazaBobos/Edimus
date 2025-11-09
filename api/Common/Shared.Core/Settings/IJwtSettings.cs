namespace Shared.Core.Settings;
public interface IJwtSettings
{
    public string Secret { get; set; }
    public int ExpirationInMinutes { get; set; }
}
