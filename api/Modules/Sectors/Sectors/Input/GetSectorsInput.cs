namespace Sectors.Input;
public class GetSectorsInput
{
    public int? Limit { get; set; }
    public int? Page { get; set; }
    public int? LayoutId { get; set; }
    public string? Name { get; set; }
    public bool? Enabled { get; set; }
}
