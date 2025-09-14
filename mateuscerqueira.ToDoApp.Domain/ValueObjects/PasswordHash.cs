public class PasswordHash
{
    public byte[] Hash { get; private set; }
    public byte[] Salt { get; private set; }
    public DateTime CreatedAt { get; private set; }

    // Construtor privado para EF Core
    private PasswordHash()
    {
        Hash = Array.Empty<byte>();
        Salt = Array.Empty<byte>();
        CreatedAt = DateTime.MinValue;
    }

    public PasswordHash(byte[] hash, byte[] salt)
    {
        Hash = hash ?? throw new ArgumentNullException(nameof(hash));
        Salt = salt ?? throw new ArgumentNullException(nameof(salt));
        CreatedAt = DateTime.UtcNow;
    }

    public bool Verify(byte[] computedHash)
    {
        return computedHash != null && Hash.SequenceEqual(computedHash);
    }

}