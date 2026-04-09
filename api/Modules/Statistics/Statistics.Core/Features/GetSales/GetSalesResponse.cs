using Statistics.Core.Abstractions;

namespace Statistics.Core.Features.GetSales;

public class GetSalesResponse
{
    public List<SalesPeriod> Data { get; set; } = [];
}
