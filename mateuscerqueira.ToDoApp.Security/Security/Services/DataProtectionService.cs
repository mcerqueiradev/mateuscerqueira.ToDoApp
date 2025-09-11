using mateuscerqueira.Application.Security.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace mateuscerqueira.Application.Security.Services
{
    public class DataProtectionService : IDataProtectionService
    {
        public record DataProtectionKeys(byte[] PasswordHash, byte[] PasswordSalt);

        public DataProtectionKeys Protect(string password)
        {
            using var hmac = new HMACSHA512();

            byte[] hashedPass = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            byte[] saltKey = hmac.Key;

            return new DataProtectionKeys(hashedPass, saltKey);
        }

        public byte[] GetComputedHash(string password, byte[] salt)
        {
            using var hmac = new HMACSHA512(salt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash;
        }
    }
}