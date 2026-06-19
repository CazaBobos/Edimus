namespace Companies.Input;
public class GetCompaniesInput
{
    public int? Limit { get; set; }
    public int? Page { get; set; }
    public string? Name { get; set; }
    public string? Slug { get; set; }
    public bool? Enabled { get; set; }
}
