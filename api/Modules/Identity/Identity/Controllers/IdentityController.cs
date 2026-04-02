using Identity.Core.Features.Login;
using Identity.Input;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Identity.Controllers;

[Route("api/auth")]
[ApiController]
[AllowAnonymous]
public class IdentityController : ControllerBase
{
    public readonly IMediator _mediator;
    public IdentityController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Authenticates a user.
    /// </summary>
    [HttpPost]
    [Route("login")]
    [EnableRateLimiting("login")]
    public async Task<LoginResponse> Login([FromBody] LoginInput input, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new LoginRequest
        {
            UserOrEmail = input.UserOrEmail,
            Password = input.Password,
            Ip = GenerateIpAddress()
        }, cancellationToken);

        return response;
    }

    /// <summary>
    /// Refreshes a user's token.
    /// </summary>
    [HttpPost]
    [Route("refresh")]
    public async Task<LoginResponse> Refresh([FromBody] RefreshInput input, CancellationToken cancellationToken)
    {
        var command = new RefreshRequest
        {
            RefreshToken = input.RefreshToken,
            Ip = GenerateIpAddress()
        };
        var response = await _mediator.Send(command, cancellationToken);

        return response;
    }

    private string GenerateIpAddress()
    {
        if (Request.Headers.ContainsKey("X-Forwarded-For"))
            return Request.Headers["X-Forwarded-For"];
        else
            return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
    }
}
