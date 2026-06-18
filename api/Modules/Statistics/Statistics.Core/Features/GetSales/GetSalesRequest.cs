using Mediator;

namespace Statistics.Core.Features.GetSales;

public class GetSalesRequest : IRequest<GetSalesResponse>
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public string GroupBy { get; set; } = "day";
}
