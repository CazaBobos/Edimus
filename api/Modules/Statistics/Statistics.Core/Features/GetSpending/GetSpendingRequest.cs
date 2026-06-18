using Mediator;

namespace Statistics.Core.Features.GetSpending;

public class GetSpendingRequest : IRequest<GetSpendingResponse>
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }
}
