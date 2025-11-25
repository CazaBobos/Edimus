using Tables.Core.Model;

namespace Tables.Core.Features.LinkTable;

public class LinkTableResponse
{
    public TableModel Table { get; set; } = new();
}