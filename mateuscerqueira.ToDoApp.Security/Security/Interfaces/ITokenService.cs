namespace mateuscerqueira.Application.Security.Interfaces;

public interface ITokenService
{
    string GenerateToken(Guid userId, string firstName, string lastName, string email, string role);
    int GetExpirationMinutes();
}