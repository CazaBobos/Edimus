using Mediator;

namespace Statistics.Core.Features.GetAttentionTimes;

public class GetAttentionTimesRequest : IRequest<GetAttentionTimesResponse>
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }
}
