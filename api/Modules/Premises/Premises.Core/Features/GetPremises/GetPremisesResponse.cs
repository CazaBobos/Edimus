using Premises.Core.Model;

namespace Premises.Core.Features.GetPremises;

public class GetPremisesResponse
{
    public int Count { get; set; }
    public int? Limit { get; set; }
    public int? Page { get; set; }
    public List<PremiseModel> Premises { get; set; } = [];
}
