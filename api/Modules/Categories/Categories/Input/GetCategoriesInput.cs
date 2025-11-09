namespace Categories.Input;
public class GetCategoriesInput
{
    public int? Limit { get; set; }
    public int? Page { get; set; }
    public long? CompanyId { get; set; }
    public string? Name { get; set; }
    public bool? Enabled { get; set; }
}
