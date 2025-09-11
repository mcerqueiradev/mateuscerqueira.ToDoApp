namespace mateuscerqueira.Application.Security.Interfaces;

public interface ITokenService
{
    public string GenerateToken(Guid userId, string firstName, string lastName);
}
