using Shared.Core.Entities;

namespace Tables.Input;
public class GetTablesInput
{
    public int? Limit { get; set; }
    public int? Page { get; set; }
    public int? LayoutId { get; set; }
    public TableStatus? Status { get; set; }
    public bool? Enabled { get; set; }
}
