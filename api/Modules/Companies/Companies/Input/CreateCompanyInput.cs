namespace Companies.Input;
public class CreateCompanyInput
{
    public string Name { get; set; } = string.Empty;
    public string Slogan { get; set; } = string.Empty;
    public string? Acronym { get; set; }
}
