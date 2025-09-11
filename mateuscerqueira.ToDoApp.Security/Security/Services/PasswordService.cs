using mateuscerqueira.Application.Security.Interfaces;
using mateuscerqueira.ToDoApp.Domain.ValueObjects;

namespace mateuscerqueira.Application.Security.Services;

public class PasswordService
{
    private readonly IDataProtectionService _protectionService;

    public PasswordService(IDataProtectionService protectionService)
    {
        _protectionService = protectionService;
    }

    public PasswordHash CreatePasswordHash(string plainPassword)
    {
        var keys = _protectionService.Protect(plainPassword);
        return new PasswordHash(keys.PasswordHash, keys.PasswordSalt);
    }

    public bool VerifyPassword(PasswordHash passwordHash, string plainPassword)
    {
        var computedHash = _protectionService.GetComputedHash(plainPassword, passwordHash.Salt);
        return passwordHash.Verify(computedHash);
    }
}
