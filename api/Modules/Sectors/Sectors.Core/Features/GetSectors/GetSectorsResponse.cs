using Sectors.Core.Model;

namespace Sectors.Core.Features.GetManySectors;

public class GetSectorsResponse
{
    public int Count { get; set; }
    public int? Limit { get; set; }
    public int? Page { get; set; }
    public List<SectorModel> Sectors { get; set; } = new();
}