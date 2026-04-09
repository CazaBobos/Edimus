namespace Statistics.Core.Abstractions;

public interface IStatisticsRepository
{
    Task<List<HourlyOccupancy>> GetHourlyOccupancy(DateTime date, CancellationToken ct = default);
    Task<List<SalesPeriod>> GetSales(DateTime from, DateTime to, string groupBy, CancellationToken ct = default);
    Task<double?> GetAverageRotationMinutes(DateTime from, DateTime to, CancellationToken ct = default);
    Task<decimal?> GetAverageSpending(DateTime from, DateTime to, CancellationToken ct = default);
    Task<List<TopProduct>> GetTopProducts(DateTime from, DateTime to, int limit, CancellationToken ct = default);
    Task<AttentionTimes> GetAttentionTimes(DateTime from, DateTime to, CancellationToken ct = default);
}

public record HourlyOccupancy(int Hour, double OccupancyRate);
public record SalesPeriod(DateTime PeriodStart, decimal Revenue);
public record TopProduct(int ProductId, string ProductName, int TotalAmount, decimal TotalRevenue);
public record AttentionTimes(double? AverageArrivalSeconds, double? AverageCallingSeconds);
