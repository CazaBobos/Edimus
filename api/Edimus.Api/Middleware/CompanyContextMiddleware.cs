using Shared.Core.Abstractions;
using Shared.Infrastructure.Extensions;

namespace Edimus.Api.Middleware;

public class CompanyContextMiddleware
{
    private readonly RequestDelegate _next;
    public CompanyContextMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext httpContext, ICurrentCompanyService companyService)
    {
        if (httpContext.User.Identity?.IsAuthenticated == true)
        {
            var userCompanies = httpContext.User.GetUserCompanies();
            var headerValue = httpContext.Request.Headers["X-Company-Id"].FirstOrDefault();

            if (headerValue is not null && int.TryParse(headerValue, out var companyId))
            {
                if (!userCompanies.Contains(companyId))
                {
                    httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return;
                }

                companyService.AllowedCompanyIds = [companyId];
            }
            else
            {
                companyService.AllowedCompanyIds = userCompanies;
            }
        }

        await _next(httpContext);
    }
}
