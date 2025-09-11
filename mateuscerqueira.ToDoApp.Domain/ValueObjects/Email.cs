using System.Text.RegularExpressions;

namespace mateuscerqueira.ToDoApp.Domain.ValueObjects;

public class Email
{
    public string Value { get; }
    public string Domain => Value.Split('@')[1];
    public string Username => Value.Split('@')[0];

    private Email() { } 

    public Email(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty", nameof(email));

        if (!IsValidEmail(email))
            throw new ArgumentException("Invalid email format", nameof(email));

        if (email.Length > 255)
            throw new ArgumentException("Email too long", nameof(email));

        Value = email.Trim().ToLower();
    }

    public static Email Create(string email) => new(email);

    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        // Regex
        var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        return regex.IsMatch(email);
    }

    public override bool Equals(object obj)
    {
        return obj is Email other && Value == other.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString() => Value;

    public static implicit operator string(Email email) => email.Value;

    public static explicit operator Email(string email) => new(email);
}