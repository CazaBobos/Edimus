using Mediator;

namespace Tags.Core.Features.GetTags;

public class GetTagsRequest : IRequest<GetTagsResponse>
{
    public int? Limit { get; set; }
    public int? Page { get; set; }
    public string? Name { get; set; }
    public bool? Enabled { get; set; }
}
