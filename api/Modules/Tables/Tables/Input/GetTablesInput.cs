namespace Tables.Input;
public class GetTablesInput
{
    public int? Limit { get; set; }
    public int? Page { get; set; }
    public long? CompanyId { get; set; }
    public bool? Enabled { get; set; }
}
