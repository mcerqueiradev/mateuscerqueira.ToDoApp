namespace mateuscerqueira.Application.Security.Interfaces;

public interface IUserResolverService
{
    Guid? GetUserId();
    string? GetUserName();
    string? GetUserEmail();
    string? GetUserRole();
    bool IsAuthenticated();
    bool IsInRole(string role);
}