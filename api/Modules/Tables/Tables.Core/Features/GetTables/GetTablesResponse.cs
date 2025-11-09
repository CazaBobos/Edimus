using Tables.Core.Model;

namespace Tables.Core.Features.GetManyTables;

public class GetTablesResponse
{
    public int Count { get; set; }
    public int? Limit { get; set; }
    public int? Page { get; set; }
    public List<TableModel> Tables { get; set; } = new();
}