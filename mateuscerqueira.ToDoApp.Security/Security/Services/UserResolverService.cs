using mateuscerqueira.Application.Security.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace mateuscerqueira.Application.Security.Services;

public class UserResolverService : IUserResolverService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserResolverService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    private ClaimsPrincipal? CurrentUser => _httpContextAccessor.HttpContext?.User;

    public Guid? GetUserId()
    {
        var userIdClaim = CurrentUser?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Guid.TryParse(userIdClaim, out var userId) ? userId : null;
    }

    public string? GetUserName()
    {
        return CurrentUser?.FindFirst(ClaimTypes.Name)?.Value;
    }

    public string? GetUserEmail()
    {
        return CurrentUser?.FindFirst(ClaimTypes.Email)?.Value;
    }

    public string? GetUserRole()
    {
        return CurrentUser?.FindFirst(ClaimTypes.Role)?.Value;
    }

    public bool IsAuthenticated()
    {
        return CurrentUser?.Identity?.IsAuthenticated ?? false;
    }

    public bool IsInRole(string role)
    {
        return CurrentUser?.IsInRole(role) ?? false;
    }
}