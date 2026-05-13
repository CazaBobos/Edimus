using Mediator;

namespace Layouts.Core.Features.GetLayouts;

public class GetLayoutsRequest : IRequest<GetLayoutsResponse>
{
    public int? Limit { get; set; }
    public int? Page { get; set; }
    public int? PremiseId { get; set; }
    public bool? Enabled { get; set; }
}
