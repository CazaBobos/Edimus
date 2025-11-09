namespace Identity.Input;

public class RefreshInput
{
    public string ExpiredToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}
