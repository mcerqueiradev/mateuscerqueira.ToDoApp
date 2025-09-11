using static mateuscerqueira.Application.Security.Services.DataProtectionService;

namespace mateuscerqueira.Application.Security.Interfaces;

public interface IDataProtectionService
{
    DataProtectionKeys Protect(string password);
    byte[] GetComputedHash(string password, byte[] salt);
}
