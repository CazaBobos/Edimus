namespace Tags.Input;

public class GetTagsInput
{
    public int? Limit { get; set; }
    public int? Page { get; set; }
    public string? Name { get; set; }
    public bool? Enabled { get; set; }
}
