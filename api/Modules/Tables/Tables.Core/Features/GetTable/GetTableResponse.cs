using Tables.Core.Model;

namespace Tables.Core.Features.GetTable;

public class GetTableResponse
{
    public TableModel Table { get; set; } = new();
}
