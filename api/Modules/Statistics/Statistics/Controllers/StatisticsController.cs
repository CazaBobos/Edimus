using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Infrastructure.Extensions;
using Statistics.Core.Features.GetAttentionTimes;
using Statistics.Core.Features.GetOccupancy;
using Statistics.Core.Features.GetRotation;
using Statistics.Core.Features.GetSales;
using Statistics.Core.Features.GetSpending;
using Statistics.Core.Features.GetTopProducts;

namespace Statistics.Controllers;

[ApiController]
[Authorize]
[Route("api/stats")]
public class StatisticsController : ControllerBase
{
    private readonly IMediator _mediator;

    public StatisticsController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Returns hourly occupancy % for a given date (0-23h)
    /// </summary>
    [HttpGet("occupancy")]
    public async Task<IActionResult> GetOccupancy([FromQuery] DateTime date, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetOccupancyRequest
        {
            Date = date,
            User = User.GetUser(),
        }, cancellationToken);

        return Ok(response.Data);
    }

    /// <summary>
    /// Returns total revenue grouped by day, week or month
    /// </summary>
    [HttpGet("sales")]
    public async Task<IActionResult> GetSales(
        [FromQuery] DateTime from,
        [FromQuery] DateTime to,
        [FromQuery] string groupBy = "day",
        CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(new GetSalesRequest
        {
            From = from,
            To = to,
            GroupBy = groupBy,
            User = User.GetUser(),
        }, cancellationToken);

        return Ok(response.Data);
    }

    /// <summary>
    /// Returns average table rotation time in minutes
    /// </summary>
    [HttpGet("rotation")]
    public async Task<IActionResult> GetRotation(
        [FromQuery] DateTime from,
        [FromQuery] DateTime to,
        CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(new GetRotationRequest
        {
            From = from,
            To = to,
            User = User.GetUser(),
        }, cancellationToken);

        return Ok(response);
    }

    /// <summary>
    /// Returns average spending per table session
    /// </summary>
    [HttpGet("spending")]
    public async Task<IActionResult> GetSpending(
        [FromQuery] DateTime from,
        [FromQuery] DateTime to,
        CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(new GetSpendingRequest
        {
            From = from,
            To = to,
            User = User.GetUser(),
        }, cancellationToken);

        return Ok(response);
    }

    /// <summary>
    /// Returns the most requested products by quantity sold
    /// </summary>
    [HttpGet("products")]
    public async Task<IActionResult> GetTopProducts(
        [FromQuery] DateTime from,
        [FromQuery] DateTime to,
        [FromQuery] int limit = 10,
        CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(new GetTopProductsRequest
        {
            From = from,
            To = to,
            Limit = limit,
            User = User.GetUser(),
        }, cancellationToken);

        return Ok(response.Data);
    }

    /// <summary>
    /// Returns average attention times in seconds (arrival and calling)
    /// </summary>
    [HttpGet("attention")]
    public async Task<IActionResult> GetAttentionTimes(
        [FromQuery] DateTime from,
        [FromQuery] DateTime to,
        CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(new GetAttentionTimesRequest
        {
            From = from,
            To = to,
            User = User.GetUser(),
        }, cancellationToken);

        return Ok(response);
    }
}
