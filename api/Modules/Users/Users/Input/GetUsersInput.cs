namespace Users.Input;

public class GetUsersInput
{
    public int? Limit { get; set; }
    public int? Page { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? PhoneNumber { get; set; }
}
