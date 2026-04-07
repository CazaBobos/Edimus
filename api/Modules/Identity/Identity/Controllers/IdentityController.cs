using Identity.Core.Features.Login;
using Identity.Core.Features.ResetPassword;
using Identity.Input;
using Users.Core.Features.RecoverPassword;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Shared.Core.Settings;

namespace Identity.Controllers;

[Route("api/auth")]
[ApiController]
[AllowAnonymous]
public class IdentityController : ControllerBase
{
    public readonly IMediator _mediator;
    private readonly IJwtSettings _jwtSettings;
    private readonly IWebHostEnvironment _environment;

    public IdentityController(IMediator mediator, IJwtSettings jwtSettings, IWebHostEnvironment environment)
    {
        _mediator = mediator;
        _jwtSettings = jwtSettings;
        _environment = environment;
    }

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

        SetAuthCookies(response.Token, response.RefreshToken);
        return response;
    }

    /// <summary>
    /// Refreshes a user's token.
    /// </summary>
    [HttpPost]
    [Route("refresh")]
    public async Task<LoginResponse> Refresh(CancellationToken cancellationToken)
    {
        var refreshToken = Request.Cookies["refreshToken"] ?? string.Empty;

        var response = await _mediator.Send(new RefreshRequest
        {
            RefreshToken = refreshToken,
            Ip = GenerateIpAddress()
        }, cancellationToken);

        SetAuthCookies(response.Token, response.RefreshToken);
        return response;
    }

    /// <summary>
    /// Ends a user's session.
    /// </summary>
    [HttpPost]
    [Route("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("token");
        Response.Cookies.Delete("refreshToken");
        return Ok();
    }

    /// <summary>
    /// Sends a password recovery email.
    /// </summary>
    [HttpPost]
    [Route("recover-password")]
    public async Task<IActionResult> RecoverPassword([FromBody] RecoverPasswordInput input, CancellationToken cancellationToken)
    {
        await _mediator.Send(new RecoverPasswordRequest { Email = input.Email }, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Resets a user's password via a reset token.
    /// </summary>
    [HttpPost]
    [Route("reset-password")]
    public async Task<ResetPasswordResponse> ResetPassword([FromBody] ResetPasswordInput input, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new ResetPasswordRequest
        {
            UserId = input.UserId,
            Token = input.Token,
            NewPassword = input.NewPassword,
        }, cancellationToken);
    }

    private void SetAuthCookies(string token, string refreshToken)
    {
        // In development the UI and API run on different ports (different origins),
        // so SameSite=None + Secure=true is required for the browser to send the cookie.
        // In production both are on the same domain, so Strict is safe and preferred.
        var sameSite = _environment.IsDevelopment() ? SameSiteMode.None : SameSiteMode.Strict;

        Response.Cookies.Append("token", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = sameSite,
            Expires = DateTimeOffset.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes)
        });

        Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = sameSite,
            Expires = DateTimeOffset.UtcNow.AddDays(_jwtSettings.RefreshExpirationInDays)
        });
    }

    private string GenerateIpAddress()
    {
        if (Request.Headers.ContainsKey("X-Forwarded-For"))
            return Request.Headers["X-Forwarded-For"];
        else
            return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
    }
}
