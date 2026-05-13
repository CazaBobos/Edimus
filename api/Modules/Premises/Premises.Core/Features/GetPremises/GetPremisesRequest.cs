using Mediator;

namespace Premises.Core.Features.GetPremises;

public class GetPremisesRequest : IRequest<GetPremisesResponse>
{
    public int? Limit { get; set; }
    public int? Page { get; set; }
    public int? CompanyId { get; set; }
    public bool? Enabled { get; set; }
}
