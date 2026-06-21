using AuditLogs.Core.Features.GetAuditLogs;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Entities;
using Shared.Infrastructure.Extensions;

namespace AuditLogs.Controllers;

[ApiController]
[Authorize]
[Route("api/audit-logs")]
public class AuditLogsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuditLogsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetAuditLogsRequest request, CancellationToken cancellationToken)
    {
        User.AllowMinRole(UserRole.Admin);
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}
