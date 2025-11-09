using Tables.Core.Model;

namespace Tables.Core.Features.UpdateTable;

public class UpdateTableResponse
{
    public TableModel Table { get; set; } = new();
}