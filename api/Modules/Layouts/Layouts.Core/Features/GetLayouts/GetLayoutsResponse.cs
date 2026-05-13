using Layouts.Core.Model;

namespace Layouts.Core.Features.GetLayouts;

public class GetLayoutsResponse
{
    public int Count { get; set; }
    public int? Limit { get; set; }
    public int? Page { get; set; }
    public List<LayoutModel> Layouts { get; set; } = [];
}
