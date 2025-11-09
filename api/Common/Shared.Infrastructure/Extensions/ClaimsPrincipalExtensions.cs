using Shared.Core.Abstractions;
using Shared.Core.Entities;
using Shared.Core.Exceptions;
using System.Security.Claims;
using System.Text.Json;

namespace Shared.Infrastructure.Extensions;
public static class ClaimsPrincipalExtensions
{
    public static IUserRecord GetUser(this ClaimsPrincipal user)
    {
        return User.MakeRecord(
            user.GetUserId(),
            user.GetUsername(),
            user.GetUserRole(),
            user.GetUserCompanies());
    }

    private static int GetUserId(this ClaimsPrincipal user)
    {
        var value = user.GetClaimValue(UserClaims.Id);
        return value is null ? default : Convert.ToInt32(value);
    }
    private static string GetUsername(this ClaimsPrincipal user)
    {
        return user.GetClaimValue(UserClaims.Username) ?? "";
    }

    private static List<int> GetUserCompanies(this ClaimsPrincipal user)
    {
        var value = user.GetClaimValue(UserClaims.Companies) ?? string.Empty;

        return JsonSerializer.Deserialize<List<int>>(value) ?? new();
    }

    private static UserRole GetUserRole(this ClaimsPrincipal user)
    {
        var value = GetClaimValue(user, UserClaims.Role);
        return value is null ? UserRole.None : (UserRole)(Convert.ToInt32(value));
    }

    private static string? GetClaimValue(this ClaimsPrincipal user, string claim)
    {
        return user.Claims.FirstOrDefault(x => x.Type == claim)?.Value;
    }

    public static void AllowMinRole(this ClaimsPrincipal user, UserRole minRole)
    {
        var userRole = user.GetUserRole();

        if (userRole < minRole)
            throw new HttpForbiddenException("You don't have the minimum role required to access this resource.");
    }
}
