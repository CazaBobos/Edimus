namespace Identity.Input;

public class ResetPasswordInput
{
    public int UserId { get; set; }
    public string Token { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}
