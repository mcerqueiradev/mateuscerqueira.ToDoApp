namespace mateuscerqueira.ToDoApp.Domain.ValueObjects;

public class PasswordHash
{
    public byte[] Hash { get; }
    public byte[] Salt { get; }
    public DateTime CreatedAt { get; }

    private PasswordHash() { }

    public PasswordHash(byte[] hash, byte[] salt)
    {
        Hash = hash ?? throw new ArgumentNullException(nameof(hash));
        Salt = salt ?? throw new ArgumentNullException(nameof(salt));
        CreatedAt = DateTime.UtcNow;
    }

    public bool Verify(byte[] computedHash)
    {
        return computedHash.SequenceEqual(Hash);
    }

    public override bool Equals(object obj) => obj is PasswordHash other && Hash.SequenceEqual(other.Hash);
    public override int GetHashCode() => HashCode.Combine(Hash, Salt);
    public override string ToString() => "[PROTECTED]";
}