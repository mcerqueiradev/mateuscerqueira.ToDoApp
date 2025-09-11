namespace mateuscerqueira.ToDoApp.Domain.ValueObjects;

public class PersonalName
{
    public string FirstName { get; }
    public string LastName { get; }
    public string FullName => $"{FirstName} {LastName}";

    private PersonalName() { }

    public PersonalName(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be empty");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be empty");

        FirstName = firstName.Trim();
        LastName = lastName.Trim();
    }

    public override bool Equals(object obj)
    {
        return obj is PersonalName other &&
               FirstName == other.FirstName &&
               LastName == other.LastName;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FirstName, LastName);
    }

    public override string ToString() => FullName;
}