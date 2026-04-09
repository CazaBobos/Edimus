using Statistics.Core.Abstractions;

namespace Statistics.Core.Features.GetOccupancy;

public class GetOccupancyResponse
{
    public List<HourlyOccupancy> Data { get; set; } = [];
}
