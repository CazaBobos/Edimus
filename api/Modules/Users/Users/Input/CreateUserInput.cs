namespace Users.Input;
public abstract class CreateUserInput
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public List<int> Companies { get; set; } = new();
}