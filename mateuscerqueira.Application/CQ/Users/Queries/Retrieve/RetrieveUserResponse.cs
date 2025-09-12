using mateuscerqueira.ToDoApp.Domain.Enums;

namespace mateuscerqueira.Application.CQ.Users.Queries.Retrieve;

public class RetrieveUserResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public bool IsActive { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int ToDoItemsCount { get; set; }
}