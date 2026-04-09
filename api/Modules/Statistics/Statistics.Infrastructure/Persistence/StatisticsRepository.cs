using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Persistence;
using Statistics.Core.Abstractions;

namespace Statistics.Infrastructure.Persistence;

public class StatisticsRepository : IStatisticsRepository
{
    private readonly DatabaseContext _context;

    public StatisticsRepository(DatabaseContext context) => _context = context;

    public async Task<List<HourlyOccupancy>> GetHourlyOccupancy(DateTime date, CancellationToken ct = default)
    {
        var dayStart = date.ToUniversalTime();
        var dayEnd = dayStart.AddDays(1);
        var totalTables = await _context.Tables.CountAsync(t => t.Enabled, ct);

        if (totalTables == 0)
            return Enumerable.Range(0, 24).Select(h => new HourlyOccupancy(h, 0)).ToList();

        var sessions = await _context.TableSessions
            .Where(s => s.OpenedAt < dayEnd && (s.ClosedAt == null || s.ClosedAt > dayStart))
            .Select(s => new { s.OpenedAt, s.ClosedAt })
            .ToListAsync(ct);

        return Enumerable.Range(0, 24).Select(hour =>
        {
            var hourStart = dayStart.AddHours(hour);
            var hourEnd = hourStart.AddHours(1);
            var occupied = sessions.Count(s =>
                s.OpenedAt < hourEnd && (s.ClosedAt == null || s.ClosedAt > hourStart));
            return new HourlyOccupancy(hour, Math.Round((double)occupied / totalTables * 100, 1));
        }).ToList();
    }

    public async Task<List<SalesPeriod>> GetSales(DateTime from, DateTime to, string groupBy, CancellationToken ct = default)
    {
        from = DateTime.SpecifyKind(from, DateTimeKind.Utc);
        to = DateTime.SpecifyKind(to, DateTimeKind.Utc).AddDays(1);

        var sessions = await _context.TableSessions
            .Where(s => s.ClosedAt != null && s.ClosedAt >= from && s.ClosedAt < to)
            .Select(s => new
            {
                ClosedAt = s.ClosedAt!.Value,
                Revenue = s.Orders.Sum(o => (decimal?)o.Amount * o.UnitPrice) ?? 0m,
            })
            .ToListAsync(ct);

        return groupBy.ToLower() switch
        {
            "month" => sessions
                .GroupBy(s => new DateTime(s.ClosedAt.Year, s.ClosedAt.Month, 1))
                .Select(g => new SalesPeriod(g.Key, g.Sum(s => s.Revenue)))
                .OrderBy(x => x.PeriodStart)
                .ToList(),
            "week" => sessions
                .GroupBy(s => s.ClosedAt.Date.AddDays(-(int)s.ClosedAt.DayOfWeek))
                .Select(g => new SalesPeriod(g.Key, g.Sum(s => s.Revenue)))
                .OrderBy(x => x.PeriodStart)
                .ToList(),
            _ => sessions
                .GroupBy(s => s.ClosedAt.Date)
                .Select(g => new SalesPeriod(g.Key, g.Sum(s => s.Revenue)))
                .OrderBy(x => x.PeriodStart)
                .ToList(),
        };
    }

    public async Task<double?> GetAverageRotationMinutes(DateTime from, DateTime to, CancellationToken ct = default)
    {
        from = DateTime.SpecifyKind(from, DateTimeKind.Utc);
        to = DateTime.SpecifyKind(to, DateTimeKind.Utc).AddDays(1);

        var sessions = await _context.TableSessions
            .Where(s => s.ClosedAt != null && s.ClosedAt >= from && s.ClosedAt < to)
            .Select(s => new { s.OpenedAt, ClosedAt = s.ClosedAt.Value })
            .ToListAsync(ct);

        if (sessions.Count == 0) return null;
        return Math.Round(sessions.Average(s => (s.ClosedAt - s.OpenedAt).TotalSeconds) / 60.0, 1);
    }

    public async Task<decimal?> GetAverageSpending(DateTime from, DateTime to, CancellationToken ct = default)
    {
        from = DateTime.SpecifyKind(from, DateTimeKind.Utc);
        to = DateTime.SpecifyKind(to, DateTimeKind.Utc).AddDays(1);

        var totals = await _context.TableSessions
            .Where(s => s.ClosedAt != null && s.ClosedAt >= from && s.ClosedAt < to)
            .Select(s => s.Orders.Sum(o => (decimal?)o.Amount * o.UnitPrice) ?? 0m)
            .ToListAsync(ct);

        if (totals.Count == 0) return null;
        return Math.Round(totals.Average(), 2);
    }

    public async Task<List<TopProduct>> GetTopProducts(DateTime from, DateTime to, int limit, CancellationToken ct = default)
    {
        from = DateTime.SpecifyKind(from, DateTimeKind.Utc);
        to = DateTime.SpecifyKind(to, DateTimeKind.Utc).AddDays(1);

        var raw = await _context.SessionOrders
            .Join(_context.TableSessions, o => o.SessionId, s => s.Id, (o, s) => new { o, s })
            .Where(x => x.s.ClosedAt != null && x.s.ClosedAt >= from && x.s.ClosedAt < to)
            .GroupBy(x => new { x.o.ProductId, x.o.ProductName })
            .Select(g => new
            {
                g.Key.ProductId,
                g.Key.ProductName,
                TotalAmount = g.Sum(x => x.o.Amount),
                TotalRevenue = g.Sum(x => x.o.Amount * x.o.UnitPrice),
            })
            .OrderByDescending(x => x.TotalAmount)
            .Take(limit)
            .ToListAsync(ct);

        return raw.Select(x => new TopProduct(x.ProductId, x.ProductName, x.TotalAmount, x.TotalRevenue)).ToList();
    }

    public async Task<AttentionTimes> GetAttentionTimes(DateTime from, DateTime to, CancellationToken ct = default)
    {
        from = DateTime.SpecifyKind(from, DateTimeKind.Utc);
        to = DateTime.SpecifyKind(to, DateTimeKind.Utc).AddDays(1);

        var sessions = await _context.TableSessions
            .Where(s => s.ClosedAt != null && s.ClosedAt >= from && s.ClosedAt < to)
            .Select(s => new
            {
                s.ArrivalAttentionSeconds,
                s.TotalCallingSeconds,
                s.CallingCount,
            })
            .ToListAsync(ct);

        var arrivalSamples = sessions
            .Where(s => s.ArrivalAttentionSeconds.HasValue)
            .Select(s => (double)s.ArrivalAttentionSeconds!.Value)
            .ToList();

        var callingSamples = sessions
            .Where(s => s.CallingCount > 0)
            .Select(s => (double)s.TotalCallingSeconds / s.CallingCount)
            .ToList();

        return new AttentionTimes(
            arrivalSamples.Count > 0 ? Math.Round(arrivalSamples.Average(), 1) : null,
            callingSamples.Count > 0 ? Math.Round(callingSamples.Average(), 1) : null);
    }
}
