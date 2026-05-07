using Tags.Core.Model;

namespace Tags.Core.Features.GetTags;

public class GetTagsResponse
{
    public int Count { get; set; }
    public int? Limit { get; set; }
    public int? Page { get; set; }
    public List<TagModel> Tags { get; set; } = [];
}
